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

            var model = new InvoiceViewModel();
            //ViewBag.Tenant_Id = new SelectList(db.tbl_tenant_individual.OrderBy(x => x.Tenant_Id), "Tenant_Id", "Tenant_Id");
            //ViewBag.Tenant_Name = new SelectList(db.tbl_tenant_individual.OrderBy(x => x.First_Name), "Tenant_Name", "Tenant_Name");
            var properties = db.tbl_propertiesmaster.Where(x=>x.Delmark!="*" && x.Property_Flag == "Property").OrderBy(x => x.Property_Id).Select(x => new { PropertyId = x.Property_Id, PropertyName = x.Property_Name, GroupedValue = x.Property_Id + ":" + x.Property_Name, PropertyIdTawtheeq = x.Property_ID_Tawtheeq });
            ViewBag.PropertyId = new SelectList(properties, "PropertyIdTawtheeq", "PropertyIdTawtheeq");
            ViewBag.PropertyName = new SelectList(properties, "PropertyIdTawtheeq", "PropertyName");

            var units = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Unit").OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new { UnitId = x.Unit_ID_Tawtheeq, UnitName = x.Unit_Property_Name, GroupedValue = x.Unit_ID_Tawtheeq + ":" + x.Unit_Property_Name });
            ViewBag.UnitId = new SelectList(units, "UnitId", "UnitId");
            ViewBag.unitName = new SelectList(units, "UnitId", "UnitName");
            ViewBag.Agreement_No = new SelectList(db.tbl_agreement.Where(x => x.Delmark != "*").OrderBy(x => x.Agreement_No).Select(x=>x.Agreement_No));
            ViewBag.invtype = new SelectList(Common.InvoiceType);
            ViewBag.month = new SelectList(Common.Month, "Value", "Text");
            var invoice = db.tbl_invoicehd.Max(x => x.incno);
            model.invno = (invoice != 0 ? invoice + 1 : 1) + DateTime.Now.Year.ToString();
            model.incno = invoice != null ? invoice + 1 : 1;
            model.bank_details = Common.Bank_number;
            model.year = DateTime.Now.Year;
            var tenant = db.view_tenant.Select(x => new { TenantId = x.Tenant_id, TenantName = x.First_Name, Type = x.type, GroupedValue = x.Tenant_id + ":" + x.First_Name + ":" + x.type });
            ViewBag.Tenantid = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantId", selectedValue: null);
            ViewBag.TenantName = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantName", selectedValue: null);
            //ViewBag.bank_details = Common.Bank_number;
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
            return PartialView("../Invoice/_AddorUpdate", model);
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
                    //var invoiceId = db.tbl_invoicehd.OrderByDescending(x => x.incno).FirstOrDefault();
                    //if (invoiceId == null)
                    //{
                    //    model.Id = 1;
                    //    model.invno = model.Id.ToString();
                    //    model.incno = 1;
                    //}
                    //else
                    //{
                    //    model.Id = invoiceId.id + 1;
                    //    model.incno = invoiceId.incno + 1;
                    //    model.invno = model.incno.ToString() + DateTime.Now.Year;
                    //}
                    PFlag = "INSERT";
                }
                else
                {
                    PFlag = "UPDATE";
                }
                string invoiceDet = null;
                if (model.InvoiceDetails != null)
                {
                    foreach (var item in model.InvoiceDetails)
                    {
                        if (string.IsNullOrWhiteSpace(invoiceDet))
                        {
                            invoiceDet = "(" + model.invno + ",'" + model.invtype + "','" + item.description +
                                "'," + item.qty + "," + item.amount + ")";
                        }
                        else
                        {
                            invoiceDet = invoiceDet + ",(" + model.invno + ",'" + model.invtype + "','" + item.description +
                                 "'," + item.qty + "," + item.amount + ")";
                        }
                    }
                }
                //if (!string.IsNullOrWhiteSpace(model.Tenant_Name))
                //{
                //    var groupedtenantValues = model.Tenant_Name.Split(':');
                //    model.Tenant_id = Convert.ToInt32(groupedtenantValues[0]);
                //    model.Tenant_Name = string.IsNullOrEmpty(groupedtenantValues[1]) ? null : groupedtenantValues[1];
                //}
                
                //if (!string.IsNullOrWhiteSpace(model.Property_Name))
                //{
                //    var groupedpropertyValues = model.Property_Name.Split(':');
                //    model.Property_ID = groupedpropertyValues[0];
                //    model.Property_Name = string.IsNullOrEmpty(groupedpropertyValues[1]) ? null : groupedpropertyValues[1];
                //}

                //if (!string.IsNullOrWhiteSpace(model.unit_Name))
                //{
                //    var groupedunitValues = model.unit_Name.Split(':');
                //    model.Unit_ID = groupedunitValues[0];
                //    model.unit_Name = string.IsNullOrEmpty(groupedunitValues[1]) ? null : groupedunitValues[1];
                //}
                
                model.bank_details = Common.Bank_number;
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
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
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
                var agreement = db.tbl_agreement.Where(x => x.Delmark != "*").OrderBy(x => x.Agreement_No);
                ViewBag.Agreement_No = new SelectList(agreement.Select(x => x.Agreement_No), model.Agreement_No);
                if (model.Agreement_No==null || model.Agreement_No == 0)
                {
                    var properties = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Property").OrderBy(x => x.Property_Id).Select(x => new { PropertyId = x.Property_Id, PropertyName = x.Property_Name, GroupedValue = x.Property_Id + ":" + x.Property_Name, PropertyIdTawtheeq = x.Property_ID_Tawtheeq });
                    ViewBag.PropertyId = new SelectList(properties, "PropertyIdTawtheeq", "PropertyIdTawtheeq", model.Property_ID);
                    ViewBag.PropertyName = new SelectList(properties, "PropertyIdTawtheeq", "PropertyName", model.Property_ID);

                    var units = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Unit").OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new { UnitId = x.Unit_ID_Tawtheeq, UnitName = x.Unit_Property_Name, GroupedValue = x.Unit_ID_Tawtheeq + ":" + x.Unit_Property_Name });
                    ViewBag.UnitId = new SelectList(units, "UnitId", "UnitId", model.Unit_ID);
                    ViewBag.unitName = new SelectList(units, "UnitId", "UnitName", model.Unit_ID);

                    var tenant = db.view_tenant.Select(x => new { TenantId = x.Tenant_id, TenantName = x.First_Name, Type = x.type, GroupedValue = x.Tenant_id + ":" + x.First_Name + ":" + x.type });
                    ViewBag.Tenantid = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantId", selectedValue: model.Tenant_id);
                    ViewBag.TenantName = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantName", selectedValue: model.Tenant_id);
                }
                else
                {
                    var agreementDetails = await agreement.FirstOrDefaultAsync(x => x.Agreement_No == model.Agreement_No);
                    List<UnitDropdown> unitsDropdown = new List<UnitDropdown>();
                    unitsDropdown.Add(new UnitDropdown()
                    {
                        Unitid = agreementDetails.Unit_ID_Tawtheeq,
                        unitName = agreementDetails.Unit_Property_Name
                    });
                    ViewBag.UnitId = new SelectList(unitsDropdown, "Unitid", "Unitid", model.Unit_ID);
                    ViewBag.unitName = new SelectList(unitsDropdown, "Unitid", "unitName", model.Unit_ID);

                    List<PropertyDropdown> propertyDropdown = new List<PropertyDropdown>();
                    propertyDropdown.Add(new PropertyDropdown()
                    {
                        Propertyid = agreementDetails.Property_ID_Tawtheeq,
                        PropertyName = agreementDetails.Properties_Name
                    });
                    ViewBag.PropertyId = new SelectList(propertyDropdown, "Propertyid", "Propertyid", model.Property_ID);
                    ViewBag.PropertyName = new SelectList(propertyDropdown, "Propertyid", "PropertyName", model.Property_ID);

                    List<TenantDropdown> tenantDropdown = new List<TenantDropdown>();
                    tenantDropdown.Add(new TenantDropdown()
                    {
                        Tenantid = agreementDetails.Ag_Tenant_id,
                        TenantName = agreementDetails.Ag_Tenant_Name,
                    });

                    ViewBag.Tenantid = new SelectList(tenantDropdown, "Tenantid", "Tenantid", model.Tenant_id);
                    ViewBag.TenantName = new SelectList(tenantDropdown, "Tenantid", "TenantName", model.Tenant_id);
                }
                
                ViewBag.invtype = new SelectList(Common.InvoiceType, model.invtype);
                ViewBag.Month = new SelectList(Common.Month, "Value", "Text", model.month);
                //ViewBag.invno = model.invno;
                
                //ViewBag.bank_details = model.bank_details;
                return PartialView("../Invoice/_AddorUpdate", model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // GET: Invoice/GetAgreementDetails/5
        public async Task<ActionResult> GetAgreementDetails(int? agreement_no)
        {
            try
            {
                var invoiceDropdown = new InvoiceDropdown();
                var agreement = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == agreement_no);
                if (agreement != null)
                {
                    var tenant = new TenantDropdown()
                    {
                        Tenantid = agreement.Ag_Tenant_id,
                        TenantName = agreement.Ag_Tenant_Name
                    };
                    var property = new PropertyDropdown()
                    {
                        Propertyid = agreement.Property_ID_Tawtheeq,
                        PropertyName = agreement.Properties_Name
                    };
                    var unit = new UnitDropdown()
                    {
                        Unitid = agreement.Unit_ID_Tawtheeq,
                        unitName = agreement.Unit_Property_Name,
                    };

                    invoiceDropdown.Properties.Add(property);
                    invoiceDropdown.Tenants.Add(tenant);
                    invoiceDropdown.Units.Add(unit);
                }
                return Json(invoiceDropdown, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        // GET: Invoice/GetDropdownValues
        public async Task<ActionResult> GetDropdownValues()
        {
            try
            {
                var invoiceDropdown = new InvoiceDropdown();
                var properties = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Property").OrderBy(x => x.Property_Id).Select(x => new PropertyDropdown { Propertyid = x.Property_ID_Tawtheeq, PropertyName = x.Property_Name }).ToListAsync();

                var units = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Unit").OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new UnitDropdown { Unitid = x.Unit_ID_Tawtheeq, unitName = x.Unit_Property_Name }).ToListAsync();

                var tenants = await db.view_tenant.Select(x => new TenantDropdown { Tenantid = x.Tenant_id, TenantName = x.First_Name }).ToListAsync();
                invoiceDropdown.Properties = properties;
                invoiceDropdown.Units = units;
                invoiceDropdown.Tenants = tenants;
                return Json(invoiceDropdown, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        // GET: Invoice/GetPropertyUnits
        public async Task<ActionResult> GetPropertyUnits(string propertyid)
        {
            try
            {
                var units = new List<UnitDropdown>();
                if (propertyid == null)
                {
                    units = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Unit").OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new UnitDropdown { Unitid = x.Unit_ID_Tawtheeq, unitName = x.Unit_Property_Name }).ToListAsync();
                }
                else
                {
                    units = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Unit" && x.Ref_unit_Property_ID_Tawtheeq == propertyid).OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new UnitDropdown { Unitid = x.Unit_ID_Tawtheeq, unitName = x.Unit_Property_Name }).ToListAsync();
                }
                return Json(units, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        // GET: Invoice/GetUnitProperty
        public async Task<ActionResult> GetUnitProperty(string unitid)
        {
            try
            {
                //var property = new List<PropertyDropdown>();
                //if (unitid==null)
                //{
                //    property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Property").Select(x => new PropertyDropdown { Propertyid = x.Property_ID_Tawtheeq, PropertyName = x.Property_Name }).ToListAsync();
                //}
                //else
                //{
                //    property = await db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Unit_ID_Tawtheeq == unitid).Select(x => new PropertyDropdown { Propertyid = x.Ref_unit_Property_ID_Tawtheeq, PropertyName = x.Ref_Unit_Property_Name }).ToListAsync();
                //}
                var property = await db.tbl_propertiesmaster.FirstOrDefaultAsync(x => x.Delmark != "*" && x.Unit_ID_Tawtheeq == unitid);
                return Json(property != null ? property.Ref_unit_Property_ID_Tawtheeq : null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }

        }


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
                         new MySqlParameter("@Pinvoicedt", null),
                         new MySqlParameter("@Premarks", model.remarks),
                         new MySqlParameter("@Pincno", model.incno),
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
