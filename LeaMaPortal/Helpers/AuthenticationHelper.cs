using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace LeaMaPortal.Helpers
{
    public static class AuthenticationHelper
    {
        public static async Task<LoggedinUser> GetLoggedinUserDetails()
        {
            try
            {
                LeamaEntities db = new LeamaEntities();
                //object[] parameters = {
                //         new MySqlParameter("@PFlag", "SELECT"),
                //         new MySqlParameter("@Pid", 0),
                //         new MySqlParameter("@PName", ""),
                //         new MySqlParameter("@PUserid", ""),
                //         new MySqlParameter("@PPwd", ""),
                //         new MySqlParameter("@PCnfpwd", ""),
                //         new MySqlParameter("@PCategory", ""),
                //         new MySqlParameter("@PEmail", ""),
                //         new MySqlParameter("@PPhoneno", ""),
                //         new MySqlParameter("@PAddConfig", 0),
                //         new MySqlParameter("@PEditConfig", 0),
                //         new MySqlParameter("@PDeleteConfig", 0),
                //         new MySqlParameter("@PMenuConfig", 0),
                //         new MySqlParameter("@PActive", 0),
                //         new MySqlParameter("@PCreateduser", ""),

                //    };
                //var userrights =await db.Database.SqlQuery<tbl_userrights>("CALL Usp_Userrights_All(@PFlag,@Pid,@PName,@PUserid,@PPwd,@PCnfpwd,@PCategory,@PEmail,@PPhoneno,@PAddConfig,@PEditConfig,@PDeleteConfig,@PMenuConfig,@PActive,@PCreateduser)", parameters).FirstOrDefaultAsync();
                var userrights = await db.tbl_userrights.FirstOrDefaultAsync(x => x.Userid == HttpContext.Current.User.Identity.Name && x.Delmark == null);
                var user = new LoggedinUser();
                if (userrights != null)
                {

                    user.Userid = userrights.Userid;
                    user.AddConfig = userrights.AddConfig;
                    user.DeleteConfig = userrights.DeleteConfig;
                    user.EditConfig = userrights.EditConfig;
                    user.Email = userrights.Email;
                    user.Category = userrights.Category;
                    user.MenuConfig = userrights.MenuConfig;
                    user.Name = userrights.Name;
                }
                HttpContext.Current.Session["user"] = user;
                return user;
            }
            catch
            {
                throw;
            }
        }
    }
}