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
    public class TenantCompanyController : Controller
    {
        private Entities db = new Entities();
        // GET: TenantCompany
        public async Task<PartialViewResult> Index()
        {
            try
            {
                //CountryViewModel model = new CountryViewModel();
                //model.List = await db.tbl_country.Where(x => x.Delmark != "*").Select(x => new CountryViewModel()
                //{
                //    Id = x.Id,
                //    Country = x.Country_name
                //}).ToListAsync();
                var model = new TenantCompanyViewModel();
                object[] parameters = {
                     new MySqlParameter("@PFlag", "SELECT"),
                     new MySqlParameter("@PTenant_Id", 0),
                     new MySqlParameter("@PCompanyName", ""),
                     new MySqlParameter("@PMarital_Status", ""),
                     new MySqlParameter("@PTitle", ""),
                     new MySqlParameter("@PFirst_Name", ""),
                     new MySqlParameter("@PMiddle_Name", ""),
                     new MySqlParameter("@PLast_Name", ""),
                     new MySqlParameter("@Paddress", ""),
                     new MySqlParameter("@Paddress1", ""),
                     new MySqlParameter("@PEmirate", ""),
                     new MySqlParameter("@PCity", ""),
                     new MySqlParameter("@PPostboxNo", ""),
                     new MySqlParameter("@PEmail", ""),
                     new MySqlParameter("@PMobile_Countrycode", ""),
                     new MySqlParameter("@PMobile_Areacode", ""),
                     new MySqlParameter("@PMobile_No", ""),
                     new MySqlParameter("@PLandline_Countrycode", ""),
                     new MySqlParameter("@PLandline_Areacode", ""),
                     new MySqlParameter("@PLandline_No", ""),
                     new MySqlParameter("@PFax_Countrycode", ""),
                     new MySqlParameter("@PFax_Areacode", ""),
                     new MySqlParameter("@PFax_No", ""),
                     new MySqlParameter("@PNationality", ""),
                     new MySqlParameter("@PActitvity", ""),
                     new MySqlParameter("@PCocandindustryuid", ""),
                     new MySqlParameter("@PTradelicenseNo", ""),
                     new MySqlParameter("@PLicense_issueDate", "1991-10-12"),
                     new MySqlParameter("@PLicense_ExpiryDate", "1991-10-12"),
                     new MySqlParameter("@PIssuance_authority", ""),
                     new MySqlParameter("@PADWEA_Regid", ""),
                     new MySqlParameter("@PType", ""),
                     new MySqlParameter("@PCreateduser", ""),
                     new MySqlParameter("@Ptenant_companydt", ""),
                     new MySqlParameter("@Ptenant_companydt1", ""),
                     new MySqlParameter("@Ptenant_companydoc", "")

                };
                var tenantCompany = await db.Database.SqlQuery<dynamic>("CALL Usp_Tenant_Company_All(@PFlag, @PTenant_Id, @PCompanyName, @PMarital_Status, @PTitle, @PFirst_Name, @PMiddle_Name, @PLast_Name, @Paddress, @Paddress1, @PEmirate, @PCity, @PPostboxNo, @PEmail, @PMobile_Countrycode, @PMobile_Areacode, @PMobile_No, @PLandline_Countrycode, @PLandline_Areacode, @PLandline_No, @PFax_Countrycode, @PFax_Areacode, @PFax_No, @PNationality, @PActitvity, @PCocandindustryuid, @PTradelicenseNo, @PLicense_issueDate, @PLicense_ExpiryDate, @PIssuance_authority, @PADWEA_Regid, @PType, @PCreateduser, @Ptenant_companydt, @Ptenant_companydt1, @Ptenant_companydoc)", parameters).ToListAsync();
                return PartialView("../Master/TenantCompany/_List", model);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}