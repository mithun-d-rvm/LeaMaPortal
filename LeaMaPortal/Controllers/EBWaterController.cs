using LeaMaPortal.DBContext;
using LeaMaPortal.Models;
using MvcPaging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class EbWaterController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: EBWater
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult List(string Search, int? page, int? defaultPageSize)
        {
            ViewData["Search"] = Search;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
            ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
            var EBDetails = db.tbl_eb_water_billentryhd.Where(x => x.Delmark != "*");
            if (!string.IsNullOrWhiteSpace(Search))
            {
                EBDetails = EBDetails.Where(x => x.Refno.ToString().Contains(Search));
            }
            var invoice = EBDetails.OrderBy(x => x.id).Select(x => new EBWaterModel()
            {
                BillEnteryNo = x.Refno,
                BillEntryDate = x.refdate,
                UtilityId = x.Utility_id,
                UtilityName = x.utility_name,
                SupplierId = x.Supplier_id,
                SupplierName = x.Supplier_name,
                Details = db.tbl_eb_water_billentrydt.Where(y => y.Delmark != "*" && y.Refno == x.Refno).Select(z => new EBWaterDetailsModel
                {
                    MeterReadingNo = z.Meterreadingno,
                    Amount = z.amount,
                    BillDate = z.billdate,
                    RefNo = z.Refno,
                    BillNo = z.billno,
                    DayOfUse = z.daysofuse,
                    DueDate = z.duedate,
                    MeterNo = z.Meterno,
                    PropertyId = z.property_id,
                    Rate = z.rate,
                    ReadingDate = z.Reading_date,
                    TotalUnits = z.Total_units,
                    UnitId = z.Unit_id
                }).ToList()
            }).ToPagedList(currentPageIndex, PageSize);
            return PartialView("../EbWater/_List", invoice);
        }

        [HttpGet]
        public async Task<PartialViewResult> AddOrUpdate()
        {
            try
            {
                EBWaterModel model = new EBWaterModel();
                int? refNo = await db.tbl_eb_water_billentryhd.MaxAsync(x => (int?)x.Refno);
                //int paymentno = await db.tbl_paymenthd.Select(x => x.PaymentNo).DefaultIfEmpty(0).MaxAsync();
                model.BillEnteryNo = refNo == null ? 1 : refNo.Value + 1;
                //model.BillEntryDate = DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                model.BillEntryDate = DateTime.UtcNow;
                //var paymentType_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','Reccategory',',',null)").ToList();
                var utilities = await db.tbl_utilitiesmaster.Where(w => w.Delmark != "*").ToListAsync();
                //ViewBag.UtilityId = new SelectList(utilities, "Utility_id", "Utility_id");
                ViewBag.Utility_Name = new SelectList(utilities, "Utility_id", "Utility_Name");

                var suppliers = await db.tbl_suppliermaster.Where(w => w.Delmark != "*").ToListAsync();
                //ViewBag.SupplierId = new SelectList(suppliers, "Supplier_Id", "Supplier_Id");
                ViewBag.Supplier_Name = new SelectList(suppliers, "Supplier_Id", "Supplier_Name");

                var meters = await db.tbl_metermaster.Where(w => w.Delmark != "*").ToListAsync();
                ViewBag.MeterNo = new SelectList(meters, "Meter_no", "Meter_no");

                return PartialView("../EbWater/_AddOrUpdate", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(EBWaterModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";
                    result.Message = "Utility bill entry created successfully";
                    if (model.Id != 0)
                    {
                        PFlag = "UPDATE";
                        result.Message = "Utility bill entry updated successfully";
                    }
                    string billEntries = null;
                    if (model.Details != null)
                    {
                        foreach (var item in model.Details)
                        {
                            var readingDate = item.ReadingDate.HasValue ? "'" + item.ReadingDate.Value.Date.ToString("yyyy-MM-dd") + "'" : "null";
                            var billDate = item.BillDate.HasValue ? "'" + item.BillDate.Value.Date.ToString("yyyy-MM-dd") + "'" : "null";
                            var dueDate = item.DueDate.HasValue ? "'" + item.DueDate.Value.Date.ToString("yyyy-MM-dd") + "'" : "null";
                            if (string.IsNullOrWhiteSpace(billEntries))
                            {
                                billEntries = "(" + model.BillEnteryNo + ",'" + item.MeterNo + "','" + item.PropertyId +
                                    "','" + item.UnitId + "'," + item.TotalUnits + ",'" + item.MeterReadingNo + "'," + readingDate + "," + billDate + "," + item.BillNo
                                    + "," + dueDate + "," + item.DayOfUse + "," + item.Rate + "," + item.Amount + ")";
                            }
                            else
                            {
                                billEntries += ",(" + model.BillEnteryNo + ",'" + item.MeterNo + "','" + item.PropertyId +
                                    "','" + item.UnitId + "'," + item.TotalUnits + ",'" + item.MeterReadingNo + "'," + readingDate + "," + billDate + "," + item.BillNo
                                    + "," + dueDate + "," + item.DayOfUse + "," + item.Rate + "," + item.Amount + ")";
                            }
                        }
                    }
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PRefno", model.BillEnteryNo),
                                           new MySqlParameter("@Prefdate",model.BillEntryDate),
                                            new MySqlParameter("@PUtility_id",model.UtilityId),
                                            new MySqlParameter("@PUtiltiy_name", model.UtilityName),
                                            new MySqlParameter("@PSupplier_id", model.SupplierId),
                                            new MySqlParameter("@PSupplier_name", model.SupplierName),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter("@Pelewaterdt", billEntries)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_eb_water_All(@PFlag,@PRefno,@Prefdate,@PUtility_id,@PUtiltiy_name,@PSupplier_id,@PSupplier_name,@PCreateduser,@Pelewaterdt)", param).ToListAsync();
                    await db.SaveChangesAsync();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> Edit(int BillEntryNo)
        {
            try
            {
                var x = await db.tbl_eb_water_billentryhd.FirstOrDefaultAsync(f => f.Refno == BillEntryNo);
                EBWaterModel model = new EBWaterModel();

                model = new EBWaterModel()
                {
                    Id = x.id,
                    BillEnteryNo = x.Refno,
                    BillEntryDate = x.refdate,
                    UtilityId = x.Utility_id,
                    UtilityName = x.utility_name,
                    SupplierId = x.Supplier_id,
                    SupplierName = x.Supplier_name,
                    Details = db.tbl_eb_water_billentrydt.Where(y => y.Delmark != "*" && y.Refno == x.Refno).Select(z => new EBWaterDetailsModel
                    {
                        MeterReadingNo = z.Meterreadingno,
                        Amount = z.amount,
                        BillDate = z.billdate,
                        RefNo = z.Refno,
                        BillNo = z.billno,
                        DayOfUse = z.daysofuse,
                        DueDate = z.duedate,
                        MeterNo = z.Meterno,
                        PropertyId = z.property_id,
                        Rate = z.rate,
                        ReadingDate = z.Reading_date,
                        TotalUnits = z.Total_units,
                        UnitId = z.Unit_id
                    }).ToList()
                };
                
                var suppliers = await db.tbl_suppliermaster.Where(w => w.Delmark != "*").ToListAsync();
                //ViewBag.Supplierid = new SelectList(suppliers, "Supplier_Id", "Supplier_Id", model.SupplierId);
                ViewBag.Supplier_Name = new SelectList(suppliers, "Supplier_Id", "Supplier_Name", model.SupplierId);

                var utilities = await db.tbl_utilitiesmaster.Where(w => w.Delmark != "*").ToListAsync();
                //ViewBag.UtilityId = new SelectList(utilities, "Utility_id", "Utility_id", model.UtilityId);
                ViewBag.Utility_Name = new SelectList(utilities, "Utility_id", "Utility_Name", model.UtilityId);

                var meters = await db.tbl_metermaster.Where(w => w.Delmark != "*").ToListAsync();
                ViewBag.MeterNo = new SelectList(meters, "Meter_no", "Meter_no");

                return PartialView("../EbWater/_AddorUpdate", model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int BillEntryNo)
        {
            try
            {
                MessageResult result = new MessageResult();
                var x = await db.tbl_eb_water_billentryhd.FirstOrDefaultAsync(f => f.Refno == BillEntryNo);
                EBWaterModel model = new EBWaterModel()
                {
                    BillEnteryNo = x.Refno,
                    BillEntryDate = x.refdate,
                    UtilityId = x.Utility_id,
                    UtilityName = x.utility_name,
                    SupplierId = x.Supplier_id,
                    SupplierName = x.Supplier_name,
                    Details = db.tbl_eb_water_billentrydt.Where(y => y.Delmark != "*" && y.Refno == x.Refno).Select(z => new EBWaterDetailsModel
                    {
                        MeterReadingNo = z.Meterreadingno,
                        Amount = z.amount,
                        BillDate = z.billdate,
                        RefNo = z.Refno,
                        BillNo = z.billno,
                        DayOfUse = z.daysofuse,
                        DueDate = z.duedate,
                        MeterNo = z.Meterno,
                        PropertyId = z.property_id,
                        Rate = z.rate,
                        ReadingDate = z.Reading_date,
                        TotalUnits = z.Total_units,
                        UnitId = z.Unit_id
                    }).ToList()
                };
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PRefno", model.BillEnteryNo),
                                           new MySqlParameter("@Prefdate",model.BillEntryDate),
                                            new MySqlParameter("@PUtility_id",model.UtilityId),
                                            new MySqlParameter("@PUtiltiy_name", model.UtilityName),
                                            new MySqlParameter("@PSupplier_id", model.SupplierId),
                                            new MySqlParameter("@PSupplier_name", model.SupplierName),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter("@Pelewaterdt", model.Details)
                                         };
                var RE = await db.Database.SqlQuery<object>("CALL Usp_eb_water_All(@PFlag,@PRefno,@Prefdate,@PUtility_id,@PUtiltiy_name,@PSupplier_id,@PSupplier_name,@PCreateduser,@Pelewaterdt)", param).ToListAsync();
                result.Message = "Utility bill entry deleted successfully";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult getMeterDetails(string MeterNo)
        {
            try
            {
                var data = db.tbl_metermaster.FirstOrDefault(f => f.Meter_no == MeterNo);
                return Json(new { Property_id = data.Property_id, unit_id = data.unit_id}, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public JsonResult getMeterDropdown()
        {
            try
            {
                var data = db.tbl_metermaster.Where(f => f.Delmark != "*");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}