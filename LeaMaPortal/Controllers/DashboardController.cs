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
        private async Task<List<DashboardModel>> GetDashboardSummaryByCategory(string category, string month = null, string year = null)
        {
            string query = "SELECT * FROM leama.dashboard_summary where category=" + category;
            if (!string.IsNullOrEmpty(month))
                query += "&Month=" + month;
            if (!string.IsNullOrEmpty(year))
                query += "&Year=" + year;
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
