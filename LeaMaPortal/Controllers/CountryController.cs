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
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MvcPaging;

namespace LeaMaPortal.Controllers
{
    public class CountryController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();
        //string user = 
        // GET: Country
        public async Task<PartialViewResult> Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int  PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                CountryViewModel model = new CountryViewModel();
                if(string.IsNullOrWhiteSpace(Search))
                {
                    model.List = db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name).Select(x => new CountryViewModel()
                    {
                        Id = x.Id,
                        Country = x.Country_name
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    model.List = db.tbl_country.Where(x => x.Delmark != "*" && x.Country_name.ToLower().Contains(Search.ToLower()))
                                  .OrderBy(x => x.Country_name).Select(x => new CountryViewModel()
                                  {
                                      Id = x.Id,
                                      Country = x.Country_name
                                  }).ToPagedList(currentPageIndex, PageSize);
                }
                 
                return PartialView("../Master/Country/_List", model.List);
            }
            catch(Exception e)
            {
                throw;
            }
        }

        // GET: Country/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_country tbl_country = db.tbl_country.Find(id);
            if (tbl_country == null)
            {
                return HttpNotFound();
            }
            return View(tbl_country);
        }

        // GET: Country/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            return PartialView("../Master/Country/_AddOrUpdate", new CountryViewModel());
        }
        // POST: Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Country,Id")] CountryViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.tbl_country.Any(a => a.Delmark != "*" && a.Country_name.ToLower() == model.Country.ToLower()) && model.Id == 0)
                    {
                        result.Errors = "Country already exists!";
                    }
                    else
                    {
                        MySqlParameter pa = new MySqlParameter();
                        string PFlag = "INSERT";

                        if (model.Id != 0)
                        {
                            PFlag = "UPDATE";
                            result.Message = "Country updated successfully";
                        }
                        else
                        {
                            result.Message = "Country created successfully";
                        }
                        object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PCountry_name",model.Country),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                        var RE = await db.Database.SqlQuery<object>("CALL Usp_Country_All(@PFlag,@PId,@PCountry_name,@PCreateduser)", param).ToListAsync();
                        await db.SaveChangesAsync();
                    }
                }
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }

            
        }

        // GET: Country/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_country tbl_country =await db.tbl_country.FindAsync(id);
                if (tbl_country == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                CountryViewModel model = new CountryViewModel()
                {
                    Id = tbl_country.Id,
                    Country = tbl_country.Country_name,
                };
                return Json(model,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json( new MessageResult() {Errors="Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Country_name,Id,Accyear,Createddatetime,Createduser,Delmark")] tbl_country tbl_country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_country);
        }

        // GET: Country/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            MessageResult result = new MessageResult();
            try
            {
                result.Message = "Country deleted successfully";
                if (id == null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_country tbl_country = await db.tbl_country.FindAsync(id);
                if (tbl_country == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_country.Id),
                                           new MySqlParameter("@PCountry_name",tbl_country.Country_name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Country_All(@PFlag,@PId,@PCountry_name,@PCreateduser)", param).ToListAsync();
                //await db.SaveChangesAsync();

                //tbl_country.Delmark = "*";
                //db.Entry(tbl_country).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_country tbl_country = db.tbl_country.Find(id);
            db.tbl_country.Remove(tbl_country);
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
