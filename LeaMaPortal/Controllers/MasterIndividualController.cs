using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using MvcPaging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LeaMaPortal.Controllers
{
    public class MasterIndividualController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: MasterIndividual
        public ActionResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
                //TenantCompanyViewModel model = new TenantCompanyViewModel();
                var query = db.tbl_tenant_individual.Where(x => x.Delmark != "*");
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    query = query.Where(x => x.First_Name.Contains(Search));
                }
                var list = query.OrderBy(x => x.Tenant_Id).ToPagedList(currentPageIndex, PageSize);
                return PartialView("../Master/TenantIndividual/_List",list);
            }
            catch
            {
                throw;
            }
        }

        // GET: MasterIndividual/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult List()
        {
            var list = db.tbl_tenant_individual.ToList();
            return View(list);
        }

        // GET: MasterIndividual/Create
        public async Task<PartialViewResult> Create()
        {
            try
            {
                TenantIndividualViewModel model = new TenantIndividualViewModel();
                model.Marital_Status = Common.DefaultMaridalStatus;
                model.Title = Common.DefaultTitle;
                //var _titleResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','Title',',',null)").ToList();
                var _titleResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant individual" && x.comboname == "Title");
                if (_titleResult != null)
                {
                    ViewBag.TitleDisplay = new SelectList(_titleResult.combovalue.Split(','), Common.DefaultTitle);
                }


                //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
                var _emirateResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Emirate");
                if (_emirateResult != null)
                {
                    ViewBag.Emirate = new SelectList(_emirateResult.combovalue.Split(','));
                }


                //var _visaTypeResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','VisaType','!#',null)").ToList();
                var _visaTypeResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant individual" && x.comboname == "VisaType");
                if (_visaTypeResult != null)
                {
                    ViewBag.VisaType = new SelectList(_visaTypeResult.combovalue.Split(','));
                }
                

                //var region = .Select(x => x.Region_Name);
                ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");

                //var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
                ViewBag.Nationality = new SelectList(Common.Nationality);
               // ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name");
                ViewBag.Profession = new SelectList(Common.Profession);
                var tenant = db.tbl_tenant_individual.OrderByDescending(x => x.Tenant_Id).FirstOrDefault();
                ViewBag.Tenant_Id = tenant != null ? tenant.Tenant_Id + 1 : 1;
                return PartialView("../Master/TenantIndividual/_AddOrUpdate", model);
            }
            catch(Exception e)
            {
                throw;
            }
        }

        // POST: MasterIndividual/Create
        [HttpPost]
        public async Task<ActionResult> Create(TenantIndividualViewModel model)
        {
            try
            {
                MessageResult result = new MessageResult();
                model.Type = "Individual";
                string PFlag =Common.UPDATE;
                if (model.Tenant_Id==0)
                {
                    PFlag = Common.INSERT;
                    var tenant = db.tbl_tenant_individual.OrderByDescending(x => x.Tenant_Id).FirstOrDefault();
                    model.Tenant_Id = tenant != null ? tenant.Tenant_Id + 1 : 1;
                }
                string tenantDoc = model.tenantdocdetails;
                try
                {
                    if(model.TenantDocumentList!=null)
                    {
                        foreach (var item in model.TenantDocumentList)
                        {
                            if (string.IsNullOrWhiteSpace(tenantDoc))
                            {
                                tenantDoc = "(" + model.Tenant_Id + ",'" + item.Doc_name + "','" + item.Doc_Path + "')";
                            }
                            else
                            {
                                tenantDoc += ",(" + model.Tenant_Id + ",'" + item.Doc_name + "','" + item.Doc_Path + "')";
                            }
                        }
                    }
                    if(model.TenantDocument!=null)
                    {
                        foreach (var item in model.TenantDocument)
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
                                    Helper.CheckDirectory(Common.TenantIndividualDocumentDirectoryName);
                                    //To save file, use SaveAs method
                                    file.SaveAs(Server.MapPath("~/" + Common.TenantIndividualDocumentContainer) + fileName); //File will be saved in application root
                                    if (string.IsNullOrWhiteSpace(tenantDoc))
                                    {
                                        tenantDoc = "(" + model.Tenant_Id + ",'" + item.Name + "','" + fileName + "')";
                                    }
                                    else
                                    {
                                        tenantDoc += ",(" + model.Tenant_Id + ",'" + item.Name + "','" + fileName + "')";
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }
                model.tenantdocdetails = tenantDoc;
                object[] param = Helper.GetMySqlParameters<TenantIndividualViewModel>(model, PFlag, System.Web.HttpContext.Current.User.Identity.Name);

                var _result = await db.Database.SqlQuery<object>(@"CALL Usp_Tenant_Individual_All(@PFlag,@PTenant_Id,@PTitle  ,@PFirst_Name  ,@PMiddle_Name  ,@PLast_Name  ,@PCompany_Educational   ,@PProfession  ,@PMarital_Status  ,@Paddress  ,@Paddress1  ,@PEmirate  ,@PCity  ,@PPostboxNo  ,@PEmail  ,@PMobile_Countrycode  ,@PMobile_Areacode  ,@PMobile_No  ,@PLandline_Countrycode  ,@PLandline_Areacode  ,@PLandline_No  ,@PFax_Countrycode  ,@PFax_Areacode  ,@PFax_No  ,@PNationality  ,@PEmiratesid  ,@PEmirate_issuedate  ,@PEmirate_expirydate  ,@PPassportno  
                ,@PPlaceofissuance  
                ,@PPassport_Issuedate
                ,@PPassport_Expirydate  
                ,@PVisaType  
                ,@PVisano  
                ,@PVisa_IssueDate  
                ,@PVisa_ExpiryDate 
                ,@PDob  
                ,@PFamilyno  
                ,@PFamilybookcity  
                ,@PADWEA_Regid  
                ,@PType  
                ,@PCreateduser  
                ,@Ptenantdocdetails 
                                    )", param).ToListAsync();
                //MasterViewModel mastermodel = new MasterViewModel();
                //ViewBag.FormMasterSelected = 11;
                //ViewBag.FormMasterId = new SelectList(db.tbl_formmaster.OrderBy(x => x.MenuName), "Id", "MenuName");
                //return View("../Master/Index", mastermodel);
                //return Json(result, JsonRequestBehavior.AllowGet);
                //return PartialView("../Master/TenantIndividual/_AddOrUpdate");
                return RedirectToAction("../Master/Index", new { selected=11 });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // GET: MasterIndividual/Edit/5
        public async Task<PartialViewResult> Edit(int tenantId,string type)
        {
            try
            {
                var tenant =await db.tbl_tenant_individual.FindAsync(tenantId, type);
                TenantIndividualViewModel model = Map(tenant);

                var _titleResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant individual" && x.comboname == "Title");
                if (_titleResult != null)
                {
                    ViewBag.TitleDisplay = new SelectList(_titleResult.combovalue.Split(','), tenant.Title);
                }


                //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
                var _emirateResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Emirate");
                if (_emirateResult != null)
                {
                    ViewBag.Emirate = new SelectList(_emirateResult.combovalue.Split(','), tenant.Emirate);
                }


                //var _visaTypeResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','VisaType','!#',null)").ToList();
                var _visaTypeResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant individual" && x.comboname == "VisaType");
                if (_visaTypeResult != null)
                {
                    ViewBag.VisaType = new SelectList(_visaTypeResult.combovalue.Split(','), tenant.VisaType);
                }

                //var _result = db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','Title',',',null)").ToList();
                //ViewBag.TitleDisplay = new SelectList(_result, tenant.Title);

                //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
                //ViewBag.Emirate = new SelectList(_emirateResult,tenant.Emirate);

                //var _visaTypeResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','VisaType','!#',null)").ToList();
                //ViewBag.VisaType = new SelectList(_visaTypeResult, tenant.VisaType);

                //var region = .Select(x => x.Region_Name);
                ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name",tenant.City);
                //var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
               // ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name",tenant.Nationality);
                ViewBag.Nationality = new SelectList(Common.Nationality, tenant.Nationality);
                ViewBag.Profession = new SelectList(Common.Profession,tenant.Profession);
                ViewBag.Tenant_Id = tenantId;
                model.TenantDocumentList =await db.tbl_tenant_individual_doc.Where(x => x.Tenant_Id == tenantId)
                                            .Select(x => new tbl_tenant_individual_docVM()
                {
                    id=x.id,
                    Tenant_Id=x.Tenant_Id,
                    Doc_name=x.Doc_name,
                    Doc_Path=x.Doc_Path
                }).ToListAsync();
                
                return PartialView("../Master/TenantIndividual/_AddOrUpdate", model);
            }
            catch
            {
                throw;
            }
        }

       public TenantIndividualViewModel Map(tbl_tenant_individual tenant)
        {
             return new TenantIndividualViewModel()
            {
                Tenant_Id = tenant.Tenant_Id,
                Title = tenant.Title,
                First_Name = tenant.First_Name,
                Middle_Name = tenant.Middle_Name,
                Last_Name = tenant.Last_Name,
                Company_Educational = tenant.Company_Educational,
                Profession = tenant.Profession,
                Marital_Status = tenant.Marital_Status,
                address = tenant.address,
                address1 = tenant.address1,
                Emirate = tenant.Emirate,
                City = tenant.City,
                PostboxNo = tenant.PostboxNo,
                Email = tenant.Email,
                Mobile_Countrycode = tenant.Mobile_Countrycode,
                Mobile_Areacode = tenant.Mobile_Areacode,
                Mobile_No = tenant.Mobile_No,
                Landline_Countrycode = tenant.Landline_Countrycode,
                Landline_Areacode = tenant.Landline_Areacode,
                Landline_No = tenant.Landline_No,
                Fax_Countrycode = tenant.Fax_Countrycode,
                Fax_Areacode = tenant.Fax_Areacode,
                Fax_No = tenant.Fax_No,
                Nationality = tenant.Nationality,
                Emiratesid = tenant.Emiratesid,
                Emirate_issuedate = tenant.Emirate_issuedate,
                Emirate_expirydate = tenant.Emirate_expirydate,
                Passportno = tenant.Passportno,
                Placeofissuance = tenant.Placeofissuance,
                Passport_Expirydate = tenant.Passport_Expirydate,
                Passport_Issuedate = tenant.Passport_Issuedate,
                VisaType = tenant.VisaType,
                Visano = tenant.Visano,
                Visa_IssueDate = tenant.Visa_IssueDate,
                Visa_ExpiryDate = tenant.Visa_ExpiryDate,
                Dob = tenant.Dob.HasValue? tenant.Dob.Value.Date: tenant.Dob,
                Familyno = tenant.Familyno,
                Familybookcity = tenant.Familybookcity,
                ADWEA_Regid = tenant.ADWEA_Regid,
                Type = tenant.Type
            };
        }
        // GET: MasterIndividual/Delete/5
        public async Task<ActionResult> Delete(int tenantId, string type)
        {
            MessageResult result = new MessageResult();
            try
            {
                var model =await db.tbl_tenant_individual.FindAsync(tenantId, type);

                object[] param = Helper.GetMySqlParameters<TenantIndividualViewModel>(Map(model), Common.DELETE, System.Web.HttpContext.Current.User.Identity.Name);

                var _result = await db.Database.SqlQuery<object>(@"CALL Usp_Tenant_Individual_All(@PFlag,@PTenant_Id,@PTitle  ,@PFirst_Name  ,@PMiddle_Name  ,@PLast_Name  ,@PCompany_Educational   ,@PProfession  ,@PMarital_Status  ,@Paddress  ,@Paddress1  ,@PEmirate  ,@PCity  ,@PPostboxNo  ,@PEmail  ,@PMobile_Countrycode  ,@PMobile_Areacode  ,@PMobile_No  ,@PLandline_Countrycode  ,@PLandline_Areacode  ,@PLandline_No  ,@PFax_Countrycode  ,@PFax_Areacode  ,@PFax_No  ,@PNationality  ,@PEmiratesid  ,@PEmirate_issuedate  ,@PEmirate_expirydate  ,@PPassportno  
                ,@PPlaceofissuance  
                ,@PPassport_Issuedate
                ,@PPassport_Expirydate  
                ,@PVisaType  
                ,@PVisano  
                ,@PVisa_IssueDate  
                ,@PVisa_ExpiryDate 
                ,@PDob  
                ,@PFamilyno  
                ,@PFamilybookcity  
                ,@PADWEA_Regid  
                ,@PType  
                ,@PCreateduser  
                ,@Ptenantdocdetails 
                                    )", param).ToListAsync();
                
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
