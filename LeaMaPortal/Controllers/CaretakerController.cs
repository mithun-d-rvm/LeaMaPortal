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
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace LeaMaPortal.Controllers
{
    public class CaretakerController : Controller
    {
        private Entities db = new Entities();

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
                        PFlag = "INSERT";
                    }
                    else
                    {
                        PFlag = "UPDATE";
                    }
                    object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PId", model.Id),
                         new MySqlParameter("@PPCaretaker_id",model.Caretaker_id),
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
                    var tenantCompany = await db.Database.SqlQuery<object>("call leama.Usp_Caretaker_All(@PFlag, @PId, @PCaretaker_id, @PCaretaker_Name, @PDob, @PAddress1, @PAddress2, @PRegion_Name, @PCountry, @PCity, @PState, @PPincode, @PPhoneno, @PEmail, @PDoj, @PCreateduser)", parameters).ToListAsync();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }


        }


        // GET: Caretaker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
            if (tbl_agreement_closure_checklist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_closure_checklist);
        }

        // GET: Caretaker/Create
        public ActionResult Create()
        {
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid");
            return View();
        }

        // POST: Caretaker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Agreement_No,Checklist_id,Checklist_Name,Status,Remarks,Delmark")] tbl_agreement_closure_checklist tbl_agreement_closure_checklist)
        {
            if (ModelState.IsValid)
            {
                db.tbl_agreement_closure_checklist.Add(tbl_agreement_closure_checklist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_closure_checklist.Agreement_No);
            return View(tbl_agreement_closure_checklist);
        }

        // GET: Caretaker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
            if (tbl_agreement_closure_checklist == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_closure_checklist.Agreement_No);
            return View(tbl_agreement_closure_checklist);
        }

        // POST: Caretaker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Agreement_No,Checklist_id,Checklist_Name,Status,Remarks,Delmark")] tbl_agreement_closure_checklist tbl_agreement_closure_checklist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_agreement_closure_checklist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement_closure, "Agreement_No", "Advance_Security_Amount_Paid", tbl_agreement_closure_checklist.Agreement_No);
            return View(tbl_agreement_closure_checklist);
        }

        // GET: Caretaker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
            if (tbl_agreement_closure_checklist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_agreement_closure_checklist);
        }

        // POST: Caretaker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_agreement_closure_checklist tbl_agreement_closure_checklist = db.tbl_agreement_closure_checklist.Find(id);
            db.tbl_agreement_closure_checklist.Remove(tbl_agreement_closure_checklist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> GetCaretakerName(int Id)
        {
            try
            {
                var tbl_caretaker = await db.tbl_caretaker.FirstOrDefaultAsync(x => x.Id == Id);
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
