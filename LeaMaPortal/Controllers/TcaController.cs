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
using MySql.Data.MySqlClient;
using System.Threading;

namespace LeaMaPortal.Controllers
{
    [Authorize]

    public class TcaController : BaseController
    {
        public static string AgreeType;
        public static string hidAgreeType;
        public int i = 0;
        public static int? agreerefno;
        public static string CheckListType;
        public static string RegNam;

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
            if (Session["Region"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            RegNam = Session["Region"].ToString();
            return View();
        }

        public async Task<PartialViewResult> List(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                var menus = CurrentUser.MenuConfig.Split(',');
                ViewBag.IsRenewalVisible = menus.Contains("20");
                ViewBag.IsClosureVisible = menus.Contains("20");
                ViewBag.IsAgreementStatusVisible = menus.Contains("21");
                //IList<CountryViewModel> list;
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    var list = db.tbl_agreement.Where(x => string.IsNullOrEmpty(x.Delmark) && x.Region_Name == RegNam && (x.Ag_Tenant_Name.Contains (Search ) || x.Properties_Name.Contains (Search ) || x.Unit_Property_Name.Contains (Search )) && x.Status == null).OrderByDescending(x => x.Agreement_No).Select(x => new AgreementFormViewModel()
                    //var list = db.tbl_agreement.Where(x => string.IsNullOrEmpty(x.Delmark) && (x.Ag_Tenant_Name.Equals(Search) || x.Properties_Name.Equals(Search) || x.Unit_Property_Name.Equals(Search)) && x.Status == null).OrderByDescending(x => x.Agreement_No).Select(x => new AgreementFormViewModel()
                    {
                        Agreement_No = x.Agreement_No,
                        Properties_Name = x.Properties_Name,
                        Ag_Tenant_Name = x.Ag_Tenant_Name,
                        Unit_Property_Name = x.Unit_Property_Name,
                        Approval_Flag = x.Approval_Flag.HasValue ? x.Approval_Flag.Value : 0,
                        Single_Multiple_Flag = x.Single_Multiple_Flag,
                        Status = x.Status,
                        property_id = x.property_id,
                        

                    }).ToPagedList(currentPageIndex, PageSize);
                   
                    return PartialView("../Tca/_List", list);
                }
                else
                {
                    var list = db.tbl_agreement.Where(x => string.IsNullOrEmpty(x.Delmark) && x.Region_Name == RegNam && x.Status == null && x.Agreement_No.ToString().ToLower().Contains(Search.ToLower()))
                                   .OrderByDescending(x => x.Agreement_No).Select(x => new AgreementFormViewModel()
                                   {
                                       Agreement_No = x.Agreement_No,
                                       Properties_Name = x.Properties_Name,
                                       Ag_Tenant_Name = x.Ag_Tenant_Name,
                                       Unit_Property_Name = x.Unit_Property_Name,
                                       Approval_Flag = x.Approval_Flag.HasValue ? x.Approval_Flag.Value : 0,
                                       Single_Multiple_Flag = x.Single_Multiple_Flag,
                                       Status = x.Status,
                                       property_id = x.property_id,


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
                
                //TempData.Peek("EntryFlag");

                //if(i == 1)
                //{
                //    hidAgreeType = AgreeType;
                //}
                AgreementFormViewModel model = new AgreementFormViewModel();
                ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType);
                ViewBag.Ag_Tenantid = new SelectList("", "");
                ViewBag.Ag_TenantName = new SelectList("", "");

                var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                ViewBag.TcaPropertyId = new SelectList(property.Where(x => x.Property_Flag == "Property").OrderBy(x => x.Property_Name).ToList(), "Property_Id", "Property_Id");
                ViewBag.TcaPropertyIDTawtheeq = new SelectList(property.Where(x => x.Property_Flag == "Property").OrderBy(x => x.Property_Name).ToList(), "Property_Id", "Property_ID_Tawtheeq");
                ViewBag.TcaPropertyName = new SelectList(property.Where(x => x.Property_Flag == "Property").OrderBy(x => x.Property_Name).ToList(), "Property_Id", "Property_Name");
                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                ViewBag.TcaUnitIDTawtheeq = new SelectList(property.Where(x => x.Property_Flag == "Unit").OrderBy(x => x.Unit_Property_Name).ToList(), "Ref_Unit_Property_ID", "Unit_ID_Tawtheeq");
                ViewBag.TcaUnitPropertyName = new SelectList(property.Where(x => x.Property_Flag == "Unit").OrderBy(x => x.Unit_Property_Name).ToList(), "Unit_ID_Tawtheeq", "Unit_Property_Name");



                ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag);
                var agreement = db.tbl_agreement.Where(x => x.Region_Name == RegNam).OrderByDescending(x => x.Agreement_No).FirstOrDefault();
                ViewBag.Agreement_No = agreement == null ? 1 : agreement.Agreement_No + 1;
                var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).OrderBy(x => x.Caretaker_Name).ToListAsync();
                ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id");
                ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_id", "Caretaker_Name");
                model.New_Renewal_flag = Common.NewAgreement;
                //TempData["EntryFlag"] = model.New_Renewal_flag;
                //TempData.Peek("EntryFlag");
                //if(i != 1)
                //{
                    AgreeType = model.New_Renewal_flag;
                //}                
                CheckListType = model.New_Renewal_flag;
                model.Agreement_No = 0;
                //model.Agreement_Start_Date = DateTime.Now;
                // model.Agreement_End_Date = DateTime.Now;
                //model.Vacantstartdate = DateTime.Now;
                model.Agreement_Date = DateTime.Now;

                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Agreement/_AgreementForm", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(AgreementFormViewModel model, FormCollection FC)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                //string UnitID = FC["hdnUnit_ID_Tawtheeq"].ToString();
                //string UnitName = FC["hdnUnit_Property_Name"].ToString();

                //if (AgreeType == "Update" || AgreeType == "Renewal")
                //{
                //    i = i + 2;
                //}
                //if (i == 2)
                //{
                    
                //}
                i = i + 1;
                string PFlag = Common.UPDATE;
                MessageResult result = new MessageResult();
                result.Message = "TCA updated successfully";
                if (((model.Agreement_No == 0 || model.Agreement_No > 0) && AgreeType == "New" && i < 2) || (model.Agreement_No > 0 && AgreeType == "Renewal")) //&& model.Agreement_Refno > 0 //  && i < 2
                {
                    
                    var Agreement = await db.tbl_agreement.Where(x => x.Region_Name == RegNam).OrderByDescending(r => r.Agreement_No).FirstOrDefaultAsync();
                    if (AgreeType == "Renewal")
                    {
                        PFlag = Common.INSERT;
                        result.Message = "TCA Renewal added successfully";
                        model.Agreement_Refno = model.Agreement_No;
                    }
                    else
                    {
                        PFlag = Common.INSERT;
                        result.Message = "TCA added successfully";
                    }
                    model.Agreement_No = Agreement != null ? Agreement.Agreement_No + 1 : 1;
                   



                    //Agreement = await db.tbl_agreement.OrderByDescending(r => r.id).FirstOrDefaultAsync();
                    //model.Id = Agreement != null ? Agreement.id + 1 : 1;
                }
                if (AgreeType == "New") //&& i >= 2
                {
                    i = 0;
                    //if (i >= 2)
                    //{                        
                    //}
                }
                else if (AgreeType != "Update")
                {
                    i = 0;
                    //if (i < 2)
                    //{

                    //}
                }
                else if (AgreeType == "Update")
                {
                    if (i >= 2)
                    {
                        i = 0;
                    }
                }


                string Agpdc = null;
                #region agpdc
                if (model.AgreementPdcList != null)
                {
                    foreach (var item in model.AgreementPdcList)
                    {

                        var Cheque_Date = item.Cheque_Date != null ? "'" + item.Cheque_Date.Value.Date.ToString("yyyy-MM-dd") + "'" : "null";
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
                                    Helper.CheckDirectory(Common.AgreementDocumentDirectoryName);
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
                            Agchk += ",(" + model.Agreement_No + ",'" + item.Checklist_id + "','" + item.Checklist_Name + "','" + item.Status + "','" + item.Remarks + "')";
                        }
                    }
                }
                #endregion
                model.Agchk = Agchk;
                string Agunit = null;
                if (model.Single_Multiple_Flag == "Multiple" && model.AgreementUnitList != null)
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
                //if(AgreeType == "Update" && model.New_Renewal_flag == "Renewal")
                //{
                //    var Agreement = await db.tbl_agreement.OrderByDescending(r => r.Agreement_No).FirstOrDefaultAsync();
                //    model.Agreement_Refno = Agreement.Agreement_Refno;
                //}
                // model.Ag_Tenant_Name = FC["TenantId"].ToString();
                model.Agunit = Agunit;
                // model.Ag_Tenant_Name = "ARUL";
                object[] parameters = Helper.GetTcaMySqlParameters<AgreementFormViewModel>(model, PFlag, System.Web.HttpContext.Current.User.Identity.Name);
                //string paramNames = Helper.GetTcaMySqlParametersNames<AgreementFormViewModel>(model, PFlag, user);
                string paramNames = "@PFlag, @PSingle_Multiple_Flag, @PAgreement_Refno, @PNew_Renewal_flag, @PAgreement_No, @PAgreement_Date, @PAg_Tenant_id, @PAg_Tenant_Name, @Pproperty_id, @PProperty_ID_Tawtheeq, @PProperties_Name, @PUnit_ID_Tawtheeq, @PUnit_Property_Name, @PCaretaker_id, @PCaretaker_Name, @PVacantstartdate, @PAgreement_Start_Date, @PAgreement_End_Date, @PTotal_Rental_amount, @PPerday_Rental, @PAdvance_Security_Amount, @PSecurity_Flag, @PSecurity_chequeno, @PSecurity_chequedate, @PNotice_Period, @Pnofopayments, @PApproval_Flag, @PApproved_By, @PApproved_Date, @PTenant_Type, @PCreateduser, @PAgpdc, @PAgdoc, @PAgfac, @PAguti, @PAgchk, @PAgunit";
                //string s =  Request.Form["UnitIDTawtheeq"];
                //string y = Request.Form["UnitPropertyName"];

