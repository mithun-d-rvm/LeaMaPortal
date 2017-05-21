using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using LeaMaPortal.Models;
using MvcPaging;
using MySql.Data.MySqlClient;
using System.Data.Entity;

namespace LeaMaPortal.Controllers
{
    public class SupplierController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: Supplier
        public async Task<PartialViewResult> Index(string Search, int? page, int? defaultPageSize)
        {
            ViewData["Search"] = Search;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
            ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
            var suppliers = db.tbl_suppliermaster.Where(x => x.Delmark != "*");
            if (!string.IsNullOrWhiteSpace(Search))
            {
                suppliers = suppliers.Where(x => x.Supplier_Name.Contains(Search));
            }
            return PartialView("../Master/Supplier/_List", suppliers.OrderBy(x=>x.Supplier_Id).Select(x => new SupplierViewModel()
            {
                Accyear = x.Accyear,
                Actitvity = x.Actitvity,
                address = x.address,
                address1 = x.address1,
                ADWEA_Regid = x.ADWEA_Regid,
                City = x.City,
                Cocandindustryuid = x.Cocandindustryuid,
                Createddatetime = x.Createddatetime,
                Createduser = x.Createduser,
                Delmark = x.Delmark,
                Email = x.Email,
                Emirate = x.Emirate,
                Fax_Areacode = x.Fax_Areacode,
                Fax_Countrycode = x.Fax_Countrycode,
                Fax_No = x.Fax_No,
                First_Name = x.First_Name,
                id = x.id,
                Issuance_authority = x.Issuance_authority,
                Landline_Areacode = x.Landline_Areacode,
                Landline_Countrycode = x.Landline_Countrycode,
                Landline_No = x.Landline_No,
                Last_Name = x.Last_Name,
                License_ExpiryDate = x.License_ExpiryDate,
                License_issueDate = x.License_issueDate,
                Locationlink = x.Locationlink,
                Marital_Status = x.Marital_Status,
                Middle_Name = x.Middle_Name,
                Mobile_Areacode = x.Mobile_Areacode,
                Mobile_Countrycode = x.Mobile_Countrycode,
                Mobile_No = x.Mobile_No,
                Nationality = x.Nationality,
                PostboxNo = x.PostboxNo,
                Supplier_Id = x.Supplier_Id,
                Supplier_Name = x.Supplier_Name,
                Title = x.Title,
                TradelicenseNo = x.TradelicenseNo,
                Type = x.Type
            }).ToPagedList(currentPageIndex, PageSize));
        }

