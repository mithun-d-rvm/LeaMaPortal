using LeaMaPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class PropertyMasterController : Controller
    {
        // GET: PropertyMaster
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            return PartialView("../Master/PropertyMaster/Index");
        }
        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            PropertyViewModel model = new PropertyViewModel();
            return PartialView("../Master/PropertyMaster/_AddOrUpdate", model);
        }
        // GET: PropertyMaster/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PropertyMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyMaster/Create
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

        // GET: PropertyMaster/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropertyMaster/Edit/5
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

        // GET: PropertyMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertyMaster/Delete/5
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
