using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using System.Data.Entity;
using System.Data;
using System.Threading.Tasks;
using MvcPaging;
using MySql.Data.MySqlClient;

namespace LeaMaPortal.Controllers
{
    public class UtilityPaymentController : Controller
    {
        LeamaEntities db = new LeamaEntities();
        // GET: UtilityPayment
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult List(string Search, int? page, int? defaultPageSize)
        {
            ViewData["Search"] = Search;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
            ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
            //TenantCompanyViewModel model = new TenantCompanyViewModel();
            var invoiceDetails = db.tbl_eb_water_paymenthd.Where(x => x.Delmark != "*");
            if (!string.IsNullOrWhiteSpace(Search))
            {
                invoiceDetails = invoiceDetails.Where(x => x.PaymentNo.ToString().Contains(Search));
            }
            var invoice = invoiceDetails.OrderBy(x => x.id).Select(x => new UtilityPaymentModel()
            {
                AdvAcCode = x.AdvAcCode,
                AmtInWords = x.AmtInWords,
                BankAcCode = x.BankAcCode,
                BankAcName = x.BankAcName,
                Cheqdate = x.Cheqdate,
                DDChequeNo = x.DDChequeNo,
                id = x.id,
                Narration = x.Narration,
                PaymentDate = x.PaymentDate,
                PaymentMode = x.PaymentMode,
                PaymentNo = x.PaymentNo,
                PaymentType = x.PaymentType,
                pdcstatus = x.pdcstatus,
                Supplier_id = x.Supplier_id,
                Supplier_name = x.Supplier_name,
                TotalAmount = x.TotalAmount,
                Utility_id = x.Utility_id,
                Utiltiy_name = x.Utiltiy_name,
                UtilityPaymentDetails = x.tbl_eb_water_paymentdt.Select(y => new UtilityPaymentDetail
                {
                    billAmount = y.billAmount,
                    billDate = y.billDate,
                    billNo = y.billNo,
                    Debitamt = y.Debitamt,
                    id = y.id,
                    Meterno = y.Meterno,
                    PaidAmount = y.PaidAmount,
                    PaymentNo = y.PaymentNo,
                    Refno = y.Refno,
                    Remarks = y.Remarks
                }).ToList()
            }).ToPagedList(currentPageIndex, PageSize);
            return PartialView("../UtilityPayment/_List", invoice);
        }

        [HttpGet]
        public async Task<PartialViewResult> AddOrUpdate()
        {
            try
            {
                UtilityPaymentModel model = new UtilityPaymentModel();
                int? paymentno = await db.tbl_eb_water_paymenthd.Select(x => x.PaymentNo).OrderByDescending(x => x).FirstOrDefaultAsync();
                model.PaymentNo = paymentno == null ? 1 : paymentno.Value + 1;


                ViewBag.Utiltiyname = new SelectList(db.tbl_utilitiesmaster.Select(x => new { UtilityId = x.Utility_id, UtilityName = x.Utility_Name }), "UtilityId", "UtilityName");
                ViewBag.Suppliername = new SelectList(db.tbl_suppliermaster.Select(x => new { SupplierId = x.Supplier_Id, SupplierName = x.Supplier_Name }), "SupplierId", "SupplierName");

                ViewBag.AdvAcCode = new SelectList(db.Database.SqlQuery<int>("SELECT PaymentNo FROM view_advance_pending_eb").ToList());
                var _paymentType = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "Reccategory");
                if (_paymentType != null)
                {
                    ViewBag.PaymentType = new SelectList(_paymentType.combovalue.Split(','));
                }
                else
                {
                    ViewBag.PaymentType = new SelectList(null);
                }
                var _paymentmode = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "RecpType");
                if (_paymentmode != null)
                {
                    ViewBag.PaymentMode = new SelectList(_paymentmode.combovalue.Split(','));
                }
                else
                {
                    ViewBag.PaymentMode = new SelectList(null);
                }
                ViewBag.BankAcCode = new SelectList(Common.BankDetails, "AccountNumber", "AccountNumber");
                ViewBag.BankAcName = new SelectList(Common.BankDetails, "AccountNumber", "BankName");

