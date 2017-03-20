using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class MasterIndividualController : Controller
    {
        // GET: MasterIndividual
        public ActionResult Index()
        {
            return View();
        }

        // GET: MasterIndividual/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MasterIndividual/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterIndividual/Create
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

        // GET: MasterIndividual/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MasterIndividual/Edit/5
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

        // GET: MasterIndividual/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MasterIndividual/Delete/5
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
