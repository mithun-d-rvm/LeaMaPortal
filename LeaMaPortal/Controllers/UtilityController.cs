using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using System.Threading.Tasks;
using LeaMaPortal.Models;
using MySql.Data.MySqlClient;
using MvcPaging;

namespace LeaMaPortal.Controllers
{
    public class UtilityController : Controller
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Utility
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<UtilityViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_utilitiesmaster.Where(x => x.Delmark != "*").OrderByDescending(x=>x.id).Select(x => new UtilityViewModel()
                    {
                        Id = x.id,
                        Utility_id = x.Utility_id,
                        Utility_Name = x.Utility_Name
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_utilitiesmaster.Where(x => x.Delmark != "*"
                                    && (x.Utility_id.ToLower().Contains(Search.ToLower())
                                    || x.Utility_Name.ToLower().Contains(Search.ToLower())))
                                  .OrderByDescending(x => x.id).Select(x => new UtilityViewModel()
                                  {
                                      Id = x.id,
                                      Utility_id = x.Utility_id,
                                      Utility_Name = x.Utility_Name
                                  }).ToPagedList(currentPageIndex, PageSize);
                }

                return PartialView("../Master/Utility/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            UtilityViewModel model = new UtilityViewModel();
            model.Utility_id = db.tbl_utilitiesmaster.OrderByDescending(o => o.id).Select(s => s.Utility_id).FirstOrDefault();
            model.Utility_id = (Convert.ToInt32(model.Utility_id) + 1).ToString();
            return PartialView("../Master/Utility/_AddOrUpdate", model);
        }
        // POST: Utility/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Utility_id,Utility_Name,Id")] UtilityViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.tbl_utilitiesmaster.Any(a => a.Utility_Name == model.Utility_Name && a.Delmark != "*") && model.Id == 0)
                    {
                        result.Errors = "Utility name already exists";
                    }
                    else
                    {
                        MySqlParameter pa = new MySqlParameter();
                        string PFlag = "INSERT";

                        if (model.Id != 0)
                        {
                            PFlag = "UPDATE";
                            result.Message = "Utility updated successfully";
                        }
                        else
                        {
                            result.Message = "Utility created successfully";
                        }
                        object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PUtility_id",model.Utility_id),
                                            new MySqlParameter("@PUtility_Name",model.Utility_Name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                        var RE = await db.Database.SqlQuery<object>("CALL Usp_Utilities_All(@PFlag,@PId,@PUtility_id,@PUtility_Name,@PCreateduser)", param).ToListAsync();
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

        public async Task<ActionResult> Edit(string UtilityId, string UtilityName)
        {
            try
            {
                if (UtilityId == null && UtilityName != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_utilitiesmaster tbl_utility = await db.tbl_utilitiesmaster.FindAsync(UtilityId, UtilityName);
                if (tbl_utility == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                UtilityViewModel model = new UtilityViewModel()
                {
                    Id = tbl_utility.id,
                    Utility_id = tbl_utility.Utility_id,
                    Utility_Name = tbl_utility.Utility_Name,
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(string UtilityId, string UtilityName)
        {
            MessageResult result = new MessageResult();
            try
            {
                result.Message = "Utility deleted successfully";
                if (UtilityId == null && UtilityName != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_utilitiesmaster tbl_utility = await db.tbl_utilitiesmaster.FindAsync(UtilityId, UtilityName);
                if (tbl_utility == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_utility.id),
                                           new MySqlParameter("@PUtility_id",tbl_utility.Utility_id),
                                           new MySqlParameter("@PUtility_Name",tbl_utility.Utility_Name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Utilities_All(@PFlag,@PId,@PUtility_id,@PUtility_Name,@PCreateduser)", param).ToListAsync();
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
