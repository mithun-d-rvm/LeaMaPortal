using LeaMaPortal.Helpers;
using LeaMaPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            try
            {
                GetUserRights();
            }
            catch(Exception e)
            {
                throw;
            }
            
        }
        public LoggedinUser CurrentUser
        {
           
            get
            {
                try
                {
                    return AuthenticationHelper.GetLoggedinUserDetails(System.Web.HttpContext.Current).Result;
                }
                catch (Exception e)
                {
                    throw;
                }
                
            }
        }
        public void GetUserRights()
        {
            ViewBag.IsEditable = CurrentUser.EditConfig;
            ViewBag.IsDeletable = CurrentUser.DeleteConfig;
            ViewBag.IsAdd = CurrentUser.AddConfig;
            ViewBag.MenuConfig = CurrentUser.MenuConfig.Split(',');
        }
    }
}