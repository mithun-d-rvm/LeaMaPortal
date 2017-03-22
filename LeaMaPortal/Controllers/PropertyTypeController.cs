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
    public class PropertyTypeController : Controller
    {
        private Entities db = new Entities();

        // GET: PropertyType
        public ActionResult Index()
        {
          
            return View();
        }

        // GET: PropertyType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement tbl_agreement = db.tbl_agreement.Find(id);
            if (tbl_agreement == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement);
        }

        // GET: PropertyType/Create
        public ActionResult Create()
        {
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_status, "Agreement_No", "Ag_Tenant_Name");
            ViewBag.Caretaker_id = new SelectList(db.tbl_caretaker, "Caretaker_id", "Address1");
            ViewBag.property_id = new SelectList(db.tbl_propertiesmaster, "Property_Id", "Property_Flag");
            return View();
        }

        // POST: PropertyType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Agreement_No,id,Single_Multiple_Flag,Agreement_Refno,New_Renewal_flag,Agreement_Date,Ag_Tenant_id,Ag_Tenant_Name,property_id,Property_ID_Tawtheeq,Properties_Name,Unit_ID_Tawtheeq,Unit_Property_Name,Caretaker_id,Caretaker_Name,Vacantstartdate,Agreement_Start_Date,Agreement_End_Date,Total_Rental_amount,Perday_Rental,Advance_Security_Amount,Security_Flag,Security_chequeno,Security_chequedate,Notice_Period,nofopayments,Approval_Flag,Approved_By,Approved_Date,Tenant_Type,Status,Accyear,Createddatetime,Createduser,Delmark")] tbl_agreement tbl_agreement)
        {
            if (ModelState.IsValid)
            {
                db.tbl_agreement.Add(tbl_agreement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_status, "Agreement_No", "Ag_Tenant_Name", tbl_agreement.Agreement_No);
            ViewBag.Caretaker_id = new SelectList(db.tbl_caretaker, "Caretaker_id", "Address1", tbl_agreement.Caretaker_id);
            ViewBag.property_id = new SelectList(db.tbl_propertiesmaster, "Property_Id", "Property_Flag", tbl_agreement.property_id);
            return View(tbl_agreement);
        }

        // GET: PropertyType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement tbl_agreement = db.tbl_agreement.Find(id);
            if (tbl_agreement == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_status, "Agreement_No", "Ag_Tenant_Name", tbl_agreement.Agreement_No);
            ViewBag.Caretaker_id = new SelectList(db.tbl_caretaker, "Caretaker_id", "Address1", tbl_agreement.Caretaker_id);
            ViewBag.property_id = new SelectList(db.tbl_propertiesmaster, "Property_Id", "Property_Flag", tbl_agreement.property_id);
            return View(tbl_agreement);
        }

        // POST: PropertyType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Agreement_No,id,Single_Multiple_Flag,Agreement_Refno,New_Renewal_flag,Agreement_Date,Ag_Tenant_id,Ag_Tenant_Name,property_id,Property_ID_Tawtheeq,Properties_Name,Unit_ID_Tawtheeq,Unit_Property_Name,Caretaker_id,Caretaker_Name,Vacantstartdate,Agreement_Start_Date,Agreement_End_Date,Total_Rental_amount,Perday_Rental,Advance_Security_Amount,Security_Flag,Security_chequeno,Security_chequedate,Notice_Period,nofopayments,Approval_Flag,Approved_By,Approved_Date,Tenant_Type,Status,Accyear,Createddatetime,Createduser,Delmark")] tbl_agreement tbl_agreement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_agreement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_status, "Agreement_No", "Ag_Tenant_Name", tbl_agreement.Agreement_No);
            ViewBag.Caretaker_id = new SelectList(db.tbl_caretaker, "Caretaker_id", "Address1", tbl_agreement.Caretaker_id);
            ViewBag.property_id = new SelectList(db.tbl_propertiesmaster, "Property_Id", "Property_Flag", tbl_agreement.property_id);
            return View(tbl_agreement);
        }

        // GET: PropertyType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement tbl_agreement = db.tbl_agreement.Find(id);
            if (tbl_agreement == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement);
        }

        // POST: PropertyType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_agreement tbl_agreement = db.tbl_agreement.Find(id);
            db.tbl_agreement.Remove(tbl_agreement);
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
