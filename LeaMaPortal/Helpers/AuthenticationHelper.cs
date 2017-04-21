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
                if (userrights == null)
                {
                    return new LoggedinUser();
                }
                return new LoggedinUser()
                {
                    Userid = userrights.Userid,
                    AddConfig = userrights.AddConfig,
                    DeleteConfig = userrights.DeleteConfig,
                    EditConfig = userrights.EditConfig,
                    Email = userrights.Email,
                    Category = userrights.Category,
                    MenuConfig = userrights.MenuConfig,
                    Name = userrights.Name
                };
            }
            catch
            {
                throw;
            }
        }
    }
}