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
using System.Threading.Tasks;
using LeaMaPortal.Helpers;

namespace LeaMaPortal.Controllers
{
    public class ReceiptsController : Controller
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Receipts
        public PartialViewResult Index()
        {
            List<SelectListItem> ReceiptType = new List<SelectListItem>
                                     ();
            ReceiptType.Add(new SelectListItem
            {
                Text = "Advance",
                Value = "1"
            });
            ReceiptType.Add(new SelectListItem
            {
                Text = "Against Invoice",
                Value = "2",
                Selected = true
            });
            ReceiptType.Add(new SelectListItem
            {
                Text = "Others",
                Value = "3"
            });

            List<SelectListItem> ReceiptMode = new List<SelectListItem>
                                    ();
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Cash",
                Value = "1"
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Cheque",
                Value = "2",
                Selected = true
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Online",
                Value = "3"
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Cash",
                Value = "4"
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Pdc",
                Value = "5",
                Selected = true
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Advance Ajustment",
                Value = "3"
            });
            List<SelectListItem> PDCStatus = new List<SelectListItem>
                               ();
            PDCStatus.Add(new SelectListItem
            {
                Text = "Received",
                Value = "1"
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Cleared",
                Value = "2",
                Selected = true
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Bounced",
                Value = "3"
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Cancelled",
                Value = "4"
            });
            ViewBag.ReceiptType = ReceiptType;
            ViewBag.ReceiptMode = ReceiptMode;
            //ViewBag.PDCStatus = PDCStatus;


            //var Pdc = db.Database.SqlQuery<string>(@"call usp_split('Receipts','PDCstatus',',',null)").ToList();
            //ViewBag.PDCStatus = new SelectList(Pdc);
            ViewBag.PDCStatus = new SelectList("");

            ReceiptViewModel model = new ReceiptViewModel();

            var receipts = db.tbl_receipthd.OrderByDescending(x => x.id).FirstOrDefault();
            model.ReceiptNo = receipts != null ? receipts.id + 1 : 1;

            model.ReceiptDate = DateTime.Today;
            ViewBag.Reccategory = new SelectList(Common.Reccategory);
            ViewBag.ReceiptMode = new SelectList(Common.ReceiptMode);




            return PartialView("../Receipts/Index", model);
        }

        // GET: Receipts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_receipthd tbl_receipthd = db.tbl_receipthd.Find(id);
            if (tbl_receipthd == null)
            {
                return HttpNotFound();
            }
            return View(tbl_receipthd);
        }

        // GET: Receipts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReceiptNo,id,Reccategory,RecpType,ReceiptDate,agreement_no,Property_id,Property_Name,Unit_ID,unit_Name,Tenant_id,Tenant_Name,TotalAmount,AmtInWords,DDChequeNo,PDCstatus,BankAcCode,BankAcName,AdvAcCode,DDChequeDate,Narration,Accyear,Createddatetime,Createduser,Delmark")] tbl_receipthd tbl_receipthd)
        {
            if (ModelState.IsValid)
            {
                db.tbl_receipthd.Add(tbl_receipthd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_receipthd);
        }

        // GET: Receipts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_receipthd tbl_receipthd = db.tbl_receipthd.Find(id);
            if (tbl_receipthd == null)
            {
                return HttpNotFound();
            }
            return View(tbl_receipthd);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReceiptNo,id,Reccategory,RecpType,ReceiptDate,agreement_no,Property_id,Property_Name,Unit_ID,unit_Name,Tenant_id,Tenant_Name,TotalAmount,AmtInWords,DDChequeNo,PDCstatus,BankAcCode,BankAcName,AdvAcCode,DDChequeDate,Narration,Accyear,Createddatetime,Createduser,Delmark")] tbl_receipthd tbl_receipthd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_receipthd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_receipthd);
        }

        // GET: Receipts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_receipthd tbl_receipthd = db.tbl_receipthd.Find(id);
            if (tbl_receipthd == null)
            {
                return HttpNotFound();
            }
            return View(tbl_receipthd);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_receipthd tbl_receipthd = db.tbl_receipthd.Find(id);
            db.tbl_receipthd.Remove(tbl_receipthd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            ReceiptViewModel model = new ReceiptViewModel();

            var receipts = db.tbl_receipthd.OrderByDescending(x => x.id).FirstOrDefault();
            model.ReceiptNo = receipts != null ? receipts.id + 1 : 1;

            model.ReceiptDate = DateTime.Today;
            ViewBag.Reccategory = new SelectList(Common.Reccategory);
            ViewBag.RecpType = new SelectList(Common.ReceiptMode);
            ViewBag.agreement_no = new SelectList("", "");
            ViewBag.AdvAcCode = new SelectList("", "");
            ViewBag.Property_id = new SelectList("", "");
            ViewBag.Property_Name = new SelectList("", "");
            ViewBag.Tenant_id = new SelectList("", "");
            ViewBag.Tenant_Name = new SelectList("", "");
            ViewBag.Unit_ID = new SelectList("", "");
            ViewBag.unit_Name = new SelectList("", "");
            ViewBag.BankAcCode = new SelectList(Common.BankAccountNumber);
            ViewBag.BankAcName = new SelectList(Common.BankAccountName);
            ViewBag.PDCstatus = new SelectList(Common.Receipts_PDCStatus);
            return PartialView("../Receipts/_AddorUpdate", model);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(ReceiptViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {

                string PFlag = Common.UPDATE;
                if (model.ReceiptNo == 0)
                {
                    PFlag = Common.INSERT;
                    var receipts = db.tbl_receipthd.OrderByDescending(x => x.id).FirstOrDefault();
                    model.ReceiptNo = receipts != null ? receipts.id + 1 : 1;
                }
                object[] param = Helper.GetMySqlParameters<ReceiptViewModel>(model, PFlag, System.Web.HttpContext.Current.User.Identity.Name);
                var _result = await db.Database.SqlQuery<object>(@"CALL Usp_Receipt_All(@PFlag,
, @PReccategory 
, @PRecpType 
, @PReceiptNo
, @PReceiptDate 
, @Pagreement_no
, @PProperty_id 
, @PProperty_Name
, @PUnit_ID 
, @Punit_Name 
, @PTenant_id 
, @PTenant_Name 
, @PTotalAmount 
, @PAmtInWords 
, @PDDChequeNo 
, @PPdcstatus 
, @PBankAcCode
, @PBankAcName
, @PAdvAcCode 
, @PDDChequeDate 
, @PCurrentBls 
, @PTotBls 
, @PAdjustment 
, @PNarration 
, @PCreateduser 
, @PReceiptdt )", param).ToListAsync();

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetReceiptNumber()
        {
            List<AdvancePendingViewModel> result = await db.Database.SqlQuery<AdvancePendingViewModel>("select * from view_advance_pending").ToListAsync();
            AdvancePendingSelectList list = new AdvancePendingSelectList();
            list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
            list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
            list.Property_Id = new SelectList(result.Select(r => r.Property_id));
            list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
            list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
            list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
            list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
            list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
            list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();
        }
        [HttpGet]
        public async Task<string> AmountInWords(decimal amount)
        {
            string amountInWords = NumberToText.Convert(amount);
            //string amountInWords = amount.Humanize();
            return amountInWords;
        }

    }
}
