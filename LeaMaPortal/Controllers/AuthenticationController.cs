using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using LeaMaPortal.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LeaMaPortal.Controllers
{
    public class AuthenticationController : Controller
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: Authentication
        public ActionResult Index()
        {
            ////var regnam = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Property" && w.Delmark != "*" && w.Region_Name == RegNam).OrderBy(o => o.id);
            //var regnam = db.tbl_region.Where(r => r.Delmark != "*").OrderBy(o => o.Id);
            ////var Regions = db.Database.SqlQuery<RegionViewModel>("select Region_Name from tbl_region where ifnull(delmark,'')<>'*'").ToListAsync();
            //ViewBag.Region_Name = new SelectList(regnam, "Region_Name", "Region_Name");
            //ViewData["Region_Name"] = new SelectList(regnam, "Region_Name", "Region_Name");
            return View();
        }

        public ActionResult Login()
        { 
            ////var regnam = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Property" && w.Delmark != "*" && w.Region_Name == RegNam).OrderBy(o => o.id);
            //var regnam = db.tbl_region.Where(r => r.Delmark != "*").OrderBy(o => o.Id);
            ////var Regions = db.Database.SqlQuery<RegionViewModel>("select Region_Name from tbl_region where ifnull(delmark,'')<>'*'").ToListAsync();
            //ViewBag.Region_Name = new SelectList(regnam, "Region_Name", "Region_Name");
            //ViewData["Region_Name"] = new SelectList(regnam, "Region_Name", "Region_Name");
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public async Task<ActionResult> Login([Bind(Include = "UserName,Password")] LoginModel LoginModel, string returnUrl)
        {
            try
            {
                var user = await db.tbl_userrights.FirstOrDefaultAsync(x => x.Userid == LoginModel.UserName && x.Pwd == LoginModel.Password && x.Delmark == null);
                if (user == null)
                {
                    ViewBag.ErrorMessage = ErrorMessage.InvalidUsernameAndPassword;
                    Session["Region"] = "";
                    Session["Country"] = "";
                    Session["Category"] = "";
                    Session["Userid"] = "";
                    return View(LoginModel);
                }
                FormsAuthentication.SetAuthCookie(LoginModel.UserName, true);
                Session["Region"] = user.Region_Name;
                Session["Country"] = user.Country;
                Session["Category"] = user.Category;
                Session["Userid"] = user.Userid;
                //Session[""];
                return RedirectToLocal(returnUrl);
            }
            catch (Exception e)
            {
                return View(LoginModel);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            Session.Abandon();
            return RedirectToAction("Login");
        }

        // GET: Authentication/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Authentication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authentication/Create
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

        // GET: Authentication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Authentication/Edit/5
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

        // GET: Authentication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Authentication/Delete/5
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
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("../Dashboard/Index");
                //return RedirectToAction("Index", "Dashboard");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetRegions()
        {
            try
            {
                var Regions = await db.Database.SqlQuery<RegionViewModel>("select Region_Name from tbl_region where ifnull(delmark,'')<>'*'").ToListAsync();
                return Json(Regions, JsonRequestBehavior.AllowGet);                                       
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
