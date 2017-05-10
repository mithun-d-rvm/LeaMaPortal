using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class UtilityPaymentController : Controller
    {
        // GET: UtilityPayment
        public ActionResult Index()
        {
            return View();
        }

        // GET: UtilityPayment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UtilityPayment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilityPayment/Create
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

        // GET: UtilityPayment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UtilityPayment/Edit/5
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

        // GET: UtilityPayment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UtilityPayment/Delete/5
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
