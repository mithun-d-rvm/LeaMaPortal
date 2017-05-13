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
using MvcPaging;
using LeaMaPortal.Helpers;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LeaMaPortal.Controllers
{
    public class MeterController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Meter
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<MeterViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_metermaster.Where(x => x.Delmark != "*").OrderBy(x => x.Meter_no).Select(x => new MeterViewModel()
                    {
                        Id = x.id,
                        Meter_no = x.Meter_no,
                        Accno = x.Accno,
                        Utility_id = x.Utility_id,
                        Utility_Name = x.Utility_Name,
                        Dueday = x.Dueday,
                        Property_id = x.Property_id,
                        unit_id = x.unit_id
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_metermaster.Where(x => x.Delmark != "*"
                                    && (x.Meter_no.ToLower().Contains(Search.ToLower())
                                    || x.Accno.ToLower().Contains(Search.ToLower())
                                    || x.Utility_Name.ToLower().Contains(Search.ToLower())))
                                  .OrderBy(x => x.Meter_no).Select(x => new MeterViewModel()
                                  {
                                      Id = x.id,
                                      Meter_no = x.Meter_no,
                                      Accno = x.Accno,
                                      Utility_id = x.Utility_id,
                                      Utility_Name = x.Utility_Name,
                                      Dueday = x.Dueday,
                                      Property_id = x.Property_id,
                                      unit_id = x.unit_id
                                  }).ToPagedList(currentPageIndex, PageSize);
                }

                return PartialView("../Master/MeterMaster/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<PartialViewResult> AddOrUpdate()
        {
            MeterViewModel model = new MeterViewModel();
            ViewBag.Utility_Name = new SelectList(db.tbl_utilitiesmaster.OrderBy(o => o.Utility_Name).Distinct(), "Utility_Name", "Utility_Name");
            ViewBag.Property_id = new SelectList(db.tbl_propertiesmaster.Where(x => x.Property_Flag == "Property").OrderBy(o => o.Property_Id).Distinct(), "Property_Id", "Property_ID_Tawtheeq");
            ViewBag.Unit_id = new SelectList(new List<OptionModel>());
            ViewBag.Dueday = new SelectList(StaticHelper.GetStaticData(StaticHelper.METER_DROPDOWN), "Id", "Id");
            return PartialView("../Master/MeterMaster/_AddOrUpdate", model);
        }
        // POST: CheckList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Meter_no,Utility_id,Utility_Name,unit_id,Accno,Dueday,Property_id,Id")] MeterViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";

                    if (model.Id == 0)
                    {

                    }
                    else
                    {
                        PFlag = "UPDATE";
                    }
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PUtility_id",model.Utility_id),
                                            new MySqlParameter("@PUtility_Name",model.Utility_Name),
                                            new MySqlParameter("@PMeter_no",model.Meter_no),
                                            new MySqlParameter("@PAccno",model.Accno),
                                            new MySqlParameter("@PProperty_id",model.Property_id),
                                            new MySqlParameter("@Punit_id",model.unit_id),
                                            new MySqlParameter("@PDueday",model.Dueday),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Metermaster_All(@PFlag,@PId,@PUtility_id,@PUtility_Name,@PMeter_no,@PAccno,@PProperty_id,@Punit_id,@PDueday,@PCreateduser)", param).ToListAsync();
                    await db.SaveChangesAsync();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> Edit(string MeterId, string MeterNumber)
        {
            try
            {
                if (MeterId == null && MeterNumber != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_metermaster tbl_meter = await db.tbl_metermaster.FindAsync(MeterNumber);
                if (tbl_meter == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                MeterViewModel model = new MeterViewModel()
                {
                    Id = tbl_meter.id,
                    Meter_no = tbl_meter.Meter_no,
                    Accno = tbl_meter.Accno,
                    Utility_id = tbl_meter.Utility_id,
                    Utility_Name = tbl_meter.Utility_Name,
                    Dueday = tbl_meter.Dueday,
                    Property_id = tbl_meter.Property_id,
                    unit_id = tbl_meter.unit_id
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(string MeterId, string MeterNumber)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (MeterId == null && MeterNumber != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_metermaster tbl_meter = await db.tbl_metermaster.FindAsync(MeterNumber);
                if (tbl_meter == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_meter.id),
                                           new MySqlParameter("@PUtility_id",tbl_meter.Utility_id),
                                            new MySqlParameter("@PUtility_Name",tbl_meter.Utility_Name),
                                            new MySqlParameter("@PMeter_no",tbl_meter.Meter_no),
                                            new MySqlParameter("@PAccno",tbl_meter.Accno),
                                            new MySqlParameter("@PProperty_id",tbl_meter.Property_id),
                                            new MySqlParameter("@Punit_id",tbl_meter.unit_id),
                                            new MySqlParameter("@PDueday",tbl_meter.Dueday),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Metermaster_All(@PFlag,@PId,@PUtility_id,@PUtility_Name,@PMeter_no,@PAccno,@PProperty_id,@Punit_id,@PDueday,@PCreateduser)", param).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<string> GetUtilityId(string UtilityName)
        {
            try
            {
                var utility = await db.tbl_utilitiesmaster.FirstOrDefaultAsync(f => f.Utility_Name == UtilityName);
                return utility.Utility_id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonResult> GetUnitId(int? PropertyId)
        {
            try
            {
                //MeterViewModel model = new MeterViewModel();
                //ViewBag.Unit_id = new SelectList(db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Ref_Unit_Property_ID == PropertyId).OrderBy(o => o.Property_Id).Distinct(), "Property_Id", "Property_Id");
                //return PartialView("../Master/MeterMaster/_AddOrUpdate", model);
                List<OptionModel> model = new List<OptionModel>();
                //var data = await db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Ref_Unit_Property_ID == PropertyId).OrderBy(o => o.Property_Id).Select(s => s.Property_Id).ToListAsync();
                return Json(new SelectList(db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Ref_Unit_Property_ID == PropertyId).OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq"));
            }
            catch (Exception ex)
            {
                throw ex;
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
