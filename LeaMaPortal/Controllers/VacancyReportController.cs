using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class VacancyReportController : Controller
    {
        // GET: VacancyReport
        public ActionResult GetVacancyReport()
        {
            ViewBag.Group = new SelectList(Common.ReportGroup);
            ViewBag.AgingFilter = new SelectList(Common.AgingDaysFilter);
            ViewBag.RentalAmountFilter = new SelectList(Common.RentalAmountFilter);
            ViewBag.Filterby = new SelectList(Common.ReportGroup);
            ViewBag.ShowFrame = false;
            ViewBag.SelectedGroup = "";
            return PartialView("../Report/VacancyReport/_VacancyReport");
        }
        public ActionResult GenerateReport()
        {
            ViewBag.Group = new SelectList(Common.ReportGroup);
            ViewBag.AgingFilter = new SelectList(Common.AgingDaysFilter);
            ViewBag.RentalAmountFilter = new SelectList(Common.RentalAmountFilter);
            ViewBag.Filterby = new SelectList(Common.ReportGroup);
            ViewBag.ShowFrame = false;
            ViewBag.SelectedGroup = "";
            return PartialView("../Report/VacancyReport/_VacancyReport");
        }
    }
}