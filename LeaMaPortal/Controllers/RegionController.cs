using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using LeaMaPortal.Models;
using MySql.Data.MySqlClient;
using MvcPaging;

namespace LeaMaPortal.Controllers
{
    public class RegionController : Controller
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Region
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<RegionViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name).Select(x => new RegionViewModel()
                    {
                        Id = x.Id,
                        Country = x.Country,
                        Region_Name = x.Region_Name
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_region.Where(x => x.Delmark != "*" 
                                    && (x.Country.ToLower().Contains(Search.ToLower())
                                    || x.Region_Name.ToLower().Contains(Search.ToLower())) )
                                  .OrderBy(x => x.Region_Name).Select(x => new RegionViewModel()
                                  {
                                      Id = x.Id,
                                      Country = x.Country,
                                      Region_Name = x.Region_Name
                                  }).ToPagedList(currentPageIndex, PageSize);
                }

                return PartialView("../Master/Region/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            RegionViewModel model = new RegionViewModel();
            ViewBag.Country = new SelectList(db.tbl_country.OrderBy(x => x.Country_name), "Country_name", "Country_name");
            return PartialView("../Master/Region/_AddOrUpdate", model);
        }

        [HttpGet]
        public async Task<ActionResult> GetCountry(string region)
        {
            try
            {
                tbl_region tbl_region = await db.tbl_region.FirstOrDefaultAsync(x => x.Region_Name == region);
                return Json(tbl_region?.Country,JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw;
            }
            
            
        }

        // POST: Region/AddOrUpdate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Region_Name,Country,Id,Region_Name_PK,Country_PK")] RegionViewModel model)
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
                        tbl_region tbl_region = await db.tbl_region.FindAsync(model.Region_Name_PK, model.Country_PK);
                        if (tbl_region != null)
                        {
                            PFlag = "UPDATE";
                            model.Id = tbl_region.Id;
                        }
                    }
                    else
                    {
                        PFlag = "UPDATE";
                    }
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PRegion_Name",model.Region_Name),
                                            new MySqlParameter("@PCountry",model.Country),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Region_All(@PFlag,@PId,@PRegion_Name,@PCountry,@PCreateduser)", param).ToListAsync();
                    await db.SaveChangesAsync();

                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET: Region/Edit/5
        public async Task<ActionResult> Edit(string RegionName, string CountryName)
        {
            try
            {
                if (CountryName == null && RegionName != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_region tbl_region = await db.tbl_region.FindAsync(RegionName, CountryName);
                if (tbl_region == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                ViewBag.Country = new SelectList(db.tbl_country, "Country_name", "Createduser", tbl_region.Country);
                RegionViewModel model = new RegionViewModel()
                {
                    Id = tbl_region.Id,
                    Region_Name = tbl_region.Region_Name,
                    Country = tbl_region.Country,
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Region/Delete/5
        public async Task<ActionResult> Delete(string RegionName, string CountryName)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (CountryName == null && RegionName != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_region tbl_region = await db.tbl_region.FindAsync(RegionName, CountryName);
                if (tbl_region == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_region.Id),
                                           new MySqlParameter("@PRegion_Name",tbl_region.Region_Name),
                                           new MySqlParameter("@PCountry",tbl_region.Country),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Region_All(@PFlag,@PId,@PRegion_Name,@PCountry,@PCreateduser)", param).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
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
