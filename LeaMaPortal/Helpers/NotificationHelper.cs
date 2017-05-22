using LeaMaPortal.DBContext;
using LeaMaPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LeaMaPortal.Helpers
{    
    public class NotificationHelper
    {
        private LeamaEntities db = new LeamaEntities();
        public async Task<List<NotificationRenewalModel>> getNotificationRenewal()
        {
            try
            {
                List<NotificationRenewalModel> data = new List<NotificationRenewalModel>();
                string query = "select y1.region_name ,x.Properties_Name,x.Unit_Property_Name,Agreement_No ,Agreement_Start_Date,Agreement_End_Date from tbl_agreement x inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq,(select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) where current_date() + INTERVAL Notice_Period DAY >= agreement_End_date ; ";
                data = await db.Database.SqlQuery<NotificationRenewalModel>(query).ToListAsync();
                foreach (var obj in data)
                {
                    obj.Properties_Name = string.IsNullOrEmpty(obj.Properties_Name) ? string.Empty : obj.Properties_Name;
                    obj.region_name = string.IsNullOrEmpty(obj.region_name) ? string.Empty : obj.region_name;
                    obj.Unit_Property_Name = string.IsNullOrEmpty(obj.Unit_Property_Name) ? string.Empty : obj.Unit_Property_Name;
                    obj.StartDate = obj.Agreement_Start_Date.ToString("dd/MM/yyyy");
                    obj.EndDate = obj.Agreement_End_Date.ToString("dd/MM/yyyy");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<NotificationPdcModel>> getNotificationPdc()
        {
            try
            {
                List<NotificationPdcModel> data = new List<NotificationPdcModel>();
                string query = "select ifnull(y1.Region_Name, ''), ifnull(x.Property_Name, ''),ifnull(x.unit_Name, ''),Tenant_Name,Agreement_No,DDChequeNo,DDChequedate,pdc_Amount from view_pdc_pending x inner join view_tenant y on x.Tenant_id = y.Tenant_id inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq = ifnull(x.Property_ID, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID = Unit_ID_Tawtheeq)  )  inner join tbl_caretaker z1 on z1.Caretaker_id = y1.Caretaker_id inner join tbl_emailtemplate z on z.templatename = 'Pdc pending' where current_date() + INTERVAL 5 DAY >= ddchequedate; ";
                data = await db.Database.SqlQuery<NotificationPdcModel>(query).ToListAsync();
                foreach (var obj in data)
                {
                    obj.DDChequeNo = string.IsNullOrEmpty(obj.DDChequeNo) ? string.Empty : obj.DDChequeNo;
                    obj.Property_Name = string.IsNullOrEmpty(obj.Property_Name) ? string.Empty : obj.Property_Name;
                    obj.Region_Name = string.IsNullOrEmpty(obj.Region_Name) ? string.Empty : obj.Region_Name;
                    obj.Tenant_Name = string.IsNullOrEmpty(obj.Tenant_Name) ? string.Empty : obj.Tenant_Name;
                    obj.unit_Name = string.IsNullOrEmpty(obj.unit_Name) ? string.Empty : obj.unit_Name;
                    obj.DDDate = obj.DDChequedate.ToString("dd/MM/yyyy");
                }
                return data;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<NotificationRentalDueModel>> getNotificationRentalDue()
        {
            try
            {
                List<NotificationRentalDueModel> data = new List<NotificationRentalDueModel>();
                string query = "SELECT Y.Region_Name,ifnull(x.Properties_Name, y2.Ref_Unit_Property_Name) as 'Properties_Name',ifnull(x.Unit_Property_Name, ''),x.Ag_Tenant_Name ,x.Agreement_No,z.payment_mode,z.Cheque_No,z.cheque_date,z.Cheque_Amount from tbl_agreement x inner join tbl_propertiesmaster y2 on x.property_id = y2.Property_Id inner join tbl_propertiesmaster y on y.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) INNER JOIN TBL_AGREEMENT_PDC Z ON Z.agreement_no = x.agreement_no inner join view_tenant y1 on x.Ag_Tenant_Name = y1.First_Name inner join tbl_caretaker z2 on z2.Caretaker_id = x.Caretaker_id inner join tbl_emailtemplate z3 on z3.templatename = 'Rental overdue single' left join tbl_receipthd z1 on Z1.agreement_no = x.agreement_no  and z.cheque_no = z1.ddchequeno and(z.cheque_date = z1.ddchequedate or z1.ddchequedate is null)  and Reccategory = 'advance' and ifnull(pdcstatus,'') in('cleared','cancelled','') where ifnull(x.status, '')= '' and ifnull(x.delmark, '')<> '*' and payment_mode<> 'pdc' or(payment_mode = 'pdc' and cheque_date <= current_date()) order by x.agreement_no;";
                data = await db.Database.SqlQuery<NotificationRentalDueModel>(query).ToListAsync();
                foreach (var obj in data)
                {
                    obj.Ag_Tenant_Name = string.IsNullOrEmpty(obj.Ag_Tenant_Name) ? string.Empty : obj.Ag_Tenant_Name;
                    obj.Cheque_No = string.IsNullOrEmpty(obj.Cheque_No) ? string.Empty : obj.Cheque_No;
                    obj.payment_mode = string.IsNullOrEmpty(obj.payment_mode) ? string.Empty : obj.payment_mode;
                    obj.Properties_Name = string.IsNullOrEmpty(obj.Properties_Name) ? string.Empty : obj.Properties_Name;
                    obj.Region_Name = string.IsNullOrEmpty(obj.Region_Name) ? string.Empty : obj.Region_Name;
                    obj.ChequeDate = obj.cheque_date.ToString("dd/MM/yyyy");
                }
                return data;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<NotificationUtilityDuesModel>> getNotificationUtilityDues()
        {
            try
            {
                List<NotificationUtilityDuesModel> data = new List<NotificationUtilityDuesModel>();
                string query = "SELECT q.Region_Name ,ifnull(q.Property_Name,q.Ref_Unit_Property_Name) as 'Property_Name'  ,q.Unit_Property_Name , x.Meterno ,p.Utility_Name ,x.billdate ,x.billno ,sum(x.amount) as Billamount,x.duedate FROM  tbl_eb_water_billentrydt x inner join tbl_eb_water_billentryhd x1 on x.refno = x1.refno inner join view_ebbill_payment_pending y on x.refno = y.refno and x.billno = y.billno and x.meterno = y.meterno inner join tbl_metermaster p on p.Meter_no = x.Meterno inner join tbl_propertiesmaster q on ifnull(q.Property_ID_Tawtheeq, Ref_unit_Property_ID_Tawtheeq)= ifnull(p.property_id, '') and ifnull(p.unit_id, '')= ifnull(q.Unit_ID_Tawtheeq, '') inner join tbl_caretaker z1 on z1.Caretaker_id = q.Caretaker_id inner join tbl_emailtemplate z on z.templatename = 'eb water pending' group by x.Refno,x.Meterno ,x.property_id ,q.Property_Name,x.Unit_id ,q.Unit_Property_Name,x.Total_units ,x.Meterreadingno,x.Reading_date ,x.billdate ,x.billno ,x.duedate ,p.utility_id,p.Utility_Name,q.Region_Name,q.country,q.Caretaker_Name,q.Caretaker_ID,z1.Email,TemplateID,z.TemplateName,z.toid ,z.cc,z.bcc,Subject,Body,SubjectParameter,BodyParameter,toparameter,ccparameter,bccparameter,signature; ";
                data = await db.Database.SqlQuery<NotificationUtilityDuesModel>(query).ToListAsync();
                foreach (var obj in data)
                {
                    obj.Property_Name = string.IsNullOrEmpty(obj.Property_Name) ? string.Empty : obj.Property_Name;
                    obj.Region_Name = string.IsNullOrEmpty(obj.Region_Name) ? string.Empty : obj.Region_Name;
                    obj.Unit_Property_Name = string.IsNullOrEmpty(obj.Unit_Property_Name) ? string.Empty : obj.Unit_Property_Name;
                    obj.Utility_Name = string.IsNullOrEmpty(obj.Utility_Name) ? string.Empty : obj.Utility_Name;
                    obj.Meterno = string.IsNullOrEmpty(obj.Meterno) ? string.Empty : obj.Meterno;
                    obj.BDate = obj.billdate.ToString("dd/MM/yyyy");
                    obj.DDate = obj.duedate.ToString("dd/MM/yyyy");
                }
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<NotificationAgreementApprovalModel>> getNotificationAgreementApproval()
        {
            try
            {
                List<NotificationAgreementApprovalModel> data = new List<NotificationAgreementApprovalModel>();
                string query = "select ifnull(y1.Region_Name , ''),ifnull(x.Properties_Name, x.Unit_Property_Name) as Properties_Name, Ag_Tenant_Name,Agreement_No,Total_Rental_amount from tbl_agreement x inner join view_tenant y on x.Ag_Tenant_Name = y.First_Name inner join tbl_caretaker z1 on z1.Caretaker_id = x.Caretaker_id inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) inner join tbl_emailtemplate z on z.templatename = 'Agreement Approval' and ifnull(Approval_Flag,0)= 0;";
                data = await db.Database.SqlQuery<NotificationAgreementApprovalModel>(query).ToListAsync();
                foreach (var obj in data)
                {
                    obj.Region_Name = string.IsNullOrEmpty(obj.Region_Name) ? string.Empty : obj.Region_Name;
                    obj.Properties_Name = string.IsNullOrEmpty(obj.Properties_Name) ? string.Empty : obj.Properties_Name;
                    obj.Unit_Property_Name = string.IsNullOrEmpty(obj.Unit_Property_Name) ? string.Empty : obj.Unit_Property_Name;
                    obj.Ag_Tenant_Name = string.IsNullOrEmpty(obj.Ag_Tenant_Name) ? string.Empty : obj.Ag_Tenant_Name;
                }
                return data;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<NotificationContractApprovalModel>> getNotificationContractApproval()
        {
            try
            {
                List<NotificationContractApprovalModel> data = 
                    await db.tbl_agreement.Where(w => w.Approval_Flag != 1)
                    .Select(s => new NotificationContractApprovalModel
                    {
                        Id = s.id,
                        AgreementNo = s.Agreement_No,
                        PropertyName = s.Properties_Name == null ? string.Empty : s.Properties_Name,
                        UnitName = s.Unit_Property_Name == null ? string.Empty : s.Unit_Property_Name,
                        TenantName = s.Ag_Tenant_Name == null ? string.Empty : s.Ag_Tenant_Name,
                    }).ToListAsync();                
                return data;
            }
            catch
            {
                throw;
            }
        }
    }
}