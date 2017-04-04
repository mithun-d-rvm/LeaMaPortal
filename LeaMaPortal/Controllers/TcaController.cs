using LeaMaPortal.Models;
using LeaMaPortal.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class TcaController : Controller
    {
        private Entities db = new Entities();
        // GET: Tca
        public ActionResult Index()
        {
            try
            {
                AgreementFormViewModel model = new AgreementFormViewModel();
                ViewBag.TenentType = new SelectList(Common.TenentType);
                ViewBag.TenentId = new SelectList("", "");
                ViewBag.TenentName = new SelectList("", "");
                ViewBag.TcaPropertyId = new SelectList("", "");
                ViewBag.TcaPropertyIDTawtheeq = new SelectList("", "");
                ViewBag.TcaPropertyName = new SelectList("", "");
                ViewBag.UnitIDTawtheeq = new SelectList("", "");
                ViewBag.UnitPropertyName = new SelectList("", "");

                ViewBag.Profession = new SelectList(Common.Profession);
                ViewBag.PropertyId = db.tbl_propertiesmaster.OrderByDescending(x => x.Property_Id).FirstOrDefault()?.Property_Id + 1;
                //return PartialView("../Tca/Agreement/_AgreementForm");
            }
            catch (Exception e)
            {
                throw;
            }
             return View();
        }

        // GET: Tca/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tca/Create
        public ActionResult Create()
        {
            try
            {
                AgreementFormViewModel model = new AgreementFormViewModel();
                ViewBag.TitleDisplay = new SelectList(Common.Title, Common.DefaultTitle);

                ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");
                //var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
                ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name");
                ViewBag.Profession = new SelectList(Common.Profession);
                ViewBag.PropertyId = db.tbl_propertiesmaster.OrderByDescending(x => x.Property_Id).FirstOrDefault()?.Property_Id + 1;
                return PartialView("../Master/TenantIndividual/_AddOrUpdate", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // POST: Tca/Create
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

        // GET: Tca/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tca/Edit/5
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

        // GET: Tca/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tca/Delete/5
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
