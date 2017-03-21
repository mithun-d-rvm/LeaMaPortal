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
    public class CaretakerController : Controller
    {
        private Entities db = new Entities();

        // GET: Caretaker
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Caretaker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
            if (tbl_agreement_closure_checklist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_closure_checklist);
        }

        // GET: Caretaker/Create
        public ActionResult Create()
        {
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid");
            return View();
        }

        // POST: Caretaker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Agreement_No,Checklist_id,Checklist_Name,Status,Remarks,Delmark")] tbl_agreement_closure_checklist tbl_agreement_closure_checklist)
        {
            if (ModelState.IsValid)
            {
                db.tbl_agreement_closure_checklist.Add(tbl_agreement_closure_checklist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_closure_checklist.Agreement_No);
            return View(tbl_agreement_closure_checklist);
        }

        // GET: Caretaker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
            if (tbl_agreement_closure_checklist == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_closure_checklist.Agreement_No);
            return View(tbl_agreement_closure_checklist);
        }

        // POST: Caretaker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Agreement_No,Checklist_id,Checklist_Name,Status,Remarks,Delmark")] tbl_agreement_closure_checklist tbl_agreement_closure_checklist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_agreement_closure_checklist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_closure_checklist.Agreement_No);
            return View(tbl_agreement_closure_checklist);
        }

        // GET: Caretaker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
            if (tbl_agreement_closure_checklist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_closure_checklist);
        }

        // POST: Caretaker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
            db.tbl_agreement_closure_checklist.Remove(tbl_agreement_closure_checklist);
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
