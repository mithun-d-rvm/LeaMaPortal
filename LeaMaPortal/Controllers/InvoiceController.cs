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
        public ActionResult Index(string Search, int? page, int? defaultPageSize)
        {
            //ViewData["Search"] = Search;
            //int currentPageIndex = page.HasValue ? page.Value : 1;
            //int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
            //ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
            ////TenantCompanyViewModel model = new TenantCompanyViewModel();
            //var invoiceDetails = db.tbl_invoicehd.Where(x => x.Delmark != "*");
            //if (!string.IsNullOrWhiteSpace(Search))
            //{
            //    invoiceDetails = invoiceDetails.Where(x => x.invno.Contains(Search));
            //}
            //var invoice = invoiceDetails.OrderBy(x => x.id).Select(x => new InvoiceViewModel()
            //{
            //    Agreement_No = x.Agreement_No,
            //    invno = x.invno,
            //    bank_details = x.bank_details,
            //    month = x.month,
            //    date = x.date,
            //    duedate = x.duedate,
            //    Id = x.id,
            //    invtype = x.invtype,
            //    Property_ID = x.Property_ID,
            //    Property_Name = x.Property_Name,
            //    remarks = x.remarks,
            //    Tenant_id = x.Tenant_id,
            //    Tenant_Name = x.Tenant_Name,
            //    totalamt = x.totalamt,
            //    Unit_ID = x.Unit_ID,
            //    unit_Name = x.unit_Name,
            //    year = x.year,
            //    InvoiceDetails = db.tbl_invoicedt.Where(y => y.invno == x.invno).Select(z => new InvoiceDetailsViewModel()
            //    {
            //        Id = z.id,
            //        amount = z.amount,
            //        description = z.description,
            //        item = z.item,
            //        qty = z.qty
            //    }).ToList()
            //}).ToPagedList(currentPageIndex, PageSize);
            return PartialView(/*"../Invoice/_List",invoice*/);
            //return View(invoice);
        }
        public PartialViewResult List()
        {

            return PartialView("../Invoice/_List");
        }

        [HttpGet]
        public PartialViewResult AddorUpdate(int? DropDownValue)
        {
            List<SelectListItem> Month = new List<SelectListItem>
                                     ();
            Month.Add(new SelectListItem
            {
                Text = "1",
                Value = "1"
            });
            Month.Add(new SelectListItem
            {
                Text = "2",
                Value = "2",
                Selected = true
            });
            Month.Add(new SelectListItem
            {
                Text = "3",
                Value = "3"
            });
            Month.Add(new SelectListItem
            {
                Text = "4",
                Value = "4"
            });
            Month.Add(new SelectListItem
            {
                Text = "5",
                Value = "5"
            });
            Month.Add(new SelectListItem
            {
                Text = "6",
                Value = "6"
            });
            Month.Add(new SelectListItem
            {
                Text = "7",
                Value = "7"
            });
            Month.Add(new SelectListItem
            {
                Text = "8",
                Value = "8"
            });
            Month.Add(new SelectListItem
            {
                Text = "9",
                Value = "9"
            });
            Month.Add(new SelectListItem
            {
                Text = "10",
                Value = "10"
            });
            Month.Add(new SelectListItem
            {
                Text = "11",
                Value = "11"
            });
            Month.Add(new SelectListItem
            {
                Text = "12",
                Value = "12"
            });
            //List<SelectListItem> InvType = new List<SelectListItem>
            //                          ();

            //InvType.Add(new SelectListItem
            //{
            //    Text = "Rental",
            //    Value = "1"
            //});
            //InvType.Add(new SelectListItem
            //{
            //    Text = "Others",
            //    Value = "1"
            //});
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
            ViewBag.IsEmailSent = false;

            // ViewBag.InvType = InvType;
            ViewBag.Month = Month;


            switch (DropDownValue)
            {
                case 1:
                    ViewBag.AgreementList = db.tbl_agreement.Select(x => new InvoiceViewModel()
                    {
                        Tenant_id = x.Ag_Tenant_id,
                        Tenant_Name = x.Ag_Tenant_Name,
                        Property_ID = x.property_id,
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
                //if (ModelState.IsValid)
                //{
                MySqlParameter pa = new MySqlParameter();
                string PFlag = null;

                if (model.Id == 0)
                {
                    var invoiceId = db.tbl_invoicehd.OrderByDescending(x => x.id).FirstOrDefault();
                    model.Id = invoiceId != null ? invoiceId.id + 1 : 1;
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
                            invoiceDet = "(" + model.Id + ",'Rental','" + item.description +
                                "','" + item.qty + "','" + item.amount + "')";
                        }
                        else
                        {
                            invoiceDet = "(" + model.Id + ",'Rental','" + item.description +
                               "','" + item.qty + "','" + item.amount + "')";
                        }
                    }
                }
                object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@PTenant_Id", 1),
                         new MySqlParameter("@PTenant_Name","Muniasamy G"),
                         new MySqlParameter("@PId", model.Id),
                         new MySqlParameter("@Pinvno", model.Id),
                         new MySqlParameter("@Pdate", DateTime.Now),
                         new MySqlParameter("@Pinvtype", "Rental"),
                         new MySqlParameter("@PAgreement_No", model.Agreement_No),
                         new MySqlParameter("@PProperty_ID", model.Property_ID),
                         new MySqlParameter("@PProperty_Name", model.Property_Name),
                         new MySqlParameter("@PUnit_ID", model.Unit_ID),
                         new MySqlParameter("@Punit_Name", model.unit_Name),
                         new MySqlParameter("@Pmonth", model.month),
                         new MySqlParameter("@Pyear", model.year),
                         new MySqlParameter("@Ptotalamt", model.totalamt),
                         new MySqlParameter("@Pduedate", DateTime.Now),
                         new MySqlParameter("@Pbank_details", model.bank_details),
                         new MySqlParameter("@Pinvoicedt", invoiceDet),
                         new MySqlParameter("@Premarks", model.remarks),
                         new MySqlParameter("@Pincno", 0),
                         new MySqlParameter("@PCreateduser", System.Web.HttpContext.Current.User.Identity.Name),


                    };
                var tenantCompany = await db.Database.SqlQuery<object>("CALL Usp_Invoice_All(@PFlag, @PId, @Pinvno, @Pdate, @PTenant_id, @PTenant_Name, @Pinvtype, @PAgreement_No, @PProperty_ID, @PProperty_Name, @PUnit_ID, @Punit_Name, @Pmonth, @Pyear, @Ptotalamt, @Pduedate, @Pbank_details, @Premarks, @Pincno, @PCreateduser, @Pinvoicedt)", parameters).ToListAsync();
                //}
                return RedirectToAction("../Master/Index", new { selected = 10 });
            }
            catch (Exception e)
            {
                return RedirectToAction("../Master/Index", new { selected = 10 });
            }
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
            if (tbl_invoicedt == null)
            {
                return HttpNotFound();
            }
            return View(tbl_invoicedt);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,invno,item,description,qty,amount,Delmark")] tbl_invoicedt tbl_invoicedt)
        {
            if (ModelState.IsValid)
            {
                db.tbl_invoicedt.Add(tbl_invoicedt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
            return View(tbl_invoicedt);
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
            if (tbl_invoicedt == null)
            {
                return HttpNotFound();
            }
            ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
            return View(tbl_invoicedt);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,invno,item,description,qty,amount,Delmark")] tbl_invoicedt tbl_invoicedt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_invoicedt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.invno = new SelectList(db.tbl_invoicehd, "invno", "Tenant_Name", tbl_invoicedt.invno);
            return View(tbl_invoicedt);
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
            if (tbl_invoicedt == null)
            {
                return HttpNotFound();
            }
            return View(tbl_invoicedt);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_invoicedt tbl_invoicedt = db.tbl_invoicedt.Find(id);
            db.tbl_invoicedt.Remove(tbl_invoicedt);
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
    }
}