                var tenantCompany = await db.Database.SqlQuery<object>("CALL Usp_Agreement_All(" + paramNames + ")", parameters).ToListAsync();
                //return RedirectToAction("Index",);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
                {
                throw e;
            }
        }

        public async Task<PartialViewResult> Edit(int AgreementNo)
        {
            try
            {
                //i = 0;
                AgreementFormViewModel model = new AgreementFormViewModel();
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam && x.Delmark != "*");
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

                ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType, agreementDet.Tenant_Type);
                if (agreementDet.Tenant_Type == "Company")
                {
                    var query = await db.tbl_tenant_company.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).OrderBy(x => x.Tenant_Id).Select(x => new { Tenant_Id = x.Tenant_Id, Tenant_Name = x.CompanyName }).ToListAsync();
                    //var query = await db.tbl_tenant_company.Where(x => x.Delmark != "*").OrderBy(x => x.).Select(x => new { Tenant_Id = x.Tenant_Id, Tenant_Name = x.CompanyName }).ToListAsync();
                    ViewBag.Ag_Tenantid = new SelectList(query, "Tenant_Id", "Tenant_Id", agreementDet.Ag_Tenant_id);
                    ViewBag.Ag_TenantName = new SelectList(query, "Tenant_Id", "Tenant_Name", agreementDet.Ag_Tenant_id);
                }
                else
                {
                    var query = await db.tbl_tenant_individual.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).OrderBy(x => x.Tenant_Id).Select(x => new { Tenant_Id = x.Tenant_Id, Tenant_Name = string.Concat(x.First_Name, " ", x.Middle_Name, " ", x.Last_Name) }).ToListAsync();
                    ViewBag.Ag_Tenantid = new SelectList(query, "Tenant_Id", "Tenant_Id", agreementDet.Ag_Tenant_id);
                    ViewBag.Ag_TenantName = new SelectList(query, "Tenant_Id", "Tenant_Name", agreementDet.Ag_Tenant_id);
                }
                model.Tenant_Type = agreementDet.Tenant_Type;
                model.Ag_Tenant_id = agreementDet.Ag_Tenant_id;
                model.Ag_Tenant_Name = agreementDet.Ag_Tenant_Name;
                model.property_id = agreementDet.property_id;

                //var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*").ToListAsync();


                var agree = await db.tbl_agreement.Where(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                //ViewBag.property_id = new SelectList(agree.Where(m => m.property_id > 0).Select(i => i.property_id).Distinct().ToList(), "property_id", "property_id", agreementDet.property_id);
                ViewBag.TcaEditPropertyIDTawtheeq = new SelectList(agree.Where(m => m.Property_ID_Tawtheeq != null).OrderBy(m => m.Properties_Name).Select(i => i.Property_ID_Tawtheeq).ToList()); //, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq
                ViewBag.TcaEditPropertyName = new SelectList(agree.Where(m => m.Properties_Name != null).OrderBy(m => m.Properties_Name).Select(i => i.Properties_Name).ToList()); //, "Properties_Name", "Properties_Name",agreementDet.Properties_Name
                //ViewBag.TcaEditPropertyName = new SelectList("select Properties_Name from tbl_agreement where agreement_no =" + AgreementNo + "" , "Properties_Name", "Properties_Name", agreementDet.Properties_Name);
                ViewBag.TcaEditUnitIDTawtheeq = new SelectList(agree.Where(m => m.Unit_ID_Tawtheeq != null).OrderBy(m => m.Unit_Property_Name).Select(i => i.Unit_ID_Tawtheeq).ToList());
                //ViewBag.TcaEditUnitIDTawtheeq = new SelectList(agree.Where(x => x.Unit_ID_Tawtheeq == "Unit").ToList(), "Ref_Unit_Property_ID", "Unit_ID_Tawtheeq");
                ViewBag.TcaEditUnitPropertyName = new SelectList(agree.Where(m => m.Unit_Property_Name != null).OrderBy(m => m.Unit_Property_Name).Select(i => i.Unit_Property_Name).ToList());


                model.Property_ID_Tawtheeq = agreementDet.Property_ID_Tawtheeq;
                //var PropertyIDTawtheeq = await db.tbl_agreement.Where(m => m.Property_ID_Tawtheeq != null).Select(i => i.Property_ID_Tawtheeq).Distinct().ToListAsync();
                //ViewBag.TcaEditPropertyIDTawtheeq = new SelectList(PropertyIDTawtheeq,"Property_ID_Taw", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq);

                

                model.Properties_Name = agreementDet.Properties_Name;
                model.Unit_ID_Tawtheeq = agreementDet.Unit_ID_Tawtheeq;
                model.Unit_Property_Name = agreementDet.Unit_Property_Name;


                // ViewBag.TcaPropertyId = new SelectList(property.Where(x => x.Property_Flag == "Property").ToList(), "Property_Id", "Property_Id", agreementDet.property_id);
                //ViewBag.TcaEditPropertyIDTawtheeq = new SelectList(property.Where(x => x.Property_Flag == "Property").ToList(), "Property_Id", "Property_ID_Tawtheeq", agreementDet.property_id);
                //ViewBag.TcaEditPropertyName = new SelectList(property.Where(x => x.Property_Flag == "Property").ToList(), "Property_Id", "Property_Name", agreementDet.property_id);

                //var agree = await db.tbl_agreement.ToListAsync();
                //ViewBag.TcaPropertyId = new SelectList(agree.Where(x => x.property_id > 0).ToList().Distinct(), "property_id", "property_id", agreementDet.property_id);
                //var agree = db.tbl_agreement.Where(m => m.Property_ID_Tawtheeq != null).Select(i => i.Property_ID_Tawtheeq).Distinct().ToList();
                //ViewBag.TcaEditPropertyIDTawtheeq = new SelectList(agree);


                //var unit = property.Where(x => x.Ref_Unit_Property_ID == agreementDet.property_id && x.Property_Flag == "Unit").ToList();
                //ViewBag.TcaEditUnitIDTawtheeq = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", agreementDet.Unit_ID_Tawtheeq);
                //ViewBag.TcaEditUnitPropertyName = new SelectList(unit, "Unit_Property_Name", "Unit_Property_Name", agreementDet.Unit_Property_Name);





                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();

                ViewBag.TcaEditSecurityFlag = new SelectList(Common.SecurityFlag, agreementDet.Security_Flag);
                ViewBag.Agreement_No = AgreementNo; //db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                //var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*").ToListAsync();
                //model.Caretaker_id = agreementDet.Caretaker_id;
                //model.Caretaker_Name = agreementDet.Caretaker_Name;
                //ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id", agreementDet.Caretaker_id);
                //ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_id", "Caretaker_Name", agreementDet.Caretaker_id);
                model.New_Renewal_flag = agreementDet.New_Renewal_flag;
                CheckListType = agreementDet.New_Renewal_flag;
                AgreeType = "Update";
                model.Agreement_No = AgreementNo;   
                if(agreementDet.Agreement_Refno == null)
                {
                    model.Agreement_Refno = 0;
                    agreerefno = 0;
                }         
                else
                {
                    model.Agreement_Refno = Convert.ToInt32(agreementDet.Agreement_Refno);
                    agreerefno = Convert.ToInt32(agreementDet.Agreement_Refno);
                }   
                
                //model.Agreement_Refno = AgreementNo;

                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Agreement/_AgreementFormEdit", model);
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
                //TempData.Peek("EntryFlag");
                //i = 0;
                AgreementFormViewModel model = new AgreementFormViewModel();
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam && x.Delmark != "*");
                AgreementRenwalMap(agreementDet, model);
                if (model.Agreement_Refno == 0)
                {
                    var Agreement = await db.tbl_agreement.Where(x => x.Region_Name == RegNam).OrderByDescending(r => r.Agreement_No).FirstOrDefaultAsync();
                    model.Agreement_Refno = Convert.ToInt32(Agreement.Agreement_No + 1);
                }

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

                ViewBag.TenantType = new SelectList(Common.TcaTenantType, agreementDet.Tenant_Type);
                model.Tenant_Type = agreementDet.Tenant_Type;
                model.Ag_Tenant_id = agreementDet.Ag_Tenant_id;
                model.Ag_Tenant_Name = agreementDet.Ag_Tenant_Name;
                ViewBag.Ag_Tenantid = new SelectList("", "");
                ViewBag.Ag_TenantName = new SelectList("", "");
                var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                model.property_id = agreementDet.property_id;
                model.Property_ID_Tawtheeq = agreementDet.Property_ID_Tawtheeq;
                model.Properties_Name = agreementDet.Properties_Name;
                ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id", agreementDet.property_id);

                //ViewBag.TcaRenewalPropertyIDTawtheeq = new SelectList(property, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq);
                //ViewBag.TcaPropertyName = new SelectList(property, "Property_Name", "Property_Name", agreementDet.Properties_Name);
                ////var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                //var unit = property.Where(x => x.Ref_Unit_Property_ID == agreementDet.property_id).ToList();
                //ViewBag.UnitIDTawtheeq = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", agreementDet.Unit_ID_Tawtheeq);
                //ViewBag.UnitPropertyName = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_Property_Name", agreementDet.Unit_Property_Name);


                var agree = await db.tbl_agreement.Where(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                ViewBag.TcaRenewalPropertyIDTawtheeq = new SelectList(agree.Where(m => m.Property_ID_Tawtheeq != null).Select(i => i.Property_ID_Tawtheeq).ToList()); //, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq
                ViewBag.TcaRenewalPropertyName = new SelectList(agree.Where(m => m.Properties_Name != null).OrderBy(m => m.Properties_Name).Select(i => i.Properties_Name).ToList()); //, "Properties_Name", "Properties_Name",agreementDet.Properties_Name                
                ViewBag.TcaRenewalUnitIDTawtheeq = new SelectList(agree.Where(m => m.Unit_ID_Tawtheeq != null).Select(i => i.Unit_ID_Tawtheeq).ToList());
                ViewBag.TcaRenewalUnitPropertyName = new SelectList(agree.Where(m => m.Unit_Property_Name != null).OrderBy(m => m.Unit_Property_Name).Select(i => i.Unit_Property_Name).ToList());

                //ViewBag.TcaRenewalSecurityFlag = new SelectList(Common.SecurityFlag, agreementDet.Security_Flag);
                ViewBag.Agreement_No = AgreementNo; //db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).OrderBy(x => x.Caretaker_Name).ToListAsync();
                model.Caretaker_id = agreementDet.Caretaker_id;
                model.Caretaker_Name = agreementDet.Caretaker_Name;
                ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id", agreementDet.Caretaker_id);
                ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_Name", "Caretaker_Name", agreementDet.Caretaker_Name);
                model.New_Renewal_flag = Common.Renewal;
                model.Agreement_No = AgreementNo;
               

                //************ RAJESH ********************
                model.Agreement_Date = DateTime.Now;
                model.Vacantstartdate = agreementDet.Agreement_End_Date;
                model.Agreement_Start_Date = DateTime.Now;
                model.Agreement_End_Date = DateTime.Now;
                model.Total_Rental_amount = 0;
                model.Perday_Rental = 0;
                model.nofopayments = 0;
                ViewBag.TcaRenewalSecurityFlag = new SelectList(Common.SecurityFlag, agreementDet.Security_Flag);
                model.Security_chequeno = agreementDet.Security_chequeno;
                model.Security_chequedate = agreementDet.Security_chequedate;
                model.Notice_Period = 0;
                model.Approval_Flag = 0;
                //ViewBag.EntryFlag = model.New_Renewal_flag;
                //ViewData["EntryFlag"] = model.New_Renewal_flag;
                //TempData["EntryFlag"] = model.New_Renewal_flag;
                //TempData.Peek("EntryFlag");
                AgreeType = model.New_Renewal_flag;
                CheckListType = model.New_Renewal_flag;
                agreerefno = 0;



                //.Agreement_Refno = AgreementNo;

                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Agreement/_AgreementFormRenewal", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<PartialViewResult> Approval(int AgreementNo)
        {
            try
            {
                RegNam = Session["Region"].ToString();
                //TempData.Peek("EntryFlag");
                AgreementFormViewModel model = new AgreementFormViewModel();
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam);
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

                ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType, agreementDet.Tenant_Type);
                model.Tenant_Type = agreementDet.Tenant_Type;
                model.Ag_Tenant_id = agreementDet.Ag_Tenant_id;
                model.Ag_Tenant_Name = agreementDet.Ag_Tenant_Name;
                ViewBag.Ag_Tenantid = new SelectList("", "");
                ViewBag.Ag_TenantName = new SelectList("", "");
                var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                model.property_id = agreementDet.property_id;
                model.Property_ID_Tawtheeq = agreementDet.Property_ID_Tawtheeq;
                model.Properties_Name = agreementDet.Properties_Name;

                //ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id", agreementDet.property_id);
                //ViewBag.TcaPropertyIDTawtheeq = new SelectList(property, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq);
                //ViewBag.TcaPropertyName = new SelectList(property, "Property_Name", "Property_Name", agreementDet.Properties_Name);
                //var unit = property.Where(x => x.Ref_Unit_Property_ID == agreementDet.property_id).ToList();
                //ViewBag.UnitIDTawtheeq = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", agreementDet.Unit_ID_Tawtheeq);
                //ViewBag.UnitPropertyName = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_Property_Name", agreementDet.Unit_Property_Name);
                //model.Unit_ID_Tawtheeq = agreementDet.Unit_ID_Tawtheeq;
                //model.Unit_Property_Name = agreementDet.Unit_Property_Name;


                var agree = await db.tbl_agreement.Where(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                ViewBag.TcaPropertyIDTawtheeq = new SelectList(agree.Where(m => m.Property_ID_Tawtheeq != null).Select(i => i.Property_ID_Tawtheeq).ToList()); //, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq
                ViewBag.TcaPropertyName = new SelectList(agree.Where(m => m.Properties_Name != null).Select(i => i.Properties_Name).ToList()); //, "Properties_Name", "Properties_Name",agreementDet.Properties_Name                
                ViewBag.UnitIDTawtheeq = new SelectList(agree.Where(m => m.Unit_ID_Tawtheeq != null).Select(i => i.Unit_ID_Tawtheeq).ToList());
                ViewBag.UnitPropertyName = new SelectList(agree.Where(m => m.Unit_Property_Name != null).Select(i => i.Unit_Property_Name).ToList());

                ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag, agreementDet.Security_Flag);
                ViewBag.Agreement_No = AgreementNo; //db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                model.Caretaker_id = agreementDet.Caretaker_id;
                model.Caretaker_Name = agreementDet.Caretaker_Name;
                ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id", agreementDet.Caretaker_id);
                ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_Name", "Caretaker_Name", agreementDet.Caretaker_Name);
                model.New_Renewal_flag = agreementDet.New_Renewal_flag; //Common.Renewal;
                model.Agreement_No = AgreementNo;
                string arn = agreementDet.Agreement_Refno.ToString();
                if(arn == "")
                {
                    model.Agreement_Refno = 0;//AgreementNo;
                }
                else
                {
                    model.Agreement_Refno = Convert.ToInt32(arn);
                }                
                agreerefno = agreementDet.Agreement_Refno;

                //AgreeType = agreementDet.New_Renewal_flag;
                AgreeType = "Approval";
                CheckListType = agreementDet.New_Renewal_flag;

                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Agreement/_TcaAproval", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<PartialViewResult> Status(int AgreementNo)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                TcaStatusDisplayModel model = new TcaStatusDisplayModel();
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam);

                ViewBag.Renewal_Close_Flag = new SelectList(Common.Renewal_Close_Flag);
                model.Tenant_Type = agreementDet.Tenant_Type;
                model.Ag_Tenant_id = agreementDet.Ag_Tenant_id;
                model.Ag_Tenant_Name = agreementDet.Ag_Tenant_Name;

                model.Properties_ID = agreementDet.property_id;
                //model.Property_ID_Tawtheeq = agreementDet.Property_ID_Tawtheeq;
                model.Properties_Name = agreementDet.Properties_Name;

                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                model.Unit_ID_Tawtheeq = agreementDet.Unit_ID_Tawtheeq;
                model.Unit_Property_Name = agreementDet.Unit_Property_Name;

                model.Caretaker_id = agreementDet.Caretaker_id;
                model.Caretaker_Name = agreementDet.Caretaker_Name;
                model.AgreementDate = agreementDet.Agreement_Date.HasValue ? agreementDet.Agreement_Date.Value.ToString("dd-MM-yyyy") : "";

                model.Agreement_No = AgreementNo;


                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Status/_StatusDetails", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // Old Function
        //public async Task<PartialViewResult> Print(int AgreementNo, string OtherTerms)
        //{
        //    try
        //    {
        //        //TempData.Peek("EntryFlag");
        //        Thread.Sleep(1000);
        //        TcaPrintModel model = new TcaPrintModel();
        //        var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam);
        //        var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Property_Id == agreementDet.property_id && x.Region_Name == RegNam);
        //        var PayMode = await db.Database.SqlQuery<TcaPrintModel>("SELECT  GROUP_CONCAT(concat(case when payment_mode = 'Cash' then 'Cash' when payment_mode = 'Online' then 'Online' when payment_mode = 'DD' then 'DD' when payment_mode = 'PDC' then 'DD' when payment_mode = 'Cheque' then 'Chq' else '' end, case when  ifnull(cheque_no, '') = ''  then ' ' else  ' #' end, case when  ifnull(cheque_no, '') = '' then ' ' else  cheque_no end, 'AED ', Cheque_amount, '/-', case when  ifnull(Cheque_date, '') = '' then ' ' else  ' dtd ' end, case when  ifnull(Cheque_date, '') = '' then ' ' else  Cheque_date end) SEPARATOR ' and ') as PaymentMode FROM tbl_agreement_pdc where agreement_no = {0}", AgreementNo).FirstOrDefaultAsync(); //and region_name = { 1 } // , Regnam                 
        //        model.PaymentMode = PayMode.PaymentMode;
        //        if (property != null)
        //            model.Property_Usage = property.Property_Usage;
        //        ViewBag.Renewal_Close_Flag = new SelectList(Common.Renewal_Close_Flag);
        //        model.Tenant_Type = agreementDet.Tenant_Type;
        //        model.Ag_Tenant_Name = agreementDet.Ag_Tenant_Name;
        //        if (agreementDet.Tenant_Type == "Individual")
        //        {
        //            var tenant = await db.tbl_tenant_individual.FirstOrDefaultAsync(x => x.Tenant_Id == agreementDet.Ag_Tenant_id && x.Region_Name == RegNam);
        //            if (tenant != null)
        //            {
        //                model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(tenant.Fax_No) ? "" : tenant.Fax_Countrycode + "-" + tenant.Fax_Areacode + "-" + tenant.Fax_No;
        //                model.Ag_Tenant_Address = tenant.address + ", " + tenant.address1 + ", " + tenant.City;
        //                model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(tenant.Landline_No) ? "" : tenant.Landline_Countrycode + "-" + tenant.Landline_Areacode + "-" + tenant.Landline_No;
        //                model.Ag_Tenant_Name = tenant.Title + " " + model.Ag_Tenant_Name;
        //            }

        //        }
        //        else
        //        {
        //            var tenant = await db.tbl_tenant_company.FirstOrDefaultAsync(x => x.Tenant_Id == agreementDet.Ag_Tenant_id && x.Region_Name == RegNam);
        //            if (tenant != null)
        //            {
        //                model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(tenant.Fax_No) ? "" : tenant.Fax_Countrycode + "-" + tenant.Fax_Areacode + "-" + tenant.Fax_No;
        //                model.Ag_Tenant_Address = tenant.address + ", " + tenant.address1 + ", " + tenant.City;
        //                model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(tenant.Landline_No) ? "" : tenant.Landline_Countrycode + "-" + tenant.Landline_Areacode + "-" + tenant.Landline_No;
        //                model.Ag_Tenant_Name = model.Ag_Tenant_Name;  //tenant.Title + " " + 
        //            }
        //        }
        //        var bankDet = Common.BankDetails.First();
        //        model.BankName = bankDet.BankName;
        //        model.AccountNo = bankDet.AccountNumber;
        //        model.CompanyFax = Common.CompanyFax;
        //        model.Ag_Tenant_id = agreementDet.Ag_Tenant_id;


        //        model.Properties_ID = agreementDet.property_id;
        //        //model.Property_ID_Tawtheeq = agreementDet.Property_ID_Tawtheeq;
        //        model.Properties_Name = agreementDet.Properties_Name;

        //        //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
        //        model.Unit_ID_Tawtheeq = agreementDet.Unit_ID_Tawtheeq;
        //        model.Unit_Property_Name = agreementDet.Unit_Property_Name;

        //        model.Caretaker_id = agreementDet.Caretaker_id;
        //        model.Caretaker_Name = agreementDet.Caretaker_Name;
        //        model.AgreementDate = agreementDet.Agreement_Date.HasValue ? agreementDet.Agreement_Date.Value.ToString(Common.DisplayDateFormat) : "";

        //        model.Agreement_No = AgreementNo;
        //        model.Agreement_Start_Date = agreementDet.Agreement_Start_Date.HasValue ? agreementDet.Agreement_Start_Date.Value.ToString(Common.DisplayDateFormat) : "";
        //        model.Agreement_End_Date = agreementDet.Agreement_End_Date.HasValue ? agreementDet.Agreement_End_Date.Value.ToString(Common.DisplayDateFormat) : "";
        //        if (agreementDet.Agreement_Start_Date.HasValue && agreementDet.Agreement_End_Date.HasValue)
        //        {
        //            DateTime startDate = agreementDet.Agreement_Start_Date.Value;
        //            DateTime endDate = agreementDet.Agreement_End_Date.Value;
        //            var totalDays = (endDate - startDate).TotalDays;
        //            var totalYears = Math.Truncate(totalDays / 365);
        //            var totalMonths = Math.Truncate((totalDays % 365) / 30);
        //            // var remainingDays = Math.Truncate((totalDays % 365) % 30);
        //            // Console.WriteLine("Estimated duration is {0} year(s), {1} month(s) and {2} day(s)", totalYears, totalMonths, remainingDays);
        //            model.ContractYearsAndMonths = totalYears + "year(s)" + "-" + totalMonths + "month(s)";
        //        }
        //        model.SecurityDeposit = agreementDet.Advance_Security_Amount;
        //        model.SecurityDeposit = 0;
        //        if (model.SecurityDeposit == 0)
        //        {
        //            ViewBag.sd = "Nil";
        //        }
        //        else
        //        {
        //            ViewBag.sd = agreementDet.Advance_Security_Amount;
        //        }

        //        model.IssueDate = System.DateTime.Now.ToString(Common.DisplayDateFormat);
        //        model.Total_Rental_amount = agreementDet.Total_Rental_amount;
        //        model.TotalAmountInWords = agreementDet.Total_Rental_amount.HasValue ? Common.NumberToWords(Convert.ToInt64(agreementDet.Total_Rental_amount.Value)) : "";
        //        model.OtherTerms = OtherTerms;
        //        //model.AgreementPd = new AgreementPdcViewModel();
        //        return PartialView("../Tca/Contract/_ContractDetails", model);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

         
        // New Function       
        public async Task<PartialViewResult> Print(int AgreementNo, string OtherTerms)
        {
            try
            {
                string paymentmod;
                //TempData.Peek("EntryFlag");
                Thread.Sleep(1000);
                TcaPrintModel model = new TcaPrintModel();
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam);
                var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Property_Id == agreementDet.property_id && x.Region_Name == RegNam);
                //var PayMode = await db.Database.SqlQuery<TcaPrintModel>("SELECT  GROUP_CONCAT(concat(case when payment_mode = 'Cash' then 'Cash' when payment_mode = 'Online' then 'Online' when payment_mode = 'DD' then 'DD' when payment_mode = 'PDC' then 'DD' when payment_mode = 'Cheque' then 'Chq' else '' end, case when  ifnull(cheque_no, '') = ''  then ' ' else  ' #' end, case when  ifnull(cheque_no, '') = '' then ' ' else  cheque_no end, 'AED ', Cheque_amount, '/-', case when  ifnull(Cheque_date, '') = '' then ' ' else  ' dtd ' end, case when  ifnull(Cheque_date, '') = '' then ' ' else  Cheque_date end) SEPARATOR ' and ') as PaymentMode FROM tbl_agreement_pdc where agreement_no = {0} and Region_Name == '"+ RegNam + "'", AgreementNo).FirstOrDefaultAsync(); //and region_name = { 1 } // , Regnam                 
                var PayMode = await db.Database.SqlQuery<TcaPrintModel>("SELECT (select count(*) from tbl_agreement_pdc where agreement_no =" + AgreementNo + " and region_name='" + RegNam  + "') as Paymentcount, GROUP_CONCAT(concat(case when payment_mode = 'Cash' then 'Cash' when payment_mode = 'Online' then 'Online' when payment_mode = 'DD' then 'DD' when payment_mode = 'PDC' then 'PDC' when payment_mode = 'Cheque' then 'Chq' else '' end, case when  ifnull(cheque_no, '') = ''  then ' ' else  ' #' end, case when  ifnull(cheque_no, '') = '' then ' ' else  cheque_no end, 'AED ', Cheque_amount, '/-', case when  ifnull(Cheque_date, '') = '' then ' ' else  ' dtd ' end, case when  ifnull(Cheque_date, '') = '' then ' ' else  Cheque_date end) SEPARATOR ' and ') as PaymentMode FROM tbl_agreement_pdc where agreement_no = {0} and Region_Name ={1}", AgreementNo, RegNam).FirstOrDefaultAsync(); //and region_name = { 1 } // , Regnam                 
                paymentmod = PayMode.Paymentcount + " Payments: ";
                paymentmod += PayMode.PaymentMode;
                model.PaymentMode = paymentmod;
                if (property != null)
                    model.Property_Usage = property.Property_Usage;
                ViewBag.Renewal_Close_Flag = new SelectList(Common.Renewal_Close_Flag);
                model.Tenant_Type = agreementDet.Tenant_Type;
                model.Ag_Tenant_Name = agreementDet.Ag_Tenant_Name;
                if (agreementDet.Tenant_Type == "Individual")
                {
                    var tenant = await db.tbl_tenant_individual.FirstOrDefaultAsync(x => x.Tenant_Id == agreementDet.Ag_Tenant_id && x.Region_Name == RegNam);
                    if (tenant != null)
                    {
                        model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(tenant.Fax_No) ? "" : tenant.Fax_Countrycode + "-" + tenant.Fax_Areacode + "-" + tenant.Fax_No;
                        model.Ag_Tenant_Address = tenant.address + ", " + tenant.address1 + ", " + tenant.City;
                        model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(tenant.Landline_No) ? "" : tenant.Landline_Countrycode + "-" + tenant.Landline_Areacode + "-" + tenant.Landline_No;
                        model.Ag_Tenant_Name = tenant.Title + " " + model.Ag_Tenant_Name;
                    }

                }
                else
                {
                    var tenant = await db.tbl_tenant_company.FirstOrDefaultAsync(x => x.Tenant_Id == agreementDet.Ag_Tenant_id && x.Region_Name == RegNam);
                    if (tenant != null)
                    {
                        model.Ag_Tenant_Faxno = string.IsNullOrWhiteSpace(tenant.Fax_No) ? "" : tenant.Fax_Countrycode + "-" + tenant.Fax_Areacode + "-" + tenant.Fax_No;
                        model.Ag_Tenant_Address = tenant.address + ", " + tenant.address1 + ", " + tenant.City;
                        model.Ag_Tenant_Telephone = string.IsNullOrWhiteSpace(tenant.Landline_No) ? "" : tenant.Landline_Countrycode + "-" + tenant.Landline_Areacode + "-" + tenant.Landline_No;
                        model.Ag_Tenant_Name = model.Ag_Tenant_Name;  //tenant.Title + " " + 
                    }
                }
                var bankDet = Common.BankDetails.First();
                model.BankName = bankDet.BankName;
                model.AccountNo = bankDet.AccountNumber;
                model.CompanyFax = Common.CompanyFax;
                model.Ag_Tenant_id = agreementDet.Ag_Tenant_id;


                model.Properties_ID = agreementDet.property_id;
                //model.Property_ID_Tawtheeq = agreementDet.Property_ID_Tawtheeq;
                model.Properties_Name = agreementDet.Properties_Name;

                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                model.Unit_ID_Tawtheeq = agreementDet.Unit_ID_Tawtheeq;
                model.Unit_Property_Name = agreementDet.Unit_Property_Name;

                model.Caretaker_id = agreementDet.Caretaker_id;
                model.Caretaker_Name = agreementDet.Caretaker_Name;
                model.AgreementDate = agreementDet.Agreement_Date.HasValue ? agreementDet.Agreement_Date.Value.ToString(Common.DisplayDateFormat) : "";

                model.Agreement_No = AgreementNo;
                model.Agreement_Start_Date = agreementDet.Agreement_Start_Date.HasValue ? agreementDet.Agreement_Start_Date.Value.ToString(Common.DisplayDateFormat) : "";
                model.Agreement_End_Date = agreementDet.Agreement_End_Date.HasValue ? agreementDet.Agreement_End_Date.Value.ToString(Common.DisplayDateFormat) : "";
                if (agreementDet.Agreement_Start_Date.HasValue && agreementDet.Agreement_End_Date.HasValue)
                {
                    DateTime startDate = agreementDet.Agreement_Start_Date.Value;
                    DateTime endDate = agreementDet.Agreement_End_Date.Value;
                    var totalDays = (endDate - startDate).TotalDays;
                    var totalYears = Math.Truncate(totalDays / 365);
                    var totalMonths = Math.Truncate((totalDays % 365) / 30);
                    // var remainingDays = Math.Truncate((totalDays % 365) % 30);
                    // Console.WriteLine("Estimated duration is {0} year(s), {1} month(s) and {2} day(s)", totalYears, totalMonths, remainingDays);
                    model.ContractYearsAndMonths = totalYears + "year(s)" + "-" + totalMonths + "month(s)";
                }
                model.SecurityDeposit = agreementDet.Advance_Security_Amount;
                model.SecurityDeposit = 0;
                if (model.SecurityDeposit == 0)
                {
                    ViewBag.sd = "Nil";
                }
                else
                {
                    ViewBag.sd = agreementDet.Advance_Security_Amount;
                }

                model.IssueDate = System.DateTime.Now.ToString(Common.DisplayDateFormat);
                model.Total_Rental_amount = agreementDet.Total_Rental_amount;
                model.TotalAmountInWords = agreementDet.Total_Rental_amount.HasValue ? Common.NumberToWords(Convert.ToInt64(agreementDet.Total_Rental_amount.Value)) : "";
                model.OtherTerms = OtherTerms;
                model.Region_Name = Session["Region"].ToString();
                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Contract/_ContractDetails", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Status(TcaStatusViewModel model)
        {
            // TempData.Peek("EntryFlag");
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrWhiteSpace(model.Renewal_Close_Flag))
                    {
                        result.Errors = "Renewal Status mandatory";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = Common.INSERT;
                    var tbl_agreement_status = await db.tbl_agreement_status.FirstOrDefaultAsync(m => m.Agreement_No == model.Agreement_No && m.Region_Name == RegNam);
                    if (tbl_agreement_status != null)
                    {
                        PFlag = Common.UPDATE;
                        model.Id = tbl_agreement_status.id;
                    }
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PAgreement_No",model.Agreement_No),
                                            new MySqlParameter("@PAg_Tenant_id",model.Ag_Tenant_id),
                                             new MySqlParameter("@PAg_Tenant_Name",model.Ag_Tenant_Name),
                                              new MySqlParameter("@PProperties_ID",model.Properties_ID),
                                               new MySqlParameter("@PProperties_Name",model.Properties_Name),
                                               new MySqlParameter("@PCaretaker_id",model.Caretaker_id),
                                               new MySqlParameter("@PCaretaker_Name",model.Caretaker_Name),
                                               new MySqlParameter("@PRenewal_Close_Flag",model.Renewal_Close_Flag),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Agreement_Status_All(@PFlag, @PId, @PAgreement_No, @PAg_Tenant_id, @PAg_Tenant_Name, @PProperties_ID, @PProperties_Name, @PCaretaker_id, @PCaretaker_Name, @PRenewal_Close_Flag, @PCreateduser)", param).ToListAsync();
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                result.Errors = "Bad request";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }


        }

        #region closure
        public async Task<PartialViewResult> Closure(int AgreementNo)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                AgreementClosureViewModel model = new AgreementClosureViewModel();
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam);
                AgreementClosureMap(agreementDet, model);

                ViewBag.Tenant_Type = new SelectList(Common.TcaTenantType);
                ViewBag.Ag_Tenantid = new SelectList("", "");
                ViewBag.Ag_TenantName = new SelectList("", "");
                model.Tenant_Type = agreementDet.Tenant_Type;
                model.Ag_Tenant_id = agreementDet.Ag_Tenant_id;
                model.Ag_Tenant_Name = agreementDet.Ag_Tenant_Name;
                var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                ViewBag.TcaPropertyId = new SelectList(property, "Property_Id", "Property_Id", agreementDet.property_id);
                ViewBag.TcaPropertyIDTawtheeq = new SelectList(property, "Property_Id", "Property_ID_Tawtheeq", agreementDet.Property_ID_Tawtheeq);
                ViewBag.TcaPropertyName = new SelectList(property, "Property_Id", "Property_Name", agreementDet.Properties_Name);
                model.property_id = agreementDet.property_id;
                model.Property_ID_Tawtheeq = agreementDet.Property_ID_Tawtheeq;
                model.Properties_Name = agreementDet.Properties_Name;
                //var unit = property.Where(x => x.Property_Flag == "Unit").ToList();
                ViewBag.UnitIDTawtheeq = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", agreementDet.Unit_ID_Tawtheeq);
                ViewBag.UnitPropertyName = new SelectList(property, "Unit_ID_Tawtheeq", "Unit_Property_Name", agreementDet.Unit_Property_Name);
                model.Unit_ID_Tawtheeq = agreementDet.Unit_ID_Tawtheeq;
                model.Unit_Property_Name = agreementDet.Unit_Property_Name;
                ViewBag.SecurityFlag = new SelectList(Common.SecurityFlag, agreementDet.Security_Flag);
                //ViewBag.Agreement_No = AgreementNo; //db.tbl_agreement.OrderByDescending(x => x.Agreement_No).FirstOrDefault()?.Agreement_No + 1;
                model.Agreement_No = AgreementNo;
                model.Advance_Security_Amount_Paid = agreementDet.Advance_Security_Amount;

                var caretaker = await db.tbl_caretaker.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                ViewBag.Caretakerid = new SelectList(caretaker, "Caretaker_id", "Caretaker_id", agreementDet.Caretaker_id);

                ViewBag.CaretakerName = new SelectList(caretaker, "Caretaker_Name", "Caretaker_Name", agreementDet.Caretaker_Name);
                var amountCalc = await db.Database.SqlQuery<ClosureAmountViewModel>("select agreement_no,totalamount,paidamount,advancepending from view_agreement_close_pending where Region_Name = '" + RegNam + "' and agreement_no=" + model.Agreement_No + " ").ToListAsync();
                var amount = amountCalc.FirstOrDefault();

                var totcontractamt = db.Database.SqlQuery<ClosureAmountViewModel>("select total_rental_amount as totalamount from tbl_agreement where Region_Name = '" + RegNam + "' and agreement_no = {0}", AgreementNo).FirstOrDefault();
                model.Total_Contract_Amount = totcontractamt.totalamount;
                float totcontractamount = totcontractamt.totalamount;
                float totamountpaid = 0.00F;
                model.Total_Amount_Paid = 0;

                var totamtpaid = db.Database.SqlQuery<ClosureAmountViewModel>("select totalamount as paidamount from tbl_receipthd where reccategory='advance' and region_name = '" + RegNam + "' and agreement_no = {0}", AgreementNo).FirstOrDefault();
                if(totamtpaid != null)
                {
                    model.Total_Amount_Paid = totamtpaid.paidamount;
                    totamountpaid = totamtpaid.paidamount;
                }
                model.Advance_pending = totcontractamount - totamountpaid;

                var SecDepPaid = db.Database.SqlQuery<ClosureAmountViewModel>("select totalamount  as Securityamount from tbl_receipthd where reccategory='security deposit' and region_name = '" + RegNam + "' and agreement_no = {0}", AgreementNo).FirstOrDefault();
                
                if (SecDepPaid != null)
                {
                    model.Advance_Security_Amount_Paid = SecDepPaid.Securityamount;
                }
                else
                {
                    model.Advance_Security_Amount_Paid = 0;
                }
                






                if (amount != null)
                {
                    //model.Advance_pending = amount.advancepending;
                    //model.Total_Contract_Amount = amount.totalamount;
                    //model.Total_Amount_Paid = amount.paidamount;
                    //model.to
                }
                // model.New_Renewal_flag = Common.Renewal
                // model.Total_Amount_Paid = agreementDet.Total_Rental_amount;
                //  model.Agreement_Refno = AgreementNo;
                CheckListType = "Closure";
                //model.AgreementPd = new AgreementPdcViewModel();
                return PartialView("../Tca/Closure/_ClosureDetails", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Closure(AgreementClosureViewModel model)
        {
            //TempData.Peek("EntryFlag");
            try
            {
                string Agpdc = null;
                #region agpdc
                if (model.AgreementPdcList != null)
                {
                    foreach (var item in model.AgreementPdcList)
                    {

                        var Cheque_Date = item.Cheque_Date != null ? "'" + item.Cheque_Date.Value.Date.ToString("yyyy-MM-dd") + "'" : "null";
                        if (string.IsNullOrWhiteSpace(Agpdc))
                        {
                            Agpdc = "(" + model.Agreement_No + ",'" + item.Month + "','" + item.Year + "','" + item.BankName + "','" + item.Cheque_No +
                                    "'," + Cheque_Date + ",'" + item.Cheque_Amount + "','" + item.Payment_Mode + "','" + item.status + "','" + Session["Region"].ToString() + "','" + Session["Country"].ToString() + "')";
                        }
                        else
                        {
                            Agpdc += ",(" + model.Agreement_No + ",'" + item.Month + "','" + item.Year + "','" + item.BankName + "','" + item.Cheque_No +
                                    "'," + Cheque_Date + ",'" + item.Cheque_Amount + "','" + item.Payment_Mode + "','" + item.status + "','" + Session["Region"].ToString() + "','" + Session["Country"].ToString() + "')";
                        }
                    }
                }
                #endregion
                model.pAgclpdc = Agpdc;

                string Agfac = null;
                #region pAgfac
                if (model.agreementFacilityList != null)
                {
                    foreach (var item in model.agreementFacilityList)
                    {
                        if (string.IsNullOrWhiteSpace(Agfac))
                        {
                            Agfac = "(" + model.Agreement_No + ",'" + item.Facility_id + "','" + item.Facility_Name + "','" + item.Numbers_available + "','" + Session["Region"].ToString() + "','" + Session["Country"].ToString() + "')";
                        }
                        else
                        {
                            Agfac += ",(" + model.Agreement_No + ",'" + item.Facility_id + "','" + item.Facility_Name + "','" + item.Numbers_available + "','" + Session["Region"].ToString() + "','" + Session["Country"].ToString() + "')";
                        }
                    }
                }
                #endregion
                model.pAgclfac = Agfac;
                string Aguti = null;
                #region Aguti
                if (model.AgreementUtilityList != null)
                {
                    foreach (var item in model.AgreementUtilityList)
                    {
                        if (string.IsNullOrWhiteSpace(Aguti))
                        {
                            Aguti = "(" + model.Agreement_No + ",'" + item.Utility_id + "','" + item.Utility_Name + "','" + item.Payable + "','" + item.Amount_Type +
                                    "','" + item.Amount + "','" + Session["Region"].ToString() + "','" + Session["Country"].ToString() + "')";
                        }
                        else
                        {
                            Aguti += ",(" + model.Agreement_No + ",'" + item.Utility_id + "','" + item.Utility_Name + "','" + item.Payable + "','" + item.Amount_Type +
                                    "','" + item.Amount + "','" + Session["Region"].ToString() + "','" + Session["Country"].ToString() + "')";
                        }
                    }
                }
                #endregion
                model.pAgcluti = Aguti;
                string Agchk = null;
                #region Agchk
                if (model.AgreementCheckList != null)
                {
                    foreach (var item in model.AgreementCheckList)
                    {
                        if (string.IsNullOrWhiteSpace(Agchk))
                        {
                            Agchk = "(" + model.Agreement_No + ",'" + item.Checklist_id + "','" + item.Checklist_Name + "','" + item.Status + "','" + item.Remarks + "','" + Session["Region"].ToString() + "','" + Session["Country"].ToString() + "')";
                        }
                        else
                        {
                            Agchk += ",(" + model.Agreement_No + ",'" + item.Checklist_id + "','" + item.Checklist_Name + "','" + item.Status + "','" + item.Remarks + "','" + Session["Region"].ToString() + "','" + Session["Country"].ToString() + "')";
                        }
                    }
                }
                #endregion
                model.pAgclchk = Agchk;
                var IsExistClosure = db.tbl_agreement_closure.Any(x => x.Agreement_No == model.Agreement_No && x.Region_Name == RegNam);
                var closureId = db.tbl_agreement_closure.Where(x => x.Region_Name == RegNam).OrderByDescending(x => x.id).FirstOrDefault();
                model.Id = closureId != null ? closureId.id + 1 : 1;
                string PFlag = IsExistClosure ? Common.UPDATE : Common.INSERT;
                object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PId", model.Id),
                         new MySqlParameter("@PAgreement_No",model.Agreement_No),
                         new MySqlParameter("@PAdvance_pending", model.Advance_pending),
                         new MySqlParameter("@PAdvance_Security_Amount_Paid", model.Advance_Security_Amount_Paid),
                         new MySqlParameter("@PLess_any_damanges", model.Less_any_damanges),
                         new MySqlParameter("@PAmount_to_be_refunded", model.Amount_to_be_refunded),
                         new MySqlParameter("@PRemarks", model.Remarks),
                         new MySqlParameter("@PAvailabledate", DateTime.Now),
                         new MySqlParameter("@pAgclpdc", model.pAgclpdc),
                         new MySqlParameter("@pAgclfac", model.pAgclfac),
                         new MySqlParameter("@pAgcluti", model.pAgcluti),
                         new MySqlParameter("@pAgclchk", model.pAgclchk),
                         new MySqlParameter("@PCreatedUser",System.Web.HttpContext.Current.User.Identity.Name),
                         new MySqlParameter("@PRegion_Name", Session["Region"].ToString()),
                         new MySqlParameter("@PCountry", Session["Country"].ToString())
                };
                var closure = await db.Database.SqlQuery<object>("call Usp_Agreement_Closuer_All(@PFlag, @Pid, @PAgreement_No, @PAdvance_pending, @PAdvance_Security_Amount_Paid, @PLess_any_damanges,@PAmount_to_be_refunded, @PRemarks, @PAvailabledate, @PCreateduser, @pAgclpdc, @pAgclfac, @pAgcluti, @pAgclchk,@PRegion_Name,@PCountry)", parameters).ToListAsync();
                //Usp_Agreement_Closuer_All(@PFlag, @Pid, @PAgreement_No, @PAdvance_pending, @PAdvance_Security_Amount_Paid, @PLess_any_damanges,@PAmount_to_be_refunded, @PRemarks, @PAvailabledate, @PCreateduser, @pAgclpdc, @pAgclfac, @pAgcluti, @pAgclchk)
                //return PartialView("../Tca/Closure/_ClosureDetails", model);
            }
            catch (Exception e)
            {
                //throw;
            }
            return View("Index");
        }
        #endregion
        #region mapper
        public void AgreementRenwalMap(tbl_agreement from, AgreementFormViewModel to)
        {
            //string elf = TempData.Peek("EntryFlag").ToString();
            //TempData.Peek("EntryFlag");
            if (from != null)
            {
                to.Single_Multiple_Flag = from.Single_Multiple_Flag;
                //if (elf == "Renewal")
                //{
                //    to.nofopayments = 0;
                //}
                //else
                //{
                //    to.nofopayments = from.nofopayments.HasValue ? from.nofopayments.Value : 0;
                //}
                //to.nofopayments = from.nofopayments.HasValue ? from.nofopayments.Value : 0;
                to.Agreement_Date = from.Agreement_Date.HasValue ? from.Agreement_Date.Value : DateTime.Now;
                to.Vacantstartdate = from.Vacantstartdate.HasValue ? from.Vacantstartdate.Value : DateTime.MinValue;
                to.Agreement_Start_Date = from.Agreement_Start_Date.HasValue ? from.Agreement_Start_Date.Value : DateTime.Now;
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
                to.Caretaker_id = from.Caretaker_id;
                to.Caretaker_Name = from.Caretaker_Name;
            }
        }
        public void AgreementClosureMap(tbl_agreement from, AgreementClosureViewModel to)
        {
            //TempData.Peek("EntryFlag");
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
        #endregion
        #region ajax loaders
        public PartialViewResult AgreementPdc(int AgreementNo)
        {
            //TempData.Peek("EntryFlag");
            //string efl = TempData["EntryFlag"].ToString();
            //TempData.Peek("EntryFlag");
            //if (AgreeType == "Contract Renewal")
            //{
            //    AgreementNo = 0;
            //}
            AgreementPdcViewModel model = new AgreementPdcViewModel();
            //AgreementFormViewModel model = new AgreementFormViewModel();
            ViewBag.Month = new SelectList(Common.Months);
            ViewBag.Payment_Mode = new SelectList(Common.PaymentMode);
            //ViewBag.AgreementPd = model.AgreementPd;
            if (AgreementNo != 0)
            {
                if (AgreeType != "Renewal")
                {
                    model.AgreementPdcList = db.tbl_agreement_pdc.Where(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam && x.Delmark != "*").AsEnumerable().Select(x => new AgreementPdcViewModel()
                    {
                        Id = x.id,
                        Month = x.Month,
                        Year = x.Year,
                        Payment_Mode = x.Payment_Mode,
                        BankName = x.BankName,
                        Cheque_No = x.Cheque_No,
                        //Cheque_Date=string.IsNullOrWhiteSpace(x.Cheque_Date.ToString())?DateTime.Parse(x.Cheque_Date.ToString()): (DateTime?)null,
                        Cheque_Date = x.Cheque_Date,
                        Cheque_Amount = x.Cheque_Amount

                    }).ToList();
                }
                return PartialView("../Tca/Renewal/_AgreementPdc", model);
            }
            return PartialView("../Tca/_AgreementPdc", model);
        }
        public PartialViewResult AgreementClosurePdc(int AgreementNo)
        {
            AgreementPdcViewModel model = new AgreementPdcViewModel();
            //AgreementFormViewModel model = new AgreementFormViewModel();
            ViewBag.Month = new SelectList(Common.Months);
            ViewBag.Payment_Mode = new SelectList(Common.PaymentMode);
            //ViewBag.AgreementPd = model.AgreementPd;
            if (AgreementNo != 0)
            {
                model.AgreementPdcList = db.tbl_agreement_pdc.Where(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam && x.Delmark != "*").AsEnumerable().Select(x => new AgreementPdcViewModel()
                {
                    Id = x.id,
                    Month = x.Month,
                    Year = x.Year,
                    Payment_Mode = x.Payment_Mode,
                    BankName = x.BankName,
                    Cheque_No = x.Cheque_No,
                    //Cheque_Date=string.IsNullOrWhiteSpace(x.Cheque_Date.ToString())?DateTime.Parse(x.Cheque_Date.ToString()): (DateTime?)null,
                    Cheque_Date = x.Cheque_Date,
                    Cheque_Amount = x.Cheque_Amount

                }).ToList();

            }
            return PartialView("../Tca/Closure/_AgreementPdcDetails", model);
        }
        public async Task<PartialViewResult> AgreementDocument(int AgreementNo)
        {
            //TempData.Peek("EntryFlag");
            AgreementDocumentViewModel model = new AgreementDocumentViewModel();
            try
            {
                var facility = await db.tbl_facilitymaster.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                ViewBag.Facility_id = new SelectList(facility, "Facility_id", "Facility_id");
                ViewBag.Facility_Name = new SelectList(facility, "Facility_id", "Facility_Name");
                if (AgreementNo != 0)
                {
                    model.agreementDocumentExistList = db.tbl_agreement_doc.Where(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam)
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
            //TempData.Peek("EntryFlag");
            //string efl = TempData["EntryFlag"].ToString();
            //if (efl == "Renewal")
            //{
            //    AgreementNo = 0;
            //}
            AgreementFacilityViewModel model = new AgreementFacilityViewModel();
            try
            {
                var facility = await db.tbl_facilitymaster.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                //var facility = await db.tbl_propertiesdt.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.Facility_id = new SelectList(facility, "Facility_id", "Facility_id");
                ViewBag.Facility_Name = new SelectList(facility, "Facility_id", "Facility_Name");
                if (AgreementNo != 0)
                {
                    model.agreementFacilityList = db.tbl_agreement_facility.Where(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam)
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
            //string efl = TempData["EntryFlag"].ToString();
            //TempData.Peek("EntryFlag");

            //if (efl == "Renewal")
            //{
            //    AgreementNo = 0;
            //}
            AgreementUtilityViewModel model = new AgreementUtilityViewModel();
            try
            {
                var utility = await db.tbl_utilitiesmaster.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                //var utility = await db.tbl_propertiesdt1.Where(x => x.Delmark != "*").ToListAsync();
                ViewBag.Utility_id = new SelectList(utility, "Utility_id", "Utility_id");
                ViewBag.Utility_Name = new SelectList(utility, "Utility_id", "Utility_Name");
                ViewBag.Amount_Type = new SelectList(Common.AmountType);
                List<PaybleName> payable = new List<PaybleName>();

                var tenantCompany = await db.tbl_tenant_company.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).Select(x => new PaybleName() { Name = x.First_Name }).ToListAsync();
                var tenant = await db.tbl_tenant_individual.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).Select(x => new PaybleName() { Name = x.First_Name }).ToListAsync();
                payable.AddRange(tenantCompany);
                payable.AddRange(tenant);
                payable = payable.OrderBy(x => x.Name).ToList();
                ViewBag.Payable = new SelectList(payable, "Name", "Name");
                if (AgreementNo != 0)
                {
                    model.AgreementUtilityList = await db.tbl_agreement_utility.Where(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam && x.Delmark != "*").
                                                Select(x => new AgreementUtilityViewModel()
                                                {
                                                    Id = x.id,
                                                    Utility_id = x.Utility_id,
                                                    Utility_Name = x.Utility_Name,
                                                    Payable = x.Payable,
                                                    Amount_Type = x.Amount_Type,
                                                    Amount = x.Amount.HasValue ? x.Amount.Value : 0
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
        public async Task<PartialViewResult> AgreementUnit(int AgreementNo)
        {
            //string efl = TempData["EntryFlag"].ToString();
            //if(efl == "Renewal")
            //{
            //    AgreementNo = 0;
            //}
            //TempData.Peek("EntryFlag");
            AgreementUnitViewModel model = new AgreementUnitViewModel();
            // string entryflag = model.New_Renewal_flag;
            var property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
            ViewBag.Property_ID = new SelectList(property.Where(x => x.Property_Flag == "Property" && (x.Status == null || x.Status != "Avail") && string.IsNullOrEmpty(x.Delmark) && x.Company_occupied_Flag != 1 && x.Noofunits > 0).ToList(), "Property_ID", "Property_ID");
            //select* from tbl_propertiesmaster where Noofunits > 0 and ifnull(status,'')<> 'Avail'and ifnull(delmark,'')<> '*' and ifnull(Company_occupied_Flag,0)<> 1
            ViewBag.Properties_Name = new SelectList(property.Where(x => x.Property_Flag == "Property" && (x.Status == null || x.Status != "Avail") && string.IsNullOrEmpty(x.Delmark) && x.Company_occupied_Flag != 1 && x.Noofunits > 0).ToList(), "Property_ID", "Property_Name");
            ViewBag.Property_ID_Tawtheeq = new SelectList(property.Where(x => x.Property_Flag == "Property" && (x.Status == null || x.Status != "Avail") && string.IsNullOrEmpty(x.Delmark) && x.Company_occupied_Flag != 1 && x.Noofunits > 0).ToList(), "Property_ID", "Property_ID_Tawtheeq");
            ViewBag.Unit_ID_Tawtheeq = new SelectList(property.Where(x => x.Property_Flag == "Unit").ToList(), "Ref_Unit_Property_ID", "Unit_ID_Tawtheeq");
            ViewBag.Unit_Property_Name = new SelectList(property.Where(x => x.Property_Flag == "Unit").ToList(), "Unit_ID_Tawtheeq", "Unit_Property_Name");
            if (AgreementNo != 0)
            {
                model.AgreementUnitList = await db.tbl_agreement_unit_inner.Where(x => x.Delmark != "*" && x.Region_Name == RegNam && x.Agreement_No == AgreementNo).Select(x => new AgreementUnitViewModel()
                {
                    Id = x.id,
                    Property_ID = x.Property_ID.ToString(),
                    Properties_Name = x.Properties_Name,
                    Property_ID_Tawtheeq = x.Property_ID_Tawtheeq,
                    Unit_ID_Tawtheeq = x.Unit_ID_Tawtheeq,
                    Unit_Property_Name = x.Unit_Property_Name
                }).ToListAsync();
                return PartialView("../Tca/Renewal/_AgreementUnit", model);
            }

            return PartialView("../Tca/_AgreementUnit", model);
        }
        public async Task<PartialViewResult> AgreementCheckList(int AgreementNo)
        {
            //TempData.Peek("EntryFlag");
            //string efl = TempData["EntryFlag"].ToString();
           // AgreementFormViewModel model1 = new AgreementFormViewModel();
            
            string agreementtyp = "";
            if (CheckListType == "Renewal")
            {
               if(agreerefno == 0)
                {
                    AgreementNo = 0;
                }               
                agreementtyp = "Contract Renewal";
            }
            else if (CheckListType == "Closure")
            {
                agreementtyp = "Contract Closure";
            }
            else
            {
                agreementtyp = "New Contract";
            }
            AgreementCheckListViewModel model = new AgreementCheckListViewModel();
            try
            {

                var agreementCheckList = await db.tbl_agreement_checklist.Where(x => x.Agreement_No == AgreementNo && x.Region_Name == RegNam).ToListAsync(); //&& x.Delmark != "*"
                //var agreementCheckList = new SelectList(db.Database.SqlQuery<int>("SELECT * from  tbl_agreement_checklist where Agreement_No = {0}",AgreementNo).ToList());
                var checkList = await db.tbl_checklistmaster.Where(x => x.Check_type == agreementtyp && x.Delmark != "*" && x.Region_Name == RegNam).ToListAsync();
                //if (efl == "Renewal")
                //{
                //    var checkList = await db.tbl_checklistmaster.Where(x => x.Check_type == Common.Renewal && x.Delmark != "*").ToListAsync();
                //}
                //else
                //{
                //    var checkList = await db.tbl_checklistmaster.Where(x => x.Check_type == Common.AgreementCheck_type && x.Delmark != "*").ToListAsync();
                //}                
                foreach (var item in checkList)
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
        #endregion

        [HttpGet]
        public async Task<ActionResult> GetTenentDetails(string Type)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                if (Type == "Company")
                {
                    var query = await db.tbl_tenant_company.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).OrderBy(x => x.Tenant_Id).Select(x => new { Tenant_Id = x.Tenant_Id, Tenant_Name = x.CompanyName }).ToListAsync();
                    return Json(query, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var query = await db.tbl_tenant_individual.Where(x => x.Delmark != "*" && x.Region_Name == RegNam).OrderBy(x => x.Tenant_Id).Select(x => new { Tenant_Id = x.Tenant_Id, Tenant_Name = string.Concat(x.First_Name, " ", x.Middle_Name, " ", x.Last_Name) }).ToListAsync();
                    return Json(query, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPropertiesDetails(string Type, int flg)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                if (Type != "Multiple")
                {
                    if (flg == 0)
                    {
                        var propertylist = await db.Database.SqlQuery<PropertyDropdownModel>("select distinct Property_ID_Tawtheeq,Property_id from tbl_propertiesmaster where Property_Flag = 'Property' and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1 and ifnull(status,'')<>'Avail' and region_name={0}", RegNam).ToListAsync();
                        return Json(propertylist, JsonRequestBehavior.AllowGet);
                    }
                    if (flg == 1)
                    {
                        var propertylist = await db.Database.SqlQuery<PropertyDropdownModel>("select Property_Name,Property_Id from tbl_propertiesmaster where property_flag = 'Property'and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1 and ifnull(status,'')<>'Avail' and region_name={0} order by Property_Name",RegNam).ToListAsync();
                        return Json(propertylist, JsonRequestBehavior.AllowGet);
                    }
                    if (flg == 2)
                    {
                        var propertylist = await db.Database.SqlQuery<PropertyDropdownModel>("select Unit_ID_Tawtheeq,Ref_Unit_Property_ID from tbl_propertiesmaster where Property_Flag = 'Unit' and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1 and ifnull(status_unit,'')<>'Avail' and region_name={0}",RegNam).ToListAsync();
                        return Json(propertylist, JsonRequestBehavior.AllowGet);
                    }
                    if (flg == 3)
                    {
                        var propertylist = await db.Database.SqlQuery<PropertyDropdownModel>("select Unit_Property_Name,Unit_ID_Tawtheeq from tbl_propertiesmaster where Property_Flag = 'Unit' and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1 and ifnull(status_unit,'')<>'Avail' and region_name={0} order by Unit_Property_Name",RegNam).ToListAsync();
                        return Json(propertylist, JsonRequestBehavior.AllowGet);
                    }
                    return Json("", JsonRequestBehavior.AllowGet);
                    //var propertylist = await db.Database.SqlQuery<PropertyDropdownModel>("select property_id,  Property_ID_Tawtheeq,Property_Name,Unit_ID_Tawtheeq,Unit_Property_Name from tbl_propertiesmaster where ifnull(Noofunits,0)=0 and Property_Flag='property'  and ifnull(status,'')<>'Avail' and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1 union all select property_id, Ref_unit_Property_ID_Tawtheeq,Ref_Unit_Property_Name, Unit_ID_Tawtheeq,Unit_Property_Name from tbl_propertiesmaster where Property_Flag='Unit' and ifnull(status,'')<>'Avail' and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1").ToListAsync();                    
                }
                else
                {
                    var propertylist = await db.Database.SqlQuery<tbl_propertiesmaster>("select * from tbl_propertiesmaster where Noofunits>0 and ifnull(status,'')<>'Avail'and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1 and region_name={0}", RegNam).ToListAsync();
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
        [HttpGet]
        public async Task<ActionResult> GetPropertiesUnitDetails(int? propertyId)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                //var unit = await db.tbl_propertiesmaster.Where(x => x.Ref_Unit_Property_ID == propertyId && x.Status != "Avail").ToListAsync();
                //ViewBag.UnitIDTawtheeq = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
                //ViewBag.UnitPropertyName = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_Property_Name");
                var propertylist = await db.Database.SqlQuery<PropertyDropdownModel>("select Unit_ID_Tawtheeq,Unit_Property_Name,property_id,Ref_Unit_Property_ID from tbl_propertiesmaster where Property_Flag = 'Unit' and ifnull(delmark,'')<>'*' and ifnull(Company_occupied_Flag,0)<>1 and ifnull(status_unit,'')<>'Avail' and Ref_Unit_Property_ID = {0} and region_name={1} order by Unit_Property_Name", propertyId, RegNam).ToListAsync();
                return Json(propertylist, JsonRequestBehavior.AllowGet);
                //return Json(new SelectList(unit, "Ref_Unit_Property_ID", "Unit_Property_Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetContractAmountPerDay(int propertyId)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                var unit = await db.tbl_propertiesmaster.Where(x => x.Ref_Unit_Property_ID == propertyId && x.Region_Name == RegNam).ToListAsync();
                //ViewBag.UnitIDTawtheeq = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
                //ViewBag.UnitPropertyName = new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_Property_Name");

                return Json(new SelectList(unit, "Unit_ID_Tawtheeq", "Unit_Property_Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPropertyOnChange(int propertyId)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                var propertyDet = await db.tbl_propertiesmaster.Where(x => x.Property_Id == propertyId && x.Region_Name == RegNam).FirstOrDefaultAsync();
                List<PropertyDropdownModel> list = new List<PropertyDropdownModel>();

                PropertyDropdownModel model = new PropertyDropdownModel();
                model.Caretaker_ID = propertyDet.Caretaker_ID.HasValue ? propertyDet.Caretaker_ID.Value : 0;
                model.Caretaker_Name = propertyDet.Caretaker_Name;
                model.Vacantstartdate = propertyDet.Vacant_Start_Date.HasValue ? propertyDet.Vacant_Start_Date.Value.ToString("yyyy-MM-dd") : "";

                list.Add(model);
                //var json=
                //property.Caretaker_Name;
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int AgreementNo, string SingleMultipleFlag,int PropertyID)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                MessageResult result = new MessageResult();
                AgreementFormViewModel model = new AgreementFormViewModel();
                model.Agreement_No = AgreementNo;
                model.Single_Multiple_Flag = SingleMultipleFlag;
                model.property_id = PropertyID;
                object[] parameters = Helper.GetTcaMySqlParameters<AgreementFormViewModel>(model, Common.DELETE, System.Web.HttpContext.Current.User.Identity.Name);
                //string paramNames = Helper.GetTcaMySqlParametersNames<AgreementFormViewModel>(model, PFlag, user);
                string paramNames = "@PFlag, @PSingle_Multiple_Flag, @PAgreement_Refno, @PNew_Renewal_flag, @PAgreement_No, @PAgreement_Date, @PAg_Tenant_id, @PAg_Tenant_Name, @Pproperty_id, @PProperty_ID_Tawtheeq, @PProperties_Name, @PUnit_ID_Tawtheeq, @PUnit_Property_Name, @PCaretaker_id, @PCaretaker_Name, @PVacantstartdate, @PAgreement_Start_Date, @PAgreement_End_Date, @PTotal_Rental_amount, @PPerday_Rental, @PAdvance_Security_Amount, @PSecurity_Flag, @PSecurity_chequeno, @PSecurity_chequedate, @PNotice_Period, @Pnofopayments, @PApproval_Flag, @PApproved_By, @PApproved_Date, @PTenant_Type, @PCreateduser, @PAgpdc, @PAgdoc, @PAgfac, @PAguti, @PAgchk, @PAgunit";

                var tenantCompany = await db.Database.SqlQuery<object>("CALL Usp_Agreement_All(" + paramNames + ")", parameters).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Tca/Create
        public ActionResult Create()
        {
            try
            {
                //TempData.Peek("EntryFlag");
                AgreementFormViewModel model = new AgreementFormViewModel();
                ViewBag.TitleDisplay = new SelectList(Common.Title);

                ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");
                //var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
                ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name");
                ViewBag.Profession = new SelectList(Common.Profession);
                ViewBag.PropertyId = db.tbl_propertiesmaster.Where(x => x.Region_Name == RegNam).OrderByDescending(x => x.Property_Id).FirstOrDefault()?.Property_Id + 1;
                model.Agreement_No = 0;
                return PartialView("../Master/TenantIndividual/_AddOrUpdate", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult> PrintAgreement(int AgreementNo, string OtherTerms)
        {
            //TempData.Peek("EntryFlag");
            AgreementFormViewModel model = new AgreementFormViewModel();
            try
            {

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> TcaApprove(int AgreementNo)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam);
                if (agreementDet != null)
                {
                    agreementDet.Approval_Flag = 1;
                    agreementDet.Approved_Date = DateTime.Now;
                    agreementDet.Approved_By = User.Identity.Name;
                    agreementDet.Region_Name = RegNam;
                    object[] param1 =
                    {
                        new MySqlParameter("@PFlag","update"),
                        new MySqlParameter("@PAgreement_no",AgreementNo),
                        new MySqlParameter("@PApproval_flag",1),
                        new MySqlParameter("@PRegion_Name",RegNam),
                        new MySqlParameter("@PApproved_By",User.Identity.Name),
                    };
                    var RE1 = await db.Database.SqlQuery<object>("Usp_approval_Screen_All(@PFlag,@PAgreement_no,@PApproval_flag,@PRegion_Name,@PApproved_By)", param1).ToListAsync();
                    //await db.SaveChangesAsync();
                }
                MessageResult result = new MessageResult();
                AgreementFormViewModel model = new AgreementFormViewModel();
                model.Agreement_No = AgreementNo;
                //var agreemet=db.
                object[] param = { new MySqlParameter("@PAgreemwnt_No", AgreementNo),

                                 };
                var RE = await db.Database.SqlQuery<object>("CALL Email_Agreement(@PAgreemwnt_No)", param).ToListAsync();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> TcaReject(int AgreementNo)
        {
            try
            {
                //TempData.Peek("EntryFlag");
                var agreementDet = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo && x.Delmark != "*" && x.Region_Name == RegNam);
                if (agreementDet != null)
                {
                    agreementDet.Approval_Flag = 2;
                    agreementDet.Approved_Date = DateTime.Now;
                    agreementDet.Approved_By = User.Identity.Name;
                    agreementDet.Region_Name = RegNam;
                    object[] param =
                   {
                        new MySqlParameter("@PFlag","update"),
                        new MySqlParameter("@PAgreement_no",AgreementNo),
                        new MySqlParameter("@PApproval_flag",2),
                        new MySqlParameter("@PRegion_Name",RegNam),
                        new MySqlParameter("@PApproved_By",User.Identity.Name),
                    };
                    var RE = await db.Database.SqlQuery<object>("Usp_approval_Screen_All(@PFlag,@PAgreement_no,@PApproval_flag,@PRegion_Name,@PApproved_By)", param).ToListAsync();
                    //await db.SaveChangesAsync();
                }
                MessageResult result = new MessageResult();
                AgreementFormViewModel model = new AgreementFormViewModel();
                model.Agreement_No = AgreementNo;


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
            }
        }

        
        [HttpGet]
        public async Task<ActionResult> GetPropertyUnitMonthlyRent(string PropertyID,string UnitTawtheeqID,string PropUnitFlag)//, string PropUnitFlag
        {
            try
            {
                var monthrent = await db.Database.SqlQuery<PropertyViewModel>("select id,Property_id,Property_Flag,Rental_Rate_Month,Rental_Rate_Month_unit from tbl_propertiesmaster where property_id = {0} and region_name={1}", PropertyID,RegNam).ToListAsync();
                if (PropUnitFlag == "Unit")
                {
                    monthrent = await db.Database.SqlQuery<PropertyViewModel>("select id,Property_id,Property_Flag,Rental_Rate_Month,Rental_Rate_Month_unit from tbl_propertiesmaster where Unit_ID_Tawtheeq = {0} and region_name={1}", UnitTawtheeqID,RegNam).ToListAsync();
                }               
                return Json(monthrent, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}





