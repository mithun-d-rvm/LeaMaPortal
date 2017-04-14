using LeaMaPortal.Models;
using LeaMaPortal.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
            //try
            //{
            //    AgreementFormViewModel model = new AgreementFormViewModel();
            //    ViewBag.TenantType = new SelectList(Common.TcaTenantType);
            //    ViewBag.TenantId = new SelectList("", "");
            //    ViewBag.TenantName = new SelectList("", "");
            //    ViewBag.TcaPropertyId = new SelectList("", "");
            //    ViewBag.TcaPropertyIDTawtheeq = new SelectList("", "");
            //    ViewBag.TcaPropertyName = new SelectList("", "");
            //    ViewBag.UnitIDTawtheeq = new SelectList("", "");
            //    ViewBag.UnitPropertyName = new SelectList("", "");
            //    ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag);
            //    ViewBag.Agreement_No = db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
            //    model.AgreementPd = new AgreementPdcViewModel();
            //    return PartialView("../Tca/Agreement/_AgreementForm", model);
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}
            return View();
        }

        public PartialViewResult AddOrUpdate()
        {
            try
            {
                AgreementFormViewModel model = new AgreementFormViewModel();
                ViewBag.TenantType = new SelectList(Common.TcaTenantType);
                ViewBag.TenantId = new SelectList("", "");
                ViewBag.TenantName = new SelectList("", "");
                ViewBag.TcaPropertyId = new SelectList("", "");
                ViewBag.TcaPropertyIDTawtheeq = new SelectList("", "");
                ViewBag.TcaPropertyName = new SelectList("", "");
                ViewBag.UnitIDTawtheeq = new SelectList("", "");
                ViewBag.UnitPropertyName = new SelectList("", "");
                ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag);
                ViewBag.Agreement_No = db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Agreement/_AgreementForm", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public PartialViewResult AgreementPdc(int AgreementNo)
        {
            //AgreementPdcViewModel model = new AgreementPdcViewModel();
            AgreementFormViewModel model = new AgreementFormViewModel();
            model.AgreementPd.Month = new SelectList(Common.Months);
            model.AgreementPd.Payment_Mode = new SelectList(Common.PaymentMode);
            ViewBag.AgreementPd = model.AgreementPd;
            return PartialView("../Tca/_AgreementPdc", model);
        }
        public PartialViewResult AgreementDocument(int AgreementNo)
        {
            AgreementDocumentViewModel model = new AgreementDocumentViewModel();
            ViewBag.Facility_id = new SelectList(db.tbl_facilitymaster.Where(x => x.Delmark != "*"), "Facility_Name", "Facility_id");
            ViewBag.Facility_Name = new SelectList(db.tbl_facilitymaster.Where(x => x.Delmark != "*"), "Facility_id", "Facility_Name");
            if (AgreementNo!=0)
            {
                model.agreementDocumentList = db.tbl_agreement_facility.Where(x => x.Agreement_No == AgreementNo)
                .Select(x => new AgreementDocumentViewModel()
                {
                    Id = x.id,
                    Facility_id=x.Facility_id,
                    Facility_Name=x.Facility_Name,
                    Numbers_available=x.Numbers_available.HasValue? x.Numbers_available.Value:0
                }).ToList();
            }
            return PartialView("../Tca/_AgreementDocument", model);
        }
        public PartialViewResult AgreementUtility(int AgreementNo)
        {
            //AgreementUtilityViewModel model = new AgreementUtilityViewModel();
            AgreementFormViewModel model = new AgreementFormViewModel();
            return PartialView("../Tca/_AgreementUtility", model);
        }
        public PartialViewResult AgreementUnit(int AgreementNo)
        {
            AgreementUnitViewModel model = new AgreementUnitViewModel();
            return PartialView("../Tca/_AgreementUnit", model);
        }
        public PartialViewResult AgreementCheckList(int AgreementNo)
        {
            AgreementCheckListViewModel model = new AgreementCheckListViewModel();
            return PartialView("../Tca/_AgreementCheckList", model);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(AgreementFormViewModel model)
        {

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> GetTenentDetails(string Type)
        {
            try
            {
                DdlTenentDetailsViewModel model = new DdlTenentDetailsViewModel();
                if (Type == "Company")
                {
                    var query = db.tbl_tenant_company.Where(x => x.Delmark != "*" && x.Type == "company");
                    model.TenantId = new SelectList(query.OrderBy(r => r.Tenant_Id).ToList(), "Tenant_Id", "Tenant_Id");
                    model.TenantName = new SelectList(query.OrderBy(r => r.First_Name).ToList(), "First_Name", "First_Name");
                }
                else
                {
                    var query = db.tbl_tenant_individual.Where(x => x.Delmark != "*" && x.Type == Type);
                    model.TenantId = new SelectList(query.OrderBy(r => r.Tenant_Id), "Tenant_Id", "Tenant_Id");
                    model.TenantName = new SelectList(query.OrderBy(r => r.First_Name), "First_Name", "First_Name");
                }
                //var tbl_caretaker = await db.tbl_tenant_company.FirstOrDefaultAsync(x => x.Id == Id);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
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
