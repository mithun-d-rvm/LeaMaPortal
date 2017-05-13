using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class UtilityBillEntryController : Controller
    {
        // GET: UtilityBillEntry
        public ActionResult Index()
        {
            return View();
        }

        // GET: UtilityBillEntry/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UtilityBillEntry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilityBillEntry/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UtilityBillEntry/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UtilityBillEntry/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UtilityBillEntry/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UtilityBillEntry/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
