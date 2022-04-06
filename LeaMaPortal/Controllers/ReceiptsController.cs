using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using LeaMaPortal.Models;
using System.Threading;
using System.Threading.Tasks;
using LeaMaPortal.Helpers;
using MvcPaging;
using MySql.Data.MySqlClient;

namespace LeaMaPortal.Controllers
{
    [Authorize]
    public class ReceiptsController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();
        public static String RegNam;

        // GET: Receipts
        public ActionResult Index()
        {
            //List<SelectListItem> ReceiptType = new List<SelectListItem>
            //                         ();
            //ReceiptType.Add(new SelectListItem
            //{
            //    Text = "Advance",
            //    Value = "1"
            //});
            //ReceiptType.Add(new SelectListItem
            //{
            //    Text = "Against Invoice",
            //    Value = "2",
            //    Selected = true
            //});
            //ReceiptType.Add(new SelectListItem
            //{
            //    Text = "Others",
            //    Value = "3"
            //});

            //List<SelectListItem> ReceiptMode = new List<SelectListItem>
            //                        ();
            //ReceiptMode.Add(new SelectListItem
            //{
            //    Text = "Cash",
            //    Value = "1"
            //});
            //ReceiptMode.Add(new SelectListItem
            //{
            //    Text = "Cheque",
            //    Value = "2",
            //    Selected = true
            //});
            //ReceiptMode.Add(new SelectListItem
            //{
            //    Text = "Online",
            //    Value = "3"
            //});
            //ReceiptMode.Add(new SelectListItem
            //{
            //    Text = "Cash",
            //    Value = "4"
            //});
            //ReceiptMode.Add(new SelectListItem
            //{
            //    Text = "Pdc",
            //    Value = "5",
            //    Selected = true
            //});
            //ReceiptMode.Add(new SelectListItem
            //{
            //    Text = "Advance Ajustment",
            //    Value = "3"
            //});
            //List<SelectListItem> PDCStatus = new List<SelectListItem>
            //                   ();
            //PDCStatus.Add(new SelectListItem
            //{
            //    Text = "Received",
            //    Value = "1"
            //});
            //PDCStatus.Add(new SelectListItem
            //{
            //    Text = "Cleared",
            //    Value = "2",
            //    Selected = true
            //});
            //PDCStatus.Add(new SelectListItem
            //{
            //    Text = "Bounced",
            //    Value = "3"
            //});
            //PDCStatus.Add(new SelectListItem
            //{
            //    Text = "Cancelled",
            //    Value = "4"
            //});
            //ViewBag.ReceiptType = ReceiptType;
            //ViewBag.ReceiptMode = ReceiptMode;
            //ViewBag.PDCStatus = PDCStatus;


            //var Pdc = db.Database.SqlQuery<object>(@"call usp_split('Receipts','PDCstatus',',',null)").ToList();
            //ViewBag.PDCStatus = new SelectList(Pdc);
            //ViewBag.PDCStatus = new SelectList("");

            //ReceiptViewModel model = new ReceiptViewModel();

            //var receipts = db.tbl_receipthd.OrderByDescending(x => x.id).FirstOrDefault();
            //model.ReceiptNo = receipts != null ? receipts.id + 1 : 1;

            //model.ReceiptDate = DateTime.Today;
            //ViewBag.Reccategory = new SelectList(Common.Reccategory);
            //ViewBag.ReceiptMode = new SelectList(Common.ReceiptMode);
            //return PartialView("../Receipts/Index", model);
            if (Session["Region"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            RegNam = Session["Region"].ToString();
            return View();
        }

        [HttpGet]
        public PartialViewResult List(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
                var Rec = db.tbl_receipthd.Where(x => x.Delmark != "*" && x.Region_Name == RegNam);
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    Rec = Rec.Where(x => x.ReceiptNo.ToString().Contains(Search) || x.Tenant_Name.Contains (Search ) || x.Property_Name.Contains (Search ) || x.unit_Name .Contains (Search ) || x.agreement_no.ToString ().Contains (Search ) || x.RecpType.Contains (Search ) || x.TotalAmount.ToString ().Contains (Search ) || x.DDChequeNo.ToString().Contains(Search) || x.PDCstatus.Contains(Search));
                }
                var receipts = Rec.OrderByDescending(x => x.id).Select(x => new ReceiptViewModel()
                {
                    AdvAcCode = x.AdvAcCode,
                    agreement_no = x.agreement_no,
                    AmtInWords = x.AmtInWords,
                    BankAcCode = x.BankAcCode,
                    BankAcName = x.BankAcName,
                    DDChequeDate = x.DDChequeDate,
                    DDChequeNo = x.DDChequeNo,
                    Id = x.id,
                    Narration = x.Narration,
                    Reccategory = x.Reccategory,
                    ReceiptNo = x.ReceiptNo,
                    RecpType = x.RecpType,
                    ReceiptDate = x.ReceiptDate,
                    PDCstatus = x.PDCstatus,
                    Property_id = x.Property_id,
                    Property_Name = x.Property_Name,
                    TotalAmount = x.TotalAmount,
                    unit_Name = x.unit_Name,
                    Tenant_Name = x.Tenant_Name,
                    ReceiptDetailsList = db.tbl_receiptdt.Where(y => y.Delmark != "*" && y.ReceiptNo == x.ReceiptNo && y.Region_Name == RegNam).Select(z => new ReceiptDetailsViewModel
                    {
                        Id = z.id,
                        CreditAmt = z.CreditAmt,
                        Description = z.Description,
                        Invno = z.Invno,
                        InvoiceAmount = z.InvoiceAmount,
                        InvoiceDate = z.InvoiceDate,
                        Invtype = z.Invtype,
                        ReceivedAmount = z.ReceivedAmount,
                        Balance = z.InvoiceAmount - (z.ReceivedAmount + z.CreditAmt),
                        Remarks = z.Remarks
                    }).ToList()
                }).ToPagedList(currentPageIndex, PageSize);
                return PartialView("../Receipts/_List", receipts);
            }
            catch
            {
                throw;
            }
            
        }
                
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var rec = await db.tbl_receipthd.Where(x => x.Region_Name == RegNam).FirstOrDefaultAsync(f => f.id == id);
                var data = new ReceiptViewModel();
                data.AdvAcCode = rec.AdvAcCode;
                data.agreement_no = rec.agreement_no;
                data.AmtInWords = rec.AmtInWords;
                data.BankAcCode = rec.BankAcCode;
                data.BankAcName = rec.BankAcName;
                data.DDChequeDate = rec.DDChequeDate;
                data.DDChequeNo = rec.DDChequeNo;
                data.Id = rec.id;
                data.Narration = rec.Narration;
                data.PDCstatus = rec.PDCstatus;
                data.Property_id = rec.Property_id;
                data.Property_Name = rec.Property_Name;
                data.Unit_ID = rec.Unit_ID;
                data.unit_Name = rec.unit_Name;
                data.Reccategory = rec.Reccategory;
                data.ReceiptDate = rec.ReceiptDate;
                data.ReceiptNo = rec.ReceiptNo;
                data.RecpType = rec.RecpType;
                data.TotalAmount = rec.TotalAmount;
                data.Tenant_id = rec.Tenant_id;
                data.Tenant_Name = rec.Tenant_Name;
                data.ReceiptDetailsList = db.tbl_receiptdt.Where(w => w.ReceiptNo == rec.ReceiptNo && w.Region_Name == RegNam)
                    .Select(s => new ReceiptDetailsViewModel
                    {
                        CreditAmt = s.CreditAmt,
                        Balance = s.InvoiceAmount - (s.ReceivedAmount + s.CreditAmt),
                        Description = s.Description,
                        Id = s.id,
                        Invno = s.Invno,
                        InvoiceAmount = s.InvoiceAmount,
                        InvoiceDate = s.InvoiceDate,
                        Invtype = s.Invtype,
                        ReceivedAmount = s.ReceivedAmount,
                        Remarks = s.Remarks
                    }).ToList();
                ViewBag.Reccategory = new SelectList(Common.Reccategory, data.Reccategory);
                ViewBag.RecpType = new SelectList(Common.ReceiptMode, data.RecpType);
                var agreements = db.tbl_agreement.Where(w => w.Delmark != "*" && w.Region_Name == RegNam).OrderBy(o => o.id);
                var agreemntno = db.tbl_receipthd.Where(f => f.Delmark != "*" && f.Region_Name == RegNam).OrderBy(o => o.id);
                var agreementdts = await db.tbl_agreement.FirstOrDefaultAsync(f => f.Agreement_No == data.agreement_no && f.Region_Name == RegNam);
                //ViewBag.agreement_no = new SelectList(agreemntno, "agreement_No", "agreement_No", data.agreement_no); //, data.agreement_no
                ViewBag.agreement_no = new SelectList(db.tbl_receipthd.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).OrderBy(x => x.agreement_no).Select(x => x.agreement_no));
                //if(agreements.)
                data.ContracAgreementDate = agreementdts.Agreement_Date;
                var tenant = db.view_tenant.Where(x => x.Region_Name == RegNam).Select(x => new { TenantId = x.Tenant_id, TenantName = x.First_Name, Type = x.type, GroupedValue = x.Tenant_id + ":" + x.First_Name + ":" + x.type });
                ViewBag.Tenant_id = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantId", selectedValue: data.Tenant_id);
                ViewBag.TenantName = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantName", selectedValue: data.Tenant_id);
                ViewBag.BankAcCode = new SelectList(Common.BankDetails, "AccountNumber", "AccountNumber", data.BankAcCode);
                ViewBag.Bank_AcName = new SelectList(Common.BankDetails, "AccountNumber", "BankName", data.BankAcCode);
                ViewBag.PDCstatus = new SelectList(Common.Receipts_PDCStatus, data.PDCstatus);
                ViewBag.InvoiceNumber = new SelectList(db.view_invoice_receipt_pending.Where(x => x.Region_name == RegNam).OrderBy(o => o.invno).Distinct(), "invno", "invno");
                var cheque = db.tbl_receipthd.Where(c => c.ReceiptNo == data.ReceiptNo && c.Region_Name == RegNam);
                ViewBag.ddChequeNumber = new SelectList(cheque,"DDChequeNo","DDChequeNo",data.DDChequeNo);
                if (data.agreement_no == null || data.agreement_no == 0)
                {
                    var properties = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Property" && w.Delmark != "*" && w.Region_Name == RegNam).OrderBy(o => o.id);

                    ViewBag.Property_id = new SelectList(properties, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", data.Property_id);
                    ViewBag.PropertyName = new SelectList(properties, "Property_ID_Tawtheeq", "Property_Name", data.Property_id);

                    var units = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Delmark != "*" && w.Region_Name == RegNam).OrderBy(o => o.id);

                    ViewBag.Unit_ID = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", data.Unit_ID);
                    ViewBag.unitName = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_Property_Name", data.Unit_ID);
                }
                else
                {
                    var agreementDetails = await agreements.FirstOrDefaultAsync(y => y.Agreement_No == data.agreement_no);
                    List<UnitDropdown> unitsDropdown = new List<UnitDropdown>();
                    unitsDropdown.Add(new UnitDropdown()
                    {
                        Unitid = agreementDetails.Unit_ID_Tawtheeq,
                        unitName = agreementDetails.Unit_Property_Name
                    });
                    ViewBag.Unit_ID = new SelectList(unitsDropdown, "Unitid", "Unitid", data.Unit_ID);
                    ViewBag.unitName = new SelectList(unitsDropdown, "Unitid", "unitName", data.Unit_ID);

                    List<PropertyDropdown> propertyDropdown = new List<PropertyDropdown>();
                    propertyDropdown.Add(new PropertyDropdown()
                    {
                        Propertyid = agreementDetails.Property_ID_Tawtheeq,
                        PropertyName = agreementDetails.Properties_Name
                    });
                    ViewBag.Property_id = new SelectList(propertyDropdown, "Propertyid", "Propertyid", data.Property_id);
                    ViewBag.PropertyName = new SelectList(propertyDropdown, "Propertyid", "PropertyName", data.Property_id);
                }
                var advancepaymentnumber = await db.Database.SqlQuery<int>("Select PaymentNo from view_advance_pending_payment").ToListAsync();
                ViewBag.AdvAcCode = new SelectList(advancepaymentnumber, data.AdvAcCode);

                return PartialView("../Receipts/_AddorUpdate", data);
            }
            catch
            {
                throw;
            }
            
            //var advancepaymentnumber = db.Database.SqlQuery<int>("Select PaymentNo from view_advance_pending_payment");
            //ViewBag.AdvAcCode = new SelectList(advancepaymentnumber, data.AdvAcCode);
            //var properties = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Property").OrderBy(x => x.Property_Id).Select(x => new { PropertyId = x.Property_Id, PropertyName = x.Property_Name, GroupedValue = x.Property_Id + ":" + x.Property_Name, PropertyIdTawtheeq = x.Property_ID_Tawtheeq });
            //ViewBag.Property_id = new SelectList(properties, "PropertyIdTawtheeq", "PropertyIdTawtheeq", data.Property_id);
            //ViewBag.Property_Name = new SelectList(properties, "PropertyIdTawtheeq", "PropertyName", data.Property_Name);

            //var units = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Unit").OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new { UnitId = x.Unit_ID_Tawtheeq, UnitName = x.Unit_Property_Name, GroupedValue = x.Unit_ID_Tawtheeq + ":" + x.Unit_Property_Name });
            //ViewBag.Unit_ID = new SelectList(units, "UnitId", "UnitId", data.Unit_ID);
            //ViewBag.unit_Name = new SelectList(units, "UnitId", "UnitName", data.unit_Name);

            
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                MessageResult result = new MessageResult();
                var rec = await db.tbl_receipthd.FirstOrDefaultAsync(f => f.id == id && f.Region_Name == RegNam);
                var data = new ReceiptViewModel();
                data.AdvAcCode = rec.AdvAcCode;
                data.agreement_no = rec.agreement_no;
                data.AmtInWords = rec.AmtInWords;
                data.BankAcCode = rec.BankAcCode;
                data.BankAcName = rec.BankAcName;
                data.DDChequeDate = rec.DDChequeDate;
                data.DDChequeNo = rec.DDChequeNo;
                data.Id = rec.id;
                data.Narration = rec.Narration;
                data.PDCstatus = rec.PDCstatus;
                data.Property_id = rec.Property_id;
                data.Property_Name = rec.Property_Name;
                data.Reccategory = rec.Reccategory;
                data.ReceiptDate = rec.ReceiptDate;
                data.ReceiptNo = rec.ReceiptNo;
                data.RecpType = rec.RecpType;
                data.Tenant_id = rec.Tenant_id;
                data.Tenant_Name = rec.Tenant_Name;
                data.ReceiptDetailsList = db.tbl_receiptdt.Where(w => w.ReceiptNo == rec.ReceiptNo && w.Region_Name == RegNam)
                    .Select(s => new ReceiptDetailsViewModel
                    {
                        CreditAmt = s.CreditAmt,
                        Balance = s.InvoiceAmount - (s.ReceivedAmount + s.CreditAmt),
                        Description = s.Description,
                        Id = s.id,
                        Invno = s.Invno,
                        InvoiceAmount = s.InvoiceAmount,
                        InvoiceDate = s.InvoiceDate,
                        Invtype = s.Invtype,
                        ReceivedAmount = s.ReceivedAmount,
                        Remarks = s.Remarks
                    }).ToList();

                string recDet = null;
                if (data.ReceiptDetailsList != null)
                {
                    foreach (var item in data.ReceiptDetailsList)
                    {
                        if (string.IsNullOrWhiteSpace(recDet))
                        {
                            recDet = "(" + data.ReceiptNo + ",'" + item.Invno + "','" + item.InvoiceDate +
                                "'," + item.Invtype + "," + item.InvoiceAmount + "," + item.CreditAmt + "," + item.ReceivedAmount + "," + item.Remarks + "," + item.Description + ")";
                        }
                        else
                        {
                            recDet = recDet + ",(" + data.ReceiptNo + ",'" + item.Invno + "','" + item.InvoiceDate +
                                "'," + item.Invtype + "," + item.InvoiceAmount + "," + item.CreditAmt + "," + item.ReceivedAmount + "," + item.Remarks + "," + item.Description + ")";
                        }
                    }
                }
                object[] parameters = {
                         new MySqlParameter("@PFlag", "DELETE"),
                         new MySqlParameter("@PReccategory", data.Reccategory),
                         new MySqlParameter("@PRecpType",data.RecpType),
                         new MySqlParameter("@PReceiptNo", data.ReceiptNo),
                         new MySqlParameter("@PReceiptDate", data.ReceiptDate),
                         new MySqlParameter("@Pagreement_no", data.agreement_no),
                         new MySqlParameter("@PProperty_id", data.Property_id),
                         new MySqlParameter("@PProperty_Name", data.Property_Name),
                         new MySqlParameter("@PUnit_ID", data.Unit_ID),
                         new MySqlParameter("@Punit_Name", data.unit_Name),
                         new MySqlParameter("@PTenant_id", data.Tenant_id),
                         new MySqlParameter("@PTenant_Name", data.Tenant_Name),
                         new MySqlParameter("@PTotalAmount", data.TotalAmount),
                         new MySqlParameter("@PAmtInWords", data.AmtInWords),
                         new MySqlParameter("@PDDChequeNo", data.DDChequeNo),
                         new MySqlParameter("@PPdcstatus", data.PDCstatus),
                         new MySqlParameter("@PBankAcCode", data.BankAcCode),
                         new MySqlParameter("@PBankAcName", data.BankAcName),
                         new MySqlParameter("@PAdvAcCode", data.AdvAcCode),
                         new MySqlParameter("@PDDChequeDate", data.DDChequeDate),
                         new MySqlParameter("@PNarration", data.Narration),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name),
                         new MySqlParameter("@PReceiptdt", recDet),
                         new MySqlParameter("@PRegion_Name", Session["Region"].ToString()),
                         new MySqlParameter("@PCountry",  Session["Country"].ToString()),
                    };
                var invoice = await db.Database.SqlQuery<object>("CALL Usp_Receipt_All(@PFlag, @PReccategory, @PRecpType, @PReceiptNo, @PReceiptDate, @Pagreement_no, @PProperty_id, @PProperty_Name,@PUnit_ID, @Punit_Name, @PTenant_id, @PTenant_Name, @PTotalAmount, @PAmtInWords, @PDDChequeNo, @PPdcstatus, @PBankAcCode, @PBankAcName, @PAdvAcCode, @PDDChequeDate, @PNarration, @PCreateduser, @PReceiptdt,@PRegion_Name,@PCountry)", parameters).ToListAsync();
                result.Message = "Receipts deleted successfully";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }        

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            try
            {
                ReceiptViewModel model = new ReceiptViewModel();

                var receipts = db.tbl_receipthd.Where(x => x.Region_Name == RegNam).OrderByDescending(x => x.id).FirstOrDefault();
                model.ReceiptNo = receipts != null ? receipts.ReceiptNo + 1 : 1;

                model.ReceiptDate = DateTime.Today;
                ViewBag.Reccategory = new SelectList(Common.Reccategory);
                ViewBag.RecpType = new SelectList(Common.ReceiptMode);
                ViewBag.agreement_no = new SelectList(db.tbl_agreement.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).OrderBy(x => x.Agreement_No).Select(x => x.Agreement_No));
                //*********************
                //Old Code
                //var advancepaymentnumber = db.Database.SqlQuery<int>("Select PaymentNo from view_advance_pending_payment");
                //ViewBag.AdvAcCode = new SelectList(advancepaymentnumber);

                //New Code
                var advancepaymentnumber = db.Database.SqlQuery<int>("Select ReceiptNo from view_advance_pending where region_name={0}",RegNam);
                ViewBag.AdvAcCode = new SelectList(advancepaymentnumber);
                //*********************
                var properties = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Property" && x.Region_Name == RegNam).OrderBy(x => x.Property_Id).Select(x => new { PropertyId = x.Property_Id, PropertyName = x.Property_Name, GroupedValue = x.Property_Id + ":" + x.Property_Name, PropertyIdTawtheeq = x.Property_ID_Tawtheeq });
                ViewBag.Property_id = new SelectList(properties, "PropertyId", "PropertyIdTawtheeq");
                ViewBag.PropertyName = new SelectList(properties, "PropertyId", "PropertyName");

                var units = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Unit" && x.Region_Name == RegNam).OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new { RefPropertyID = x.Ref_Unit_Property_ID, UnitId = x.Unit_ID_Tawtheeq, UnitName = x.Unit_Property_Name, GroupedValue = x.Unit_ID_Tawtheeq + ":" + x.Unit_Property_Name });
                ViewBag.Unit_ID = new SelectList(units, "RefPropertyID", "UnitId");
                ViewBag.unitName = new SelectList(units, "UnitId", "UnitName");

                var tenant = db.view_tenant.Where(x => x.Region_Name == RegNam).Select(x => new { TenantId = x.Tenant_id, TenantName = x.First_Name, Type = x.type, GroupedValue = x.Tenant_id + ":" + x.First_Name + ":" + x.type });
                ViewBag.Tenant_id = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantId", selectedValue: null);
                ViewBag.TenantName = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantName", selectedValue: null);
                ViewBag.BankAcCode = new SelectList(Common.BankDetails, "AccountNumber", "AccountNumber");
                ViewBag.Bank_AcName = new SelectList(Common.BankDetails, "AccountNumber", "BankName");
                ViewBag.PDCstatus = new SelectList(Common.Receipts_PDCStatus);
                ViewBag.InvoiceNumber = new SelectList(db.view_invoice_receipt_pending.OrderBy(o => o.invno).Distinct(), "invno", "invno");
                ViewBag.ddChequeNumber = new SelectList("", "");
                return PartialView("../Receipts/_AddorUpdate", model);
            }
            catch
            {
                throw;
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(ReceiptViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {

                string PFlag = Common.UPDATE;
                result.Message = "Receipts updated successfully";
                if (model.Id == 0)
                {
                    PFlag = Common.INSERT;
                    var receipts = db.tbl_receipthd.Where(x => x.Region_Name == RegNam).OrderByDescending(x => x.id).FirstOrDefault();
                    model.ReceiptNo = receipts != null ? receipts.ReceiptNo + 1 : 1;
                    result.Message = "Receipts added successfully";
                }
                string recDet = null;
                if (model.ReceiptDetailsList != null)
                {
                    foreach (var item in model.ReceiptDetailsList)
                    {
                        var invoicedate = item.InvoiceDate.HasValue ? "'" + item.InvoiceDate.Value.Date.ToString("yyyy-MM-dd") + "'" : "null";
                        if (string.IsNullOrWhiteSpace(recDet))
                        {
                            recDet = "(" + model.ReceiptNo + ",'" + item.Invno + "'," + invoicedate +
                                ",'" + item.Invtype + "'," + item.InvoiceAmount + "," + item.CreditAmt + "," + item.ReceivedAmount + ",'" + item.Remarks + "','" + item.Description + "')";
                        }
                        else
                        {
                            recDet = recDet + ",(" + model.ReceiptNo + ",'" + item.Invno + "','" + item.InvoiceDate +
                                "'," + item.Invtype + "," + item.InvoiceAmount + "," + item.CreditAmt + "," + item.ReceivedAmount + "," + item.Remarks + "," + item.Description + ")";
                        }
                    }
                }
                object[] param = Helper.GetMySqlParameters<ReceiptViewModel>(model, PFlag, System.Web.HttpContext.Current.User.Identity.Name);
                //var _result = await db.Database.SqlQuery<object>(@"CALL Usp_Receipt_All(@PFlag,
                //                                                , @PReccategory 
                //                                                , @PRecpType 
                //                                                , @PReceiptNo
                //                                                , @PReceiptDate 
                //                                                , @Pagreement_no
                //                                                , @PProperty_id 
                //                                                , @PProperty_Name
                //                                                , @PUnit_ID 
                //                                                , @Punit_Name 
                //                                                , @PTenant_id 
                //                                                , @PTenant_Name 
                //                                                , @PTotalAmount 
                //                                                , @PAmtInWords 
                //                                                , @PDDChequeNo 
                //                                                , @PPdcstatus 
                //                                                , @PBankAcCode
                //                                                , @PBankAcName
                //                                                , @PAdvAcCode 
                //                                                , @PDDChequeDate 
                //                                                , @PNarration 
                //                                                , @PCreateduser 
                //                                                , @PReceiptdt )", param).ToListAsync();

                object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PReccategory", model.Reccategory),
                         new MySqlParameter("@PRecpType",model.RecpType),
                         new MySqlParameter("@PReceiptNo", model.ReceiptNo),
                         new MySqlParameter("@PReceiptDate", model.ReceiptDate),
                         new MySqlParameter("@Pagreement_no", model.agreement_no),
                         new MySqlParameter("@PProperty_id", model.Property_id),
                         new MySqlParameter("@PProperty_Name", model.Property_Name),
                         new MySqlParameter("@PUnit_ID", model.Unit_ID),
                         new MySqlParameter("@Punit_Name", model.unit_Name),  //Request.Form["unit_Name"]
                         new MySqlParameter("@PTenant_id", model.Tenant_id),
                         new MySqlParameter("@PTenant_Name", model.Tenant_Name),
                         new MySqlParameter("@PTotalAmount", model.TotalAmount),
                         new MySqlParameter("@PAmtInWords", model.AmtInWords),
                         new MySqlParameter("@PDDChequeNo", model.DDChequeNo),
                         new MySqlParameter("@PPdcstatus", model.PDCstatus),
                         new MySqlParameter("@PBankAcCode", model.BankAcCode),
                         new MySqlParameter("@PBankAcName", model.BankAcName),
                         new MySqlParameter("@PAdvAcCode", model.AdvAcCode),
                         new MySqlParameter("@PDDChequeDate", model.DDChequeDate),
                         new MySqlParameter("@PNarration", model.Narration),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name),                         
                         new MySqlParameter("@PReceiptdt", recDet),
                         new MySqlParameter("@PRegion_Name",Session["Region"].ToString()),
                         new MySqlParameter("@PCountry",Session["Country"].ToString()),

                    };
                var rec = await db.Database.SqlQuery<object>("CALL Usp_Receipt_All(@PFlag, @PReccategory, @PRecpType, @PReceiptNo, @PReceiptDate, @Pagreement_no, @PProperty_id, @PProperty_Name,@PUnit_ID, @Punit_Name, @PTenant_id, @PTenant_Name, @PTotalAmount, @PAmtInWords, @PDDChequeNo, @PPdcstatus, @PBankAcCode, @PBankAcName, @PAdvAcCode, @PDDChequeDate, @PNarration, @PCreateduser, @PReceiptdt,@PRegion_Name,@PCountry)", parameters).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetReceiptNumber()
        {
            try
            {
                List<ReceiptsDDLChangeViewModel> result = await db.Database.SqlQuery<ReceiptsDDLChangeViewModel>("select * from view_advance_pending where region_name ={0}",RegNam).ToListAsync();
                AdvancePendingSelectList list = new AdvancePendingSelectList();
                list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
                list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
                list.Property_Id = new SelectList(result.Select(r => r.Property_id));
                list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
                list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
                list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
                list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
                list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
                list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
            
            //return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAgreementNoByPdc()
        {
            List<ReceiptsDDLChangeViewModel> result = await db.Database.SqlQuery<ReceiptsDDLChangeViewModel>("select * from view_pdc_pending where region_name={0}",RegNam).ToListAsync();
            AdvancePendingSelectList list = new AdvancePendingSelectList();
            //list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
            list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
            list.Property_Id = new SelectList(result.Select(r => r.Property_id));
            list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
            list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
            list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
            list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
            list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
            list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
            list.pdc_Amount = new SelectList(result.Select(r => r.pdc_Amount));
            list.DDChequeNo = new SelectList(result.Select(r => r.DDChequeNo));
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAgreementNoByInvoice()
        {
            List<ReceiptsDDLChangeViewModel> result = await db.Database.SqlQuery<ReceiptsDDLChangeViewModel>("select * from view_invoice_receipt_pending where region_name ='" + RegNam + "' order by incno").ToListAsync();
            AdvancePendingSelectList list = new AdvancePendingSelectList();
            //list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
            list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
            list.Property_Id = new SelectList(result.Select(r => r.Property_id));
            list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
            list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
            list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
            list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
            list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
            //list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
            list.totalamt = new SelectList(result.Select(r => r.totalamt));
            list.month = new SelectList(result.Select(r => r.month));
            list.year = new SelectList(result.Select(r => r.year));
            list.date = new SelectList(result.Select(r => r.date));
            list.incno = new SelectList(result.Select(r => r.incno));
            list.invtype = new SelectList(result.Select(r => r.invtype));
            list.InvoiceAmount = new SelectList(result.Select(r => r.InvoiceAmount));
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAgreementNoBySecurity()
        {
            List<ReceiptsDDLChangeViewModel> result = await db.Database.SqlQuery<ReceiptsDDLChangeViewModel>("select * from tbl_agreement where ifnull(Advance_Security_Amount,0)>0 and Agreement_No=1  and ifnull(status,'')='' and ifnull(delmark,'')<>'*' and region_name={0}",RegNam).ToListAsync();
            AdvancePendingSelectList list = new AdvancePendingSelectList();
            list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
            list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
            list.Property_Id = new SelectList(result.Select(r => r.Property_id));
            list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
            list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
            list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
            list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
            list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
            list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();
        }
        [HttpGet]
        public string AmountInWords(decimal amount)
        {
            string decimalPart="", text = "";
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
            if (t1 != "" && t2 !="")
            {  text = t1 + "" + t3 + " Dirhams " + "and" + t2 + "" + "" + " fils "; }
            else
            {  text = t1 + "" + t3 + " Dirhams "; }
        

            //string amountInWords = NumberToText.Convert(amount);
            //string amountInWords = amount.Humanize();
            return text;
            //    if (amount == 0)
            //    return "ZERO";
            //if (amount < 0)
            //    return "minus " + AmountInWords(Math.Abs(amount));
            //string words = "";
            //if ((amount / 1000000) > 0)
            //{
            //    words += AmountInWords(amount / 1000000) + " MILLION ";
            //    amount %= 1000000;
            //}
            //if ((amount / 1000) > 0)
            //{
            //    words += AmountInWords(amount / 1000) + " THOUSAND ";
            //    amount %= 1000;
            //}
            //if ((amount / 100) > 0)
            //{
            //    words += AmountInWords(amount / 100) + " HUNDRED ";
            //    amount %= 100;
            //}
            //if (amount > 0)
            //{
            //    if (words != "")
            //        words += "AND ";
            //    var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            //    var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

            //    //if (number < 20)
            //    //    words += unitsMap[number];
            //    //else
            //    //{
            //    //    words += tensMap[number / 10];
            //    //    if ((number % 10) > 0)
            //    //        words += " " + unitsMap[number % 10];
            //    //}
            //}
            //return words;
        }
        [HttpGet]
        public ActionResult GetInvoiceDetails(string InvoiceNumber)
        {
            try
            {
                //var invoice = await db.view_invoice_receipt_pending.FirstOrDefaultAsync(f => f.invno == InvoiceNumber);
                //return Json(new { invtype = invoice.invtype, day = invoice.date, InvoiceAmount = invoice.InvoiceAmount, invDesc =  invoice.Description, invRemark = invoice.Remark });

                var invoice = db.Database.SqlQuery<ViewInvoiceReceiptPending>("select * from view_invoice_receipt_pending where invno = {0} and region_name='{1}'", InvoiceNumber,RegNam).ToList().FirstOrDefault();
                return Json(invoice, JsonRequestBehavior.AllowGet);
                //return Json(new { invtype = invoice.invtype, day = invoice.date.Value.Date, month = invoice.date.Value.Month, year = invoice.date.Value.Year, InvoiceAmount = invoice.InvoiceAmount });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<JsonResult> GetAggrementNumberByMode(string Mode, int Flg)
        {
            try
            {
                if(Flg == 0) // For Receipt Type = 'Advance' and Any Receipt Mode
                {
                    var agreementNumbers = await db.Database.SqlQuery<int>("select Agreement_No from view_receipt_screen_advance_pending where balanceamount <> 0 and payment_mode='" + Mode + "' and region_name ='" + RegNam + "'").ToListAsync();
                    return Json(agreementNumbers.Distinct(), JsonRequestBehavior.AllowGet);
                }
                //else if (Flg == 1) // For Receipt Type = 'security deposit' and Aany Receipt Mode
                //{
                //    var agreementNumbers = await db.Database.SqlQuery<int>("select distinct Agreement_No from view_security_pending where !isnull(Agreement_No)").ToListAsync();
                //    return Json(agreementNumbers.Distinct(), JsonRequestBehavior.AllowGet);
                //}
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<JsonResult> GetAggrementNumbers(string Mode,int Flg)
        {
            try
            {
                if(Flg == 0) // For Receipt Type = 'Others'
                {
                    //Old Query
                    //var agreementNumbers = await db.tbl_agreement.Where(x => x.Delmark != "*").OrderBy(x => x.Agreement_No).Select(x => x.Agreement_No).ToListAsync();
                    //New Query
                    var agreementNumbers = await db.tbl_agreement.Where(x => string.IsNullOrEmpty(x.Delmark) && x.Status != "closed" && x.Region_Name == RegNam).OrderBy(x => x.Agreement_No).Select(x => x.Agreement_No).ToListAsync();
                    return Json(agreementNumbers, JsonRequestBehavior.AllowGet);
                }
                else if (Flg == 1) // For Receipt Type = 'Against Invoice'
                {
                    var agreementNumbers = await db.Database.SqlQuery<int>("select distinct Agreement_No from view_invoice_receipt_pending where !isnull(Agreement_No) and region_name={0}",RegNam).ToListAsync();
                    return Json(agreementNumbers.Distinct(), JsonRequestBehavior.AllowGet);
                }
                else if (Flg == 2) // For Receipt Type = 'security deposit'
                {
                    var agreementNumbers = await db.Database.SqlQuery<int>("select distinct Agreement_No from view_security_pending where !isnull(Agreement_No) and region_name={0}", RegNam).ToListAsync();
                    return Json(agreementNumbers.Distinct(), JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<ActionResult> GetAgreementDetails(int? agreement_no,string Mode, string ReceiptCat)
        {
            try
            {
                var receiptsDropdown = new InvoiceDropdown();
                if (agreement_no == null)
                {
                    receiptsDropdown.Properties = await db.tbl_propertiesmaster.Where(x => string.IsNullOrEmpty(x.Delmark) && x.Property_Flag == "Property" && x.Region_Name == RegNam).OrderBy(x => x.Property_Id).Select(x => new PropertyDropdown { PropertyName = x.Property_Name, Propertyid = x.Property_ID_Tawtheeq }).ToListAsync();

                    receiptsDropdown.Units = await db.tbl_propertiesmaster.Where(x => string.IsNullOrEmpty(x.Delmark) && x.Property_Flag == "Unit" && x.Region_Name == RegNam).OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new UnitDropdown { Unitid = x.Unit_ID_Tawtheeq, unitName = x.Unit_Property_Name }).ToListAsync();

                    receiptsDropdown.Tenants = await db.view_tenant.Where(x => x.Region_Name == RegNam).Select(x => new TenantDropdown{ Tenantid = x.Tenant_id, TenantName = x.First_Name}).ToListAsync();
                    
                    //receiptsDropdown.InvoiceNo = await db.view_invoice_receipt_pending.Select(x => new InvDropDown { InvNum = x.invno }).ToListAsync();

                }
                else
                {
                    var agreement = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == agreement_no && x.Region_Name == RegNam);
                    //var agreementpdc = 
                   
                    if (agreement != null)
                    {
                       // receiptsDropdown.BankAcName = agreement.Ag_Tenant_Name;
                    var tenant = new TenantDropdown()
                    {
                        Tenantid = agreement.Ag_Tenant_id,
                        TenantName = agreement.Ag_Tenant_Name,                                               
                    };
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
                    
                    if (ReceiptCat != "security deposit")
                    {
                            var query = String.Format("select Cheque_No,cheque_date from view_receipt_screen_advance_pending where balanceamount > 0 and payment_mode='{0}' and Agreement_No={1} and region_name='{2}'", Mode, agreement_no,RegNam);
                            //var query = String.Format("select Cheque_No,cheque_date from view_receipt_screen_advance_pending where balanceamount<>0", Mode, agreement_no);
                            List<ChequeDropdown> cheques = await db.Database.SqlQuery<ChequeDropdown>(query).ToListAsync();
                            receiptsDropdown.Properties.Add(property);
                            receiptsDropdown.Tenants.Add(tenant);
                            receiptsDropdown.Units.Add(unit);
                            receiptsDropdown.Cheques.AddRange(cheques);
                            receiptsDropdown.AgreementDate = agreement.Agreement_Date;
                            return Json(receiptsDropdown, JsonRequestBehavior.AllowGet);
                    }
                    else if (ReceiptCat == "security deposit")
                    {
                            var query = String.Format("select DDChequeNo from view_security_pending where Agreement_No={0} and region_name={1}",agreement_no,RegNam);
                            List<ChequeDropdown> cheques = await db.Database.SqlQuery<ChequeDropdown>(query).ToListAsync();
                            var agreementdetails = db.Database.SqlQuery<ViewSecurityPending>("select * from view_security_pending where agreement_no = {0} and region_name='{1}'", agreement_no,RegNam).ToList().FirstOrDefault();
                            receiptsDropdown.Properties.Add(property);
                            receiptsDropdown.Tenants.Add(tenant);
                            receiptsDropdown.Units.Add(unit);
                            receiptsDropdown.AgreementDate = agreement.Agreement_Date;
                           //receiptsDropdown.DDChequeNo = agreementdetails.DDChequeNo;
                            receiptsDropdown.Cheques.AddRange(cheques);
                            receiptsDropdown.Security_chequedate = agreementdetails.Security_chequedate;
                            receiptsDropdown.Security_Flag = agreementdetails.Security_Flag;
                            receiptsDropdown.Security_Amount = agreementdetails.Security_Amount;
                            return Json(receiptsDropdown, JsonRequestBehavior.AllowGet);
                        }
                    
                   }
                    
                }
                return Json(receiptsDropdown, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<JsonResult> GetRecBasInvoiceNo(int? agreement_no)
        {
            try
            {
                if(agreement_no == null)
                {
                    var invoicenumbers = await db.Database.SqlQuery<int>("select invno from view_invoice_receipt_pending where region_name={0}",RegNam).ToListAsync();
                    return Json(invoicenumbers.Distinct(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var invoicenumbers = await db.Database.SqlQuery<int>("select invno from view_invoice_receipt_pending where Agreement_No = {0} and region_name={1}", agreement_no,RegNam).ToListAsync();
                    return Json(invoicenumbers.Distinct(), JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<ActionResult> GetAgreementDetailsOnAdvAdj(int? AdvanceReceiptNo)
        {
            try
            {
                var receiptsDropdown = new InvoiceDropdown();
                int agreementnumber = await db.Database.SqlQuery<int>("select agreement_no from view_advance_pending where ReceiptNo = {0} and region_name={1}", AdvanceReceiptNo,RegNam).FirstOrDefaultAsync();
                var agreement = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == agreementnumber && x.Region_Name == RegNam);
                if (agreement != null)
                {
                    var agreementno = new AgreementDropDown()
                    {
                        AgreementNo = agreementnumber
                    };

                    var tenant = new TenantDropdown()
                    {
                        Tenantid = agreement.Ag_Tenant_id,
                        TenantName = agreement.Ag_Tenant_Name,
                    };
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
                    //var query = String.Format("select Cheque_No,cheque_date from view_receipt_screen_advance_pending where balanceamount<>0 and payment_mode='{0}' and Agreement_No={1}", Mode, agreement_no);
                    ////var query = String.Format("select Cheque_No,cheque_date from view_receipt_screen_advance_pending where balanceamount<>0", Mode, agreement_no);
                    //List<ChequeDropdown> cheques = await db.Database.SqlQuery<ChequeDropdown>(query).ToListAsync();
                    receiptsDropdown.Agreement.Add(agreementno);
                    receiptsDropdown.Properties.Add(property);
                    receiptsDropdown.Tenants.Add(tenant);
                    receiptsDropdown.Units.Add(unit);
                    //receiptsDropdown.Cheques.AddRange(cheques);
                    receiptsDropdown.AgreementDate = agreement.Agreement_Date;
                    
                }
                return Json(receiptsDropdown, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetAdvanceAdjustmentAmount(int? advanceCode)
        {

            try
            {
                var advancepaymentnumber = await db.Database.SqlQuery<int>("Select TotalAmount from view_advance_pending where ReceiptNo =" + advanceCode + " and region_name='" + RegNam + "'").FirstOrDefaultAsync();
                return Json(advancepaymentnumber, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<JsonResult> GetAggrementDetailsByMode(string Mode,int agreement_no,string cheque_no)
        {
            try
            {
                string query;
                if (Mode == "cash")
                {
                    query = String.Format("select BankName,cheque_date,Cheque_Amount from view_receipt_screen_advance_pending where balanceamount<>0 and payment_mode='{0}' and Agreement_No={1} and region_name='{2}'", Mode, agreement_no,RegNam);
                }
                else
                {
                    query = String.Format("select BankName,cheque_date,Cheque_Amount from view_receipt_screen_advance_pending where balanceamount<>0 and payment_mode='{0}' and Agreement_No={1} and Cheque_No='{2}' and region_name='{3}'", Mode, agreement_no, cheque_no,RegNam);
                }
                
                ChequeDetails cheques = await db.Database.SqlQuery<ChequeDetails>(query).FirstOrDefaultAsync();
                return Json(cheques, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PartialViewResult> Print(int ReceiptNo, string OtherTerms)
        {
            try
            {
                Thread.Sleep(1000);
                ReceiptPrintModel model = new ReceiptPrintModel();
                //TcaPrintModel model = new TcaPrintModel();
                var ReceiptPayment = await db.tbl_receipthd.FirstOrDefaultAsync(x => x.ReceiptNo == ReceiptNo && x.Region_Name == RegNam);

                //var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Property_Id == agreementDet.property_id);
                if (ReceiptPayment != null)
                    model.id = ReceiptPayment.id;
                model.ReceiptNo = ReceiptPayment.ReceiptNo;
                if (ReceiptPayment.ReceiptDate.HasValue)
                { model.ReceiptDate = ReceiptPayment.ReceiptDate.Value.ToShortDateString(); }

                if (model.agreement_no > 0)
                { model.agreement_no = int.Parse(ReceiptPayment.agreement_no.ToString()); }

                model.Property_id = ReceiptPayment.Property_id;
                model.Property_Name = ReceiptPayment.Property_Name;
                model.Unit_ID = ReceiptPayment.Unit_ID;
                model.unit_Name = ReceiptPayment.unit_Name;
                model.Tenant_id = int.Parse(ReceiptPayment.Tenant_id.ToString());
                model.Tenant_Name = ReceiptPayment.Tenant_Name;
                model.TotalAmount = float.Parse(ReceiptPayment.TotalAmount.ToString());
                string decimalPart = "";
                if (ReceiptPayment.TotalAmount > 0)
                {
                    float amt1 = float.Parse(ReceiptPayment.TotalAmount.ToString());
                    int i = (int)amt1;
                    float n1 = amt1 - i;
                    if (n1 > 0)
                    {
                        decimalPart = amt1.ToString().Split('.')[1];
                    }
                    model.Dhirams = Convert.ToString(i);
                    if (decimalPart == "")
                    { model.Fills = "0"; }
                    else
                    {
                        model.Fills = decimalPart;
                    }
                }
                model.RecpType = ReceiptPayment.RecpType;
                model.AmtInWords = ReceiptPayment.AmtInWords;
                model.AmtInWords = AmountInWords(Convert.ToDecimal(ReceiptPayment.TotalAmount));
                model.DDChequeNo = ReceiptPayment.DDChequeNo;
                if (ReceiptPayment.DDChequeDate.HasValue)
                { model.DDChequeDate = ReceiptPayment.DDChequeDate.Value.ToShortDateString(); }

                model.PDCstatus = ReceiptPayment.PDCstatus;
                model.BankAcCode = ReceiptPayment.BankAcCode;
                model.BankAcName = ReceiptPayment.BankAcName;
                model.AdvAcCode = ReceiptPayment.AdvAcCode;
                model.Narration = ReceiptPayment.Narration;
                //model.IssueDate = printInvoicehd.date ;

                if (model.agreement_no > 0)
                {
                    var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == ReceiptPayment.agreement_no && x.Region_Name == RegNam);

                    // var agreementtype = await db.view_tenant.FirstOrDefaultAsync (x => x.Tenant_id == agreementDet.Agreement_No )
                    //var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Property_Id == agreementDet.property_id);
                    if (agreementDet.Tenant_Type == "Individual")
                    {
                        var tenant = await db.tbl_tenant_individual.FirstOrDefaultAsync(x => x.Tenant_Id == agreementDet.Ag_Tenant_id && x.Region_Name == RegNam);
                        if (tenant != null)
                        {
                            model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(tenant.Fax_No) ? "" : tenant.Fax_Countrycode + "-" + tenant.Fax_Areacode + "-" + tenant.Fax_No;
                            model.Ag_Tenant_Address = tenant.address + ", " + tenant.address1 + ", " + tenant.City;
                            model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(tenant.Landline_No) ? "" : tenant.Landline_Countrycode + "-" + tenant.Landline_Areacode + "-" + tenant.Landline_No;
                            //model.Ag_Tenant_Name = tenant.Title + " " + model.Ag_Tenant_Name;
                            model.Ag_Tenant_Name = tenant.Title + " " + tenant.First_Name + tenant.Last_Name;
                        }

                    }
                    else if (agreementDet.Tenant_Type == "Company")
                    {
                        var tenant = await db.tbl_tenant_company.FirstOrDefaultAsync(x => x.Tenant_Id == agreementDet.Ag_Tenant_id && x.Region_Name == RegNam);
                        if (tenant != null)
                        {
                            model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(tenant.Fax_No) ? "" : tenant.Fax_Countrycode + "-" + tenant.Fax_Areacode + "-" + tenant.Fax_No;
                            model.Ag_Tenant_Address = tenant.address + ", " + tenant.address1 + ", " + tenant.City;
                            model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(tenant.Landline_No) ? "" : tenant.Landline_Countrycode + "-" + tenant.Landline_Areacode + "-" + tenant.Landline_No;
                            //  model.Ag_Tenant_Name = tenant.Title + " " + model.Ag_Tenant_Name;
                            model.Ag_Tenant_Name = tenant.Title + " " + tenant.First_Name + tenant.Last_Name;
                        }
                    }
                }
                // dt table of receipt
                //LeamaEntities l = new LeamaEntities();
                //var filter = l.tbl_receiptdt.Where(x => x.ReceiptNo == model.ReceiptNo);
                //var Receiptdata = filter.ToList();
                //ViewBag.Recdtas = Receiptdata;

                //if (Receiptdata.Count > 0)
                //{
                //    ViewBag.Recdtas = Receiptdata;
                //}

                return PartialView("../Receipts/_ReceiptsPrint", model);
                //return PartialView("../Views/Receipts/_ReceiptsPrint", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //get agreement security no for security deposit type
        //new function by kumaresh
        public async Task<JsonResult> GetAggrementSecurityNumbers(string Mode, int Flg)
        {
            try
            {
                if (Flg == 0) // For Receipt Type = 'Others'
                {
                    //Old Query
                    //var agreementNumbers = await db.tbl_agreement.Where(x => x.Delmark != "*").OrderBy(x => x.Agreement_No).Select(x => x.Agreement_No).ToListAsync();
                    //New Query
                    var agreementNumbers = await db.tbl_agreement.Where(x => string.IsNullOrEmpty(x.Delmark) && x.Status != "closed" && x.Region_Name == RegNam).OrderBy(x => x.Agreement_No).Select(x => x.Agreement_No).ToListAsync();
                    return Json(agreementNumbers, JsonRequestBehavior.AllowGet);
                }
                else if (Flg == 2) // For Receipt Type = 'security deposit'
                {
                    //  var agreementNumbers = await db.Database.SqlQuery<int>("select distinct Agreement_No from view_invoice_receipt_pending where !isnull(Agreement_No)").ToListAsync();
                    var agreementNumbers = await db.Database.SqlQuery<int>("select distinct Agreement_No from view_security_pending where !isnull(Agreement_No) and region_name={0}", RegNam).ToListAsync();
                    return Json(agreementNumbers.Distinct(), JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
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
