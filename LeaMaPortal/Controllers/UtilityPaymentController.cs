using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using System.Data.Entity;
using System.Data;
using System.Threading.Tasks;

namespace LeaMaPortal.Controllers
{
    public class UtilityPaymentController : Controller
    {
        LeamaEntities db = new LeamaEntities();
        // GET: UtilityPayment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> AddOrUpdate()
        {

            UtilityPaymentModel model = new UtilityPaymentModel();
            int? paymentno = await db.tbl_eb_water_paymenthd.Select(x => x.PaymentNo).OrderByDescending(x => x).FirstOrDefaultAsync();
            model.PaymentNo = paymentno == null ? 1 : paymentno.Value + 1;


            ViewBag.Utiltiy_name = new SelectList(db.tbl_utilitiesmaster.Select(x => new { UtilityId = x.Utility_id, UtilityName = x.Utility_Name }), "UtilityId", "UtilityName");
            ViewBag.Supplier_name = new SelectList(db.tbl_suppliermaster.Select(x => new { SupplierId = x.Supplier_Id, SupplierName = x.Supplier_Name }), "SupplierId", "SupplierName");

            ViewBag.AdvAcCode = new SelectList(db.Database.SqlQuery<int>("SELECT PaymentNo FROM view_advance_pending_eb").ToList());
            var _paymentType = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "Reccategory");
            if (_paymentType != null)
            {
                ViewBag.PaymentType = new SelectList(_paymentType.combovalue.Split(','));
            }
            else
            {
                ViewBag.PaymentType = new SelectList(null);
            }
            var _paymentmode = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "RecpType");
            if (_paymentmode != null)
            {
                ViewBag.PaymentMode = new SelectList(_paymentmode.combovalue.Split(','));
            }
            else
            {
                ViewBag.PaymentMode = new SelectList(null);
            }
            ViewBag.BankAcCode = new SelectList(Common.BankDetails, "AccountNumber", "AccountNumber");
            ViewBag.BankAcName = new SelectList(Common.BankDetails, "AccountNumber", "BankName");

            var _pdcstatus = await db.tbl_combo_master.FirstOrDefaultAsync(x => x.screen_name == "Receipts" && x.comboname == "PDCstatus");
            if (_paymentmode != null)
            {
                ViewBag.pdcstatus = new SelectList(_pdcstatus.combovalue.Split(','));
            }
            else
            {
                ViewBag.pdcstatus = new SelectList(null);
            }
            var ebBillPayment = db.Database.SqlQuery<EBBillPaymentPending>("select * from view_ebbill_payment_pending").ToList();

            ViewBag.Refno = new SelectList(ebBillPayment, "refno", "refno");
            ViewBag.Meterno = new SelectList(ebBillPayment, "refno", "meterno");
            ViewBag.billNo = new SelectList(ebBillPayment, "refno", "billno");
            
            return PartialView("../UtilityPayment/_AddorUpdate", model);
        }

        // GET: UtilityPayment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UtilityPayment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilityPayment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UtilityPayment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UtilityPayment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UtilityPayment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UtilityPayment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
