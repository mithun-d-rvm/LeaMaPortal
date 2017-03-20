using LeaMaPortal.Models;
using LeaMaPortal.Models.DBContext;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class MasterIndividualController : Controller
    {
        private Entities db = new Entities();
        // GET: MasterIndividual
        public ActionResult Index()
        {
            return View();
        }

        // GET: MasterIndividual/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MasterIndividual/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterIndividual/Create
        [HttpPost]
        public async Task<ActionResult> Create(TenantIndividualViewModel model)
        {
            try
            {
                List<MySqlParameter> param = Helper.GetMySqlParameters<TenantIndividualViewModel>(model);
                var RE = db.Database.SqlQuery<tbl_tenant_individual>(@"Usp_Tenant_Individual_All(
                    @PFlag ,@PTenant_Id  ,@PTitle  ,@PFirst_Name  ,@PMiddle_Name  ,@PLast_Name  ,@PCompany_Educational   ,@PProfession  ,@PMarital_Status  ,@Paddress  ,@Paddress1  ,@PEmirate  ,@PCity  ,@PPostboxNo  ,@PEmail  ,@PMobile_Countrycode  ,@PMobile_Areacode  ,@PMobile_No  ,@PLandline_Countrycode  ,@PLandline_Areacode  ,@PLandline_No  ,@PFax_Countrycode  ,@PFax_Areacode  ,@PFax_No  ,@PNationality  ,@PEmiratesid  ,@PEmirate_issuedate  ,@PEmirate_expirydate  ,@PPassportno  
,@PPlaceofissuance  
,@PPassport_Issuedate
,@PPassport_Expirydate  
,@PVisaType  
,@PVisano  
,@PVisa_IssueDate  
,@PVisa_ExpiryDate 
,@PDob  
,@PFamilyno  
,@PFamilybookcity  
,@PADWEA_Regid  
,@PType  
,@PCreateduser  
,@Ptenantdocdetails 

                    )", param);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterIndividual/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MasterIndividual/Edit/5
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

        // GET: MasterIndividual/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MasterIndividual/Delete/5
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
