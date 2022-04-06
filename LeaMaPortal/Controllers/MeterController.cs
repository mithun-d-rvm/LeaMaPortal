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
    [Authorize]
    public class MeterController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();
        public static string Regnam;

        // GET: Meter
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                Regnam = Session["Region"].ToString();
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<MeterViewModel> list;
                string regname = Session["Region"].ToString();
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_metermaster.Where(x => x.Delmark != "*" && x.Region_Name == regname ).OrderBy(x => x.Utility_Name).Select(x => new MeterViewModel()
                    {
                        Id = x.id,
                        Meter_no = x.Meter_no,
                        Accno = x.Accno,
                        Utility_id = x.Utility_id,
                        Utility_Name = x.Utility_Name,
                        Dueday = x.Dueday,
                        Property_id = x.Property_id,
                        Property_Name = x.Property_Name,
                        unit_id = x.unit_id,
                        Unit_Name = x.Unit_Name
                        
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_metermaster.Where(x => x.Delmark != "*" && (x.Region_Name == regname)
                                    && (x.Meter_no.ToLower().Contains(Search.ToLower())
                                    || x.Accno.ToLower().Contains(Search.ToLower())
                                    || x.Utility_Name.ToLower().Contains(Search.ToLower()) || x.Property_id .Contains (Search ) || x.Property_Name.Contains (Search ) || x.unit_id .ToString ().Contains (Search ) || x.Unit_Name .Contains (Search ) ))
                                  .OrderBy(x => x.Utility_Name).Select(x => new MeterViewModel()
                                  {
                                      Id = x.id,
                                      Meter_no = x.Meter_no,
                                      Accno = x.Accno,
                                      Utility_id = x.Utility_id,
                                      Utility_Name = x.Utility_Name,
                                      Dueday = x.Dueday,
                                      Property_id = x.Property_id,
                                      Property_Name = x.Property_Name,
                                      unit_id = x.unit_id,
                                      Unit_Name = x.Unit_Name
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
            ViewBag.Utility_Name = new SelectList(db.tbl_utilitiesmaster.Where (x => x.Region_Name == Session["Region"].ToString()).OrderBy(o => o.Utility_Name).Distinct(), "Utility_Name", "Utility_Name");

            var property = db.tbl_propertiesmaster.Where(p => p.Property_Flag == "Property" && p.Delmark != "*" && p.Region_Name == Session["Region"].ToString()).OrderBy(o => o.id); //p.Createduser == System.Web.HttpContext.Current.User.Identity.Name
            ViewBag.PropertyThawtheeqID = new SelectList(property, "Property_Id", "Property_ID_Tawtheeq");
            ViewBag.PropertyName = new SelectList(property, "Property_Id", "Property_Name");
            //ViewBag.PropertyThawtheeqID = new SelectList(db.tbl_propertiesmaster.Where(x => x.Property_Flag == "Property").OrderBy(o => o.Property_Id).Distinct(), "Property_Id", "Property_ID_Tawtheeq");

            var units = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Delmark != "*" && w.Region_Name == Session["Region"].ToString()).OrderBy(o => o.id); //w.Createduser == System.Web.HttpContext.Current.User.Identity.Name
            ViewBag.UnitThawtheeqID = new SelectList(units, "Ref_Unit_Property_ID", "Unit_ID_Tawtheeq");
            ViewBag.UnitName = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_Property_Name");
            ViewBag.Dueday = new SelectList(StaticHelper.GetStaticData(StaticHelper.METER_DROPDOWN), "Id", "Id");
            return PartialView("../Master/MeterMaster/_AddOrUpdate", model);
        }
        // POST: CheckList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Meter_no,Utility_id,Utility_Name,unit_id,Unit_Name,Accno,Dueday,Property_id,Property_Name,Id")] MeterViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                //if (ModelState.IsValid)
                //{
                    if (db.tbl_metermaster.Any(a => a.Delmark != "*" && a.Meter_no == model.Meter_no && a.Utility_id == model.Utility_id && a.id != model.Id))
                    {
                        result.Errors = "Meter number already exists!";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    if (model.unit_id == null)
                    {
                        if (db.tbl_metermaster.Any(x => x.Delmark != "*" && x.Utility_id == model.Utility_id && x.Property_id == model.Property_id && x.id!=model.Id))
                        {
                            result.Errors = "Combination of utility and property already exists";
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        if (db.tbl_metermaster.Any(x => x.Delmark != "*" && x.Utility_id == model.Utility_id && x.Property_id == model.Property_id && x.unit_id == model.unit_id && x.id != model.Id))
                        {
                            result.Errors = "Combination of utility, property and unit already exists";
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";

                    if (model.Id != 0)
                    {
                        PFlag = "UPDATE";
                        result.Message = "Meter details updated successfully";
                    }
                    else
                    {
                        result.Message = "Meter details created successfully";
                    }
                string regname = Session["Region"].ToString();
                if (regname != "")
                {
                    model.Region_Name = regname;

                    var countryname = db.tbl_region.Where(x => x.Region_Name == model.Region_Name).OrderByDescending(x => x.Id).FirstOrDefault();

                    model.Country = countryname.Country;
                }
                object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PUtility_id",model.Utility_id),
                                            new MySqlParameter("@PUtility_Name",model.Utility_Name),
                                            new MySqlParameter("@PMeter_no",model.Meter_no),
                                            new MySqlParameter("@PAccno",model.Accno),
                                            new MySqlParameter("@PProperty_id",model.Property_id),
                                            new MySqlParameter("@PProperty_name",model.Property_Name),
                                            new MySqlParameter("@Punit_id",model.unit_id),
                                            new MySqlParameter("@Punit_name",model.Unit_Name),
                                            new MySqlParameter("@PDueday",model.Dueday),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter ("@PRegion_Name",model.Region_Name ),
                                           new MySqlParameter ("@PCountry",model.Country )
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Metermaster_All(@PFlag,@PId,@PUtility_id,@PUtility_Name,@PMeter_no,@PAccno,@PProperty_id,@PProperty_name,@Punit_id,@Punit_name,@PDueday,@PCreateduser,@PRegion_Name,@PCountry)", param).ToListAsync();
                    await db.SaveChangesAsync();
                //}
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
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
                var meterlist = await db.Database.SqlQuery<MeterViewModel>("select * from tbl_metermaster where id = {0} and meter_no = {1} and ifnull(delmark,'')<>'*'", MeterId, MeterNumber).ToListAsync();
                MeterViewModel model = new MeterViewModel()
                {
                    Id = tbl_meter.id,
                    Meter_no = tbl_meter.Meter_no,
                    Accno = tbl_meter.Accno,
                    Utility_id = tbl_meter.Utility_id,
                    Utility_Name = tbl_meter.Utility_Name,
                    Dueday = tbl_meter.Dueday,
                    Property_id = tbl_meter.Property_id,
                    unit_id = tbl_meter.unit_id,
                    Property_Name = tbl_meter.Property_Name,
                    Unit_Name = tbl_meter.Unit_Name
                    
                };
                return Json(model, JsonRequestBehavior.AllowGet);

                //var meterlist = await db.Database.SqlQuery<MeterViewModel>("select * from tbl_metermaster where id = {0} and meter_no = {1} and ifnull(delmark,'')<>'*'", MeterId, MeterNumber).ToListAsync();
                //return Json(meterlist, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(int MeterId, string MeterNumber)
        {
            MessageResult result = new MessageResult();
            try
            {
                result.Message = "Meter details deleted successfully";
                if (MeterId == null && MeterNumber != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_metermaster tbl_meter = await db.tbl_metermaster.FindAsync(MeterNumber);
                if (tbl_meter == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }

                ////var meterdetails = await db.tbl_metermaster.FirstOrDefaultAsync(x => x.id == MeterId && x.Meter_no == MeterNumber && x.Delmark != "*");
                //MeterViewModel model = new MeterViewModel();
                //var meterdetails = await db.Database.SqlQuery<MeterViewModel>("select * from tbl_metermaster where id = {0} and meter_no = {1} and ifnull(delmark,'')<>'*'", MeterId, MeterNumber).ToListAsync();
                //if (meterdetails == null)
                //{
                //    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                //}
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId",tbl_meter.id),
                                           new MySqlParameter("@PUtility_id",tbl_meter.Utility_id),
                                            new MySqlParameter("@PUtility_Name",tbl_meter.Utility_Name),
                                            new MySqlParameter("@PMeter_no",tbl_meter.Meter_no),
                                            new MySqlParameter("@PAccno",tbl_meter.Accno),
                                            new MySqlParameter("@PProperty_id",tbl_meter.Property_id),
                                            new MySqlParameter("@PProperty_name",null),
                                            new MySqlParameter("@Punit_id",tbl_meter.unit_id),
                                            new MySqlParameter("@Punit_name",null),
                                            new MySqlParameter("@PDueday",tbl_meter.Dueday),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                               new MySqlParameter ("@PRegion_Name",tbl_meter.Region_Name ),
                                           new MySqlParameter ("@PCountry",tbl_meter.Country )
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Metermaster_All(@PFlag,@PId,@PUtility_id,@PUtility_Name,@PMeter_no,@PAccno,@PProperty_id,@PProperty_name,@Punit_id,@Punit_name,@PDueday,@PCreateduser,@PRegion_Name,@PCountry)", param).ToListAsync();
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
                //var data = await db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Ref_Unit_Property_ID == PropertyId).OrderBy(o => o.Property_Id).Select(s => s.Property_Id).ToListAsync();

                //List<OptionModel> model = new List<OptionModel>();
                //return Json(new SelectList(db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Ref_Unit_Property_ID == PropertyId).OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_Property_Name"));

                var propertylist = await db.Database.SqlQuery<PropertyDropdownModel>("select Unit_ID_Tawtheeq,Unit_Property_Name,property_id,Ref_Unit_Property_ID from tbl_propertiesmaster where Property_Flag = 'Unit' and ifnull(delmark,'')<>'*' and Ref_Unit_Property_ID = {0}", PropertyId).ToListAsync();
                return Json(propertylist, JsonRequestBehavior.AllowGet);
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