        [HttpGet]
        public async Task<PartialViewResult> AddOrUpdate()
        {
            //var supplierId = await db.tbl_suppliermaster.Select(x => x.Supplier_Id).DefaultIfEmpty(0).MaxAsync();
            var supplierId = db.tbl_suppliermaster.OrderByDescending(x => x.id).FirstOrDefault();
            ViewBag.SupplierId = supplierId != null ? supplierId.Supplier_Id + 1 : 1;


            //int? supplierId = await db.tbl_suppliermaster.MaxAsync(x => (int?)x.Supplier_Id);
            //ViewBag.SupplierId = supplierId == null ? 1 : supplierId + 1;

            ViewBag.Type = new SelectList(Common.TenantType);

            //var _titleResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','Title',',',null)").ToList();
            var _titleResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant individual" && x.comboname == "Title");
            if (_titleResult != null)
            {
                ViewBag.Title = new SelectList(_titleResult.combovalue.Split(','));
            }

            //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
            var _emirateResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Emirate");
            if (_emirateResult != null)
            {
                ViewBag.Emirate = new SelectList(_emirateResult.combovalue.Split(','));
            }
            //ViewBag.Emirate = new SelectList(_emirateResult);

            //var _companyActivity = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Actitvity',',',null);").ToList();
            var _companyActivity = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Actitvity");
            if (_companyActivity != null)
            {
                ViewBag.Actitvity = new SelectList(_companyActivity.combovalue.Split(','));
            }
            //ViewBag.Actitvity = new SelectList(_companyActivity);

            ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");
            
            ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name");
            
            ViewBag.Issuance_authority = new SelectList(Common.Issuance_authority);
            return PartialView("../Master/Supplier/_AddOrUpdate", new SupplierViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(SupplierViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = null;
                    if (model.id == 0)
                    {
                        var supplierId = db.tbl_suppliermaster.OrderByDescending(x => x.id).FirstOrDefault();
                        model.Supplier_Id = supplierId != null ? supplierId.Supplier_Id + 1 : 1;
                        PFlag = "INSERT";
                        result.Message = "Supplier created successfully";
                    }
                    else
                    {
                        PFlag = "UPDATE";
                        result.Message = "supplier updated successfully";
                    }

                    string companyDet = null;
                    if (model.SupplierdtList != null)
                    {
                        foreach (var item in model.SupplierdtList)
                        {
                            if (string.IsNullOrWhiteSpace(companyDet))
                            {
                                companyDet = "(" + model.Supplier_Id + ",'" + item.Branch + "','" + item.BranchAdd +
                                    "','" + item.Branchadd1 + "','" + item.City + "','" + item.State + "','" + item.Country + "','" + item.Pincode
                                    + "','" + item.PhoneNo + "','" + item.EMailID + "','" + item.FaxNo + "','" + item.Remarks + "','" + DateTime.Now.Year + "')";
                            }
                            else
                            {
                                companyDet += ",(" + model.Supplier_Id + ",'" + item.Branch + "','" + item.BranchAdd +
                                   "','" + item.Branchadd1 + "','" + item.City + "','" + item.State + "','" + item.Country + "','" + item.Pincode
                                   + "','" + item.PhoneNo + "','" + item.EMailID + "','" + item.FaxNo + "','" + item.Remarks + "','" + DateTime.Now.Year + "')";
                            }
                        }
                    }
                    model.supplierdt = companyDet;
                    string companyContactDetails = null;
                    if (model.Supplierdt1List != null)
                    {
                        foreach (var item in model.Supplierdt1List)
                        {

                            if (string.IsNullOrWhiteSpace(companyContactDetails))
                            {
                                companyContactDetails = "(" + model.Supplier_Id + ",'"+ item.Cpercode+ "','" + item.Cper + "','" + item.Desig +
                                        "','" + item.Dept + "','" + item.Phone + "','" + item.Ext + "','" + item.MobNo +
                                        "','" + item.Salutations + "','" + DateTime.Now.Year + "')";
                            }
                            else
                            {
                                companyContactDetails += ",(" + model.Supplier_Id + ",'" + item.Cpercode + "','" + item.Cper + "','" + item.Desig +
                                        "','" + item.Dept + "','" + item.Phone + "','" + item.Ext + "','" + item.MobNo +
                                        "','" + item.Salutations + "','" + DateTime.Now.Year + "')";
                            }
                        }
                    }
                    model.supplierdt1 = companyContactDetails;

                    object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PSupplier_Id", model.Supplier_Id),
                         new MySqlParameter("@PSupplier_Name",model.Supplier_Name),
                         new MySqlParameter("@PMarital_Status", model.Marital_Status),
                         new MySqlParameter("@PTitle", model.Title),
                         new MySqlParameter("@PFirst_Name", model.First_Name),
                         new MySqlParameter("@PMiddle_Name", model.Middle_Name),
                         new MySqlParameter("@PLast_Name", model.Last_Name),
                         new MySqlParameter("@Paddress", model.address),
                         new MySqlParameter("@Paddress1", model.address1),
                         new MySqlParameter("@PLocationlink", model.Locationlink),
                         new MySqlParameter("@PEmirate", model.Emirate),
                         new MySqlParameter("@PCity", model.City),
                         new MySqlParameter("@PPostboxNo", model.PostboxNo),
                         new MySqlParameter("@PEmail", model.Email),
                         new MySqlParameter("@PMobile_Countrycode", model.Mobile_Countrycode),
                         new MySqlParameter("@PMobile_Areacode", model.Mobile_Areacode),
                         new MySqlParameter("@PMobile_No", model.Mobile_No),
                         new MySqlParameter("@PLandline_Countrycode", model.Landline_Countrycode),
                         new MySqlParameter("@PLandline_Areacode", model.Landline_Areacode),
                         new MySqlParameter("@PLandline_No", model.Landline_No),
                         new MySqlParameter("@PFax_Countrycode", model.Fax_Countrycode),
                         new MySqlParameter("@PFax_Areacode", model.Fax_Areacode),
                         new MySqlParameter("@PFax_No", model.Fax_No),
                         new MySqlParameter("@PNationality", model.Nationality),
                         new MySqlParameter("@PActitvity", model.Actitvity),
                         new MySqlParameter("@PCocandindustryuid", model.Cocandindustryuid),
                         new MySqlParameter("@PTradelicenseNo", model.TradelicenseNo),
                         new MySqlParameter("@PLicense_issueDate", model.License_issueDate),
                         new MySqlParameter("@PLicense_ExpiryDate", model.License_ExpiryDate),
                         new MySqlParameter("@PIssuance_authority", model.Issuance_authority),
                         new MySqlParameter("@PADWEA_Regid", model.ADWEA_Regid),
                         new MySqlParameter("@PType", model.Type),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name),
                         new MySqlParameter("@Psupplierdt", model.supplierdt),
                         new MySqlParameter("@Psupplierdt1", model.supplierdt1)
                    };



                    //object[] param = Helper.GetMySqlParameters<SupplierViewModel>(model, PFlag, System.Web.HttpContext.Current.User.Identity.Name);

                    var _result = await db.Database.SqlQuery<object>(@"call Usp_Supplier_All(@PFlag, @PSupplier_Id, @PSupplier_Name, @PMarital_Status, @PTitle, @PFirst_Name, @PMiddle_Name, @PLast_Name, @Paddress, @Paddress1, @PLocationlink, @PEmirate, @PCity, @PPostboxNo, @PEmail, @PMobile_Countrycode, @PMobile_Areacode, @PMobile_No, @PLandline_Countrycode, @PLandline_Areacode, @PLandline_No, @PFax_Countrycode, @PFax_Areacode, @PFax_No, @PNationality, @PActitvity, @PCocandindustryuid, @PTradelicenseNo, @PLicense_issueDate, @PLicense_ExpiryDate, @PIssuance_authority, @PADWEA_Regid, @PType, @PCreateduser, @Psupplierdt, @Psupplierdt1)", parameters).ToListAsync();
                }
                return RedirectToAction("../Master/Index", new { selected = 15 });
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return RedirectToAction("../Master/Index", new { selected = 15 });
            }


        }

        //// GET: Supplier/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Supplier/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Supplier/Edit/5
        public async Task<PartialViewResult> Edit(int Supplier_Id)
        {
            try
            {
                var supplier = await db.tbl_suppliermaster.FirstOrDefaultAsync(x => x.Supplier_Id == Supplier_Id);
                if (supplier == null)
                {
                    return PartialView("../Master/Supplier/_AddOrUpdate", new SupplierViewModel());
                }
                var supplierViewData = Map(supplier);
                supplierViewData.SupplierdtList = await db.tbl_supplierdt.Where(x => x.Supplier_Id == Supplier_Id).Select(x => new Supplierdt()
                {
                    Accyear = x.Accyear,
                    Branch = x.Branch,
                    BranchAdd = x.BranchAdd,
                    Branchadd1 = x.Branchadd1,
                    City = x.City,
                    Country = x.Country,
                    Delmark = x.Delmark,
                    EMailID = x.EMailID,
                    FaxNo = x.FaxNo,
                    id = x.id,
                    PhoneNo = x.PhoneNo,
                    Pincode = x.Pincode,
                    Remarks = x.Remarks,
                    State = x.State,
                    Supplier_Id = x.Supplier_Id
                }).ToListAsync();

                supplierViewData.Supplierdt1List = await db.tbl_supplierdt1.Where(x => x.Supplier_Id == Supplier_Id).Select(x => new Supplierdt1()
                {
                    Accyear = x.Accyear,
                    Cper = x.Cper,
                    Cpercode = x.Cpercode,
                    Delmark = x.Delmark,
                    Dept = x.Dept,
                    Desig = x.Desig,
                    Ext = x.Ext,
                    id = x.id,
                    MobNo = x.MobNo,
                    Phone = x.Phone,
                    Salutations = x.Salutations,
                    Supplier_Id = x.Supplier_Id
                }).ToListAsync();

                //var supplierId = db.tbl_suppliermaster.OrderBy(x => x.Supplier_Id).FirstOrDefault();
                ViewBag.SupplierId = supplierViewData.Supplier_Id;

                ViewBag.Type = new SelectList(Common.TenantType, supplierViewData.Type);

                var _titleResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant individual" && x.comboname == "Title");
                if (_titleResult != null)
                {
                    ViewBag.Title = new SelectList(_titleResult.combovalue.Split(','), supplierViewData.Title);
                }

                //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
                var _emirateResult = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Emirate");
                if (_emirateResult != null)
                {
                    ViewBag.Emirate = new SelectList(_emirateResult.combovalue.Split(','), supplierViewData.Emirate);
                }
                //ViewBag.Emirate = new SelectList(_emirateResult);

                //var _companyActivity = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Actitvity',',',null);").ToList();
                var _companyActivity = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Tenant Company" && x.comboname == "Actitvity");
                if (_companyActivity != null)
                {
                    ViewBag.Actitvity = new SelectList(_companyActivity.combovalue.Split(','), supplierViewData.Actitvity);
                }
                //ViewBag.Actitvity = new SelectList(_companyActivity);


                //var _titleResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant individual','Title',',',null)").ToList();
                //ViewBag.Title = new SelectList(_titleResult, supplierViewData.Title);

                //var _emirateResult = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Emirate',',',null)").ToList();
                //ViewBag.Emirate = new SelectList(_emirateResult, supplierViewData.Emirate);

                //var _companyActivity = db.Database.SqlQuery<string>(@"call usp_split('Tenant Company','Actitvity',',',null);").ToList();
                //ViewBag.Actitvity = new SelectList(_companyActivity, supplierViewData.Actitvity);

                ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name", supplierViewData.City);

                ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name", supplierViewData.Nationality);

                ViewBag.Issuance_authority = new SelectList(Common.Issuance_authority, supplierViewData.Issuance_authority);
                return PartialView("../Master/Supplier/_AddOrUpdate", supplierViewData);
            }
            catch(Exception ex)
            {
                return PartialView("../Master/Supplier/_AddOrUpdate", new SupplierViewModel());
            }
            


        }

        // POST: Supplier/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        // POST: Supplier/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Supplier_Id)
        {
            try
            {
                MessageResult result = new MessageResult();
                try
                {
                    var model = await db.tbl_suppliermaster.FirstOrDefaultAsync(r => r.Supplier_Id == Supplier_Id);
                    object[] param = Helper.GetMySqlParameters<SupplierViewModel>(Map(model), Common.DELETE, System.Web.HttpContext.Current.User.Identity.Name);

                    var _result = await db.Database.SqlQuery<object>(@"call Usp_Supplier_All(@PFlag, @PSupplier_Id, @PSupplier_Name, @PMarital_Status, @PTitle, @PFirst_Name, @PMiddle_Name, @PLast_Name, @Paddress, @Paddress1, @PLocationlink, @PEmirate, @PCity, @PPostboxNo, @PEmail, @PMobile_Countrycode, @PMobile_Areacode, @PMobile_No, @PLandline_Countrycode, @PLandline_Areacode, @PLandline_No, @PFax_Countrycode, @PFax_Areacode, @PFax_No, @PNationality, @PActitvity, @PCocandindustryuid, @PTradelicenseNo, @PLicense_issueDate, @PLicense_ExpiryDate, @PIssuance_authority, @PADWEA_Regid, @PType, @PCreateduser, @Psupplierdt, @Psupplierdt1)", param).ToListAsync();
                    result.Message = "Supplier deleted successfully";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
        private SupplierViewModel Map(tbl_suppliermaster model)
        {
            SupplierViewModel viewModel = new SupplierViewModel()
            {
                Accyear = model.Accyear,
                Actitvity = model.Actitvity,
                address = model.address,
                address1 = model.address1,
                ADWEA_Regid = model.ADWEA_Regid,
                City = model.City,
                Cocandindustryuid = model.Cocandindustryuid,
                Createddatetime = model.Createddatetime,
                Createduser = model.Createduser,
                Delmark = model.Delmark,
                Email = model.Email,
                Emirate = model.Emirate,
                Fax_Areacode = model.Fax_Areacode,
                Fax_Countrycode = model.Fax_Countrycode,
                Fax_No = model.Fax_No,
                First_Name = model.First_Name,
                id = model.id,
                Issuance_authority = model.Issuance_authority,
                Landline_Areacode = model.Landline_Areacode,
                Landline_Countrycode = model.Landline_Countrycode,
                Landline_No = model.Landline_No,
                Last_Name = model.Last_Name,
                License_ExpiryDate = model.License_ExpiryDate,
                License_issueDate = model.License_issueDate,
                Locationlink = model.Locationlink,
                Marital_Status = model.Marital_Status,
                Middle_Name = model.Middle_Name,
                Mobile_Areacode = model.Mobile_Areacode,
                Mobile_Countrycode = model.Mobile_Countrycode,
                Mobile_No = model.Mobile_No,
                Nationality = model.Nationality,
                PostboxNo = model.PostboxNo,
                Supplier_Id = model.Supplier_Id,
                Supplier_Name = model.Supplier_Name,
                Title = model.Title,
                TradelicenseNo = model.TradelicenseNo,
                Type = model.Type
            };
            return viewModel;
        }
    }
}
