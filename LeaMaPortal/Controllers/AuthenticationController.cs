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
            return View();
        }

        public ActionResult Login()
        {
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
                    return View(LoginModel);
                }
                FormsAuthentication.SetAuthCookie(LoginModel.UserName, true);
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
            //Session.Abandon();
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
    }
}