                var _pdcstatus = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "PDCstatus");
                if (_paymentmode != null)
                {
                    ViewBag.pdcstatus = new SelectList(_pdcstatus.combovalue.Split(','));
                }
                else
                {
                    ViewBag.pdcstatus = new SelectList(null);
                }
                var ebBillPayment = db.Database.SqlQuery<EBBillPaymentPending>("select * from view_ebbill_payment_pending").ToList();

                ViewBag.Refno = new SelectList(ebBillPayment, "refno", "refno");
                ViewBag.Meterno = new SelectList(ebBillPayment, "refno", "meterno");
                ViewBag.billNo = new SelectList(ebBillPayment, "refno", "billno");

                return PartialView("../UtilityPayment/_AddorUpdate", model);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(UtilityPaymentModel model)
        {

            MessageResult result = new MessageResult();
            try
            {
                MySqlParameter pa = new MySqlParameter();
                string PFlag = null;
                model.BankAcName = Common.BankDetails.FirstOrDefault(x => x.AccountNumber == x.AccountNumber)?.BankName;
                if (model.id == 0)
                {
                    int? paymentno = await db.tbl_eb_water_paymenthd.Select(x => x.PaymentNo).OrderByDescending(x => x).FirstOrDefaultAsync();
                    model.PaymentNo = paymentno == null ? 1 : paymentno.Value + 1;
                    PFlag = "INSERT";
                }
                else
                {
                    PFlag = "UPDATE";
                }
                string utilityPayment = null;
                if (model.UtilityPaymentDetails != null)
                {
                    foreach (var item in model.UtilityPaymentDetails)
                    {
                        var date = item.billDate.HasValue ? "'" + item.billDate.Value.Date.ToString("yyyy-MM-dd") + "'" : "null";
                        if (string.IsNullOrWhiteSpace(utilityPayment))
                        {
                            utilityPayment = "(" + model.PaymentNo + "," + item.Refno + ",'" + item.Meterno + "','" + item.billNo + "'," + date +
                                "," + item.billAmount + "," + item.PaidAmount + "," + item.Debitamt + ",'" + item.Remarks + "')";
                        }
                        else
                        {
                            utilityPayment = utilityPayment + ",(" + model.PaymentNo + "," + item.Refno + ",'" + item.Meterno + "','" + item.billNo + "'," + date +
                                "," + item.billAmount + "," + item.PaidAmount + "," + item.Debitamt + ",'" + item.Remarks + "')";
                        }
                    }
                }
                //if (!string.IsNullOrWhiteSpace(model.Tenant_Name))
                //{
                //    var groupedtenantValues = model.Tenant_Name.Split(':');
                //    model.Tenant_id = Convert.ToInt32(groupedtenantValues[0]);
                //    model.Tenant_Name = string.IsNullOrEmpty(groupedtenantValues[1]) ? null : groupedtenantValues[1];
                //}

                //if (!string.IsNullOrWhiteSpace(model.Property_Name))
                //{
                //    var groupedpropertyValues = model.Property_Name.Split(':');
                //    model.Property_ID = groupedpropertyValues[0];
                //    model.Property_Name = string.IsNullOrEmpty(groupedpropertyValues[1]) ? null : groupedpropertyValues[1];
                //}

                //if (!string.IsNullOrWhiteSpace(model.unit_Name))
                //{
                //    var groupedunitValues = model.unit_Name.Split(':');
                //    model.Unit_ID = groupedunitValues[0];
                //    model.unit_Name = string.IsNullOrEmpty(groupedunitValues[1]) ? null : groupedunitValues[1];
                //}

                object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PPaymentNo", model.PaymentNo),
                         new MySqlParameter("@PPaymentDate",model.PaymentDate),
                         new MySqlParameter("@PUtility_id", model.Utility_id),
                         new MySqlParameter("@PUtiltiy_name", model.Utiltiy_name),
                         new MySqlParameter("@PSupplier_id", model.Supplier_id),
                         new MySqlParameter("@PSupplier_name", model.Supplier_name),
                         new MySqlParameter("@PPaymentType", model.PaymentType),
                         new MySqlParameter("@PPaymentMode", model.PaymentMode),
                         new MySqlParameter("@PTotalAmount", model.TotalAmount),
                         new MySqlParameter("@PAmtInWords", model.AmtInWords),
                         new MySqlParameter("@PDDChequeNo", model.DDChequeNo),
                         new MySqlParameter("@PCheqdate", model.Cheqdate),
                         new MySqlParameter("@Ppdcstatus", model.pdcstatus),
                         new MySqlParameter("@PBankAcCode", model.BankAcCode),
                         new MySqlParameter("@PBankAcName", model.BankAcName),
                         new MySqlParameter("@PAdvAcCode", model.AdvAcCode),
                         new MySqlParameter("@PNarration",model.Narration),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name),
                         new MySqlParameter("@PPaymentdt", utilityPayment)
                    };
                var ebpayment = await db.Database.SqlQuery<object>("call Usp_eb_water_paymenthd_All(@PFlag, @PPaymentNo, @PPaymentDate, @PUtility_id, @PUtiltiy_name, @PSupplier_id, @PSupplier_name, @PPaymentType, @PPaymentMode, @PTotalAmount, @PAmtInWords, @PDDChequeNo, @PCheqdate, @Ppdcstatus, @PBankAcCode, @PBankAcName, @PAdvAcCode, @PNarration, @PCreateduser, @PPaymentdt)", parameters).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: UtilityPayment/Details/5
        public ActionResult GetUtilityPaymentDetail(int id)
        {
            try
            {
                var ebBillPayment = db.Database.SqlQuery<EBBillPaymentPending>("select * from view_ebbill_payment_pending where refno=" + id).ToList().FirstOrDefault();
                return Json(ebBillPayment, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        // GET: UtilityPayment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UtilityPayment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilityPayment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UtilityPayment/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var eb_water_payment = await db.tbl_eb_water_paymenthd.FirstOrDefaultAsync(x => x.id == id);
                if (eb_water_payment == null)
                {
                    return HttpNotFound();
                }
                var model = new UtilityPaymentModel()
                {
                    AdvAcCode = eb_water_payment.AdvAcCode,
                    AmtInWords = eb_water_payment.AmtInWords,
                    BankAcCode = eb_water_payment.BankAcCode,
                    BankAcName = eb_water_payment.BankAcName,
                    Cheqdate = eb_water_payment.Cheqdate,
                    DDChequeNo = eb_water_payment.DDChequeNo,
                    id = eb_water_payment.id,
                    Narration = eb_water_payment.Narration,
                    PaymentDate = eb_water_payment.PaymentDate,
                    PaymentMode = eb_water_payment.PaymentMode,
                    PaymentNo = eb_water_payment.PaymentNo,
                    PaymentType = eb_water_payment.PaymentType,
                    pdcstatus = eb_water_payment.pdcstatus,
                    Supplier_id = eb_water_payment.Supplier_id,
                    Supplier_name = eb_water_payment.Supplier_name,
                    TotalAmount = eb_water_payment.TotalAmount,
                    Utility_id = eb_water_payment.Utility_id,
                    Utiltiy_name = eb_water_payment.Utiltiy_name,
                    UtilityPaymentDetails = eb_water_payment.tbl_eb_water_paymentdt.Select(y => new UtilityPaymentDetail
                    {
                        billAmount = y.billAmount,
                        billDate = y.billDate,
                        billNo = y.billNo,
                        Debitamt = y.Debitamt,
                        id = y.id,
                        Meterno = y.Meterno,
                        PaidAmount = y.PaidAmount,
                        PaymentNo = y.PaymentNo,
                        Refno = y.Refno,
                        Remarks = y.Remarks
                    }).ToList()
                };
                //int? paymentno = await db.tbl_eb_water_paymenthd.Select(x => x.PaymentNo).OrderByDescending(x => x).FirstOrDefaultAsync();
                //model.PaymentNo = paymentno == null ? 1 : paymentno.Value + 1;


                ViewBag.Utiltiyname = new SelectList(db.tbl_utilitiesmaster.Select(x => new { UtilityId = x.Utility_id, UtilityName = x.Utility_Name }), "UtilityId", "UtilityName", model.Utility_id);
                ViewBag.Suppliername = new SelectList(db.tbl_suppliermaster.Select(x => new { SupplierId = x.Supplier_Id, SupplierName = x.Supplier_Name }), "SupplierId", "SupplierName", model.Supplier_id);

                ViewBag.AdvAcCode = new SelectList(db.Database.SqlQuery<int>("SELECT PaymentNo FROM view_advance_pending_eb").ToList(), model.AdvAcCode);
                var _paymentType = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "Reccategory");
                if (_paymentType != null)
                {
                    ViewBag.PaymentType = new SelectList(_paymentType.combovalue.Split(','), model.PaymentType);
                }
                else
                {
                    ViewBag.PaymentType = new SelectList(null);
                }
                var _paymentmode = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "RecpType");
                if (_paymentmode != null)
                {
                    ViewBag.PaymentMode = new SelectList(_paymentmode.combovalue.Split(','), model.PaymentMode);
                }
                else
                {
                    ViewBag.PaymentMode = new SelectList(null);
                }
                ViewBag.BankAcCode = new SelectList(Common.BankDetails, "AccountNumber", "AccountNumber", model.BankAcCode);
                ViewBag.BankAcName = new SelectList(Common.BankDetails, "AccountNumber", "BankName", model.BankAcCode);

                var _pdcstatus = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "PDCstatus");
                if (_paymentmode != null)
                {
                    ViewBag.pdcstatus = new SelectList(_pdcstatus.combovalue.Split(','), model.pdcstatus);
                }
                else
                {
                    ViewBag.pdcstatus = new SelectList(null);
                }
                var ebBillPayment = db.Database.SqlQuery<EBBillPaymentPending>("select * from view_ebbill_payment_pending").ToList();

