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
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LeaMaPortal.Controllers
{
    public class FacilityController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Facility
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<FacilityViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_facilitymaster.Where(x => x.Delmark != "*").OrderByDescending(x => x.Id).Select(x => new FacilityViewModel()
                    {
                        Id = x.Id,
                        Facility_id = x.Facility_id,
                        Facility_Name = x.Facility_Name
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_facilitymaster.Where(x => x.Delmark != "*"
                                    && (x.Facility_id.ToLower().Contains(Search.ToLower())
                                    || x.Facility_Name.ToLower().Contains(Search.ToLower()))).OrderByDescending(x=>x.Id)
                                  .Select(x => new FacilityViewModel()
                                  {
                                      Id = x.Id,
                                      Facility_id = x.Facility_id,
                                      Facility_Name = x.Facility_Name
                                  }).ToPagedList(currentPageIndex, PageSize);
                }

                return PartialView("../Master/Facility/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            FacilityViewModel model = new FacilityViewModel();
            model.Facility_id = db.tbl_facilitymaster.OrderByDescending(o => o.Id).Select(s => s.Facility_id).FirstOrDefault();
            model.Facility_id = (Convert.ToInt32(model.Facility_id) + 1).ToString();
            return PartialView("../Master/Facility/_AddOrUpdate", model);
        }
        // POST: Facility/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Facility_id,Facility_Name,Id")] FacilityViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.tbl_facilitymaster.Any(a => a.Facility_Name == model.Facility_Name && a.Delmark != "*" && a.Id != model.Id))
                    {
                        result.Errors = "Facility name already exists";
                    }
                    else
                    {
                        MySqlParameter pa = new MySqlParameter();
                        string PFlag = "INSERT";

                        if (model.Id != 0)
                        {
                            PFlag = "UPDATE";
                            result.Message = "Facility updated successfully";
                        }
                        else
                        {
                            result.Message = "Facility created successfully";
                        }
                        object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PFacility_id",model.Facility_id),
                                            new MySqlParameter("@PFacility_Name",model.Facility_Name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                        var RE = await db.Database.SqlQuery<object>("CALL Usp_Facility_All(@PFlag,@PId,@PFacility_id,@PFacility_Name,@PCreateduser)", param).ToListAsync();
                        await db.SaveChangesAsync();
                    }
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> Edit(string FacilityId, string FacilityName)
        {
            try
            {
                if (FacilityId == null && FacilityName != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_facilitymaster tbl_facility = await db.tbl_facilitymaster.FindAsync(FacilityId, FacilityName);
                if (tbl_facility == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                FacilityViewModel model = new FacilityViewModel()
                {
                    Id = tbl_facility.Id,
                    Facility_id = tbl_facility.Facility_id,
                    Facility_Name = tbl_facility.Facility_Name,
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(string FacilityId, string FacilityName)
        {
            MessageResult result = new MessageResult();
            try
            {
                result.Message = "Facility deleted successfully";
                if (FacilityId == null && FacilityName != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_facilitymaster tbl_facility = await db.tbl_facilitymaster.FindAsync(FacilityId, FacilityName);
                if (tbl_facility == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_facility.Id),
                                           new MySqlParameter("@PFacility_id",tbl_facility.Facility_id),
                                           new MySqlParameter("@PFacility_Name",tbl_facility.Facility_Name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Facility_All(@PFlag,@PId,@PFacility_id,@PFacility_Name,@PCreateduser)", param).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
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
