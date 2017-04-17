using LeaMaPortal.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;

namespace LeaMaPortal.Controllers
{
    public class VacancyReportController : Controller
    {
        LeamaEntities db = new LeamaEntities();
        // GET: VacancyReport
        public ActionResult GetVacancyReport()
        {
            ViewBag.Group = new SelectList(Common.ReportGroup);
            ViewBag.AgingFilter = new SelectList(Common.AgingDaysFilter);
            ViewBag.RentalAmountFilter = new SelectList(Common.RentalAmountFilter);
            ViewBag.Filterby = new SelectList(Common.ReportGroup);
            ViewBag.ShowFrame = false;
            ViewBag.SelectedGroup = "";
            return PartialView("../Report/VacancyReport/_VacancyReport", new VacancyReportModel());
        }

        [HttpPost]
        public async Task<ActionResult> GenerateReport(VacancyReportModel model)
        {
            ViewBag.Group = new SelectList(Common.ReportGroup, model.Group);
            ViewBag.AgingFilter = new SelectList(Common.AgingDaysFilter, model.AgingFilter);
            ViewBag.RentalAmountFilter = new SelectList(Common.RentalAmountFilter, model.RentalAmountFilter);
            ViewBag.Filterby = new SelectList(Common.ReportGroup, model.Filterby);
            ViewBag.ShowFrame = true;
            ViewBag.SelectedGroup = model.Group;
            //ViewData["Value"] = model.Value;
            //ViewData["FromDate"] = model.FromDate;
            //ViewData["ToDate"] = model.ToDate;
            //ViewData["AgingFrom"] = model.AgingFrom;
            //ViewData["AgingTo"] = model.AgingTo;
            //ViewData["RentalAmountFrom"] = model.RentalAmountFrom;
            //ViewData["RentalAmountTo"] = model.RentalAmountTo;
            object[] parameters = {
                         new MySqlParameter("@Pgroup", model.Group),
                         new MySqlParameter("@Pfromdate", model.FromDate),
                         new MySqlParameter("@Ptodate",model.ToDate),
                         new MySqlParameter("@Pfilter_field", model.Filterby),
                         new MySqlParameter("@Pfilter_value", model.Value),
                         new MySqlParameter("@Pagin_Filter", model.AgingFilter),
                         new MySqlParameter("@Pagin_Filter_From", model.AgingFrom),
                         new MySqlParameter("@Pagin_Filter_To", model.AgingTo),
                         new MySqlParameter("@Prentalamt_Filter", model.RentalAmountFilter),
                         new MySqlParameter("@Prentalamt_Filter_From", model.RentalAmountFrom),
                         new MySqlParameter("@Prentalamt_Filter_To", model.RentalAmountTo),
                         new MySqlParameter("@PCreateduser", model.CreatedUser)
                    };
            if (model.Group == "Property")
            {
                var vacantReportData = await db.Database.SqlQuery<vacancy_report>("CALL Usp_Vacant_Report_all(@Pgroup, @Pfromdate, @Ptodate, @Pfilter_field, @Pfilter_value, @Pagin_Filter, @Pagin_Filter_From, @Pagin_Filter_To, @Prentalamt_Filter, @Prentalamt_Filter_From, @Prentalamt_Filter_To, @PCreateduser)", parameters).ToListAsync();
                Session["ReportData"] = vacantReportData;
            }
            else if (model.Group == "Region")
            {
                //var data = db.Usp_Vacant_Report_all(model.Group, model.FromDate, model.ToDate, model.Filterby, model.Value, model.AgingFilter, model.AgingFrom, model.AgingTo, model.RentalAmountFilter, model.RentalAmountFrom, model.RentalAmountTo, model.CreatedUser);
                var vacantReportData = db.Database.SqlQuery<VacantRegionReportModel>("CALL Usp_Vacant_Report_all(@Pgroup, @Pfromdate, @Ptodate, @Pfilter_field, @Pfilter_value, @Pagin_Filter, @Pagin_Filter_From, @Pagin_Filter_To, @Prentalamt_Filter, @Prentalamt_Filter_From, @Prentalamt_Filter_To, @PCreateduser)", parameters).ToList();
                //var vacantReportData = await db.Database.SqlQuery(typeof(List<VacantRegionReportModel>), "CALL Usp_Vacant_Report_all(@Pgroup, @Pfromdate, @Ptodate, @Pfilter_field, @Pfilter_value, @Pagin_Filter, @Pagin_Filter_From, @Pagin_Filter_To, @Prentalamt_Filter, @Prentalamt_Filter_From, @Prentalamt_Filter_To, @PCreateduser)", parameters).ToListAsync();
                Session["ReportData"] = vacantReportData;
            }
            else if (model.Group == "Caretaker")
            {
                var vacantReportData = await db.Database.SqlQuery<VacantCaretakerReportModel>("CALL Usp_Vacant_Report_all(@Pgroup, @Pfromdate, @Ptodate, @Pfilter_field, @Pfilter_value, @Pagin_Filter, @Pagin_Filter_From, @Pagin_Filter_To, @Prentalamt_Filter, @Prentalamt_Filter_From, @Prentalamt_Filter_To, @PCreateduser)", parameters).ToListAsync();
                Session["ReportData"] = vacantReportData;
            }
            return PartialView("../Report/VacancyReport/_VacancyReport", model);
            //return Json(model);
        }
    }
}