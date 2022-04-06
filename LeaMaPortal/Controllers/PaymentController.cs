using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using System.Threading;
using System.Threading.Tasks;
using LeaMaPortal.Models;
using MySql.Data.MySqlClient;
using MvcPaging;
using LeaMaPortal.Helpers;

namespace LeaMaPortal.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: Payments
        public ActionResult Index()
        {
            if (Session["Region"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            return View();
        }
        [HttpGet]
        public PartialViewResult List(string Search, int? page, int? defaultPageSize)
        {
            ViewData["Search"] = Search;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
            ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
            var PaymentDetails = db.tbl_paymenthd.Where(x => x.Delmark != "*");
            if (!string.IsNullOrWhiteSpace(Search))
            {
                PaymentDetails = PaymentDetails.Where(x => x.PaymentNo.ToString().Contains(Search) || x.PaymentType .Contains (Search ) || x.PaymentMode.Contains (Search ) || x.agreement_no.ToString ().Contains (Search ) || x.Property_ID.ToString ().Contains(Search) || x.Supplier_Name.ToString().Contains(Search));
            }
            var invoice = PaymentDetails.OrderByDescending(x => x.id).Select(x => new PaymentViewModel()
            {
                AdvAcCode = x.AdvAcCode,
                agreement_no = x.agreement_no,
                AmtInWords = x.AmtInWords,
                BankAcCode = x.BankAcCode,
                BankAcName = x.BankAcName,
                DDChequeDate = x.Cheqdate,
                DDChequeNo = x.DDChequeNo,
                Id = x.id,
                Narration = x.Narration,
                PaymentDate = x.PaymentDate,
                PaymentMode = x.PaymentMode,
                PaymentNo = x.PaymentNo,
                PaymentType = x.PaymentType,
                PDCstatus = x.pdcstatus,
                Property_id = x.Property_ID,
                Property_Name = x.Property_Name,
                Supplier_id = x.Supplier_id,
                Supplier_Name = x.Supplier_Name,
                TotalAmount = x.TotalAmount,
                Unit_ID = x.Unit_ID,
                unit_Name = x.unit_Name,
                PaymentDetailsViewModel = db.tbl_paymentdt.Where(y => y.Delmark != "*" && y.PaymentNo == x.PaymentNo).Select(z => new PaymentDetailsViewModel
                {
                    Id = z.id,
                    DebitAmount = z.Debitamt,
                    Description = z.Description,
                    Invno = z.InvoiceNo,
                    InvoiceAmount = z.InvoiceAmount,
                    InvoiceDate = z.InvoiceDate,
                    Invtype = z.Invtype,
                    PaidAmount = z.PaidAmount,
                    Remarks = z.Remarks
                }).ToList()
            }).ToPagedList(currentPageIndex, PageSize);
            return PartialView("../Payment/_List", invoice);
        }

        [HttpGet]
        public async Task<PartialViewResult> AddOrUpdate()
        {
            try
            {
                PaymentViewModel model = new PaymentViewModel();
                int? paymentno = await db.tbl_paymenthd.MaxAsync(x => (int?)x.PaymentNo);
                //int paymentno = await db.tbl_paymenthd.Select(x => x.PaymentNo).DefaultIfEmpty(0).MaxAsync();
                model.PaymentNo = paymentno == null ? 1 : paymentno.Value + 1;
                model.PaymentDate = DateTime.Now.Date;
                //var paymentType_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','Reccategory',',',null)").ToList();

                var paymentType_result = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "Reccategory");
                if (paymentType_result != null)
                {
                    ViewBag.PaymentType = new SelectList(paymentType_result.combovalue.Split(','), model.PaymentType);
                }

                //var paymentMode_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','RecpType',',',null)").ToList();
                var paymentMode_result = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Payment" && x.comboname == "PaymentMode");
                if (paymentMode_result != null)
                {
                    ViewBag.PaymentMode = new SelectList(paymentMode_result.combovalue.Split(','), model.PaymentMode);
                }

                //var PDCstatus_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','PDCstatus',',',null)").ToList();
                var PDCstatus_result = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "PDCstatus");
                if (PDCstatus_result != null)
                {
                    ViewBag.PDCstatus = new SelectList(PDCstatus_result.combovalue.Split(','), model.PDCstatus);
                }

                var suppliers = await db.tbl_suppliermaster.Where(w => w.Delmark != "*").ToListAsync();
                ViewBag.Supplierid = new SelectList(suppliers, "Supplier_Id", "Supplier_Id");
                ViewBag.SupplierName = new SelectList(suppliers, "Supplier_Id", "Supplier_Name");

                var properties = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Property" && w.Delmark != "*").OrderBy(o => o.id);

                ViewBag.Propertyid = new SelectList(properties, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq");
                ViewBag.PropertyName = new SelectList(properties, "Property_ID_Tawtheeq", "Property_Name");

                var units = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Delmark != "*").OrderBy(o => o.id);

                ViewBag.UnitID = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
                ViewBag.unitName = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_Property_Name");


                ViewBag.agreement_no = new SelectList(db.tbl_agreement.Where(w => w.Delmark != "*" && w.Approval_Flag != 1 && string.IsNullOrEmpty(w.Status)).OrderBy(o => o.id).Distinct(), "Agreement_No", "Agreement_No");
                ViewBag.BankAcCode = new SelectList(Common.BankDetails, "AccountNumber", "AccountNumber");
                ViewBag.BankAcName = new SelectList(Common.BankDetails, "AccountNumber", "BankName");

                //ViewBag.BankAcName = new SelectList(StaticHelper.GetStaticData(StaticHelper.ACCOUNT_NAME), "Name", "Name");
                //ViewBag.BankAcCode = new SelectList(StaticHelper.GetStaticData(StaticHelper.ACCOUNT_NUMBER), "Name", "Name");

                var advancepaymentnumber = db.Database.SqlQuery<int>("Select PaymentNo from view_advance_pending_payment");
                ViewBag.AdvAcCode = new SelectList(advancepaymentnumber);
                return PartialView("../Payment/_AddOrUpdate", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(PaymentViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";
                    result.Message = "Payment details created successfully";
                    if (model.Id != 0)
                    {
                        PFlag = "UPDATE";
                        result.Message = "Payment details updated successfully";
                    }
                    string invoice = null;
                    if (model.PaymentDetailsViewModel != null && model.PaymentType == "against invoice")
                    {
                        foreach (var item in model.PaymentDetailsViewModel)
                        {
                            var invoicedate = item.InvoiceDate.HasValue ? "'" + item.InvoiceDate.Value.Date.ToString("yyyy-MM-dd") + "'" : "null";
                            if (string.IsNullOrWhiteSpace(invoice))
                            {
                                invoice = "(" + model.PaymentNo + ",'" + item.Invtype + "','" + item.Description +
                                    "','" + item.Invno + "'," + invoicedate + ",'" + item.InvoiceAmount + "','" + item.PaidAmount + "','" + item.DebitAmount
                                    + "','" + item.Remarks + "')";
                            }
                            else
                            {
                                invoice += ",(" + model.PaymentNo + ",'" + item.Invtype + "','" + item.Description +
                                    "','" + item.Invno + "'," + invoicedate + ",'" + item.InvoiceAmount + "','" + item.PaidAmount + "','" + item.DebitAmount
                                    + "','" + item.Remarks + "')";
                            }
                        }
                    }
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PPaymentNo", model.PaymentNo),
                                           new MySqlParameter("@PPaymentDate",model.PaymentDate),
                                            new MySqlParameter("@Pagreement_no",model.agreement_no),
                                            new MySqlParameter("@PProperty_ID", model.Property_id),
                                            new MySqlParameter("@PProperty_Name", model.Property_Name),
                                            new MySqlParameter("@PUnit_ID", model.Unit_ID),
                                            new MySqlParameter("@Punit_Name", model.unit_Name),
                                            new MySqlParameter("@PSupplier_id", model.Supplier_id),
                                            new MySqlParameter("@PSupplier_Name", model.Supplier_Name),
                                            new MySqlParameter("@PPaymentType", model.PaymentType),
                                            new MySqlParameter("@PPaymentMode", model.PaymentMode),
                                            new MySqlParameter("@PTotalAmount", model.TotalAmount),
                                            new MySqlParameter("@PAmtInWords", model.AmtInWords),
                                            new MySqlParameter("@PDDChequeNo", model.DDChequeNo),
                                            new MySqlParameter("@PCheqdate", model.DDChequeDate),
                                            new MySqlParameter("@Ppdcstatus", model.PDCstatus),
                                            new MySqlParameter("@PBankAcCode", model.BankAcCode),
                                            new MySqlParameter("@PBankAcName", model.BankAcName),
                                            new MySqlParameter("@PAdvAcCode", model.AdvAcCode),
                                            new MySqlParameter("@PNarration", model.Narration),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter("@PPaymentdt", invoice)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Payment_All(@PFlag,@PPaymentNo,@PPaymentDate,@Pagreement_no,@PProperty_ID,@PProperty_Name,@PUnit_ID,@Punit_Name,@PSupplier_id,@PSupplier_Name,@PPaymentType,@PPaymentMode,@PTotalAmount,@PAmtInWords,@PDDChequeNo,@PCheqdate,@Ppdcstatus,@PBankAcCode,@PBankAcName,@PAdvAcCode,@PNarration,@PCreateduser,@PPaymentdt)", param).ToListAsync();
                    await db.SaveChangesAsync();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> Edit(int Payment_No)
        {
            try
            {
                var x = await db.tbl_paymenthd.FirstOrDefaultAsync(f => f.PaymentNo == Payment_No);
                PaymentViewModel model = new PaymentViewModel()
                {
                    AdvAcCode = x.AdvAcCode,
                    agreement_no = x.agreement_no,
                    AmtInWords = x.AmtInWords,
                    BankAcCode = x.BankAcCode,
                    BankAcName = x.BankAcName,
                    DDChequeDate = x.Cheqdate,
                    DDChequeNo = x.DDChequeNo,
                    Id = x.id,
                    Narration = x.Narration,
                    PaymentDate = x.PaymentDate,
                    PaymentMode = x.PaymentMode,
                    PaymentNo = x.PaymentNo,
                    PaymentType = x.PaymentType,
                    PDCstatus = x.pdcstatus,
                    Property_id = x.Property_ID,
                    Property_Name = x.Property_Name,
                    Supplier_id = x.Supplier_id,
                    Supplier_Name = x.Supplier_Name,
                    TotalAmount = x.TotalAmount,
                    Unit_ID = x.Unit_ID,
                    unit_Name = x.unit_Name,
                    PaymentDetailsViewModel = db.tbl_paymentdt.Where(y => y.Delmark != "*" && y.PaymentNo == x.PaymentNo).Select(z => new PaymentDetailsViewModel
                    {
                        Id = z.id,
                        DebitAmount = z.Debitamt,
                        Description = z.Description,
                        Invno = z.InvoiceNo,
                        InvoiceAmount = z.InvoiceAmount,
                        InvoiceDate = z.InvoiceDate,
                        Invtype = z.Invtype,
                        PaidAmount = z.PaidAmount,
                        Remarks = z.Remarks
                    }).ToList()
                };

                //var paymentType_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','Reccategory',',',null)").ToList();
                //ViewBag.PaymentType = new SelectList(paymentType_result, model.PaymentType);
                //var paymentMode_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','RecpType',',',null)").ToList();
                //ViewBag.PaymentMode = new SelectList(paymentMode_result, model.PaymentMode);
                //var PDCstatus_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','PDCstatus',',',null)").ToList();
                //ViewBag.PDCstatus = new SelectList(PDCstatus_result, model.PDCstatus);


                //var paymentType_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','Reccategory',',',null)").ToList();

                var paymentType_result = await db.tbl_combo_master.FirstOrDefaultAsync(y => y.screen_name == "Receipts" && y.comboname == "Reccategory");
                if (paymentType_result != null)
                {
                    ViewBag.PaymentType = new SelectList(paymentType_result.combovalue.Split(','), model.PaymentType);
                }

                //var paymentMode_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','RecpType',',',null)").ToList();
                var paymentMode_result = await db.tbl_combo_master.FirstOrDefaultAsync(y => y.screen_name == "Receipts" && y.comboname == "RecpType");
                if (paymentMode_result != null)
                {
                    ViewBag.PaymentMode = new SelectList(paymentMode_result.combovalue.Split(','), model.PaymentMode);
                }

                //var PDCstatus_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','PDCstatus',',',null)").ToList();
                var PDCstatus_result = await db.tbl_combo_master.FirstOrDefaultAsync(y => y.screen_name == "Receipts" && y.comboname == "PDCstatus");
                if (PDCstatus_result != null)
                {
                    ViewBag.PDCstatus = new SelectList(PDCstatus_result.combovalue.Split(','), model.PDCstatus);
                }


                var suppliers = await db.tbl_suppliermaster.Where(w => w.Delmark != "*").ToListAsync();
                ViewBag.Supplierid = new SelectList(suppliers, "Supplier_Id", "Supplier_Id", model.Supplier_id);
                ViewBag.SupplierName = new SelectList(suppliers, "Supplier_Id", "Supplier_Name", model.Supplier_id);
                var agreements = db.tbl_agreement.Where(w => w.Delmark != "*").OrderBy(o => o.id);
                ViewBag.agreement_no = new SelectList(agreements, "Agreement_No", "Agreement_No", model.agreement_no);
                ViewBag.BankAcCode = new SelectList(Common.BankDetails, "AccountNumber", "AccountNumber", model.BankAcCode);
                ViewBag.BankAcName = new SelectList(Common.BankDetails, "AccountNumber", "BankName", model.BankAcName);
                if (model.agreement_no == null || model.agreement_no == 0)
                {
                    var properties = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Property" && w.Delmark != "*").OrderBy(o => o.id);

                    ViewBag.Propertyid = new SelectList(properties, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", model.Property_id);
                    ViewBag.PropertyName = new SelectList(properties, "Property_ID_Tawtheeq", "Property_Name", model.Property_id);

                    var units = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Delmark != "*").OrderBy(o => o.id);

                    ViewBag.UnitID = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", model.Unit_ID);
                    ViewBag.unitName = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_Property_Name", model.Unit_ID);
                }
                else
                {
                    var agreementDetails = await agreements.FirstOrDefaultAsync(y => y.Agreement_No == model.agreement_no);
                    List<UnitDropdown> unitsDropdown = new List<UnitDropdown>();
                    unitsDropdown.Add(new UnitDropdown()
                    {
                        Unitid = agreementDetails.Unit_ID_Tawtheeq,
                        unitName = agreementDetails.Unit_Property_Name
                    });
                    ViewBag.UnitID = new SelectList(unitsDropdown, "Unitid", "Unitid", model.Unit_ID);
                    ViewBag.unitName = new SelectList(unitsDropdown, "Unitid", "unitName", model.Unit_ID);

                    List<PropertyDropdown> propertyDropdown = new List<PropertyDropdown>();
                    propertyDropdown.Add(new PropertyDropdown()
                    {
                        Propertyid = agreementDetails.Property_ID_Tawtheeq,
                        PropertyName = agreementDetails.Properties_Name
                    });
                    ViewBag.Propertyid = new SelectList(propertyDropdown, "Propertyid", "Propertyid", model.Property_id);
                    ViewBag.PropertyName = new SelectList(propertyDropdown, "Propertyid", "PropertyName", model.Property_id);
                }
                var advancepaymentnumber = await db.Database.SqlQuery<int>("Select PaymentNo from view_advance_pending_payment").ToListAsync();
                ViewBag.AdvAcCode = new SelectList(advancepaymentnumber, Convert.ToInt32(model.AdvAcCode));
                return PartialView("../Payment/_AddorUpdate", model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Payment_No)
        {
            try
            {
                MessageResult result = new MessageResult();
                var x = await db.tbl_paymenthd.FirstOrDefaultAsync(f => f.PaymentNo == Payment_No);
                PaymentViewModel model = new PaymentViewModel()
                {
                    AdvAcCode = x.AdvAcCode,
                    agreement_no = x.agreement_no,
                    AmtInWords = x.AmtInWords,
                    BankAcCode = x.BankAcCode,
                    BankAcName = x.BankAcName,
                    DDChequeDate = x.Cheqdate,
                    DDChequeNo = x.DDChequeNo,
                    Id = x.id,
                    Narration = x.Narration,
                    PaymentDate = x.PaymentDate,
                    PaymentMode = x.PaymentMode,
                    PaymentNo = x.PaymentNo,
                    PaymentType = x.PaymentType,
                    PDCstatus = x.pdcstatus,
                    Property_id = x.Property_ID,
                    Property_Name = x.Property_Name,
                    Supplier_id = x.Supplier_id,
                    Supplier_Name = x.Supplier_Name,
                    TotalAmount = x.TotalAmount,
                    Unit_ID = x.Unit_ID,
                    unit_Name = x.unit_Name,
                    PaymentDetailsViewModel = db.tbl_paymentdt.Where(y => y.Delmark != "*" && y.PaymentNo == x.PaymentNo).Select(z => new PaymentDetailsViewModel
                    {
                        Id = z.id,
                        DebitAmount = z.Debitamt,
                        Description = z.Description,
                        Invno = z.InvoiceNo,
                        InvoiceAmount = z.InvoiceAmount,
                        InvoiceDate = z.InvoiceDate,
                        Invtype = z.Invtype,
                        PaidAmount = z.PaidAmount,
                        Remarks = z.Remarks
                    }).ToList()
                };
                object[] param = {
                         new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PPaymentNo", model.PaymentNo),
                                           new MySqlParameter("@PPaymentDate",model.PaymentDate),
                                            new MySqlParameter("@Pagreement_no",model.agreement_no),
                                            new MySqlParameter("@PProperty_ID", model.Property_id),
                                            new MySqlParameter("@PProperty_Name", model.Property_Name),
                                            new MySqlParameter("@PUnit_ID", model.Unit_ID),
                                            new MySqlParameter("@Punit_Name", model.unit_Name),
                                            new MySqlParameter("@PSupplier_id", model.Supplier_id),
                                            new MySqlParameter("@PSupplier_Name", model.Supplier_Name),
                                            new MySqlParameter("@PPaymentType", model.PaymentType),
                                            new MySqlParameter("@PPaymentMode", model.PaymentMode),
                                            new MySqlParameter("@PTotalAmount", model.TotalAmount),
                                            new MySqlParameter("@PAmtInWords", model.AmtInWords),
                                            new MySqlParameter("@PDDChequeNo", model.DDChequeNo),
                                            new MySqlParameter("@PCheqdate", model.DDChequeDate),
                                            new MySqlParameter("@Ppdcstatus", model.PDCstatus),
                                            new MySqlParameter("@PBankAcCode", model.BankAcCode),
                                            new MySqlParameter("@PBankAcName", model.BankAcName),
                                            new MySqlParameter("@PAdvAcCode", model.AdvAcCode),
                                            new MySqlParameter("@PNarration", model.Narration),
                                            new MySqlParameter("@PPaymentdt", null),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var RE = await db.Database.SqlQuery<object>("CALL Usp_Payment_All(@PFlag,@PPaymentNo,@PPaymentDate,@Pagreement_no,@PProperty_ID,@PProperty_Name,@PUnit_ID,@Punit_Name,@PSupplier_id,@PSupplier_Name,@PPaymentType,@PPaymentMode,@PTotalAmount,@PAmtInWords,@PDDChequeNo,@PCheqdate,@Ppdcstatus,@PBankAcCode,@PBankAcName,@PAdvAcCode,@PNarration,@PCreateduser,@PPaymentdt)", param).ToListAsync();
                result.Message = "Payment details deleted successfully";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetUnitId(string PropertyId)
        {
            try
            {
                List<OptionModel> model = new List<OptionModel>();
                if (!String.IsNullOrEmpty(PropertyId))
                {
                    return Json(new
                    {
                        UnitId = new SelectList(db.tbl_propertiesmaster.Where(w => w.Delmark != "*" && w.Property_Flag == "Unit" && w.Ref_unit_Property_ID_Tawtheeq == PropertyId).OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq"),
                        UnitName = new SelectList(db.tbl_propertiesmaster.Where(w => w.Delmark != "*" && w.Property_Flag == "Unit" && w.Ref_unit_Property_ID_Tawtheeq == PropertyId).OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_Property_Name"),
                        AgreementNo = new SelectList(db.tbl_agreement.Where(w => w.Delmark != "*" && w.Property_ID_Tawtheeq == PropertyId).OrderBy(o => o.property_id), "Agreement_No", "Agreement_No"),
                        PropertyId = PropertyId
                    });
                }
                else
                {
                    return Json(new
                    {
                        UnitId = new SelectList(db.tbl_propertiesmaster.Where(w => w.Delmark != "*" && w.Property_Flag == "Unit").OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq"),
                        UnitName = new SelectList(db.tbl_propertiesmaster.Where(w => w.Delmark != "*" && w.Property_Flag == "Unit").OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_Property_Name"),
                        AgreementNo = new SelectList(db.tbl_agreement.Where(w => w.Delmark != "*").OrderBy(o => o.property_id), "Agreement_No", "Agreement_No"),
                        //PropertyId = db.tbl_propertiesmaster.FirstOrDefault(w => w.Delmark != "*" && w.Property_Name == PropertyName && w.Property_Flag == "Property").Property_ID_Tawtheeq,
                        //PropertyName = PropertyName
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetProperty(string UnitId)
        {
            try
            {
                List<OptionModel> model = new List<OptionModel>();
                if (!String.IsNullOrEmpty(UnitId))
                {
                    return Json(new
                    {
                        PropertyId = new SelectList(db.tbl_propertiesmaster.FirstOrDefault(w => w.Delmark != "*" && w.Property_Flag == "Unit" && w.Unit_ID_Tawtheeq == UnitId).Ref_unit_Property_ID_Tawtheeq, "Ref_unit_Property_ID_Tawtheeq", "Ref_unit_Property_ID_Tawtheeq"),
                        PropertyName = new SelectList(db.tbl_propertiesmaster.FirstOrDefault(w => w.Delmark != "*" && w.Property_Flag == "Unit" && w.Unit_ID_Tawtheeq == UnitId).Ref_Unit_Property_Name, "Ref_unit_Property_ID_Tawtheeq", "Ref_Unit_Property_Name"),
                        AgreementNo = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Unit_ID_Tawtheeq == UnitId).Agreement_No.ToString(), "Agreement_No", "Agreement_No"),
                        UnitName = db.tbl_propertiesmaster.FirstOrDefault(w => w.Delmark != "*" && w.Unit_ID_Tawtheeq == UnitId && w.Property_Flag == "Unit").Unit_ID_Tawtheeq,
                        UnitId = UnitId
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetAgreementDetails(int? AgreementNo)
        {

            try
            {
                var invoiceDropdown = new InvoiceDropdown();
                var agreement = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo);
                if (agreement != null)
                {
                    var property = new PropertyDropdown()
                    {
                        Propertyid = agreement.Property_ID_Tawtheeq,
                        PropertyName = agreement.Properties_Name
                    };
                    var unit = new UnitDropdown()
                    {
                        Unitid = agreement.Unit_ID_Tawtheeq,
                        unitName = agreement.Unit_Property_Name,
                    };

                    invoiceDropdown.Properties.Add(property);
                    invoiceDropdown.Units.Add(unit);
                }
                return Json(invoiceDropdown, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }

            //try
            //{
            //    List<OptionModel> model = new List<OptionModel>();
            //    if (AgreementNo != 0 && db.tbl_agreement.Any(w => w.Delmark != "*" && w.Agreement_No == AgreementNo))
            //    {
            //        return Json(new
            //        {
            //            PropertyId = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Agreement_No == AgreementNo).Property_ID_Tawtheeq, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq"),
            //            PropertyName = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Agreement_No == AgreementNo).Properties_Name, "Property_ID_Tawtheeq", "Properties_Name"),
            //            UnitName = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Agreement_No == AgreementNo).Unit_Property_Name, "Unit_ID_Tawtheeq", "Unit_Property_Name"),
            //            UnitId = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Agreement_No == AgreementNo).Unit_ID_Tawtheeq, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq"),
            //        });
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        [HttpGet]
        public async Task<JsonResult> GetAdvanceAdjustmentAmount(int? advanceCode)
        {

            try
            {
                var advancepaymentnumber = await db.Database.SqlQuery<int>("Select TotalAmount from view_advance_pending_payment where PaymentNo=" + advanceCode).FirstOrDefaultAsync();
                return Json(advancepaymentnumber, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<PartialViewResult> Print(int PaymentNo, string OtherTerms)
        {
            try
            {
                Thread.Sleep(1000);
                PaymentPrintModel model = new PaymentPrintModel();
                //TcaPrintModel model = new TcaPrintModel();
                var printPayment = await db.tbl_paymenthd.FirstOrDefaultAsync(x => x.PaymentNo == PaymentNo);

                //var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Property_Id == agreementDet.property_id);
                if (printPayment != null)
                    model.Id = printPayment.id;
                model.PaymentNo = printPayment.PaymentNo;
                model.Paymentdate = printPayment.PaymentDate.Value.ToShortDateString();
                model.Agreement_No = int.Parse(printPayment.agreement_no.ToString());
                model.Properties_ID = printPayment.Property_ID;
                model.Property_Name = printPayment.Property_Name;
                model.Unit_ID = printPayment.Unit_ID;
                model.unit_Name = printPayment.unit_Name;
                model.SupplierId = int.Parse(printPayment.Supplier_id.ToString());
                model.SupplierName = printPayment.Supplier_Name;
                model.Paymenttype = printPayment.PaymentType;
                model.PaymentMode = printPayment.PaymentMode;
                model.totalamt = printPayment.TotalAmount;
                //if (model.totalamt > 0)
                //{ 
                //string decimalPart = "", text = "";
                //decimal amount = 0;
                //decimal amt1 = decimal.Parse(model.totalamt.ToString());
                //int i = (int)amt1;
                //decimal n1 = amt1 - i;
                //if (n1 > 0)
                //{
                //    decimalPart = amt1.ToString().Split('.')[1];
                //}

                //string t1 = LeaMaPortal.Helpers.NumberToText1.NumberToText(i, true, false);
                //    // string t2 = LeaMaPortal.Helpers.NumberToText1.DecimalToText(decimalPart);
                //string t2 = LeaMaPortal.Helpers.NumberToText1.NumberToText (int.Parse ( decimalPart),true ,false );
                //    if (t1 != "" && t2 != "")
                //{ text = t1 + "" + " " + "Dirhams " + "and " + t2 +  " Fils"; }
                //else
                //{ text = t1 +  " Dirhams "; }
                //model.Amountinwords =  text;
                //}
                string decimalPart = "";
                if (printPayment.TotalAmount > 0)
                {
                    float amt1 = float.Parse(printPayment.TotalAmount.ToString());
                    int i = (int)amt1;
                    float n1 = amt1 - i;
                    if (n1 > 0)
                    {
                        decimalPart = amt1.ToString().Split('.')[1];
                    }
                    model.Dhirams = Convert.ToString(i);
                    if(decimalPart != "")
                    { 
                    model.Fils = decimalPart;
                    }
                    else
                    { model.Fils = "0"; }
                }
                model.Amountinwords = printPayment.AmtInWords;
                model.Amountinwords = AmountInWords(Convert.ToDecimal(printPayment.TotalAmount));
                model.DDChequeNo = printPayment.DDChequeNo;
                if (printPayment.Cheqdate.HasValue)
                { model.Cheqdate = printPayment.Cheqdate.Value.ToShortDateString(); }

                model.pdcstatus = printPayment.pdcstatus;
                model.BankAcCode = printPayment.BankAcCode;
                model.BankAcName = printPayment.BankAcName;
                model.AdvAcCode = printPayment.AdvAcCode;
                model.Narration = printPayment.Narration;
                //model.IssueDate = printInvoicehd.date ;
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == printPayment.agreement_no);

                var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Property_Id == agreementDet.property_id);
                model.Properties_Name = property.Property_Name;
                model.Properties_Address = property.Address1;


                model.Agreementdate = agreementDet.Agreement_Date.Value.ToShortDateString();


                var supplier = await db.tbl_suppliermaster.FirstAsync(x => x.Supplier_Id == printPayment.Supplier_id);
                if (supplier != null)
                {
                    model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(supplier.Fax_No) ? "" : supplier.Fax_Countrycode + "-" + supplier.Fax_Areacode + "-" + supplier.Fax_No;
                    model.Ag_Tenant_Address = supplier.address + ", " + supplier.address1 + ", " + supplier.City;
                    model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(supplier.Landline_No) ? "" : supplier.Landline_Countrycode + "-" + supplier.Landline_Areacode + "-" + supplier.Landline_No;
                    model.Ag_Tenant_Name = supplier.Supplier_Name;
                }


                //else
                //{
                //    var tenant = await db.tbl_tenant_company.FirstOrDefaultAsync(x => x.Tenant_Id == agreementDet.Ag_Tenant_id);
                //    if (tenant != null)
                //    {
                //        model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(tenant.Fax_No) ? "" : tenant.Fax_Countrycode + "-" + tenant.Fax_Areacode + "-" + tenant.Fax_No;
                //        model.Ag_Tenant_Address = tenant.address + ", " + tenant.address1 + ", " + tenant.City;
                //        model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(tenant.Landline_No) ? "" : tenant.Landline_Countrycode + "-" + tenant.Landline_Areacode + "-" + tenant.Landline_No;
                //        model.Ag_Tenant_Name = tenant.Title + " " + tenant.First_Name + tenant.Last_Name;
                //    }
                //}

                LeamaEntities l = new LeamaEntities();
                var filter = l.tbl_paymentdt.Where(x => x.PaymentNo == model.PaymentNo);
                var Paymentdata = filter.ToList();

                //var invdata = printInvoicedt .ToString().ToList();
                ViewBag.Paymentdtas = Paymentdata;
                //  //  model.Property_Usage = property.Property_Usage;
                return PartialView("../Payment/_PaymentPrint", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        //public async Task<PartialViewResult> Print(int PaymentNo, string OtherTerms)
        //{
        //    try
        //    {
        //        Thread.Sleep(1000);
        //        PaymentPrintModel model = new PaymentPrintModel();
        //        //TcaPrintModel model = new TcaPrintModel();
        //        var printPayment = await db.tbl_paymenthd.FirstOrDefaultAsync(x => x.PaymentNo == PaymentNo);

        //        //var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Property_Id == agreementDet.property_id);
        //        if (printPayment != null)
        //            model.Id = printPayment.id;
        //        model.PaymentNo = printPayment.PaymentNo;
        //        model.Paymentdate = printPayment.PaymentDate.Value.ToShortDateString();
        //        model.Agreement_No = int.Parse(printPayment.agreement_no.ToString());
        //        model.Properties_ID = printPayment.Property_ID;
        //        model.Property_Name = printPayment.Property_Name;
        //        model.Unit_ID = printPayment.Unit_ID;
        //        model.unit_Name = printPayment.unit_Name;
        //        model.SupplierId = int.Parse(printPayment.Supplier_id.ToString());
        //        model.SupplierName = printPayment.Supplier_Name;
        //        model.Paymenttype = printPayment.PaymentType;
        //        model.PaymentMode = printPayment.PaymentMode;
        //        model.totalamt = printPayment.TotalAmount;
        //        model.Amountinwords = printPayment.AmtInWords;
        //        model.DDChequeNo = printPayment.DDChequeNo;
        //        if (printPayment.Cheqdate.HasValue)
        //        { model.Cheqdate = printPayment.Cheqdate.Value.ToShortDateString(); }

        //        model.pdcstatus = printPayment.pdcstatus;
        //        model.BankAcCode = printPayment.BankAcCode;
        //        model.BankAcName = printPayment.BankAcName;
        //        model.AdvAcCode = printPayment.AdvAcCode;
        //        model.Narration = printPayment.Narration;
        //        //model.IssueDate = printInvoicehd.date ;
        //        var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == printPayment.agreement_no);

        //        var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Property_Id == agreementDet.property_id);
        //        model.Properties_Name = property.Property_Name;
        //        model.Properties_Address = property.Address1;


        //        model.Agreementdate = agreementDet.Agreement_Date.Value.ToShortDateString();


        //            var supplier = await db.tbl_suppliermaster.FirstAsync(x => x.Supplier_Id == printPayment.Supplier_id);
        //            if (supplier != null)
        //            {
        //                model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(supplier.Fax_No) ? "" : supplier.Fax_Countrycode + "-" + supplier.Fax_Areacode + "-" + supplier.Fax_No;
        //                model.Ag_Tenant_Address = supplier.address + ", " + supplier.address1 + ", " + supplier.City;
        //                model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(supplier.Landline_No) ? "" : supplier.Landline_Countrycode + "-" + supplier.Landline_Areacode + "-" + supplier.Landline_No;
        //                model.Ag_Tenant_Name = supplier.Supplier_Name ;
        //            }


        //        //else
        //        //{
        //        //    var tenant = await db.tbl_tenant_company.FirstOrDefaultAsync(x => x.Tenant_Id == agreementDet.Ag_Tenant_id);
        //        //    if (tenant != null)
        //        //    {
        //        //        model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(tenant.Fax_No) ? "" : tenant.Fax_Countrycode + "-" + tenant.Fax_Areacode + "-" + tenant.Fax_No;
        //        //        model.Ag_Tenant_Address = tenant.address + ", " + tenant.address1 + ", " + tenant.City;
        //        //        model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(tenant.Landline_No) ? "" : tenant.Landline_Countrycode + "-" + tenant.Landline_Areacode + "-" + tenant.Landline_No;
        //        //        model.Ag_Tenant_Name = tenant.Title + " " + tenant.First_Name + tenant.Last_Name;
        //        //    }
        //        //}

        //        LeamaEntities l = new LeamaEntities();
        //        var filter = l.tbl_paymentdt.Where(x => x.PaymentNo == model.PaymentNo);
        //        var Paymentdata = filter.ToList();

        //        //var invdata = printInvoicedt .ToString().ToList();
        //        ViewBag.Paymentdtas = Paymentdata;
        //        //  //  model.Property_Usage = property.Property_Usage;
        //        return PartialView("../Payment/_PaymentPrint", model);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

        [HttpGet]
        public string AmountInWords(decimal amount)
        {
            string decimalPart = "", text = "";
            decimal amt1 = amount;
            int i = (int)amt1;
            decimal n1 = amount - (int)amount;
            if (n1 > 0)
            {
                decimalPart = amt1.ToString().Split('.')[1];
            }

            string t1 = NumberToText1.NumberToText(i, true, false);
            string t2 = NumberToText1.DecimalToText(decimalPart);
            string t3 = "";
            //string text = NumberToText1.NumberToText (i, true, false) +"" +""+ "Dirhams " + "and"+ NumberToText1. DecimalToText(decimalPart) +""+ ""+"fils";
            if (t1 != "" && t2 != "")
            { text = t1 + "" + t3 + " Dirhams " + "and" + t2 + "" + "" + " fils "; }
            else
            { text = t1 + "" + t3 + " Dirhams "; }
            return text;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