                ViewBag.Refno = new SelectList(ebBillPayment, "refno", "refno");
                ViewBag.Meterno = new SelectList(ebBillPayment, "refno", "meterno");
                ViewBag.billNo = new SelectList(ebBillPayment, "refno", "billno");

                return PartialView("../UtilityPayment/_AddorUpdate", model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: UtilityPayment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // POST: UtilityPayment/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                MessageResult result = new MessageResult();
                var model = await db.tbl_eb_water_paymenthd.FirstOrDefaultAsync(x => x.id == id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                object[] parameters = {
                         new MySqlParameter("@PFlag", "DELETE"),
                         new MySqlParameter("@PPaymentNo", model.PaymentNo),
                         new MySqlParameter("@PPaymentDate",model.PaymentDate),
                         new MySqlParameter("@PUtility_id", model.Utility_id),
                         new MySqlParameter("@PUtiltiy_name", model.Utiltiy_name),
                         new MySqlParameter("@PSupplier_id", model.Supplier_id),
                         new MySqlParameter("@PSupplier_name", model.Supplier_name),
                         new MySqlParameter("@PPaymentType", model.PaymentType),
                         new MySqlParameter("@PPaymentMode", model.PaymentMode),
                         new MySqlParameter("@PTotalAmount", model.TotalAmount),
                         new MySqlParameter("@PAmtInWords", model.AmtInWords),
                         new MySqlParameter("@PDDChequeNo", model.DDChequeNo),
                         new MySqlParameter("@PCheqdate", model.Cheqdate),
                         new MySqlParameter("@Ppdcstatus", model.pdcstatus),
                         new MySqlParameter("@PBankAcCode", model.BankAcCode),
                         new MySqlParameter("@PBankAcName", model.BankAcName),
                         new MySqlParameter("@PAdvAcCode", model.AdvAcCode),
                         new MySqlParameter("@PNarration",model.Narration),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name),
                         new MySqlParameter("@PPaymentdt", null)
                    };
                var ebpayment = await db.Database.SqlQuery<object>("call Usp_eb_water_paymenthd_All(@PFlag, @PPaymentNo, @PPaymentDate, @PUtility_id, @PUtiltiy_name, @PSupplier_id, @PSupplier_name, @PPaymentType, @PPaymentMode, @PTotalAmount, @PAmtInWords, @PDDChequeNo, @PCheqdate, @Ppdcstatus, @PBankAcCode, @PBankAcName, @PAdvAcCode, @PNarration, @PCreateduser, @PPaymentdt)", parameters).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
