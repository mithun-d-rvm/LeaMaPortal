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
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LeaMaPortal.Controllers
{
    public class EmailTemplateController : Controller
    {
        private Entities db = new Entities();

        // GET: EmailTemplate
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<EmailTemplateViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_emailtemplate.Where(x => x.Delmark != "*").OrderBy(x => x.TemplateName).Select(x => new EmailTemplateViewModel()
                    {
                        Id = x.Id,
                        TemplateID = x.TemplateID,
                        TemplateName = x.TemplateName,
                        Body = x.Body,
                        BodyText = x.Bodytext,
                        Subject = x.Subject,
                        SubjectParameter = x.SubjectParameter
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_emailtemplate.Where(x => x.Delmark != "*"
                                    && x.TemplateName.ToLower().Contains(Search.ToLower()))
                                  .OrderBy(x => x.TemplateName).Select(x => new EmailTemplateViewModel()
                                  {
                                      Id = x.Id,
                                      TemplateID = x.TemplateID,
                                      TemplateName = x.TemplateName,
                                      Body = x.Body,
                                      BodyText = x.Bodytext,
                                      Subject = x.Subject,
                                      SubjectParameter = x.SubjectParameter
                                  }).ToPagedList(currentPageIndex, PageSize);
                }

                return PartialView("../Master/EmailTemplate/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            EmailTemplateViewModel model = new EmailTemplateViewModel();
            return PartialView("../Master/EmailTemplate/_AddOrUpdate", model);
        }
        // POST: EmailTemplate/AddOrUpadate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "TemplateID,TemplateName,Id,Body,BodyText, Subject,SubjectParameter")] EmailTemplateViewModel model)
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
                                           new MySqlParameter("@PTemplateID",model.TemplateID),
                                            new MySqlParameter("@PTemplateName",model.TemplateName),
                                            new MySqlParameter("@PSubject",model.Subject),
                                            new MySqlParameter("@PBodytext",model.BodyText),
                                            new MySqlParameter("@PSubjectParameter",model.SubjectParameter),
                                            new MySqlParameter("@PBodyParameter",""),
                                            new MySqlParameter("@PInActive",false),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Emailtemplate_All(@PFlag,@PId,@PTemplateID,@PTemplateName,@PSubject,@PBodytext,@PSubjectParameter,@PBodyParameter,@PInActive,@PCreateduser)", param).ToListAsync();
                    await db.SaveChangesAsync();

                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> Edit(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_emailtemplate tbl_email = await db.tbl_emailtemplate.FindAsync(Id);
                if (tbl_email == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                EmailTemplateViewModel model = new EmailTemplateViewModel()
                {
                    Id = tbl_email.Id,
                    TemplateID = tbl_email.TemplateID,
                    TemplateName = tbl_email.TemplateName,
                    Body = tbl_email.Body,
                    BodyText = tbl_email.Bodytext,
                    Subject = tbl_email.Subject,
                    SubjectParameter = tbl_email.SubjectParameter
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(int Id)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (Id == 0)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_emailtemplate tbl_email = await db.tbl_emailtemplate.FindAsync(Id);
                if (tbl_email == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_email.Id),
                                           new MySqlParameter("@PTemplateID",tbl_email.TemplateID),
                                            new MySqlParameter("@PTemplateName",tbl_email.TemplateName),
                                            new MySqlParameter("@PSubject",tbl_email.Subject),
                                            new MySqlParameter("@PBodytext",tbl_email.Bodytext),
                                            new MySqlParameter("@PSubjectParameter",tbl_email.SubjectParameter),
                                            new MySqlParameter("@PBodyParameter",tbl_email.BodyParameter),
                                            new MySqlParameter("@PInActive",true),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Emailtemplate_All(@PFlag,@PId,@PTemplateID,@PTemplateName,@PSubject,@PBodytext,@PSubjectParameter,@PBodyParameter,@PInActive,@PCreateduser)", param).ToListAsync();
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
