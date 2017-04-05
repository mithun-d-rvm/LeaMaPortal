using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.Models.DBContext;
using LeaMaPortal.Models;
using MvcPaging;
using LeaMaPortal.Helpers;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LeaMaPortal.Controllers
{
    public class PropertyTypeController : Controller
    {
        private Entities db = new Entities();

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
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_propertytypemaster.Where(x => x.Delmark != "*").OrderBy(x => x.Type_name).Select(x => new PropertyTypeViewModel()
                    {
                        Id = x.Id,
                        PropertyType = x.Type_name,
                        PropertyCategory = x.Type_Flag,
                        Usage_name = x.Usage_name
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_propertytypemaster.Where(x => x.Delmark != "*"
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
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";

                    if (model.Id == 0)
                    {

                    }
                    else
                    {
                        PFlag = "UPDATE";
                    }
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PType_name",model.PropertyType),
                                            new MySqlParameter("@PType_Flag",model.PropertyCategory),
                                            new MySqlParameter("@PUsage_name",model.Usage_name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Propertytype_All(@PFlag,@PId,@PType_name,@PType_Flag,@PUsage_name,@PCreateduser)", param).ToListAsync();
                    await db.SaveChangesAsync();

                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> Edit(string PropertyType, string PropertyCategory, string Usage_name)
        {
            try
            {
                if (PropertyType == null && PropertyCategory != null && Usage_name != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_propertytypemaster tbl_propertytype = await db.tbl_propertytypemaster.FindAsync(PropertyType, PropertyCategory, Usage_name);
                if (tbl_propertytype == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                PropertyTypeViewModel model = new PropertyTypeViewModel()
                {
                    Id = tbl_propertytype.Id,
                    PropertyType = tbl_propertytype.Type_name,
                    PropertyCategory = tbl_propertytype.Type_Flag,
                    Usage_name = tbl_propertytype.Usage_name
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(string PropertyType, string PropertyCategory, string Usage_name)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (PropertyType == null && PropertyCategory != null && Usage_name != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_propertytypemaster tbl_propertytype = await db.tbl_propertytypemaster.FindAsync(PropertyType, PropertyCategory, Usage_name);
                if (tbl_propertytype == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_propertytype.Id),
                                           new MySqlParameter("@PType_name",tbl_propertytype.Type_name),
                                            new MySqlParameter("@PType_Flag",tbl_propertytype.Type_Flag),
                                            new MySqlParameter("@PUsage_name",tbl_propertytype.Usage_name),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Propertytype_All(@PFlag,@PId,@PType_name,@PType_Flag,@PUsage_name,@PCreateduser)", param).ToListAsync();
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
