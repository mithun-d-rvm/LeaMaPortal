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

namespace LeaMaPortal.Controllers
{
    public class InvoiceController : Controller
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Invoice
        public PartialViewResult Index()
        {
            List<SelectListItem> Month = new List<SelectListItem>
                                      ();
            Month.Add(new SelectListItem
            {
                Text = "1",
                Value = "1"
            });
            Month.Add(new SelectListItem
            {
                Text = "2",
                Value = "2",
                Selected = true
            });
            Month.Add(new SelectListItem
            {
                Text = "3",
                Value = "3"
            });
            Month.Add(new SelectListItem
            {
                Text = "4",
                Value = "4"
            });
            Month.Add(new SelectListItem
            {
                Text = "5",
                Value = "5"
            });
            Month.Add(new SelectListItem
            {
                Text = "6",
                Value = "6"
            });
            Month.Add(new SelectListItem
            {
                Text = "7",
                Value = "7"
            });
            Month.Add(new SelectListItem
            {
                Text = "8",
                Value = "8"
            });
            Month.Add(new SelectListItem
            {
                Text = "9",
                Value = "9"
            });
            Month.Add(new SelectListItem
            {
                Text = "10",
                Value = "10"
            });
            Month.Add(new SelectListItem
            {
                Text = "11",
                Value = "11"
            });
            Month.Add(new SelectListItem
            {
                Text = "12",
                Value = "12"
            });
            //List<SelectListItem> InvType = new List<SelectListItem>
            //                          ();

            //InvType.Add(new SelectListItem
            //{
            //    Text = "Rental",
            //    Value = "1"
            //});
            //InvType.Add(new SelectListItem
            //{
            //    Text = "Others",
            //    Value = "1"
            //});
            //var InvType = db.Database.SqlQuery<string>(@"call usp_split('Invoice','invtype',',',null);").ToList();
            //ViewBag.InvType = new SelectList(InvType);
            //var Month = db.Database.SqlQuery<string>(@"call usp_split('Invoice','Month',',',null)").ToList();
            //ViewBag.InvType = new SelectList(Month);


            //ViewBag.Tenant_Id = new SelectList(db.tbl_tenant_individual.OrderBy(x => x.Tenant_Id), "Tenant_Id", "Tenant_Id");
            //ViewBag.Tenant_Name = new SelectList(db.tbl_tenant_individual.OrderBy(x => x.First_Name), "Tenant_Name", "Tenant_Name");
            ViewBag.Property_Id = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Property_Id), "Property_Id", "Property_Id");
            ViewBag.Property_Name = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Property_Name), "Property_Id", "Property_Name");
           ViewBag.Unit_Id = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Unit_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
          ViewBag.unit_Name = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Unit_Property_Name), "Unit_Property_Name", "Unit_Property_Name");
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement.OrderBy(x => x.Agreement_No), "id", "Agreement_No");



            // ViewBag.InvType = InvType;
            ViewBag.Month = Month;
           
            return PartialView("../Invoice/Index");
        }
        [HttpGet]
        public PartialViewResult AddorUpdate()
        {
            return PartialView("../Master/Invoice/_AddorUpdate", new InvoiceViewModel());
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
            if (tbl_invoicedt == null)
            {
                return HttpNotFound();
            }
            return View(tbl_invoicedt);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,invno,item,description,qty,amount,Delmark")] tbl_invoicedt tbl_invoicedt)
        {
            if (ModelState.IsValid)
            {
                db.tbl_invoicedt.Add(tbl_invoicedt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
            return View(tbl_invoicedt);
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
            if (tbl_invoicedt == null)
            {
                return HttpNotFound();
            }
            ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
            return View(tbl_invoicedt);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,invno,item,description,qty,amount,Delmark")] tbl_invoicedt tbl_invoicedt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_invoicedt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
            return View(tbl_invoicedt);
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
            if (tbl_invoicedt == null)
            {
                return HttpNotFound();
            }
            return View(tbl_invoicedt);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
            db.tbl_invoicedt.Remove(tbl_invoicedt);
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
