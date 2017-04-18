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
using MySql.Data.MySqlClient;
using MvcPaging;


namespace LeaMaPortal.Controllers
{
    public class InvoiceController : Controller
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult List(string Search, int? page, int? defaultPageSize)
        {
            ViewData["Search"] = Search;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
            ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
            //TenantCompanyViewModel model = new TenantCompanyViewModel();
            var invoiceDetails = db.tbl_invoicehd.Where(x => x.Delmark != "*");
            if (!string.IsNullOrWhiteSpace(Search))
            {
                invoiceDetails = invoiceDetails.Where(x => x.invno.Contains(Search));
            }
            var invoice = invoiceDetails.OrderBy(x => x.id).Select(x => new InvoiceViewModel()
            {
                Agreement_No = x.Agreement_No,
                invno = x.invno,
                bank_details = x.bank_details,
                month = x.month,
                date = x.date,
                duedate = x.duedate,
                Id = x.id,
                invtype = x.invtype,
                Property_ID = x.Property_ID,
                Property_Name = x.Property_Name,
                remarks = x.remarks,
                Tenant_id = x.Tenant_id,
                Tenant_Name = x.Tenant_Name,
                totalamt = x.totalamt,
                Unit_ID = x.Unit_ID,
                unit_Name = x.unit_Name,
                year = x.year,
                InvoiceDetails = db.tbl_invoicedt.Where(y => y.invno == x.invno).Select(z => new InvoiceDetailsViewModel()
                {
                    Id = z.id,
                    amount = z.amount,
                    description = z.description,
                    item = z.item,
                    qty = z.qty
                }).ToList()
            }).ToPagedList(currentPageIndex, PageSize);
            return PartialView("../Invoice/_List", invoice);
        }

