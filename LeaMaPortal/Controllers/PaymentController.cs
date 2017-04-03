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
    public class PaymentController : Controller
    {
        private Entities db = new Entities();

        // GET: Payments
        public ActionResult Index()
        {
            List<SelectListItem> PaymentType = new List<SelectListItem>
                                     ();
            PaymentType.Add(new SelectListItem
            {
                Text = "Advance",
                Value = "1"
            });
            PaymentType.Add(new SelectListItem
            {
                Text = "Against Invoice",
                Value = "2",
                Selected = true
            });
            PaymentType.Add(new SelectListItem
            {
                Text = "Others",
                Value = "3"
            });

            List<SelectListItem> PaymentMode = new List<SelectListItem>
                                    ();
            PaymentMode.Add(new SelectListItem
            {
                Text = "Cash",
                Value = "1"
            });
            PaymentMode.Add(new SelectListItem
            {
                Text = "Cheque",
                Value = "2",
                Selected = true
            });
            PaymentMode.Add(new SelectListItem
            {
                Text = "Online",
                Value = "3"
            });
            PaymentMode.Add(new SelectListItem
            {
                Text = "Cash",
                Value = "4"
            });
            PaymentMode.Add(new SelectListItem
            {
                Text = "Pdc",
                Value = "5",
                Selected = true
            });
            PaymentMode.Add(new SelectListItem
            {
                Text = "Advance Ajustment",
                Value = "3"
            });
            List<SelectListItem> PDCStatus = new List<SelectListItem>
                               ();
            PDCStatus.Add(new SelectListItem
            {
                Text = "Received",
                Value = "1"
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Cleared",
                Value = "2",
                Selected = true
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Bounced",
                Value = "3"
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Cancelled",
                Value = "4"
            });
            ViewBag.PaymentType = PaymentType;
            ViewBag.PaymentMode = PaymentMode;
            ViewBag.PDCStatus = PDCStatus;
            return PartialView("../Payment/Index");
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_status tbl_agreement_status = db.tbl_agreement_status.Find(id);
            if (tbl_agreement_status == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_status);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement, "Agreement_No", "Single_Multiple_Flag");
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Agreement_No,id,Ag_Tenant_id,Ag_Tenant_Name,Properties_ID,Properties_Name,Caretaker_id,Caretaker_Name,Unit_id,Unit_name,Renewal_Close_Flag,Accyear,Createddatetime,Createduser,Delmark")] tbl_agreement_status tbl_agreement_status)
        {
            if (ModelState.IsValid)
            {
                db.tbl_agreement_status.Add(tbl_agreement_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Agreement_No = new SelectList(db.tbl_agreement, "Agreement_No", "Single_Multiple_Flag", tbl_agreement_status.Agreement_No);
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_status.Agreement_No);
            return View(tbl_agreement_status);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_status tbl_agreement_status = db.tbl_agreement_status.Find(id);
            if (tbl_agreement_status == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement, "Agreement_No", "Single_Multiple_Flag", tbl_agreement_status.Agreement_No);
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_status.Agreement_No);
            return View(tbl_agreement_status);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Agreement_No,id,Ag_Tenant_id,Ag_Tenant_Name,Properties_ID,Properties_Name,Caretaker_id,Caretaker_Name,Unit_id,Unit_name,Renewal_Close_Flag,Accyear,Createddatetime,Createduser,Delmark")] tbl_agreement_status tbl_agreement_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_agreement_status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement, "Agreement_No", "Single_Multiple_Flag", tbl_agreement_status.Agreement_No);
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_status.Agreement_No);
            return View(tbl_agreement_status);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_status tbl_agreement_status = db.tbl_agreement_status.Find(id);
            if (tbl_agreement_status == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_status);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_agreement_status tbl_agreement_status = db.tbl_agreement_status.Find(id);
            db.tbl_agreement_status.Remove(tbl_agreement_status);
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
