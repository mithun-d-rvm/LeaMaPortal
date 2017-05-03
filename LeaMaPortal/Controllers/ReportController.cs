using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index(int selected = 0)
        {
            try
            {
                ViewBag.ReportSelected = selected == 0 ? Common.DefaultReport : selected;
                ViewBag.Reports = new SelectList(Common.ReportList, "Id", "ReportName");
                return View();
            }
            catch
            {
                return View();
            }
        }

        public PartialViewResult Filter(int selected = 1)
        {
            try
            {
                ViewBag.Reports = new SelectList(Common.ReportList, "Id", "ReportName", selected);
                return PartialView("_Filter");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult GetContractReport()
        {
            return PartialView("../Report/ContractReport/_ContractReport");
        }
        public ActionResult GetOutStandingReport()
        {
            return PartialView("../Report/OutStandingReport/_OutStandingReport");
        }
        public ActionResult GetUtilityReport()
        {
            return PartialView("../Report/UtilityReport/_EBWaterReport");
        }
        public ActionResult GetPDCReport()
        {
            return PartialView("../Report/PDCReport/_PDCReport");
        }
        public ActionResult GetCollectionSummaryReport()
        {
            return PartialView("../Report/CollectionSummary/_CollectionSummaryReport");
        }
        public ActionResult GetSummaryEBWaterReport()
        {
            return PartialView("../Report/SummaryEBWaterReport/_SummaryEBWaterReport");
        }
        public ActionResult GetVacancyReport()
        {
            return PartialView("../Report/VacancyReport/_VacancyReport");
        }

        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
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

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
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

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
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
