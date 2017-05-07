using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.Helpers;
using System.Threading.Tasks;
using LeaMaPortal.DBContext;
using LeaMaPortal.Models;

namespace LeaMaPortal.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IntialLoader()
        {
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpGet]
        public async Task<JsonResult> Summary()
        {
            try
            {
                var result = await db.Database.SqlQuery<DashboardModel>("call Usp_Dashboardreport").ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetDashboardSummary(string category, string month = null, string year = null)
        {
            try
            {
                var summary = await GetDashboardSummaryByCategory(category);
                return Json(summary, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetPDCNotification()
        {
            try
            {
                string query = @"select count(*) as pdccount from (
select 
TemplateID
,z.TemplateName
,z.toid 
,z.cc
,z.bcc
,Subject
,Body
,SubjectParameter
,BodyParameter
,toparameter
,ccparameter
,bccparameter
,signature
,Agreement_No 
,y.Tenant_id 
,Tenant_Name
,ifnull(x.Property_id,'') as Property_id
,ifnull(x.Property_Name,'') as Property_Name
,ifnull(x.Unit_ID ,'') as Unit_ID
,ifnull(x.unit_Name ,'') as unit_Name
,pdc_Amount 
,DDChequeNo 
,DDChequedate 
,z1.Caretaker_id
,z1.Caretaker_Name
,y.Emailid as Tenantemailid
,z1.email as  caretakeremailid
,ifnull(y1.address1,'') as address1
,ifnull(y1.address2,'') as address2
,ifnull(y1.address3,'') as address3
,ifnull(y1.Region_Name ,'') as Region_Name
,ifnull(y1.Country,'') as Country
from view_pdc_pending x 
inner join view_tenant y on x.Tenant_id=y.Tenant_id

inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq =ifnull(x.Property_ID,
 (select  Ref_unit_Property_ID_Tawtheeq from
 tbl_propertiesmaster where  x.Unit_ID=Unit_ID_Tawtheeq)
 )
 inner join tbl_caretaker z1 on z1.Caretaker_id=y1.Caretaker_id
inner join tbl_emailtemplate z on z.templatename='Pdc pending'
where current_date()+INTERVAL 5 DAY>=ddchequedate
)x 
";

                var result = await db.Database.SqlQuery<int>(query).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetRenewals()
        {
            try
            {
                string query = @"select count(*) from(
select Agreement_no from tbl_agreement where current_date() + INTERVAL Notice_Period DAY =agreement_End_date 
union 
select Agreement_no from tbl_agreement where current_date() + INTERVAL Notice_Period DAY >=agreement_End_date 
union 
select Agreement_no from tbl_agreement where agreement_End_date between current_date() and date_ADD(current_date(), INTERVAL 7 DAY)
)x
";

                var result = await db.Database.SqlQuery<int>(query).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetContractExpiry()
        {
            try
            {
                string query = @"select count(*) from(
select Agreement_no from tbl_agreement where current_date() + INTERVAL Notice_Period DAY =agreement_End_date 
union 
select Agreement_no from tbl_agreement where current_date() + INTERVAL Notice_Period DAY >=agreement_End_date 
union 
select Agreement_no from tbl_agreement where agreement_End_date between current_date() and date_ADD(current_date(), INTERVAL 7 DAY)
)x
";

                var result = await db.Database.SqlQuery<int>(query).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<List<DashboardModel>> GetDashboardSummaryByCategory(string category, string month = null, string year = null)
        {
            string query = "SELECT * FROM dashboard_summary where category=" + category+"";
            if (!string.IsNullOrEmpty(month))
                query += " and Month=" + month;
            if (!string.IsNullOrEmpty(year))
                query += " and Year=" + year;
            var result = await db.Database.SqlQuery<DashboardModel>(query).ToListAsync();
            return result;
        }
        //[HttpGet]
        //public async Task<JsonResult> GetRentDues()
        //{
        //    try
        //    {
        //        var result = await db.Database.SqlQuery<DashboardModel>("call Usp_Dashboardreport").ToListAsync();
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //[HttpGet]
        //public async Task<JsonResult> GetTotalExpense()
        //{
        //    try
        //    {
        //        var result = await db.Database.SqlQuery<DashboardModel>("call Usp_Dashboardreport").ToListAsync();
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        // GET: Dashboard/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
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

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
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

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
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
