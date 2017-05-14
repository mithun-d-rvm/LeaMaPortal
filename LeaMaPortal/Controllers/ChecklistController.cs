using System;
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
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using LeaMaPortal.Helpers;

namespace LeaMaPortal.Controllers
{
    public class ChecklistController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Checklist
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<CheckListViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_checklistmaster.Where(x => x.Delmark != "*").OrderBy(x => x.Checklist_Name).Select(x => new CheckListViewModel()
                    {
                        Id = x.Id,
                        Checklist_id = x.Checklist_id,
                        Checklist_Name = x.Checklist_Name,
                        Checklist_Type = x.Check_type
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_checklistmaster.Where(x => x.Delmark != "*"
                                    && (x.Checklist_id.ToLower().Contains(Search.ToLower())
                                    || x.Checklist_Name.ToLower().Contains(Search.ToLower())))
                                  .OrderBy(x => x.Checklist_Name).Select(x => new CheckListViewModel()
                                  {
                                      Id = x.Id,
                                      Checklist_id = x.Checklist_id,
                                      Checklist_Name = x.Checklist_Name,
                                      Checklist_Type = x.Check_type
                                  }).ToPagedList(currentPageIndex, PageSize);
                }

                return PartialView("../Master/CheckList/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            CheckListViewModel model = new CheckListViewModel();
            ViewBag.Checklist_Type = new SelectList(StaticHelper.GetStaticData(StaticHelper.CHECKLIST_DROPDOWN), "Name", "Name");
            model.Checklist_id = db.tbl_checklistmaster.OrderByDescending(o => o.Id).Select(s => s.Checklist_id).FirstOrDefault();
            model.Checklist_id = (Convert.ToInt32(model.Checklist_id) + 1).ToString();
            return PartialView("../Master/CheckList/_AddOrUpdate", model);
        }
        // POST: CheckList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Checklist_id,Checklist_Name,Checklist_Type,Id")] CheckListViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    var checkList = await db.tbl_checklistmaster.FirstOrDefaultAsync(r => r.Checklist_Name.ToLower() == model.Checklist_Name.ToLower());
                    if (checkList != null)
                    {
                        result.Errors = "Check list name already added";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
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
                                           new MySqlParameter("@PChecklist_id",model.Checklist_id),
                                            new MySqlParameter("@PChecklist_Name",model.Checklist_Name),
                                            new MySqlParameter("@Pcheck_type",model.Checklist_Type),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Checklist_All(@PFlag,@PId,@PChecklist_id,@PChecklist_Name,@Pcheck_type,@PCreateduser)", param).ToListAsync();
                    await db.SaveChangesAsync();

                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> Edit(string CheckListId, string CheckListName)
        {
            try
            {
                if (CheckListId == null && CheckListName != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_checklistmaster tbl_checklist = await db.tbl_checklistmaster.FindAsync(CheckListId, CheckListName);
                if (tbl_checklist == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                CheckListViewModel model = new CheckListViewModel()
                {
                    Id = tbl_checklist.Id,
                    Checklist_id = tbl_checklist.Checklist_id,
                    Checklist_Name = tbl_checklist.Checklist_Name,
                    Checklist_Type = tbl_checklist.Check_type
                };
                ViewBag.Checklist_Type = new SelectList(StaticHelper.GetStaticData(StaticHelper.CHECKLIST_DROPDOWN), "Name", "Name", model.Checklist_Type);
                //return Json(model, JsonRequestBehavior.AllowGet);
                return PartialView("../Master/CheckList/_AddOrUpdate", model);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(string CheckListId, string CheckListName)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (CheckListId == null && CheckListName != null)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_checklistmaster tbl_checklist = await db.tbl_checklistmaster.FindAsync(CheckListId, CheckListName);
                if (tbl_checklist == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_checklist.Id),
                                           new MySqlParameter("@PChecklist_id",tbl_checklist.Checklist_id),
                                           new MySqlParameter("@PChecklist_Name",tbl_checklist.Checklist_Name),
                                           new MySqlParameter("@Pcheck_type",tbl_checklist.Check_type),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Checklist_All(@PFlag,@PId,@PChecklist_id,@PChecklist_Name,@Pcheck_type,@PCreateduser)", param).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
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
