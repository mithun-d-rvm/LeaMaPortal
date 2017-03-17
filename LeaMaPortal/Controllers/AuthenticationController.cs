using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        // GET: Authentication/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Authentication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authentication/Create
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

        // GET: Authentication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Authentication/Edit/5
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

        // GET: Authentication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Authentication/Delete/5
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
