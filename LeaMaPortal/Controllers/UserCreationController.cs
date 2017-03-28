using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class UserCreationController : Controller
    {
        // GET: UserCreation
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserCreation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserCreation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserCreation/Create
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

        // GET: UserCreation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserCreation/Edit/5
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

        // GET: UserCreation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserCreation/Delete/5
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
