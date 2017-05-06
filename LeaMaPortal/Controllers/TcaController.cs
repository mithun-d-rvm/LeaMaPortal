using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcPaging;

namespace LeaMaPortal.Controllers
{
    public class TcaController : Controller
    {
        private LeamaEntities db = new LeamaEntities();
        //private string user = "rmv";
        // GET: Tca
        public ActionResult Index()
        {
            //try
            //{
            //    AgreementFormViewModel model = new AgreementFormViewModel();
            //    ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType);
            //    ViewBag.TenantId = new SelectList("", "");
            //    ViewBag.Ag_Tenant_Name = new SelectList("", "");
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

        public async Task<PartialViewResult>List(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                
                //IList<CountryViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                   var list = db.tbl_agreement.Where(x => x.Delmark != "*").OrderBy(x => x.Agreement_No).Select(x => new AgreementFormViewModel()
                    {
                       Agreement_No=x.Agreement_No, 
                       Properties_Name=x.Properties_Name,
                       Ag_Tenant_Name=x.Ag_Tenant_Name,
                       Unit_Property_Name=x.Unit_Property_Name

                    }).ToPagedList(currentPageIndex, PageSize);
                    return PartialView("../Tca/_List", list);
                }
                else
                {
                   var list = db.tbl_agreement.Where(x => x.Delmark != "*" && x.Agreement_No.ToString().ToLower().Contains(Search.ToLower()))
                                  .OrderBy(x => x.Agreement_No).Select(x => new AgreementFormViewModel()
                                  {
                                      Agreement_No = x.Agreement_No,
                                      Properties_Name = x.Properties_Name,
                                      Ag_Tenant_Name = x.Ag_Tenant_Name,
                                      Unit_Property_Name = x.Unit_Property_Name
                                  }).ToPagedList(currentPageIndex, PageSize);
                    return PartialView("../Tca/_List", list);
                }

