using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;

namespace LeaMaPortal.Controllers
{
    public class ContractApprovalSettingController : Controller
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: ContractApprovalSetting
        public ActionResult Index()
        {
           
            return View();
        }

        // GET: ContractApprovalSetting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_checklist tbl_agreement_checklist = db.tbl_agreement_checklist.Find(id);
            if (tbl_agreement_checklist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_checklist);
        }

        // GET: ContractApprovalSetting/Create
        public ActionResult Create()
        {
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement, "Agreement_No", "Single_Multiple_Flag");
            ViewBag.Checklist_id = new SelectList(db.tbl_checklistmaster, "Checklist_id", "Check_type");
            return View();
        }

        // POST: ContractApprovalSetting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Agreement_No,Checklist_id,Checklist_Name,Status,Remarks,Delmark")] tbl_agreement_checklist tbl_agreement_checklist)
        {
            if (ModelState.IsValid)
            {
                db.tbl_agreement_checklist.Add(tbl_agreement_checklist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Agreement_No = new SelectList(db.tbl_agreement, "Agreement_No", "Single_Multiple_Flag", tbl_agreement_checklist.Agreement_No);
            ViewBag.Checklist_id = new SelectList(db.tbl_checklistmaster, "Checklist_id", "Check_type", tbl_agreement_checklist.Checklist_id);
            return View(tbl_agreement_checklist);
        }

        // GET: ContractApprovalSetting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_checklist tbl_agreement_checklist = db.tbl_agreement_checklist.Find(id);
            if (tbl_agreement_checklist == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement, "Agreement_No", "Single_Multiple_Flag", tbl_agreement_checklist.Agreement_No);
            ViewBag.Checklist_id = new SelectList(db.tbl_checklistmaster, "Checklist_id", "Check_type", tbl_agreement_checklist.Checklist_id);
            return View(tbl_agreement_checklist);
        }

        // POST: ContractApprovalSetting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Agreement_No,Checklist_id,Checklist_Name,Status,Remarks,Delmark")] tbl_agreement_checklist tbl_agreement_checklist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_agreement_checklist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement, "Agreement_No", "Single_Multiple_Flag", tbl_agreement_checklist.Agreement_No);
            ViewBag.Checklist_id = new SelectList(db.tbl_checklistmaster, "Checklist_id", "Check_type", tbl_agreement_checklist.Checklist_id);
            return View(tbl_agreement_checklist);
        }

        // GET: ContractApprovalSetting/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_checklist tbl_agreement_checklist = db.tbl_agreement_checklist.Find(id);
            if (tbl_agreement_checklist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_checklist);
        }

        // POST: ContractApprovalSetting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_agreement_checklist tbl_agreement_checklist = db.tbl_agreement_checklist.Find(id);
            db.tbl_agreement_checklist.Remove(tbl_agreement_checklist);
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
