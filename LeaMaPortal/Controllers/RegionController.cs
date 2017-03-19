using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.Models.DBContext;

namespace LeaMaPortal.Controllers
{
    public class RegionController : Controller
    {
        private Entities db = new Entities();

        // GET: Region
        public ActionResult Index()
        {
            
            return View();
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

        // POST: Region/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Region_Name,Country,Id,Accyear,Createddatetime,Createduser,Delmark")] tbl_region tbl_region)
        {
            if (ModelState.IsValid)
            {
                db.tbl_region.Add(tbl_region);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Country = new SelectList(db.tbl_country, "Country_name", "Createduser", tbl_region.Country);
            return View(tbl_region);
        }

        // GET: Region/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.Country = new SelectList(db.tbl_country, "Country_name", "Createduser", tbl_region.Country);
            return View(tbl_region);
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
        public ActionResult Delete(string id)
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
