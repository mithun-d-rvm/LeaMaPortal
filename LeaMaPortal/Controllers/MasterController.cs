using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.Models.DBContext;
using LeaMaPortal.Models;
namespace LeaMaPortal.Controllers
{
    public class MasterController : Controller
    {
        private Entities db = new Entities();

        // GET: Master
        public async Task<ActionResult> Index()
        {
            try
            {
                MasterViewModel model = new MasterViewModel();
                ViewBag.FormMasterSelected = Common.DefaultMaster;
                ViewBag.FormMasterId = new SelectList( db.tbl_formmaster.OrderBy(x => x.MenuName), "Id", "MenuName");
                return View(model);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
           
        }

        public PartialViewResult Filter(int selected=1)
        {
            try
            {
                ViewBag.FormMasterId = new SelectList(db.tbl_formmaster.OrderBy(x => x.MenuName), "Id", "MenuName", selected);
                return PartialView("_Filter");
            }
            catch
            { throw; }
        }

        // GET: Master/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_country tbl_country = await db.tbl_country.FindAsync(id);
            if (tbl_country == null)
            {
                return HttpNotFound();
            }
            return View(tbl_country);
        }

        // GET: Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Country_name,Id,Accyear,Createddatetime,Createduser,Delmark")] tbl_country tbl_country)
        {
            if (ModelState.IsValid)
            {
                db.tbl_country.Add(tbl_country);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tbl_country);
        }

        // GET: Master/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_country tbl_country = await db.tbl_country.FindAsync(id);
            if (tbl_country == null)
            {
                return HttpNotFound();
            }
            return View(tbl_country);
        }

        // POST: Master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Country_name,Id,Accyear,Createddatetime,Createduser,Delmark")] tbl_country tbl_country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_country).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tbl_country);
        }

        // GET: Master/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_country tbl_country = await db.tbl_country.FindAsync(id);
            if (tbl_country == null)
            {
                return HttpNotFound();
            }
            return View(tbl_country);
        }

        // POST: Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            tbl_country tbl_country = await db.tbl_country.FindAsync(id);
            db.tbl_country.Remove(tbl_country);
            await db.SaveChangesAsync();
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
