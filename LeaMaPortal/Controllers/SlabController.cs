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
    public class SlabController : Controller
    {
        private Entities db = new Entities();

        // GET: Slab
        public ActionResult Index()
        {
           
            return View();
        }

        // GET: Slab/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure tbl_agreement_closure = db.tbl_agreement_closure.Find(id);
            if (tbl_agreement_closure == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_closure);
        }

        // GET: Slab/Create
        public ActionResult Create()
        {
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_status, "Agreement_No", "Ag_Tenant_Name");
            return View();
        }

        // POST: Slab/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Agreement_No,id,Advance_pending,Advance_Security_Amount_Paid,Less_any_damanges,Amount_to_be_refunded,Remarks,Availabledate,Accyear,Createddatetime,Createduser,Delmark")] tbl_agreement_closure tbl_agreement_closure)
        {
            if (ModelState.IsValid)
            {
                db.tbl_agreement_closure.Add(tbl_agreement_closure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_status, "Agreement_No", "Ag_Tenant_Name", tbl_agreement_closure.Agreement_No);
            return View(tbl_agreement_closure);
        }

        // GET: Slab/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure tbl_agreement_closure = db.tbl_agreement_closure.Find(id);
            if (tbl_agreement_closure == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_status, "Agreement_No", "Ag_Tenant_Name", tbl_agreement_closure.Agreement_No);
            return View(tbl_agreement_closure);
        }

        // POST: Slab/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Agreement_No,id,Advance_pending,Advance_Security_Amount_Paid,Less_any_damanges,Amount_to_be_refunded,Remarks,Availabledate,Accyear,Createddatetime,Createduser,Delmark")] tbl_agreement_closure tbl_agreement_closure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_agreement_closure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_status, "Agreement_No", "Ag_Tenant_Name", tbl_agreement_closure.Agreement_No);
            return View(tbl_agreement_closure);
        }

        // GET: Slab/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure tbl_agreement_closure = db.tbl_agreement_closure.Find(id);
            if (tbl_agreement_closure == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_closure);
        }

        // POST: Slab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_agreement_closure tbl_agreement_closure = db.tbl_agreement_closure.Find(id);
            db.tbl_agreement_closure.Remove(tbl_agreement_closure);
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
