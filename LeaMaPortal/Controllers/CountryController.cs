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
    public class CountryController : Controller
    {
        private Entities db = new Entities();

        // GET: Country
        public ActionResult Index()
        {
            return View();
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

        // POST: Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Country_name,Id,Accyear,Createddatetime,Createduser,Delmark")] tbl_country tbl_country)
        {
            if (ModelState.IsValid)
            {
                db.tbl_country.Add(tbl_country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_country);
        }

        // GET: Country/Edit/5
        public ActionResult Edit(string id)
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
        public ActionResult Delete(string id)
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
