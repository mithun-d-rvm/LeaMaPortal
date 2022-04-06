﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using LeaMaPortal.Models;
using MvcPaging;
using LeaMaPortal.Helpers;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LeaMaPortal.Controllers
{
    [Authorize]
    public class PropertyTypeController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: PropertyType
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<PropertyTypeViewModel> list;
                string regname = Session["Region"].ToString();
                if (string.IsNullOrWhiteSpace(Search))
                {
                   
                    list = db.tbl_propertytypemaster.Where(x => x.Delmark != "*" && x.Region_Name == regname).OrderBy(x => x.Type_name).Select(x => new PropertyTypeViewModel()
                    {
                        Id = x.Id,
                        PropertyType = x.Type_name,
                        PropertyCategory = x.Type_Flag,
                        Usage_name = x.Usage_name
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_propertytypemaster.Where(x => x.Delmark != "*" && x.Region_Name == regname
                                    && (x.Type_name.ToLower().Contains(Search.ToLower())
                                    || x.Usage_name.ToLower().Contains(Search.ToLower())))
                                  .OrderBy(x => x.Type_name).Select(x => new PropertyTypeViewModel()
                                  {
                                      Id = x.Id,
                                      PropertyType = x.Type_name,
                                      PropertyCategory = x.Type_Flag,
                                      Usage_name = x.Usage_name
                                  }).ToPagedList(currentPageIndex, PageSize);
                }

                return PartialView("../Master/PropertyType/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            PropertyTypeViewModel model = new PropertyTypeViewModel();
            ViewBag.PropertyType = new SelectList(StaticHelper.GetStaticData(StaticHelper.PROPERTYTYPE_DROPDOWN), "Name", "Name");
            ViewBag.PropertyCategory = new SelectList(StaticHelper.GetStaticData(StaticHelper.PROPERTYCATEGORY_DROPDOWN), "Name", "Name");
            ViewBag.Usage_name = new SelectList(new List<OptionModel>());
            return PartialView("../Master/PropertyType/_AddOrUpdate", model);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "PropertyType,Usage_name,PropertyCategory,Id")] PropertyTypeViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                string regname = Session["Region"].ToString();
                if (ModelState.IsValid)
                {
                    if (db.tbl_propertytypemaster.Any(a => a.Type_name == model.PropertyCategory
                         && a.Usage_name == model.Usage_name
                          && a.Type_Flag==model.PropertyType && a.Id!=model.Id && a.Region_Name == regname  ))
                    {
                        result.Errors = "Property type already exists!";
                    }
                    else
                    {
                        MySqlParameter pa = new MySqlParameter();
                        string PFlag = "INSERT";
                        result.Message = "Property type created successfully";
                        if (model.Id != 0)
                        { 
                            PFlag = "UPDATE";
                            result.Message = "Property type updated successfully";
                        }

                     
                        if (regname != "")
                        {
                            model.Region_Name = regname;

                            var countryname = db.tbl_region.Where(x => x.Region_Name == model.Region_Name).OrderByDescending(x => x.Id).FirstOrDefault();

                            model.Country = countryname.Country;
                        }
                        object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PType_name",model.PropertyType),
                                            new MySqlParameter("@PType_Flag",model.PropertyCategory),
                                            new MySqlParameter("@PUsage_name",model.Usage_name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter ("@PRegion_Name",model.Region_Name ),
                                           new MySqlParameter ("@PCountry",model.Country )
                                         };
                        var RE = await db.Database.SqlQuery<object>("CALL Usp_Propertytype_All(@PFlag,@PId,@PType_name,@PType_Flag,@PUsage_name,@PCreateduser,@PRegion_Name,@PCountry)", param).ToListAsync();
                        await db.SaveChangesAsync();
                    }
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id==null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_propertytypemaster tbl_propertytype = await db.tbl_propertytypemaster.FirstOrDefaultAsync(x => x.Id == id);
                if (tbl_propertytype == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                PropertyTypeViewModel model = new PropertyTypeViewModel()
                {
                    Id = tbl_propertytype.Id,
                    PropertyType = tbl_propertytype.Type_Flag,
                    PropertyCategory = tbl_propertytype.Type_name,
                    Usage_name = tbl_propertytype.Usage_name
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(int? id)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (id== null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_propertytypemaster tbl_propertytype = await db.tbl_propertytypemaster.FirstOrDefaultAsync(x => x.Id == id);
                if (tbl_propertytype == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_propertytype.Id),
                                           new MySqlParameter("@PType_name",tbl_propertytype.Type_name),
                                            new MySqlParameter("@PType_Flag",tbl_propertytype.Type_Flag),
                                            new MySqlParameter("@PUsage_name",tbl_propertytype.Usage_name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter("@PRegion_Name",tbl_propertytype.Region_Name ),
                                           new MySqlParameter ("@PCountry",tbl_propertytype.Country )
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Propertytype_All(@PFlag,@PId,@PType_name,@PType_Flag,@PUsage_name,@PCreateduser,@PRegion_Name,@PCountry)", param).ToListAsync();
                result.Message = "Property details deleted successfully";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetUsage(string PropertyType)
        {
            try
            {
                List<OptionModel> model = new List<OptionModel>();
                if (PropertyType.ToLower() == StaticHelper.PROPERTYTYPE_PROPERTY.ToLower())
                {
                    return Json(new SelectList(StaticHelper.GetStaticData(StaticHelper.PROPERTY), "Name", "Name"));
                }
                else
                {
                    return Json(new SelectList(StaticHelper.GetStaticData(StaticHelper.UNIT), "Name", "Name"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
