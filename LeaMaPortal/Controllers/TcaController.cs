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
        private string user = "rmv";
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

        public async Task<PartialViewResult> AddOrUpdate()
        {
            try
            {
                AgreementFormViewModel model = new AgreementFormViewModel();
                ViewBag.TenantType = new SelectList(Common.TcaTenantType);
                ViewBag.TenantId = new SelectList("", "");
                ViewBag.TenantName = new SelectList("", "");
                var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id");
                ViewBag.TcaPropertyIDTawtheeq = new SelectList(property,"Property_Id", "Property_ID_Tawtheeq");
                ViewBag.TcaPropertyName = new SelectList(property,"Property_Id", "Property_Name");
                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                ViewBag.UnitIDTawtheeq = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
                ViewBag.UnitPropertyName = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_Property_Name");
                ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag);
                ViewBag.Agreement_No = db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Agreement/_AgreementForm", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public PartialViewResult AgreementPdc(int AgreementNo)
        {
            AgreementPdcViewModel model = new AgreementPdcViewModel();
            //AgreementFormViewModel model = new AgreementFormViewModel();
            ViewBag.Month = new SelectList(Common.Months);
            ViewBag.Payment_Mode = new SelectList(Common.PaymentMode);
            //ViewBag.AgreementPd = model.AgreementPd;
            return PartialView("../Tca/_AgreementPdc", model);
        }
        public async Task<PartialViewResult> AgreementDocument(int AgreementNo)
        {
            AgreementDocumentViewModel model = new AgreementDocumentViewModel();
            try
            {
                var facility = await db.tbl_facilitymaster.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.Facility_id = new SelectList(facility, "Facility_Name", "Facility_id");
                ViewBag.Facility_Name = new SelectList(facility, "Facility_id", "Facility_Name");
                if (AgreementNo != 0)
                {
                    model.agreementDocumentList = db.tbl_agreement_facility.Where(x => x.Agreement_No == AgreementNo)
                    .Select(x => new AgreementDocumentViewModel()
                    {
                        Id = x.id,
                        Facility_id = x.Facility_id,
                        Facility_Name = x.Facility_Name,
                        Numbers_available = x.Numbers_available.HasValue ? x.Numbers_available.Value : 0
                    }).ToList();
                }
                return PartialView("../Tca/_AgreementDocument", model);
            }
            catch
            {
                throw;
            }
        }
        public async Task<PartialViewResult> AgreementUtility(int AgreementNo)
        {
            AgreementUtilityViewModel model = new AgreementUtilityViewModel();
            try
            {
                var utility =await db.tbl_utilitiesmaster.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.Utility_id = new SelectList(utility, "Utility_Name", "Utility_id");
                ViewBag.Utility_Name = new SelectList(utility, "Utility_id", "Utility_Name");
                ViewBag.Amount_Type = new SelectList(Common.PaymentMode);
                List<PaybleName> payable = new List<PaybleName>();

                var tenantCompany =await db.tbl_tenant_company.Where(x => x.Delmark != "*").Select(x => new PaybleName() {Name=x.First_Name }).ToListAsync();
                var tenant =await db.tbl_tenant_individual.Where(x => x.Delmark != "*").Select(x => new PaybleName() { Name = x.First_Name  }).ToListAsync();
                payable.AddRange(tenantCompany);
                payable.AddRange(tenant);
                payable= payable.OrderBy(x => x.Name).ToList();
                ViewBag.Payable = new SelectList(payable,"Name", "Name");
                if(AgreementNo!=0)
                {
                    model.AgreementUtilityList =await db.tbl_agreement_utility.Where(x => x.Agreement_No == AgreementNo && x.Delmark != "*").
                                                Select(x => new AgreementUtilityViewModel() {
                                                    Id=x.id,
                                                    Utility_id=x.Utility_id,
                                                    Utility_Name=x.Utility_Name,
                                                    Payable=x.Payable,
                                                    Amount_Type=x.Amount_Type,
                                                    Amount=x.Amount.HasValue?decimal.Parse(x.Amount.ToString()):0
                                                }).ToListAsync();
                }
                return PartialView("../Tca/_AgreementUtility", model);
            }
            catch
            {
                throw;
            }
            //AgreementFormViewModel model = new AgreementFormViewModel();
           
        }
    public class PaybleName
    {
        public string Name { get; set; }
    }
        public async  Task<PartialViewResult> AgreementUnit(int AgreementNo)
        {
            AgreementUnitViewModel model = new AgreementUnitViewModel();
            var property =await db.tbl_propertiesmaster.Where(x => x.Delmark != "*").ToListAsync();
            
            ViewBag.Property_ID = new SelectList(property, "Property_ID", "Property_ID");
            ViewBag.Properties_Name = new SelectList(property, "Property_Name", "Property_Name");
            ViewBag.Property_ID_Tawtheeq = new SelectList(property, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq");
            ViewBag.Unit_ID_Tawtheeq = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
            ViewBag.Unit_Property_Name = new SelectList(property, "Unit_Property_Name", "Unit_Property_Name");

            return PartialView("../Tca/_AgreementUnit", model);
        }
        public async Task<PartialViewResult> AgreementCheckList(int AgreementNo)
        {
            AgreementCheckListViewModel model = new AgreementCheckListViewModel();
            try
            {
                if (AgreementNo != 0)
                {
                    var checkList = await db.tbl_agreement_checklist.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*");
                    if (checkList != null)
                    {
                        model.Status = checkList.Status.HasValue ? checkList.Status.Value : 0;
                        model.Remarks = checkList.Remarks;
                    }
                }
                return PartialView("../Tca/_AgreementCheckList", model);
            }
            catch
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(AgreementFormViewModel model,FormCollection FC)
        {
            try
            {
                string PFlag = Common.UPDATE;
                if (model.Agreement_No == 0)
                {
                    PFlag = Common.INSERT;
                    var Agreement = await db.tbl_agreement.OrderByDescending(r => r.Agreement_No).FirstOrDefaultAsync();
                    model.Agreement_No = Agreement != null ? Agreement.Agreement_No + 1 : 1;
                }
                object[] param = Helper.GetMySqlParameters<AgreementFormViewModel>(model, PFlag, user);
//                PFlag VARCHAR(10)
//, PSingle_Multiple_Flag varchar(20)
//, PAgreement_Refno  int
//, PNew_Renewal_flag  varchar(20)
//, PAgreement_No  int(11)
//, PAgreement_Date  datetime
//, PAg_Tenant_id  int
//, PAg_Tenant_Name  varchar(100)
//, Pproperty_id int
//, PProperty_ID_Tawtheeq  varchar(100)
//, PProperties_Name  varchar(100)
//, PUnit_ID_Tawtheeq  varchar(100)
//, PUnit_Property_Name varchar(100)
//, PCaretaker_id  int
//, PCaretaker_Name  varchar(100)
//, Ptenant_source  varchar(100)
//, PAgent_id  int
//, PAgent_name  varchar(100)
//, PEmp_id  int
//, PEmp_name  varchar(100)
//, PVacantstartdate  datetime
//, PAgreement_Start_Date  datetime
//, PAgreement_End_Date  datetime
//, PTotal_Rental_amount  float
//, PPerday_Rental  float
//, PAdvance_Security_Amount  float
//, PSecurity_Flag  varchar(20)
//, PSecurity_chequeno  varchar(50)
//, PSecurity_chequedate  datetime
//, PNotice_Period  int(11)
//, Pnofopayments  int(11)
//, PApproval_Flag  int(11)
//, PApproved_By  varchar(75)
//, PApproved_Date  datetime
//, PTenant_Type varchar(25)
//, PCreateduser  varchar(150)
//, PAgpdc longtext
//, pAgdoc longtext
//, pAgfac longtext
//, pAguti longtext
//, pAgchk longtext
//, pAgunit longtext

            }
            catch
            {

            }
            return View("Index");
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
