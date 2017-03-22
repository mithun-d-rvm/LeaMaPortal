using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using LeaMaPortal.Models.DBContext;
using LeaMaPortal.Models;
using MySql.Data.MySqlClient;
using MvcPaging;

namespace LeaMaPortal.Controllers
{
    public class RegionController : Controller
    {
        private Entities db = new Entities();

        // GET: Region
        public async Task<PartialViewResult> Index(string Search, int? page, int? defaultPageSize)
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
                                    && x.Country.ToLower().Contains(Search.ToLower())
                                    && x.Country.ToLower().Contains(Search.ToLower()) )
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

        public PartialViewResult Region()
        {
            try
            {
                RegionViewModel model = new RegionViewModel();
                ViewBag.Country = new SelectList(db.tbl_country.OrderBy(x => x.Country_name), "Country_name", "Country_name");
                return PartialView("../Master/Region/_AddOrUpdate", model);
            }
            catch
            {
                throw;
            }
        }

        // GET: Region/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_region tbl_region = db.tbl_region.Find(id);
            if (tbl_region == null)
            {
                return HttpNotFound();
            }
            return View(tbl_region);
        }

        // GET: Region/Create
        public ActionResult Create()
        {
            ViewBag.Country = new SelectList(db.tbl_country, "Country_name", "Createduser");
            return View();
        }

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            RegionViewModel model = new RegionViewModel();
            ViewBag.Country = new SelectList(db.tbl_country.OrderBy(x => x.Country_name), "Country_name", "Country_name");
            return PartialView("../Master/Region/_AddOrUpdate", model);
        }

        // POST: Region/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Region_Name,Country,Id")] RegionViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";

                    //Accyear,Createddatetime,Createduser,Delmark
                    //tbl_country tbl_country = new tbl_country();
                    //tbl_country.Country_name = model.Country;
                    if (model.Id == 0)
                    {
                        tbl_region tbl_region = await db.tbl_region.FindAsync(model.Region_Name, model.Country);
                        if (tbl_region != null)
                        {
                            PFlag = "UPDATE";
                            model.Id = tbl_region.Id;
                        }
                        //tbl_country.Createddatetime = DateTime.Now;
                        //tbl_country.Accyear = DateTime.Now.Year;
                        //tbl_country.Createduser = "arul";
                        //db.tbl_country.Add(tbl_country);
                    }
                    else
                    {
                        PFlag = "UPDATE";
                        // db.Entry(tbl_country).State = EntityState.Modified;
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

        // POST: Region/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Region_Name,Country,Id,Accyear,Createddatetime,Createduser,Delmark")] tbl_region tbl_region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_region).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Country = new SelectList(db.tbl_country, "Country_name", "Createduser", tbl_region.Country);
            return View(tbl_region);
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

        // POST: Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_region tbl_region = db.tbl_region.Find(id);
            db.tbl_region.Remove(tbl_region);
            db.SaveChanges();
            return RedirectToAction("Index");
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
