using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using MvcPaging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LeaMaPortal.Controllers
{
    public class TenantCompanyController : Controller
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: TenantCompany
        public async Task<PartialViewResult> Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                //CountryViewModel model = new CountryViewModel();
                //model.List = await db.tbl_country.Where(x => x.Delmark != "*").Select(x => new CountryViewModel()
                //{
                //    Id = x.Id,
                //    Country = x.Country_name
                //}).ToListAsync();

                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
                //TenantCompanyViewModel model = new TenantCompanyViewModel();
                var tenantCompanies = db.tbl_tenant_company.Where(x => x.Delmark != "*");
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    tenantCompanies = tenantCompanies.Where(x => x.CompanyName.Contains(Search));
                }
                var tenantCompany = tenantCompanies.OrderBy(x => x.Id).Select(x => new TenantCompanyViewModel()
                {
                    Address = x.address,
                    Address1 = x.address1,
                    ADWEARegid = x.ADWEA_Regid,
                    City = x.City,
                    Cocandindustryuid = x.Cocandindustryuid,
                    ComapanyActivity = x.Cocandindustryuid,
                    CompanyName = x.CompanyName,
                    Email = x.Email,
                    Emirate = x.Emirate,
                    FaxAreaCode = x.Fax_Areacode,
                    FaxCountryCode = x.Fax_Countrycode,
                    FaxNo = x.Fax_No,
                    FirstName = x.First_Name,
                    Id = x.Id,
                    Issuance_authority = x.Issuance_authority,
                    LicenseExpiryDate = x.License_ExpiryDate,
                    LandlineAreaCode = x.Landline_Areacode,
                    LandlineCountryCode = x.Landline_Countrycode,
                    LandlineNo = x.Landline_No,
                    LastName = x.Last_Name,
                    LicenseIssueDate = x.License_issueDate,
                    MaritalStatus = x.Marital_Status,
                    MiddleName = x.Middle_Name,
                    MobileAreaCode = x.Mobile_Areacode,
                    MobileCountryCode = x.Mobile_Countrycode,
                    MobileNo = x.Mobile_No,
                    Nationality = x.Nationality,
                    PostboxNo = x.PostboxNo,
                    TenantId = x.Tenant_Id,
                    TenantType = x.Type,
                    Title = x.Title,
                    TradelicenseNo = x.TradelicenseNo,
                    CompanyContactDetails = db.tbl_tenant_companydt1.Where(y => y.Tenant_Id == x.Tenant_Id).Select(z => new CompanyContactDetail()
                    {
                        ContactPersonName = z.Cper,
                        Department = z.Dept,
                        Designation = z.Desig,
                        Extension = z.Ext,
                        Id = z.Id,
                        MobileNo = z.MobNo,
                        Phone = z.Phone,
                        Salutations = z.Salutations,
                        TenantId = z.Tenant_Id
                    }).ToList(),
                    CompanyDetails = db.tbl_tenant_companydt.Where(y => y.Tenant_Id == x.Tenant_Id).Select(z => new CompanyDetail()
                    {
                        Branch = z.Branch,
                        BranchAddress = z.BranchAdd,
                        BranchAddress1 = z.Branchadd1,
                        City = z.City,
                        Country = z.Country,
                        EmailId = z.EMailID,
                        FaxNo = z.FaxNo,
                        Id = z.Id,
                        Phoneno = z.PhoneNo,
                        Pincode = z.Pincode,
                        Remarks = z.Remarks,
                        State = z.State,
                        TenantId = z.Tenant_Id
                    }).ToList()
                }).ToPagedList(currentPageIndex, PageSize);

                //}
                //else
                //{
                //    model.List = db.tbl_country.Where(x => x.Delmark != "*" && x.Country_name.ToLower().Contains(Search.ToLower()))
                //                  .OrderBy(x => x.Country_name).Select(x => new CountryViewModel()
                //                  {
                //                      Id = x.Id,
                //                      Country = x.Country_name
                //                  }).ToPagedList(currentPageIndex, PageSize);
                //}

                //var model = new TenantCompanyViewModel();
                //object[] parameters = {
                //     new MySqlParameter("@PFlag", "SELECT"),
                //     new MySqlParameter("@PTenant_Id", 0),
                //     new MySqlParameter("@PCompanyName", ""),
                //     new MySqlParameter("@PMarital_Status", ""),
                //     new MySqlParameter("@PTitle", ""),
                //     new MySqlParameter("@PFirst_Name", ""),
                //     new MySqlParameter("@PMiddle_Name", ""),
                //     new MySqlParameter("@PLast_Name", ""),
                //     new MySqlParameter("@Paddress", ""),
                //     new MySqlParameter("@Paddress1", ""),
                //     new MySqlParameter("@PEmirate", ""),
                //     new MySqlParameter("@PCity", ""),
                //     new MySqlParameter("@PPostboxNo", ""),
                //     new MySqlParameter("@PEmail", ""),
                //     new MySqlParameter("@PMobile_Countrycode", ""),
                //     new MySqlParameter("@PMobile_Areacode", ""),
                //     new MySqlParameter("@PMobile_No", ""),
                //     new MySqlParameter("@PLandline_Countrycode", ""),
                //     new MySqlParameter("@PLandline_Areacode", ""),
                //     new MySqlParameter("@PLandline_No", ""),
                //     new MySqlParameter("@PFax_Countrycode", ""),
                //     new MySqlParameter("@PFax_Areacode", ""),
                //     new MySqlParameter("@PFax_No", ""),
                //     new MySqlParameter("@PNationality", ""),
                //     new MySqlParameter("@PActitvity", ""),
                //     new MySqlParameter("@PCocandindustryuid", ""),
                //     new MySqlParameter("@PTradelicenseNo", ""),
                //     new MySqlParameter("@PLicense_issueDate", "1991-10-12"),
                //     new MySqlParameter("@PLicense_ExpiryDate", "1991-10-12"),
                //     new MySqlParameter("@PIssuance_authority", ""),
                //     new MySqlParameter("@PADWEA_Regid", ""),
                //     new MySqlParameter("@PType", ""),
                //     new MySqlParameter("@PCreateduser", ""),
                //     new MySqlParameter("@Ptenant_companydt", ""),
                //     new MySqlParameter("@Ptenant_companydt1", ""),
                //     new MySqlParameter("@Ptenant_companydoc", "")

                //};
                //var tenantCompany = await db.Database.SqlQuery<dynamic>("CALL Usp_Tenant_Company_All(@PFlag, @PTenant_Id, @PCompanyName, @PMarital_Status, @PTitle, @PFirst_Name, @PMiddle_Name, @PLast_Name, @Paddress, @Paddress1, @PEmirate, @PCity, @PPostboxNo, @PEmail, @PMobile_Countrycode, @PMobile_Areacode, @PMobile_No, @PLandline_Countrycode, @PLandline_Areacode, @PLandline_No, @PFax_Countrycode, @PFax_Areacode, @PFax_No, @PNationality, @PActitvity, @PCocandindustryuid, @PTradelicenseNo, @PLicense_issueDate, @PLicense_ExpiryDate, @PIssuance_authority, @PADWEA_Regid, @PType, @PCreateduser, @Ptenant_companydt, @Ptenant_companydt1, @Ptenant_companydoc)", parameters).ToListAsync();
                return PartialView("../Master/TenantCompany/_List", tenantCompany);
            }
            catch(Exception e)
            {
                throw;
            }
        }

        //[HttpGet]
        //public PartialViewResult AddCompanyDetail()
        //{
        //    return PartialView("../Master/TenantCompany/_CompanyDetail", new CompanyDetail()
        //    {
        //        Branch = "",
        //        BranchAddress = "",
        //        BranchAddress1 = "",
        //        City = "",
        //        Country = "",
        //        EmailId = "",
        //        FaxNo = "",
        //        Id = 0,
        //        Phoneno = "",
        //        Pincode = "",
        //        Remarks = "",
        //        State = "",
        //        TenantId = 0
        //    });
        //}

        [HttpGet]
        public async Task<PartialViewResult> AddOrUpdate()
        {
            try
            {
                TenantCompanyViewModel model = new TenantCompanyViewModel();
                model.MaritalStatus = Common.DefaultMaridalStatus;
                var tenantId = await db.tbl_tenant_company.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                ViewBag.TenantId = tenantId != null ? tenantId.Tenant_Id + 1 : 1;
                ViewBag.TenantType = new SelectList(Common.TenantType);

                //var _titleResult = db.Usp_split("Tenant individual", "Title", ",", null).ToList();
                var _titleResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant individual" && x.comboname == "Title");
                if (_titleResult != null)
                {
                    ViewBag.TitleDisplay = new SelectList(_titleResult.combovalue.Split(','), Common.DefaultTitle);
                }
                //var _titleResult = await db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','Title',',',null)").ToListAsync();
                
                //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
                var _emirateResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Emirate");
                if (_emirateResult != null)
                {
                    ViewBag.Emirate = new SelectList(_emirateResult.combovalue.Split(','));
                }

                //var _companyActivity = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Actitvity',',',null);").ToList();
                var _companyActivity = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Actitvity");
                if (_companyActivity != null)
                {
                    ViewBag.ComapanyActivity = new SelectList(_companyActivity.combovalue.Split(','));
                }

                ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");
                //var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
                ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name");
                //ViewBag.Emirate = new SelectList(Common.Emirate);
                //ViewBag.ComapanyActivity = new SelectList(Common.ComapanyActivity);
                ViewBag.Issuance_authority = new SelectList(Common.Issuance_authority);
                return PartialView("../Master/TenantCompany/_AddOrUpdate", model);
            }
            catch(Exception e)
            {
                throw;
            }
            
        }

        [HttpGet]
        public async Task<PartialViewResult> Edit(int id)
        {
            var tenantCompany = db.tbl_tenant_company.Where(x => x.Delmark != "*" && x.Tenant_Id == id).FirstOrDefault();
            if (tenantCompany == null)
            {
                return PartialView("../Master/TenantCompany/_AddOrUpdate", new TenantCompanyViewModel());
            }
            ViewBag.TenantId = tenantCompany.Tenant_Id;
            ViewBag.TenantType = new SelectList(Common.TenantType, tenantCompany.Type);

            var _titleResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant individual" && x.comboname == "Title");
            if (_titleResult != null)
            {
                ViewBag.TitleDisplay = new SelectList(_titleResult.combovalue.Split(','), tenantCompany.Title);
            }
            //var _titleResult = await db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','Title',',',null)").ToListAsync();

            //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
            var _emirateResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Emirate");
            if (_emirateResult != null)
            {
                ViewBag.Emirate = new SelectList(_emirateResult.combovalue.Split(','), tenantCompany.Emirate);
            }

            //var _companyActivity = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Actitvity',',',null);").ToList();
            var _companyActivity = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Actitvity");
            if (_companyActivity != null)
            {
                ViewBag.ComapanyActivity = new SelectList(_companyActivity.combovalue.Split(','), tenantCompany.Actitvity);
            }


            //var _titleResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','Title',',',null)").ToList();
            //ViewBag.TitleDisplay = new SelectList(_titleResult, tenantCompany.Title);

            //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
            //ViewBag.Emirate = new SelectList(_emirateResult, tenantCompany.Emirate);

            //var _companyActivity = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Actitvity',',',null);").ToList();
            //ViewBag.ComapanyActivity = new SelectList(_companyActivity, tenantCompany.Actitvity);

            ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name",tenantCompany.City);
            //var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
            ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name",tenantCompany.Nationality);
            //ViewBag.Emirate = new SelectList(Common.Emirate,tenantCompany.Emirate);
            //ViewBag.ComapanyActivity = new SelectList(Common.ComapanyActivity,tenantCompany.Actitvity);
            ViewBag.Issuance_authority = new SelectList(Common.Issuance_authority,tenantCompany.Issuance_authority);
            var tenantCompanyData = new TenantCompanyViewModel()
            {
                Address = tenantCompany.address,
                Address1 = tenantCompany.address1,
                ADWEARegid = tenantCompany.ADWEA_Regid,
                City = tenantCompany.City,
                Cocandindustryuid = tenantCompany.Cocandindustryuid,
                ComapanyActivity = tenantCompany.Cocandindustryuid,
                CompanyName = tenantCompany.CompanyName,
                Email = tenantCompany.Email,
                Emirate = tenantCompany.Emirate,
                FaxAreaCode = tenantCompany.Fax_Areacode,
                FaxCountryCode = tenantCompany.Fax_Countrycode,
                FaxNo = tenantCompany.Fax_No,
                FirstName = tenantCompany.First_Name,
                Id = tenantCompany.Id,
                Issuance_authority = tenantCompany.Issuance_authority,
                LicenseExpiryDate = tenantCompany.License_ExpiryDate,
                LandlineAreaCode = tenantCompany.Landline_Areacode,
                LandlineCountryCode = tenantCompany.Landline_Countrycode,
                LandlineNo = tenantCompany.Landline_No,
                LastName = tenantCompany.Last_Name,
                LicenseIssueDate = tenantCompany.License_issueDate,
                MaritalStatus = tenantCompany.Marital_Status,
                MiddleName = tenantCompany.Middle_Name,
                MobileAreaCode = tenantCompany.Mobile_Areacode,
                MobileCountryCode = tenantCompany.Mobile_Countrycode,
                MobileNo = tenantCompany.Mobile_No,
                Nationality = tenantCompany.Nationality,
                PostboxNo = tenantCompany.PostboxNo,
                TenantId = tenantCompany.Tenant_Id,
                TenantType = tenantCompany.Type,
                Title = tenantCompany.Title,
                TradelicenseNo = tenantCompany.TradelicenseNo,
                CompanyContactDetails = db.tbl_tenant_companydt1.Where(y => y.Tenant_Id == tenantCompany.Tenant_Id).Select(z => new CompanyContactDetail()
                {
                    ContactPersonName = z.Cper,
                    Department = z.Dept,
                    Designation = z.Desig,
                    Extension = z.Ext,
                    Id = z.Id,
                    MobileNo = z.MobNo,
                    Phone = z.Phone,
                    Salutations = z.Salutations,
                    TenantId = z.Tenant_Id
                }).ToList(),
                CompanyDetails = db.tbl_tenant_companydt.Where(y => y.Tenant_Id == tenantCompany.Tenant_Id).Select(z => new CompanyDetail()
                {
                    Branch = z.Branch,
                    BranchAddress = z.BranchAdd,
                    BranchAddress1 = z.Branchadd1,
                    City = z.City,
                    Country = z.Country,
                    EmailId = z.EMailID,
                    FaxNo = z.FaxNo,
                    Id = z.Id,
                    Phoneno = z.PhoneNo,
                    Pincode = z.Pincode,
                    Remarks = z.Remarks,
                    State = z.State,
                    TenantId = z.Tenant_Id
                }).ToList(),
                CompanyDocumentsExist=db.tbl_tenant_company_government_doc.Where(r=>r.Tenant_Id==tenantCompany.Tenant_Id).Select(r=>new CompanyDocuments()
                {
                    Type=r.Type,Doc_name =r.Doc_name,Doc_path=r.Doc_Path,Tenant_Id= tenantCompany.Tenant_Id
                }).ToList()

            };
            return PartialView("../Master/TenantCompany/_AddOrUpdate", tenantCompanyData);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(TenantCompanyViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = null;
                    if (model.Id == 0)
                    {
                        var tenantId = db.tbl_tenant_company.OrderByDescending(x => x.Id).FirstOrDefault();
                        model.TenantId = tenantId != null ? tenantId.Tenant_Id + 1 : 1;
                        PFlag = "INSERT";
                    }
                    else
                    {
                        PFlag = "UPDATE";
                    }
                    
                    string companyDet = null;
                    foreach (var item in model.CompanyDetails)
                    {
                        if (string.IsNullOrWhiteSpace(companyDet))
                        {
                            companyDet = "(" + model.TenantId + ",'" + item.Branch + "','" + item.BranchAddress +
                                "','" + item.BranchAddress1 + "','" + item.City + "','" + item.State + "','" + item.Country + "','" + item.Pincode
                                + "','" + item.Phoneno + "','" + item.EmailId + "','" + item.FaxNo + "','" + item.Remarks + "','" + DateTime.Now.Year + "')";
                        }
                        else
                        {
                            companyDet += ",(" + model.TenantId + ",'" + item.Branch + "','" + item.BranchAddress +
                               "','" + item.BranchAddress1 + "','" + item.City + "','" + item.State + "','" + item.Country + "','" + item.Pincode
                               + "','" + item.Phoneno + "','" + item.EmailId + "','" + item.FaxNo + "','" + item.Remarks + "','" + DateTime.Now.Year + "')";
                        }
                    }
                    string companyContactDetails = null;
                    if (model.CompanyContactDetails != null)
                    {
                        foreach (var item in model.CompanyContactDetails)
                        {

                            if (string.IsNullOrWhiteSpace(companyContactDetails))
                            {
                                companyContactDetails = "(" + model.TenantId + ",'" + item.ContactPersonName + "','" + item.Designation +
                                        "','" + item.Department + "','" + item.Phone + "','" + item.Extension + "','" + item.MobileNo +
                                        "','" + item.Salutations + "','" + DateTime.Now.Year + "')";
                            }
                            else
                            {
                                companyContactDetails += ",(" + model.TenantId + ",'" + item.ContactPersonName + "','" + item.Designation +
                                        "','" + item.Department + "','" + item.Phone + "','" + item.Extension + "','" + item.MobileNo +
                                        "','" + item.Salutations + "','" + DateTime.Now.Year + "')";
                            }
                        }
                    }
                    string companyDoc = null;
                    if(model.CompanyDocumentsExist!=null)
                    {
                        foreach(var item in model.CompanyDocumentsExist)
                        {
                            companyDoc += !string.IsNullOrWhiteSpace(companyDoc) ?
                                ",(" + model.TenantId + ",'" + item.Type + "','" + item.Doc_name + "','" + item.Doc_path + "')" :
                                "(" + model.TenantId + ",'" + item.Type + "','" + item.Doc_name + "','" + item.Doc_path + "')";
                        }
                    }
                    if(model.CompanyNewDocuments != null)
                    {
                        foreach (var item in model.CompanyNewDocuments)
                        {
                            //TenantCompany
                            Guid guid = Guid.NewGuid();
                            try
                            {
                                if (item.File != null)
                                {
                                    HttpPostedFileBase file = item.File; //Uploaded file
                                    //int fileSize = file.ContentLength;
                                    string fileName = file.FileName;
                                    //string mimeType = file.ContentType;
                                    //System.IO.Stream fileContent = file.InputStream;
                                    fileName = guid + fileName;
                                    //To save file, use SaveAs method
                                    file.SaveAs(Server.MapPath("~/" + Common.TenantCompanyDocumentContainer) + fileName); //File will be saved in application root


                                    companyDoc += !string.IsNullOrWhiteSpace(companyDoc) ?
                                                ",(" + model.TenantId + ",'" + item.Type + "','" + item.Name + "','" + fileName + "')" :
                                                "(" + model.TenantId + ",'" + item.Type + "','" + item.Name + "','" + fileName + "')";

                                }
                            }
                            catch
                            {

                            }
                            
                        }
                    }

                    object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PTenant_Id", model.TenantId),
                         new MySqlParameter("@PCompanyName",model.CompanyName),
                         new MySqlParameter("@PMarital_Status", model.MaritalStatus),
                         new MySqlParameter("@PTitle", model.Title),
                         new MySqlParameter("@PFirst_Name", model.FirstName),
                         new MySqlParameter("@PMiddle_Name", model.MiddleName),
                         new MySqlParameter("@PLast_Name", model.LastName),
                         new MySqlParameter("@Paddress", model.Address),
                         new MySqlParameter("@Paddress1", model.Address1),
                         new MySqlParameter("@PEmirate", model.Emirate),
                         new MySqlParameter("@PCity", model.City),
                         new MySqlParameter("@PPostboxNo", model.PostboxNo),
                         new MySqlParameter("@PEmail", model.Email),
                         new MySqlParameter("@PMobile_Countrycode", model.MobileCountryCode),
                         new MySqlParameter("@PMobile_Areacode", model.MobileAreaCode),
                         new MySqlParameter("@PMobile_No", model.MobileNo),
                         new MySqlParameter("@PLandline_Countrycode", model.LandlineCountryCode),
                         new MySqlParameter("@PLandline_Areacode", model.LandlineAreaCode),
                         new MySqlParameter("@PLandline_No", model.LandlineNo),
                         new MySqlParameter("@PFax_Countrycode", model.FaxCountryCode),
                         new MySqlParameter("@PFax_Areacode", model.FaxAreaCode),
                         new MySqlParameter("@PFax_No", model.FaxNo),
                         new MySqlParameter("@PNationality", model.Nationality),
                         new MySqlParameter("@PActitvity", model.ComapanyActivity),
                         new MySqlParameter("@PCocandindustryuid", model.Cocandindustryuid),
                         new MySqlParameter("@PTradelicenseNo", model.TradelicenseNo),
                         new MySqlParameter("@PLicense_issueDate", model.LicenseIssueDate),
                         new MySqlParameter("@PLicense_ExpiryDate", model.LicenseExpiryDate),
                         new MySqlParameter("@PIssuance_authority", model.Issuance_authority),
                         new MySqlParameter("@PADWEA_Regid", model.ADWEARegid),
                         new MySqlParameter("@PType", model.TenantType),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name),
                         new MySqlParameter("@Ptenant_companydt", companyDet),
                         new MySqlParameter("@Ptenant_companydt1", companyContactDetails),
                         new MySqlParameter("@Ptenant_companydoc", companyDoc)

                    };
                    var tenantCompany = await db.Database.SqlQuery<object>("CALL Usp_Tenant_Company_All(@PFlag, @PTenant_Id, @PCompanyName, @PMarital_Status, @PTitle, @PFirst_Name, @PMiddle_Name, @PLast_Name, @Paddress, @Paddress1, @PEmirate, @PCity, @PPostboxNo, @PEmail, @PMobile_Countrycode, @PMobile_Areacode, @PMobile_No, @PLandline_Countrycode, @PLandline_Areacode, @PLandline_No, @PFax_Countrycode, @PFax_Areacode, @PFax_No, @PNationality, @PActitvity, @PCocandindustryuid, @PTradelicenseNo, @PLicense_issueDate, @PLicense_ExpiryDate, @PIssuance_authority, @PADWEA_Regid, @PType, @PCreateduser, @Ptenant_companydt, @Ptenant_companydt1, @Ptenant_companydoc)", parameters).ToListAsync();
                }
                return RedirectToAction("../Master/Index", new { selected = 10 });
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return RedirectToAction("../Master/Index", new { selected = 10 });
            }


        }

        public async Task<ActionResult> Delete(int id)
        {
            MessageResult result = new MessageResult();
            try
            {
                var tenantCompany = await db.tbl_tenant_company.FirstOrDefaultAsync(x => x.Tenant_Id == id);
                if (tenantCompany == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] parameters = {
                     new MySqlParameter("@PFlag", "DELETE"),
                     new MySqlParameter("@PTenant_Id", id),
                     new MySqlParameter("@PCompanyName", ""),
                     new MySqlParameter("@PMarital_Status", ""),
                     new MySqlParameter("@PTitle", ""),
                     new MySqlParameter("@PFirst_Name", ""),
                     new MySqlParameter("@PMiddle_Name", ""),
                     new MySqlParameter("@PLast_Name", ""),
                     new MySqlParameter("@Paddress", ""),
                     new MySqlParameter("@Paddress1", ""),
                     new MySqlParameter("@PEmirate", ""),
                     new MySqlParameter("@PCity", ""),
                     new MySqlParameter("@PPostboxNo", ""),
                     new MySqlParameter("@PEmail", ""),
                     new MySqlParameter("@PMobile_Countrycode", ""),
                     new MySqlParameter("@PMobile_Areacode", ""),
                     new MySqlParameter("@PMobile_No", ""),
                     new MySqlParameter("@PLandline_Countrycode", ""),
                     new MySqlParameter("@PLandline_Areacode", ""),
                     new MySqlParameter("@PLandline_No", ""),
                     new MySqlParameter("@PFax_Countrycode", ""),
                     new MySqlParameter("@PFax_Areacode", ""),
                     new MySqlParameter("@PFax_No", ""),
                     new MySqlParameter("@PNationality", ""),
                     new MySqlParameter("@PActitvity", ""),
                     new MySqlParameter("@PCocandindustryuid", ""),
                     new MySqlParameter("@PTradelicenseNo", ""),
                     new MySqlParameter("@PLicense_issueDate", "1991-10-12"),
                     new MySqlParameter("@PLicense_ExpiryDate", "1991-10-12"),
                     new MySqlParameter("@PIssuance_authority", ""),
                     new MySqlParameter("@PADWEA_Regid", ""),
                     new MySqlParameter("@PType", ""),
                     new MySqlParameter("@PCreateduser", ""),
                     new MySqlParameter("@Ptenant_companydt", ""),
                     new MySqlParameter("@Ptenant_companydt1", ""),
                     new MySqlParameter("@Ptenant_companydoc", "")

                };
                var spResult = await db.Database.SqlQuery<object>("CALL Usp_Tenant_Company_All(@PFlag, @PTenant_Id, @PCompanyName, @PMarital_Status, @PTitle, @PFirst_Name, @PMiddle_Name, @PLast_Name, @Paddress, @Paddress1, @PEmirate, @PCity, @PPostboxNo, @PEmail, @PMobile_Countrycode, @PMobile_Areacode, @PMobile_No, @PLandline_Countrycode, @PLandline_Areacode, @PLandline_No, @PFax_Countrycode, @PFax_Areacode, @PFax_No, @PNationality, @PActitvity, @PCocandindustryuid, @PTradelicenseNo, @PLicense_issueDate, @PLicense_ExpiryDate, @PIssuance_authority, @PADWEA_Regid, @PType, @PCreateduser, @Ptenant_companydt, @Ptenant_companydt1, @Ptenant_companydoc)", parameters).ToListAsync();
                
                //await db.SaveChangesAsync();
                //tbl_country.Delmark = "*";
                //db.Entry(tbl_country).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}