        [HttpGet]
        public PartialViewResult AddorUpdate(int? DropDownValue)
        {
            
            //var InvType = db.Database.SqlQuery<string>(@"call usp_split('Invoice','invtype',',',null);").ToList();
            //ViewBag.InvType = new SelectList(InvType);
            //var Month = db.Database.SqlQuery<string>(@"call usp_split('Invoice','Month',',',null)").ToList();
            //ViewBag.InvType = new SelectList(Month);


            //ViewBag.Tenant_Id = new SelectList(db.tbl_tenant_individual.OrderBy(x => x.Tenant_Id), "Tenant_Id", "Tenant_Id");
            //ViewBag.Tenant_Name = new SelectList(db.tbl_tenant_individual.OrderBy(x => x.First_Name), "Tenant_Name", "Tenant_Name");
            ViewBag.Property_Id = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Property_Id), "Property_Id", "Property_Id");
            ViewBag.Property_Name = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Property_Name), "Property_Name", "Property_Name");
            ViewBag.Unit_Id = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Unit_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
            ViewBag.unit_Name = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Unit_Property_Name), "Unit_Property_Name", "Unit_Property_Name");
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement.OrderBy(x => x.Agreement_No), "id", "Agreement_No");
            ViewBag.invtype = new SelectList(Common.InvoiceType);
            ViewBag.Month = new SelectList(Common.Month, "Value", "Text");
            var invoice = db.tbl_invoicehd.OrderByDescending(x => x.incno).FirstOrDefault();
            var tenant = db.view_tenant.Select(x => new { TenantId = x.Tenant_id, TenantName = x.First_Name, Type = x.type });
            ViewBag.invno = invoice != null ? invoice.incno + 1 : 1;
            ViewBag.Tenant_id = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantId", dataGroupField: "Type", selectedValue: null);
            ViewBag.Tenant_Name = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantName", dataGroupField: "Type", selectedValue: null);
            ViewBag.bank_details = Common.Bank_number;
            switch (DropDownValue)
            {
                case 1:
                    ViewBag.AgreementList = db.tbl_agreement.Select(x => new InvoiceViewModel()
                    {
                        Tenant_id = x.Ag_Tenant_id,
                        Tenant_Name = x.Ag_Tenant_Name,
                        Property_ID = x.property_id.ToString(),
                        Property_Name = x.Properties_Name,
                        Unit_ID = x.Unit_ID_Tawtheeq,
                        unit_Name = x.Unit_Property_Name
                    }).ToList();
                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;

            }
            return PartialView("../Invoice/_AddorUpdate", new InvoiceViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(InvoiceViewModel model)
        {

            MessageResult result = new MessageResult();
            try
            {
                MySqlParameter pa = new MySqlParameter();
                string PFlag = null;

                if (model.Id == 0)
                {
                    var invoiceId = db.tbl_invoicehd.OrderByDescending(x => x.incno).FirstOrDefault();
                    if (invoiceId == null)
                    {
                        model.Id = 1;
                        model.invno = model.Id.ToString();
                        model.incno = 1;
                    }
                    else
                    {
                        model.Id = invoiceId.id + 1;
                        model.incno = invoiceId.incno + 1;
                        model.invno = model.incno.ToString();
                    }
                    PFlag = "INSERT";
                }
                else
                {
                    PFlag = "UPDATE";
                }
                string invoiceDet = "";
                if (model.InvoiceDetails != null)
                {
                    foreach (var item in model.InvoiceDetails)
                    {
                        if (string.IsNullOrWhiteSpace(invoiceDet))
                        {
                            invoiceDet = "(" + model.invno + ",'" + model.invtype + ",'" + item.description +
                                "','" + item.qty + "','" + item.amount + "')";
                        }
                        else
                        {
                            invoiceDet = ",(" + model.invno + ",'" + model.invtype + ",'" + item.description +
                                 "','" + item.qty + "','" + item.amount + "')";
                        }
                    }
                }
                object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PTenant_Id", model.Tenant_id),
                         new MySqlParameter("@PTenant_Name",model.Tenant_Name),
                         new MySqlParameter("@PId", model.Id),
                         new MySqlParameter("@Pinvno", model.invno),
                         new MySqlParameter("@Pdate", model.date),
                         new MySqlParameter("@Pinvtype", model.invtype),
                         new MySqlParameter("@PAgreement_No", model.Agreement_No),
                         new MySqlParameter("@PProperty_ID", model.Property_ID),
                         new MySqlParameter("@PProperty_Name", model.Property_Name),
                         new MySqlParameter("@PUnit_ID", model.Unit_ID),
                         new MySqlParameter("@Punit_Name", model.unit_Name),
                         new MySqlParameter("@Pmonth", model.month),
                         new MySqlParameter("@Pyear", model.year),
                         new MySqlParameter("@Ptotalamt", model.totalamt),
                         new MySqlParameter("@Pduedate", model.duedate),
                         new MySqlParameter("@Pbank_details", model.bank_details),
                         new MySqlParameter("@Pinvoicedt", invoiceDet),
                         new MySqlParameter("@Premarks", model.remarks),
                         new MySqlParameter("@Pincno", model.incno),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name)
                    };
                var invoice = await db.Database.SqlQuery<object>("CALL Usp_Invoice_All(@PFlag, @PId, @Pinvno, @Pdate, @PTenant_id, @PTenant_Name, @Pinvtype, @PAgreement_No, @PProperty_ID, @PProperty_Name, @PUnit_ID, @Punit_Name, @Pmonth, @Pyear, @Ptotalamt, @Pduedate, @Pbank_details, @Premarks, @Pincno, @PCreateduser, @Pinvoicedt)", parameters).ToListAsync();
                return RedirectToAction("../Invoice/Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("../Invoice/Index");
            }
        }

        public async Task<ActionResult> Edit(string Invoice_No)
        {
            try
            {
                var invoice = await db.tbl_invoicehd.FindAsync(Invoice_No);
                InvoiceViewModel model = new InvoiceViewModel()
                {
                    Id = invoice.id,
                    Agreement_No = invoice.Agreement_No,
                    bank_details = invoice.bank_details,
                    date = invoice.date,
                    duedate = invoice.duedate,
                    incno = invoice.incno,
                    invno = invoice.invno,
                    invtype = invoice.invtype,
                    month = invoice.month,
                    Property_ID = invoice.Property_ID,
                    Property_Name = invoice.Property_Name,
                    remarks = invoice.remarks,
                    Tenant_id = invoice.Tenant_id,
                    Tenant_Name = invoice.Tenant_Name,
                    totalamt = invoice.totalamt,
                    Unit_ID = invoice.Unit_ID,
                    unit_Name = invoice.unit_Name,
                    year = invoice.year,
                    InvoiceDetails = invoice.tbl_invoicedt.Select(x => new InvoiceDetailsViewModel()
                    {
                        amount = x.amount,
                        description = x.description,
                        Id = x.id,
                        item = x.item,
                        qty = x.qty
                    }).ToList()
                };
                ViewBag.Property_Id = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Property_Id), "Property_Id", "Property_Id",model.Property_ID);
                ViewBag.Property_Name = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Property_Name), "Property_Name", "Property_Name",model.Property_Name);
                ViewBag.Unit_Id = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Unit_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq",model.Unit_ID);
                ViewBag.unit_Name = new SelectList(db.tbl_propertiesmaster.OrderBy(x => x.Unit_Property_Name), "Unit_Property_Name", "Unit_Property_Name",model.unit_Name);
                ViewBag.Agreement_No = new SelectList(db.tbl_agreement.OrderBy(x => x.Agreement_No), "Agreement_No", "Agreement_No", model.Agreement_No);
                ViewBag.invtype = new SelectList(Common.InvoiceType, model.invtype);
                ViewBag.Month = new SelectList(Common.Month, "Value", "Text", model.month);
                var tenant = db.view_tenant.Select(x => new { TenantId = x.Tenant_id, TenantName = x.First_Name, Type = x.type });
                ViewBag.invno = model.invno;
                ViewBag.Tenant_id = new SelectList(tenant, "TenantId", "TenantId", "Type", model.Tenant_id);
                ViewBag.Tenant_Name = new SelectList(tenant, "TenantId", "TenantName", "Type", model.Tenant_id);
                ViewBag.bank_details = model.bank_details;
                return PartialView("../Invoice/_AddorUpdate", model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //// GET: Invoice/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
        //    if (tbl_invoicedt == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbl_invoicedt);
        //}

        //// GET: Invoice/Create
        //public ActionResult Create()
        //{
        //    ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name");
        //    return View();
        //}

        //// POST: Invoice/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,invno,item,description,qty,amount,Delmark")] tbl_invoicedt tbl_invoicedt)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbl_invoicedt.Add(tbl_invoicedt);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
        //    return View(tbl_invoicedt);
        //}

        // GET: Invoice/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
        //    if (tbl_invoicedt == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
        //    return View(tbl_invoicedt);
        //}

        //// POST: Invoice/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,invno,item,description,qty,amount,Delmark")] tbl_invoicedt tbl_invoicedt)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tbl_invoicedt).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
        //    return View(tbl_invoicedt);
        //}

        // GET: Invoice/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string Invoice_No)
        {
            try
            {
                MessageResult result = new MessageResult();
                var invoice = await db.tbl_invoicehd.FirstOrDefaultAsync(x => x.invno == Invoice_No);
                InvoiceViewModel model = new InvoiceViewModel()
                {
                    Id = invoice.id,
                    Agreement_No = invoice.Agreement_No,
                    bank_details = invoice.bank_details,
                    date = invoice.date,
                    duedate = invoice.duedate,
                    incno = invoice.incno,
                    invno = invoice.invno,
                    invtype = invoice.invtype,
                    month = invoice.month,
                    Property_ID = invoice.Property_ID,
                    Property_Name = invoice.Property_Name,
                    remarks = invoice.remarks,
                    Tenant_id = invoice.Tenant_id,
                    Tenant_Name = invoice.Tenant_Name,
                    totalamt = invoice.totalamt,
                    Unit_ID = invoice.Unit_ID,
                    unit_Name = invoice.unit_Name,
                    year = invoice.year,
                    InvoiceDetails = invoice.tbl_invoicedt.Select(x => new InvoiceDetailsViewModel()
                    {
                        amount = x.amount,
                        description = x.description,
                        Id = x.id,
                        item = x.item,
                        qty = x.qty
                    }).ToList()
                };
                object[] parameters = {
                         new MySqlParameter("@PFlag", "DELETE"),
                         new MySqlParameter("@PTenant_Id", 1),
                         new MySqlParameter("@PTenant_Name","Muniasamy G"),
                         new MySqlParameter("@PId", model.Id),
                         new MySqlParameter("@Pinvno", model.Id),
                         new MySqlParameter("@Pdate", model.date),
                         new MySqlParameter("@Pinvtype", "Rental"),
                         new MySqlParameter("@PAgreement_No", model.Agreement_No),
                         new MySqlParameter("@PProperty_ID", model.Property_ID),
                         new MySqlParameter("@PProperty_Name", model.Property_Name),
                         new MySqlParameter("@PUnit_ID", model.Unit_ID),
                         new MySqlParameter("@Punit_Name", model.unit_Name),
                         new MySqlParameter("@Pmonth", model.month),
                         new MySqlParameter("@Pyear", model.year),
                         new MySqlParameter("@Ptotalamt", model.totalamt),
                         new MySqlParameter("@Pduedate", model.duedate),
                         new MySqlParameter("@Pbank_details", model.bank_details),
                         new MySqlParameter("@Pinvoicedt", null),
                         new MySqlParameter("@Premarks", model.remarks),
                         new MySqlParameter("@Pincno", 0),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name)
                    };
                var sp_result = await db.Database.SqlQuery<object>("CALL Usp_Invoice_All(@PFlag, @PId, @Pinvno, @Pdate, @PTenant_id, @PTenant_Name, @Pinvtype, @PAgreement_No, @PProperty_ID, @PProperty_Name, @PUnit_ID, @Punit_Name, @Pmonth, @Pyear, @Ptotalamt, @Pduedate, @Pbank_details, @Premarks, @Pincno, @PCreateduser, @Pinvoicedt)", parameters).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch(Exception e)
            {
                return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }

        //// POST: Invoice/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
        //    db.tbl_invoicedt.Remove(tbl_invoicedt);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
