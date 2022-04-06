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
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using LeaMaPortal.Helpers;

namespace LeaMaPortal.Controllers
{
    [Authorize]
    public class ApprovalSettingsController : BaseController
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
                string regname = Session["Region"].ToString();
                IList<ApprovalSettingsViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                    
                    list = db.tbl_approvalconfig.Where(x => x.Delmark != "*" && x.Region_Name == regname ).OrderBy(x => x.Userid).Select(x => new ApprovalSettingsViewModel()
                    {
                        Id = x.Id,
                        Approval_flag = x.Approval_flag,
                        Userid = x.Userid,
                    }).ToPagedList(currentPageIndex, PageSize);
                   
                }
                else
                {
                    list = db.tbl_approvalconfig.Where(x => x.Delmark != "*" && x.Region_Name == regname
                                    && (x.Userid.ToLower().Contains(Search.ToLower())
                                    || x.Approval_flag.ToLower().Contains(Search.ToLower())))
                                  .OrderBy(x => x.Userid).Select(x => new ApprovalSettingsViewModel()
                                  {
                                      Id = x.Id,
                                      Approval_flag = x.Approval_flag,
                                      Userid = x.Userid,
                                  }).ToPagedList(currentPageIndex, PageSize);
                }
                return PartialView("../Master/ApprovalSettings/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            ApprovalSettingsViewModel model = new ApprovalSettingsViewModel();
            var users = db.tbl_approvalconfig.Where(x => x.Delmark != "*").Select(r => r.Userid).ToList();
            ViewBag.Userid = new SelectList(db.tbl_userrights.Where(d => !users.Contains(d.Userid)).OrderBy(o => o.Userid).ToList(), "Userid", "Userid");
            return PartialView("../Master/ApprovalSettings/_AddOrUpdate", model);
        }
        // POST: CheckList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "Approval_flag,Userid,Id")] ApprovalSettingsViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";
                    string regname = Session["Region"].ToString();
                    //bool isDataAvailable = await db.tbl_approvalconfig.AnyAsync(x => x.Delmark != "*" );
                    bool isDataAvailable = await db.tbl_approvalconfig.AnyAsync(x => x.Delmark != "*" && x.Region_Name == regname);

                    if (isDataAvailable)
                    {
                        result.Errors = "You can't add more than one user for approval";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Id == 0)
                    {
                        result.Message = "Tca approval setting added successfully";
                    }
                    else
                    {
                        PFlag = "UPDATE";
                        result.Message = "Tca approval setting updated successfully";
                    }
                    //tbl_approvalconfig tbl_approval = new tbl_approvalconfig();
                    //tbl_approval.Userid = model.Userid;
                    //tbl_approval.Approval_flag = model.Approval_flag;
                    //tbl_approval.Createduser = System.Web.HttpContext.Current.User.Identity.Name;
                    //tbl_approval.Createddatetime = DateTime.Now;
                    //db.tbl_approvalconfig.Add(tbl_approval);
                 
                    if (regname != "")
                    {
                        model.Region_Name = regname;

                        var countryname = db.tbl_region.Where(x => x.Region_Name == model.Region_Name).OrderByDescending(x => x.Id).FirstOrDefault();

                        model.Country = countryname.Country;
                    }
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PId", model.Id),
                                           new MySqlParameter("@PApproval_flag",model.Approval_flag),
                                            new MySqlParameter("@PUserid",model.Userid),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter ("@PRegion_Name",model.Region_Name ),
                                           new MySqlParameter ("@PCountry",model .Country )
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_approvalconfig_All(@PFlag,@PId,@PApproval_flag,@PUserid,@PCreateduser,@PRegion_Name,@PCountry)", param).ToListAsync();
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
                tbl_approvalconfig tbl_approval = await db.tbl_approvalconfig.FindAsync(Id);
                if (tbl_approval == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                ApprovalSettingsViewModel model = new ApprovalSettingsViewModel()
                {
                    Id = tbl_approval.Id,
                    Approval_flag = tbl_approval.Approval_flag,
                    //Userid = tbl_approval.Userid
                };
                var users = db.tbl_approvalconfig.Where(x => x.Userid != tbl_approval.Userid).Select(r => r.Userid).ToList();
                ViewBag.Userid = new SelectList(db.tbl_userrights.Where(d => !users.Contains(d.Userid)).OrderBy(o => o.Userid).ToList(), "Userid", "Userid", tbl_approval.Userid);
                return PartialView("../Master/ApprovalSettings/_AddOrUpdate", model);
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
                tbl_approvalconfig tbl_approval = await db.tbl_approvalconfig.FindAsync(Id);
                if (tbl_approval == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PId", tbl_approval.Id),
                                           new MySqlParameter("@PApproval_flag",tbl_approval.Approval_flag),
                                            new MySqlParameter("@PUserid",tbl_approval.Userid),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter("@PRegion_Name",Session["Region"].ToString()),
                                           new MySqlParameter("@PCountry",Session["Country"].ToString())
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_approvalconfig_All(@PFlag,@PId,@PApproval_flag,@PUserid,@PCreateduser,@PRegion_Name,@PCountry)", param).ToListAsync();
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