                //return PartialView("../Tca/_List", model.List);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<PartialViewResult> AddOrUpdate()
        {
            try
            {
                AgreementFormViewModel model = new AgreementFormViewModel();
                ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType);
                ViewBag.Ag_Tenantid = new SelectList("", "");
                ViewBag.Ag_TenantName = new SelectList("", "");
                var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id");
                ViewBag.TcaPropertyIDTawtheeq = new SelectList(property,"Property_Id", "Property_ID_Tawtheeq");
                ViewBag.TcaPropertyName = new SelectList(property,"Property_Id", "Property_Name");
                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                ViewBag.UnitIDTawtheeq = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
                ViewBag.UnitPropertyName = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_Property_Name");
                ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag);
                ViewBag.Agreement_No = db.tbl_agreement.OrderByDescending(x=> x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                var caretaker=await db.tbl_caretaker.Where(x=> x.Delmark != "*").ToListAsync();
                ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id");
                ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_id", "Caretaker_Name");
                model.New_Renewal_flag = Common.NewAgreement;
                model.Agreement_No = 0;
                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Agreement/_AgreementForm", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<PartialViewResult> Renewal(int AgreementNo)
        {
            try
            {
                AgreementFormViewModel model = new AgreementFormViewModel();
                var agreementDet =await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*");
                AgreementRenwalMap(agreementDet, model);
                //ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType);
                //ViewBag.Ag_Tenantid = new SelectList("", "");
                //ViewBag.Ag_TenantName = new SelectList("", "");
                //var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*").ToListAsync();
                //ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id");
                //ViewBag.TcaPropertyIDTawtheeq = new SelectList(property, "Property_Id", "Property_ID_Tawtheeq");
                //ViewBag.TcaPropertyName = new SelectList(property, "Property_Id", "Property_Name");
                ////var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                //ViewBag.UnitIDTawtheeq = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
                //ViewBag.UnitPropertyName = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_Property_Name");
                //ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag);
                //ViewBag.Agreement_No = db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                //var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*").ToListAsync();
                //ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id");
                //ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_id", "Caretaker_Name");

                ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType,agreementDet.Tenant_Type);
                model.Tenant_Type = agreementDet.Tenant_Type;
                model.Ag_Tenant_id = agreementDet.Ag_Tenant_id;
                model.Ag_Tenant_Name = agreementDet.Ag_Tenant_Name;
                ViewBag.Ag_Tenantid = new SelectList("", "");
                ViewBag.Ag_TenantName = new SelectList("", "");
                var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*").ToListAsync();
                model.property_id = agreementDet.property_id;
                model.Property_ID_Tawtheeq = agreementDet.Property_ID_Tawtheeq;
                model.Properties_Name = agreementDet.Properties_Name;
                ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id", agreementDet.property_id);
                ViewBag.TcaPropertyIDTawtheeq = new SelectList(property, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq);
                ViewBag.TcaPropertyName = new SelectList(property, "Property_Name", "Property_Name",agreementDet.Properties_Name);
                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                ViewBag.UnitIDTawtheeq = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq",agreementDet.Unit_ID_Tawtheeq);
                ViewBag.UnitPropertyName = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_Property_Name",agreementDet.Unit_Property_Name);
                ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag, agreementDet.Security_Flag);
                ViewBag.Agreement_No = AgreementNo; //db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*").ToListAsync();
                model.Caretaker_id = agreementDet.Caretaker_id;
                model.Caretaker_Name = agreementDet.Caretaker_Name;
                ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id", agreementDet.Caretaker_id);
                ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_Name", "Caretaker_Name",agreementDet.Caretaker_Name);
                model.New_Renewal_flag = Common.Renewal;
                model.Agreement_No = AgreementNo;
                model.Agreement_Refno = AgreementNo;
                
                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Agreement/_AgreementFormRenewal", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<PartialViewResult> Closure(int AgreementNo)
        {
            try
            {
                AgreementClosureViewModel model = new AgreementClosureViewModel();
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*");
                AgreementClosureMap(agreementDet, model);
                
                ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType);
                ViewBag.Ag_Tenantid = new SelectList("", "");
                ViewBag.Ag_TenantName = new SelectList("", "");
                var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id", agreementDet.property_id);
                ViewBag.TcaPropertyIDTawtheeq = new SelectList(property, "Property_Id", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq);
                ViewBag.TcaPropertyName = new SelectList(property, "Property_Id", "Property_Name", agreementDet.Properties_Name);
                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                ViewBag.UnitIDTawtheeq = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", agreementDet.Unit_ID_Tawtheeq);
                ViewBag.UnitPropertyName = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_Property_Name", agreementDet.Unit_Property_Name);
                ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag, agreementDet.Security_Flag);
                ViewBag.Agreement_No = AgreementNo; //db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id", agreementDet.Caretaker_id);
               
                ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_Name", "Caretaker_Name", agreementDet.Caretaker_Name);
               // model.New_Renewal_flag = Common.Renewal;
                model.Agreement_No = AgreementNo;
              //  model.Agreement_Refno = AgreementNo;

                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Closure/_ClosureDetails", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
         [HttpPost]
        public async Task<PartialViewResult> Closure(AgreementClosureViewModel model)
        {
            try
            {
                //var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*");
                //AgreementClosureMap(agreementDet, model);

                //ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType);
                //ViewBag.Ag_Tenantid = new SelectList("", "");
                //ViewBag.Ag_TenantName = new SelectList("", "");
                //var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*").ToListAsync();
                //ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id", agreementDet.property_id);
                //ViewBag.TcaPropertyIDTawtheeq = new SelectList(property, "Property_Id", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq);
                //ViewBag.TcaPropertyName = new SelectList(property, "Property_Id", "Property_Name", agreementDet.Properties_Name);
                ////var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                //ViewBag.UnitIDTawtheeq = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", agreementDet.Unit_ID_Tawtheeq);
                //ViewBag.UnitPropertyName = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_Property_Name", agreementDet.Unit_Property_Name);
                //ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag, agreementDet.Security_Flag);
                //ViewBag.Agreement_No = AgreementNo; //db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                //var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*").ToListAsync();
                //ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id", agreementDet.Caretaker_id);
                //ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_Name", "Caretaker_Name", agreementDet.Caretaker_Name);
                //// model.New_Renewal_flag = Common.Renewal;
                //model.Agreement_No = AgreementNo;
                ////  model.Agreement_Refno = AgreementNo;

                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Closure/_ClosureDetails", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void AgreementRenwalMap(tbl_agreement from, AgreementFormViewModel to)
        {
            to.Single_Multiple_Flag = from.Single_Multiple_Flag;
            to.nofopayments = from.nofopayments.HasValue?from.nofopayments.Value:0;
            to.Agreement_Date = from.Agreement_Date.HasValue ? from.Agreement_Date.Value : DateTime.Now;
            to.Vacantstartdate = from.Vacantstartdate.HasValue ? from.Vacantstartdate.Value :DateTime.MinValue; 
            to.Agreement_Start_Date=from.Agreement_Start_Date.HasValue? from.Agreement_Start_Date.Value: DateTime.Now;
            to.Agreement_End_Date = from.Agreement_End_Date.HasValue ? from.Agreement_End_Date.Value : DateTime.Now;
            to.Total_Rental_amount = from.Total_Rental_amount.HasValue ? from.Total_Rental_amount.Value : 0;
            to.Perday_Rental = from.Perday_Rental.HasValue ? from.Perday_Rental.Value : 0;
            to.nofopayments = from.nofopayments.HasValue ? from.nofopayments.Value : 0;
            to.Advance_Security_Amount = from.Advance_Security_Amount.HasValue ? from.Advance_Security_Amount.Value : 0;
            to.Security_Flag = from.Security_Flag;
            to.Security_chequeno = from.Security_chequeno;
            to.Security_chequedate = from.Security_chequedate.HasValue ? from.Security_chequedate.Value : DateTime.MinValue;
            to.Notice_Period = from.Notice_Period.HasValue ? from.Notice_Period.Value : 0;
            to.Approval_Flag = from.Approval_Flag.HasValue ? from.Approval_Flag.Value : 0;
            to.Approved_By = from.Approved_By;
            to.Approved_Date = from.Approved_Date.HasValue ? from.Approved_Date.Value : DateTime.MinValue;
        }
        public void AgreementClosureMap(tbl_agreement from, AgreementClosureViewModel to)
        {
            //to.Single_Multiple_Flag = from.Single_Multiple_Flag;
            //to.nofopayments = from.nofopayments.HasValue ? from.nofopayments.Value : 0;
            to.Agreement_Date = from.Agreement_Date.HasValue ? from.Agreement_Date.Value : DateTime.Now;
            //to.Vacantstartdate = from.Vacantstartdate.HasValue ? from.Vacantstartdate.Value : DateTime.MinValue;
            to.Agreement_Start_Date = from.Agreement_Start_Date.HasValue ? from.Agreement_Start_Date.Value : DateTime.Now;
            to.Agreement_End_Date = from.Agreement_End_Date.HasValue ? from.Agreement_End_Date.Value : DateTime.Now;
            //to.Total_Rental_amount = from.Total_Rental_amount.HasValue ? from.Total_Rental_amount.Value : 0;
            //to.Perday_Rental = from.Perday_Rental.HasValue ? from.Perday_Rental.Value : 0;
            //to.nofopayments = from.nofopayments.HasValue ? from.nofopayments.Value : 0;
            //to.Advance_Security_Amount = from.Advance_Security_Amount.HasValue ? from.Advance_Security_Amount.Value : 0;
            //to.Security_Flag = from.Security_Flag;
            //to.Security_chequeno = from.Security_chequeno;
            //to.Security_chequedate = from.Security_chequedate.HasValue ? from.Security_chequedate.Value : DateTime.MinValue;
            //to.Notice_Period = from.Notice_Period.HasValue ? from.Notice_Period.Value : 0;
            //to.Approval_Flag = from.Approval_Flag.HasValue ? from.Approval_Flag.Value : 0;
            //to.Approved_By = from.Approved_By;
            //to.Approved_Date = from.Approved_Date.HasValue ? from.Approved_Date.Value : DateTime.MinValue;
        }

        public PartialViewResult AgreementPdc(int AgreementNo)
        {
            AgreementPdcViewModel model = new AgreementPdcViewModel();
            //AgreementFormViewModel model = new AgreementFormViewModel();
            ViewBag.Month = new SelectList(Common.Months);
            ViewBag.Payment_Mode = new SelectList(Common.PaymentMode);
            //ViewBag.AgreementPd = model.AgreementPd;
            if(AgreementNo!=0)
            {
                model.AgreementPdcList = db.tbl_agreement_pdc.Where(x => x.Agreement_No == AgreementNo && x.Delmark != "*").AsEnumerable().Select(x => new AgreementPdcViewModel()
                {
                Id=x.id,
                Month=x.Month,
                Year=x.Year,
                Payment_Mode=x.Payment_Mode,
                BankName=x.BankName,
                Cheque_No=x.Cheque_No,
                //Cheque_Date=string.IsNullOrWhiteSpace(x.Cheque_Date.ToString())?DateTime.Parse(x.Cheque_Date.ToString()): (DateTime?)null,
                Cheque_Date= x.Cheque_Date,
                Cheque_Amount =x.Cheque_Amount

                }).ToList();
                return PartialView("../Tca/Renewal/_AgreementPdc", model);
            }
            return PartialView("../Tca/_AgreementPdc", model);
        }
        public async Task<PartialViewResult> AgreementDocument(int AgreementNo)
        {
            AgreementDocumentViewModel model = new AgreementDocumentViewModel();
            try
            {
                var facility = await db.tbl_facilitymaster.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.Facility_id = new SelectList(facility, "Facility_id", "Facility_id");
                ViewBag.Facility_Name = new SelectList(facility, "Facility_id", "Facility_Name");
                if (AgreementNo != 0)
                {
                    model.agreementDocumentExistList = db.tbl_agreement_doc.Where(x => x.Agreement_No == AgreementNo)
                    .Select(x => new AgreementDocumentExist()
                    {
                        Id = x.id,
                        Doc_name = x.Doc_name,
                        Doc_Path = x.Doc_Path,
                    }).ToList();
                    
                   return PartialView("../Tca/Renewal/_AgreementDocument", model);
                }
                return PartialView("../Tca/_AgreementDocument", model);
            }
            catch
            {
                throw;
            }
        }
        public async Task<PartialViewResult> AgreementFacility(int AgreementNo)
        {
            AgreementFacilityViewModel model = new AgreementFacilityViewModel();
            try
            {
                var facility = await db.tbl_facilitymaster.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.Facility_id = new SelectList(facility, "Facility_id", "Facility_id");
                ViewBag.Facility_Name = new SelectList(facility, "Facility_id", "Facility_Name");
                if (AgreementNo != 0)
                {
                    model.agreementFacilityList = db.tbl_agreement_facility.Where(x => x.Agreement_No == AgreementNo)
                    .Select(x => new AgreementFacilityViewModel()
                    {
                        Id = x.id,
                        Facility_id = x.Facility_id,
                        Facility_Name = x.Facility_Name,
                        Numbers_available = x.Numbers_available.HasValue ? x.Numbers_available.Value : 0
                    }).ToList();
                    
                        return PartialView("../Tca/Renewal/_AgreementFacility", model);
                }
                return PartialView("../Tca/_AgreementFacility", model);
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
                ViewBag.Utility_id = new SelectList(utility, "Utility_id", "Utility_id");
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
                                                    Amount=x.Amount.HasValue?x.Amount.Value:0
                                                }).ToListAsync();
                    
                return PartialView("../Tca/Renewal/_AgreementUtility", model);

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
            ViewBag.Properties_Name = new SelectList(property, "Property_ID", "Property_Name");
            ViewBag.Property_ID_Tawtheeq = new SelectList(property, "Property_ID", "Property_ID_Tawtheeq");
            ViewBag.Unit_ID_Tawtheeq = new SelectList(property, "Property_ID", "Unit_ID_Tawtheeq");
            ViewBag.Unit_Property_Name = new SelectList(property, "Property_ID", "Unit_Property_Name");
            if (AgreementNo != 0)
            {
                model.AgreementUnitList =await db.tbl_agreement_unit_inner.Where(x => x.Delmark != "*" && x.Agreement_No == AgreementNo).Select(x => new AgreementUnitViewModel()
                {
                       Id=x.id,
                       Property_ID=x.Property_ID.ToString(),
                       Properties_Name=x.Properties_Name,
                       Property_ID_Tawtheeq=x.Property_ID_Tawtheeq,
                       Unit_ID_Tawtheeq=x.Unit_ID_Tawtheeq,
                       Unit_Property_Name=x.Unit_Property_Name
                }).ToListAsync();
                return PartialView("../Tca/Renewal/_AgreementUnit", model);
            }

            return PartialView("../Tca/_AgreementUnit", model);
        }
        public async Task<PartialViewResult> AgreementCheckList(int AgreementNo)
        {
            AgreementCheckListViewModel model = new AgreementCheckListViewModel();
            try
            {
                
                    var agreementCheckList = await db.tbl_agreement_checklist.Where(x => x.Agreement_No == AgreementNo && x.Delmark != "*").ToListAsync();
                    var checkList = await db.tbl_checklistmaster.Where(x => x.Check_type == Common.AgreementCheck_type && x.Delmark != "*").ToListAsync();
                    foreach(var item in checkList)
                    {
                        var agreementCheck = agreementCheckList != null ? agreementCheckList.FirstOrDefault(x => x.Checklist_id == item.Checklist_id) : null;
                        model.AgreementCheckList.Add(new AgreementCheckListViewModel()
                        {
                            Checklist_id = item.Checklist_id,
                            Checklist_Name = item.Checklist_Name,
                            Status = agreementCheck != null ? (agreementCheck.Status.HasValue ? agreementCheck.Status.Value : 0) : 0,
                            Remarks = agreementCheck != null ? agreementCheck.Remarks : ""
                        });

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
                    //Agreement = await db.tbl_agreement.OrderByDescending(r => r.id).FirstOrDefaultAsync();
                    //model.Id = Agreement != null ? Agreement.id + 1 : 1;
                }
                string Agpdc = null;
                #region agpdc
                if (model.AgreementPdcList != null)
                {
                    foreach (var item in model.AgreementPdcList)
                    {
                        
                        var Cheque_Date = item.Cheque_Date != null ? "'"+item.Cheque_Date.Value.Date.ToString("yyyy-MM-dd")+"'" :"null";
                        if (string.IsNullOrWhiteSpace(Agpdc))
                        {
                            Agpdc = "(" + model.Agreement_No + ",'" + item.Month + "','" + item.Year + "','" + item.BankName + "','" + item.Cheque_No +
                                    "'," + Cheque_Date + ",'" + item.Cheque_Amount + "','" + item.Payment_Mode + "')";
                        }
                        else
                        {
                            Agpdc += ",(" + model.Agreement_No + ",'" + item.Month + "','" + item.Year + "','" + item.BankName + "','" + item.Cheque_No +
                                    "'," + Cheque_Date + ",'" + item.Cheque_Amount + "','" + item.Payment_Mode + "')";
                        }
                    }
                }
                #endregion
                model.Agpdc = Agpdc;
                #region agreement document
                string Agdoc = null;
               
                try
                {
                    if (model.agreementDocumentExistList != null)
                    {
                        foreach (var item in model.agreementDocumentExistList)
                        {
                            if (string.IsNullOrWhiteSpace(Agdoc))
                            {
                                Agdoc = "(" + model.Agreement_No + ",'" + item.Doc_name + "','" + item.Doc_Path + "')";
                            }
                            else
                            {
                                Agdoc += ",(" + model.Agreement_No + ",'" + item.Doc_name + "','" + item.Doc_Path + "')";
                            }
                        }
                    }
                    if (model.agreementDocumentList != null)
                    {
                        foreach (var item in model.agreementDocumentList)
                        {
                            Guid guid = Guid.NewGuid();
                            try
                            {
                                if (item.File != null)
                                {
                                    HttpPostedFileBase file = item.File; //Uploaded file
                                    int fileSize = file.ContentLength;
                                    string fileName = file.FileName;
                                    string mimeType = file.ContentType;
                                    //System.IO.Stream fileContent = file.InputStream;
                                    fileName = guid + fileName;
                                    //To save file, use SaveAs method
                                    file.SaveAs(Server.MapPath("~/" + Common.AgreementDocumentContainer) + fileName); //File will be saved in application root
                                    if (string.IsNullOrWhiteSpace(Agdoc))
                                    {
                                        Agdoc = "(" + model.Agreement_No + ",'" + item.Name + "','" + fileName + "')";
                                    }
                                    else
                                    {
                                        Agdoc += ",(" + model.Agreement_No + ",'" + item.Name + "','" + fileName + "')";
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }
                #endregion
                model.Agdoc = Agdoc;
                string Agfac = null;
                #region pAgfac
                if (model.agreementFacilityList != null)
                {
                    foreach (var item in model.agreementFacilityList)
                    {
                        if (string.IsNullOrWhiteSpace(Agfac))
                        {
                            Agfac = "(" + model.Agreement_No + ",'" + item.Facility_id + "','" + item.Facility_Name + "','" + item.Numbers_available + "')";
                        }
                        else
                        {
                            Agfac += ",(" + model.Agreement_No + ",'" + item.Facility_id + "','" + item.Facility_Name + "','" + item.Numbers_available + "')";
                        }
                    }
                }
                #endregion
                model.Agfac = Agfac;
                string Aguti = null;
                #region Aguti
                if (model.AgreementUtilityList != null)
                {
                    foreach (var item in model.AgreementUtilityList)
                    {
                        if (string.IsNullOrWhiteSpace(Aguti))
                        {
                            Aguti = "(" + model.Agreement_No + ",'" + item.Utility_id + "','" + item.Utility_Name + "','" + item.Payable + "','" + item.Amount_Type +
                                    "','" + item.Amount + "')";
                        }
                        else
                        {
                            Aguti += ",(" + model.Agreement_No + ",'" + item.Utility_id + "','" + item.Utility_Name + "','" + item.Payable + "','" + item.Amount_Type +
                                    "','" + item.Amount + "')";
                        }
                    }
                }
                #endregion
                model.Aguti = Aguti;
                string Agchk = null;
                #region Agchk
                if (model.AgreementCheckList != null)
                {
                    foreach (var item in model.AgreementCheckList)
                    {
                        if (string.IsNullOrWhiteSpace(Agchk))
                        {
                            Agchk = "(" + model.Agreement_No + ",'" + item.Checklist_id + "','" + item.Checklist_Name + "','" + item.Status + "','" + item.Remarks + "')";
                        }
                        else
                        {
                            Agchk += ",(" + model.Agreement_No + ",'" + item.Checklist_id + "','" + item.Checklist_Name + "','" + item.Status + "','" + item.Remarks +"')";
                        }
                    }
                }
                #endregion
                model.Agchk = Agchk;
                string Agunit = null;
                if(model.Single_Multiple_Flag== "Multiple" && model.AgreementUnitList!=null)
                {
                    #region agpdc
                    if (model.AgreementUnitList != null)
                    {
                        foreach (var item in model.AgreementUnitList)
                        {
                            if (string.IsNullOrWhiteSpace(Agunit))
                            {
                                Agunit = "(" + model.Agreement_No + ",'" + item.Property_ID + "','" + item.Property_ID_Tawtheeq + "','" + item.Properties_Name + "','" +
                                    item.Unit_ID_Tawtheeq + "','" + item.Unit_Property_Name + "')";
                            }
                            else
                            {
                                Agunit += ",(" + model.Agreement_No + ",'" + item.Property_ID + "','" + item.Property_ID_Tawtheeq + "','" + item.Properties_Name + "','" + item.Unit_ID_Tawtheeq +
                                        "','" + item.Unit_Property_Name + "')";
                            }
                        }
                    }
                    #endregion
                }
               // model.Ag_Tenant_Name = FC["TenantId"].ToString();
                model.Agunit = Agunit;
               // model.Ag_Tenant_Name = "ARUL";
                object[] parameters = Helper.GetTcaMySqlParameters<AgreementFormViewModel>(model, PFlag, System.Web.HttpContext.Current.User.Identity.Name);
                //string paramNames = Helper.GetTcaMySqlParametersNames<AgreementFormViewModel>(model, PFlag, user);
                string paramNames = "@PFlag, @PSingle_Multiple_Flag, @PAgreement_Refno, @PNew_Renewal_flag, @PAgreement_No, @PAgreement_Date, @PAg_Tenant_id, @PAg_Tenant_Name, @Pproperty_id, @PProperty_ID_Tawtheeq, @PProperties_Name, @PUnit_ID_Tawtheeq, @PUnit_Property_Name, @PCaretaker_id, @PCaretaker_Name, @PVacantstartdate, @PAgreement_Start_Date, @PAgreement_End_Date, @PTotal_Rental_amount, @PPerday_Rental, @PAdvance_Security_Amount, @PSecurity_Flag, @PSecurity_chequeno, @PSecurity_chequedate, @PNotice_Period, @Pnofopayments, @PApproval_Flag, @PApproved_By, @PApproved_Date, @PTenant_Type, @PCreateduser, @PAgpdc, @PAgdoc, @PAgfac, @PAguti, @PAgchk, @PAgunit";
                var c = paramNames.Split(',').Count();
                var tenantCompany = await db.Database.SqlQuery<object>("CALL Usp_Agreement_All(" + paramNames +")", parameters).ToListAsync();
               
            }
            catch   (Exception e)
            {

            }
            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> GetTenentDetails(string Type)
        {
            try
            {
                if (Type == "Company")
                {
                    var query = await db.tbl_tenant_company.Where(x => x.Delmark != "*").OrderBy(x => x.Tenant_Id).Select(x => new { Tenant_Id = x.Tenant_Id, Tenant_Name = x.First_Name }).ToListAsync();
                    return Json(query, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var query = await db.tbl_tenant_individual.Where(x => x.Delmark != "*").OrderBy(x => x.Tenant_Id).Select(x => new { Tenant_Id = x.Tenant_Id, Tenant_Name = x.First_Name }).ToListAsync();
                    return Json(query, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPropertiesDetails(string Type)
        {
            try
            {
                if (Type != "Multiple")
                {
                    var propertylist = await db.Database.SqlQuery<PropertyDropdownModel>("select property_id,  Property_ID_Tawtheeq,Property_Name,Unit_ID_Tawtheeq,Unit_Property_Name from tbl_propertiesmaster where ifnull(Noofunits,0)=0 and Property_Flag='property'  and ifnull(status,'')<>'Avail' and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1 union all select property_id, Ref_unit_Property_ID_Tawtheeq,Ref_Unit_Property_Name, Unit_ID_Tawtheeq,Unit_Property_Name from tbl_propertiesmaster where Property_Flag='Unit'   and  Caretaker_id=2 and ifnull(status,'')<>'Avail' and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1").ToListAsync();
                    return Json(propertylist, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var propertylist = await db.Database.SqlQuery<tbl_propertiesmaster>("select * from tbl_propertiesmaster where Noofunits>0 and ifnull(status,'')<>'Avail'and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1").ToListAsync();
                    var details = propertylist.Select(x => new PropertyDropdownModel()
                    {
                        property_id = x.Property_Id,
                        Property_ID_Tawtheeq = x.Property_ID_Tawtheeq,
                        Property_Name = x.Property_Name,
                        Unit_Property_Name = x.Unit_Property_Name,
                        Unit_ID_Tawtheeq = x.Unit_ID_Tawtheeq
                    }).ToList();
                    return Json(details, JsonRequestBehavior.AllowGet);
                }
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
                model.Agreement_No = 0;
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
