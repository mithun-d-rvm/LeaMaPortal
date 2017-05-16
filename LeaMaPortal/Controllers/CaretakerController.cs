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

namespace LeaMaPortal.Controllers
{
    public class CaretakerController : BaseController
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Caretaker
        public ActionResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                var caretakers = db.tbl_caretaker.Where(x => x.Delmark != "*");
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    caretakers = caretakers.Where(x => x.Caretaker_Name.Contains(Search));
                }

                return PartialView("../Master/Caretaker/_List", caretakers.OrderBy(x => x.Id).Select(x => new CaretakerViewModel()
                {
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    Caretaker_id = x.Caretaker_id,
                    Caretaker_Name = x.Caretaker_Name,
                    City = x.City,
                    Country = x.Country,
                    Dob = x.Dob,
                    Doj = x.Doj,
                    Email = x.Email,
                    Id = x.Id,
                    Phoneno = x.Phoneno,
                    Pincode = x.Pincode,
                    Region_Name = x.Region_Name,
                    State = x.State
                }).ToPagedList(currentPageIndex, PageSize));
            }
            catch(Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            ViewBag.Region_Name = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");
            var caretaker = db.tbl_caretaker.OrderByDescending(x => x.Caretaker_id).FirstOrDefault();
            if (caretaker != null)
            {
                ViewBag.CaretakerId = caretaker.Caretaker_id + 1;
            }
            else
            {
                ViewBag.CaretakerId = 1;
            }
            
            return PartialView("../Master/Caretaker/_AddOrUpdate", new CaretakerViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(CaretakerViewModel model)
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
                        var caretaker = db.tbl_caretaker.OrderByDescending(x => x.Caretaker_id).FirstOrDefault();
                        if (caretaker != null)
                        {
                            model.Caretaker_id = caretaker.Caretaker_id + 1;
                        }
                        else
                        {
                            model.Caretaker_id = 1;
                        }
                        PFlag = "INSERT";
                        result.Message = "Caretaker master added successfully";
                    }
                    else
                    {
                        PFlag = "UPDATE";
                        result.Message = "Caretaker master updated successfully";
                    }
                    object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PId", model.Id),
                         new MySqlParameter("@PCaretaker_id",model.Caretaker_id),
                         new MySqlParameter("@PCaretaker_name", model.Caretaker_Name),
                         new MySqlParameter("@PDob", model.Dob),
                         new MySqlParameter("@PAddress1", model.Address1),
                         new MySqlParameter("@PAddress2", model.Address2),
                         new MySqlParameter("@PRegion_name", model.Region_Name),
                         new MySqlParameter("@PCountry", model.Country),
                         new MySqlParameter("@PCity", model.City),
                         new MySqlParameter("@PState", model.State),
                         new MySqlParameter("@PPincode", model.Pincode),
                         new MySqlParameter("@PPhoneno", model.Phoneno),
                         new MySqlParameter("@PEmail", model.Email),
                         new MySqlParameter("@PDoj", model.Doj),
                         new MySqlParameter("@PCreatedUser",System.Web.HttpContext.Current.User.Identity.Name)
                    };
                    var tenantCompany = await db.Database.SqlQuery<object>("call Usp_Caretaker_All(@PFlag, @PId, @PCaretaker_id, @PCaretaker_Name, @PDob, @PAddress1, @PAddress2, @PRegion_Name, @PCountry, @PCity, @PState, @PPincode, @PPhoneno, @PEmail, @PDoj, @PCreateduser)", parameters).ToListAsync();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }


        }
        public PartialViewResult Edit(int? id)
        {
            try
            {
                tbl_caretaker caretaker = db.tbl_caretaker.FirstOrDefault(x =>x.Delmark != "*" && x.Id == id);
                if (caretaker == null)
                {
                    return PartialView("../Master/Caretaker/_AddOrUpdate", new CaretakerViewModel());
                    //return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                ViewBag.Region_Name = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name",caretaker.Region_Name);
                ViewBag.CaretakerId = caretaker.Caretaker_id;
                CaretakerViewModel model = new CaretakerViewModel()
                {
                    Id = caretaker.Id,
                    Caretaker_id = caretaker.Caretaker_id,
                    Address1 = caretaker.Address1,
                    Address2 = caretaker.Address2,
                    Caretaker_Name = caretaker.Caretaker_Name,
                    City = caretaker.City,
                    Dob = caretaker.Dob,
                    Doj = caretaker.Doj,
                    Email = caretaker.Email,
                    Phoneno = caretaker.Phoneno,
                    Pincode = caretaker.Pincode,
                    Region_Name = caretaker.Region_Name,
                    State = caretaker.State,
                    Country = caretaker.Country
                };
                return PartialView("../Master/Caretaker/_AddOrUpdate", model);
            }
            catch
            {
                return PartialView("../Master/Caretaker/_AddOrUpdate", new CaretakerViewModel());
            }
        }

        // GET: Caretaker/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
        //    if (tbl_agreement_closure_checklist == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbl_agreement_closure_checklist);
        //}

        // GET: Caretaker/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid");
        //    return View();
        //}

        // POST: Caretaker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,Agreement_No,Checklist_id,Checklist_Name,Status,Remarks,Delmark")] tbl_agreement_closure_checklist tbl_agreement_closure_checklist)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbl_agreement_closure_checklist.Add(tbl_agreement_closure_checklist);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_closure_checklist.Agreement_No);
        //    return View(tbl_agreement_closure_checklist);
        //}


        // GET: Caretaker/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            MessageResult result = new MessageResult();
            if (id == null)
            {
                return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
            }
            object[] parameters = {
                         new MySqlParameter("@PFlag", "DELETE"),
                         new MySqlParameter("@PId", id),
                         new MySqlParameter("@PCaretaker_id",0),
                         new MySqlParameter("@PCaretaker_name", ""),
                         new MySqlParameter("@PDob", "01-01-0001"),
                         new MySqlParameter("@PAddress1", ""),
                         new MySqlParameter("@PAddress2", ""),
                         new MySqlParameter("@PRegion_name", ""),
                         new MySqlParameter("@PCountry", ""),
                         new MySqlParameter("@PCity", ""),
                         new MySqlParameter("@PState", ""),
                         new MySqlParameter("@PPincode", ""),
                         new MySqlParameter("@PPhoneno", ""),
                         new MySqlParameter("@PEmail", ""),
                         new MySqlParameter("@PDoj", "01-01-0001"),
                         new MySqlParameter("@PCreatedUser",System.Web.HttpContext.Current.User.Identity.Name)
                    };
            var spresult = await db.Database.SqlQuery<object>("call Usp_Caretaker_All(@PFlag, @PId, @PCaretaker_id, @PCaretaker_Name, @PDob, @PAddress1, @PAddress2, @PRegion_Name, @PCountry, @PCity, @PState, @PPincode, @PPhoneno, @PEmail, @PDoj, @PCreateduser)", parameters).ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Caretaker/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
        //    db.tbl_agreement_closure_checklist.Remove(tbl_agreement_closure_checklist);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public async Task<ActionResult> GetCaretakerName(int Id)
        {
            try
            {
                var tbl_caretaker = await db.tbl_caretaker.FirstOrDefaultAsync(x => x.Caretaker_id == Id);
                return Json(tbl_caretaker?.Caretaker_Name, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
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
