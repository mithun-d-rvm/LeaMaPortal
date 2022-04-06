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
        public string regnam;

        private LeamaEntities db = new LeamaEntities();

        public async Task<List<NotificationRenewalModel>> getNotificationRenewal()
        {
            try
            {
                regnam = HttpContext.Current.Session["Region"].ToString();
               // string regname = _context.Session["Region"].ToString();
                List<NotificationRenewalModel> data = new List<NotificationRenewalModel>();
                //string query = "select y1.region_name ,x.Properties_Name,x.Unit_Property_Name,Agreement_No ,Agreement_Start_Date,Agreement_End_Date from tbl_agreement x inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq,(select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) where current_date() + INTERVAL Notice_Period DAY >= agreement_End_date ; ";
                // string query = "select y1.region_name ,x.Properties_Name,x.Unit_Property_Name,Agreement_No ,Agreement_Start_Date,Agreement_End_Date from tbl_agreement x inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) where current_date() + INTERVAL Notice_Period DAY >= agreement_End_date and ifnull(x.status, '') <> 'Closed'" ;
                //string query = "select y1.region_name ,x.Properties_Name,x.Unit_Property_Name,Agreement_No ,Agreement_Start_Date,Agreement_End_Date from tbl_agreement x inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq,(select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) )  where current_date() + INTERVAL Notice_Period DAY >= agreement_End_date  and ifnull(x.status, '') = '' and ifnull(x.delmark, '') <> '*' and ifnull(x.Approval_Flag, 0) = 1";
                string query = "select x.region_name ,x.Properties_Name,x.Unit_Property_Name,Agreement_No ,Agreement_Start_Date,Agreement_End_Date from tbl_agreement x where current_date() + INTERVAL Notice_Period DAY >= agreement_End_date  and ifnull(x.delmark, '') <> '*' and ifnull(x.Approval_Flag, 0) = 1 and x.Region_name = '" + regnam + "'"; //ifnull(x.status, '') = '' and

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
                regnam = HttpContext.Current.Session["Region"].ToString();
                List<NotificationPdcModel> data = new List<NotificationPdcModel>();
                //string query = "select distinct ifnull(y1.Region_Name, '') as region_name, ifnull(x.Property_Name, '') as Property_Name,ifnull(x.unit_Name, '') as unit_Name,Tenant_Name,Agreement_No,DDChequeNo,DDChequedate,pdc_Amount from view_pdc_pending x inner join view_tenant y on x.Tenant_id = y.Tenant_id inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq = ifnull(x.Property_ID, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID = Unit_ID_Tawtheeq)  )  inner join tbl_caretaker z1 on z1.Caretaker_id = y1.Caretaker_id inner join tbl_emailtemplate z on z.templatename = 'Pdc pending' where current_date() + INTERVAL 5 DAY >= ddchequedate; ";
                //string query = "select distinct ifnull(x.Region_Name, '') as region_name, ifnull(x.Property_Name, '') as Property_Name,ifnull(x.unit_Name, '') as unit_Name,Tenant_Name,Agreement_No,DDChequeNo,DDChequedate,pdc_Amount  from view_pdc_pending x  inner join view_tenant y on x.Tenant_id = y.Tenant_id inner join tbl_emailtemplate z on z.templatename = 'Pdc pending' where current_date() + INTERVAL 5 DAY >= ddchequedate and x.Region_Name ='" + regnam + "'";
                string query = "select distinct ifnull(x.Region_Name, '') as region_name, ifnull(x.Property_Name, '') as Property_Name,ifnull(x.unit_Name, '') as unit_Name,Tenant_Name,Agreement_No,DDChequeNo,DDChequedate,pdc_Amount from view_pdc_pending x inner join view_tenant y on x.Tenant_id = y.Tenant_id inner join tbl_emailtemplate z on z.templatename = 'Pdc pending' where current_date() + INTERVAL 5 DAY >= ddchequedate and x.region_name='" + regnam + "'";
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
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<List<NotificationRentalDueModel>> getNotificationRentalDue()
        {
            try
            {
                regnam = HttpContext.Current.Session["Region"].ToString();
                List<NotificationRentalDueModel> data = new List<NotificationRentalDueModel>();
                // string query = "SELECT Y.Region_Name,ifnull(x.Properties_Name, y2.Ref_Unit_Property_Name) as 'Properties_Name',ifnull(x.Unit_Property_Name, ''),x.Ag_Tenant_Name ,x.Agreement_No,z.payment_mode,z.Cheque_No,z.cheque_date,z.Cheque_Amount from tbl_agreement x inner join tbl_propertiesmaster y2 on x.property_id = y2.Property_Id inner join tbl_propertiesmaster y on y.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) INNER JOIN TBL_AGREEMENT_PDC Z ON Z.agreement_no = x.agreement_no inner join view_tenant y1 on x.Ag_Tenant_Name = y1.First_Name inner join tbl_caretaker z2 on z2.Caretaker_id = x.Caretaker_id inner join tbl_emailtemplate z3 on z3.templatename = 'Rental overdue single' left join tbl_receipthd z1 on Z1.agreement_no = x.agreement_no  and z.cheque_no = z1.ddchequeno and(z.cheque_date = z1.ddchequedate or z1.ddchequedate is null)  and Reccategory = 'advance' and ifnull(pdcstatus,'') in('cleared','cancelled','') where ifnull(x.status, '')= '' and ifnull(x.delmark, '')<> '*' and payment_mode<> 'pdc' or(payment_mode = 'pdc' and cheque_date <= current_date()) order by x.agreement_no;";
                //string query = "SELECT Y.Region_Name,ifnull(x.Properties_Name, y2.Ref_Unit_Property_Name) as 'Properties_Name',ifnull(x.Unit_Property_Name, ''),x.Ag_Tenant_Name ,x.Agreement_No,z.payment_mode,case when z.Cheque_No = '' then 0 else z.Cheque_No end as Cheque_No,ifnull(z.cheque_date,'1900-00-01') as cheque_date,z.Cheque_Amount from tbl_agreement x inner join tbl_propertiesmaster y2 on x.property_id = y2.Property_Id inner join tbl_propertiesmaster y on y.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) INNER JOIN TBL_AGREEMENT_PDC Z ON Z.agreement_no = x.agreement_no inner join view_tenant y1 on x.Ag_Tenant_Name = y1.First_Name inner join tbl_caretaker z2 on z2.Caretaker_id = x.Caretaker_id inner join tbl_emailtemplate z3 on z3.templatename = 'Rental overdue single' left join tbl_receipthd z1 on Z1.agreement_no = x.agreement_no  and z.cheque_no = z1.ddchequeno and(z.cheque_date = z1.ddchequedate or z1.ddchequedate is null)  and Reccategory = 'advance' and ifnull(pdcstatus,'') in('cleared','cancelled','') where ifnull(x.status, '')= '' and ifnull(x.delmark, '')<> '*' and payment_mode<> 'pdc' or(payment_mode = 'pdc' and cheque_date <= current_date()) order by x.agreement_no;";
                // string query = "SELECT distinct Y.Region_Name,ifnull(x.Properties_Name, y2.Ref_Unit_Property_Name) as 'Properties_Name',ifnull(x.Unit_Property_Name, ''),x.Ag_Tenant_Name ,x.Agreement_No,z.payment_mode,case when z.Cheque_No = '' then 0 else z.Cheque_No end as Cheque_No,ifnull(z.cheque_date, '1900-00-01') as cheque_date,z.Cheque_Amount from tbl_agreement x inner join tbl_propertiesmaster y2 on x.property_id = y2.Property_Id inner join tbl_propertiesmaster y on y.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) INNER JOIN TBL_AGREEMENT_PDC Z ON Z.agreement_no = x.agreement_no inner join view_tenant y1 on x.Ag_Tenant_Name = y1.First_Name inner join tbl_caretaker z2 on z2.Caretaker_id = x.Caretaker_id inner join tbl_emailtemplate z3 on z3.templatename = 'Rental overdue single' left join tbl_receipthd z1 on Z1.agreement_no = x.agreement_no  and z.cheque_no = z1.ddchequeno and(z.cheque_date = z1.ddchequedate or z1.ddchequedate is null)  and Reccategory = 'advance' and ifnull(pdcstatus,'') in('cleared','cancelled','') where ifnull(x.status, '')= '' and ifnull(x.delmark, '')<> '*' and ifnull(x.Approval_Flag,0)=1 and payment_mode<> 'pdc' or(payment_mode = 'pdc' and cheque_date <= current_date()) order by x.agreement_no;";
                //string query = "SELECT distinct Y.Region_Name ,ifnull(x.Properties_Name, y2.Ref_Unit_Property_Name) as 'Properties_Name',ifnull(x.Unit_Property_Name, '') as 'Unit_Property_Name' ,x.Ag_Tenant_Name as 'Ag_Tenant_Name',x.Agreement_No as 'Agreement_No',z.payment_mode as 'payment_mode',z.Cheque_No as 'Cheque_No' ,ifnull(z.cheque_date, '') as 'cheque_date',z.Cheque_Amount as 'Cheque_Amount',z4.Amount from tbl_agreement x inner join tbl_propertiesmaster y2 on x.property_id = y2.Property_Id inner join tbl_propertiesmaster y on y.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq,  (select  Ref_unit_Property_ID_Tawtheeq from  tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq)  ) inner JOIN TBL_AGREEMENT_PDC Z ON Z.agreement_no = x.agreement_no inner join view_tenant y1 on x.Ag_Tenant_Name = y1.First_Name inner join tbl_caretaker z2 on z2.Caretaker_id = x.Caretaker_id inner join tbl_emailtemplate z3 on z3.templatename = 'Rental overdue single' inner join view_agree_recp_chequenowise_pending z4 on z4.agreement_no = z.agreement_no and ifnull(z4.Cheque_No, '')= ifnull(z.Cheque_No, '') and ifnull(z4.Cheque_Date, '')= ifnull(z.cheque_date, '') and ifnull(z4.Payment_Mode, '')= ifnull(z.Payment_Mode, '') where ifnull(x.Approval_Flag, 0)= 1 and ifnull(x.status, '')= '' and ifnull(x.delmark, '')<> '*' and(z.payment_mode <> 'pdc' or z.payment_mode = 'pdc') and z.cheque_date <= current_date() order by x.agreement_no;";

                string query = "SELECT distinct z.Region_Name ,ifnull(x.Properties_Name, y2.Ref_Unit_Property_Name) as 'Properties_Name', ifnull(x.Unit_Property_Name, '') as 'Unit_Property_Name' ,x.Ag_Tenant_Name as 'Ag_Tenant_Name', x.Agreement_No as 'Agreement_No',z.payment_mode as 'payment_mode',z.Cheque_No as 'Cheque_No',ifnull(z.cheque_date, '') as 'cheque_date',z.Cheque_Amount as 'Cheque_Amount',z4.Amount from tbl_agreement x inner join tbl_propertiesmaster y2 on x.property_id = y2.Property_Id inner JOIN TBL_AGREEMENT_PDC Z ON Z.agreement_no = x.agreement_no inner join view_tenant y1 on x.Ag_Tenant_Name = y1.First_Name inner join tbl_caretaker z2 on z2.Caretaker_id = x.Caretaker_id inner join tbl_emailtemplate z3 on z3.templatename = 'Rental overdue single' inner join view_agree_recp_chequenowise_pending z4 on z4.agreement_no = z.agreement_no and z4.Region_name = z.Region_name and ifnull(z4.Cheque_No, '')= ifnull(z.Cheque_No, '') and ifnull(z4.Cheque_Date, '')= ifnull(z.cheque_date, '') and ifnull(z4.Payment_Mode, '')= ifnull(z.Payment_Mode, '') where ifnull(x.Approval_Flag, 0)= 1 and ifnull(x.delmark, '')<> '*' and(z.payment_mode <> 'pdc' or z.payment_mode = 'pdc') and z.cheque_date <= current_date() and z.region_name = '" + regnam  + "' order by x.agreement_no"; //ifnull(x.status, '')= '' and
                data = await db.Database.SqlQuery<NotificationRentalDueModel>(query).ToListAsync();
                foreach (var obj in data)
                {
                    obj.Ag_Tenant_Name = string.IsNullOrEmpty(obj.Ag_Tenant_Name ) ? string.Empty : obj.Ag_Tenant_Name;
                    obj.Cheque_No = string.IsNullOrEmpty(obj.Cheque_No) ? string.Empty : obj.Cheque_No;
                    obj.payment_mode = string.IsNullOrEmpty(obj.payment_mode) ? string.Empty : obj.payment_mode;
                    obj.Properties_Name = string.IsNullOrEmpty(obj.Properties_Name) ? string.Empty : obj.Properties_Name;
                    obj.Region_Name = string.IsNullOrEmpty(obj.Region_Name) ? string.Empty : obj.Region_Name;
                    obj.ChequeDate = obj.cheque_date.ToString("dd/MM/yyyy");
                }

                //foreach (var obj in data)
                //{
                //    obj.Ag_Tenant_Name = string.IsNullOrEmpty(obj.Ag_Tenant_Name) ? string.Empty : obj.Ag_Tenant_Name;
                //    obj.Cheque_No = string.IsNullOrEmpty(obj.Cheque_No) ? string.Empty : obj.Cheque_No;
                //    obj.payment_mode = string.IsNullOrEmpty(obj.payment_mode) ? string.Empty : obj.payment_mode;
                //    obj.Properties_Name = string.IsNullOrEmpty(obj.Properties_Name) ? string.Empty : obj.Properties_Name;
                //    obj.Region_Name = string.IsNullOrEmpty(obj.Region_Name) ? string.Empty : obj.Region_Name;
                //    obj.ChequeDate = obj.cheque_date.ToString("dd/MM/yyyy");
                //}
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<NotificationUtilityDuesModel>> getNotificationUtilityDues()
        {
            try
            {
                regnam = HttpContext.Current.Session["Region"].ToString();
                List<NotificationUtilityDuesModel> data = new List<NotificationUtilityDuesModel>();
                //string query = "SELECT q.Region_Name ,ifnull(q.Property_Name,q.Ref_Unit_Property_Name) as 'Property_Name'  ,q.Unit_Property_Name , x.Meterno ,p.Utility_Name ,x.billdate ,x.billno ,sum(x.amount) as Billamount,x.duedate FROM  tbl_eb_water_billentrydt x inner join tbl_eb_water_billentryhd x1 on x.refno = x1.refno inner join view_ebbill_payment_pending y on x.refno = y.refno and x.billno = y.billno and x.meterno = y.meterno inner join tbl_metermaster p on p.Meter_no = x.Meterno inner join tbl_propertiesmaster q on ifnull(q.Property_ID_Tawtheeq, Ref_unit_Property_ID_Tawtheeq)= ifnull(p.property_id, '') and ifnull(p.unit_id, '')= ifnull(q.Unit_ID_Tawtheeq, '') inner join tbl_caretaker z1 on z1.Caretaker_id = q.Caretaker_id inner join tbl_emailtemplate z on z.templatename = 'eb water pending' group by x.Refno,x.Meterno ,x.property_id ,q.Property_Name,q.Ref_Unit_Property_Name,x.Unit_id ,q.Unit_Property_Name,x.Total_units ,x.Meterreadingno,x.Reading_date ,x.billdate ,x.billno ,x.duedate ,p.utility_id,p.Utility_Name,q.Region_Name,q.country,q.Caretaker_Name,q.Caretaker_ID,z1.Email,TemplateID,z.TemplateName,z.toid ,z.cc,z.bcc,Subject,Body,SubjectParameter,BodyParameter,toparameter,ccparameter,bccparameter,signature; ";
                string query = "SELECT x.Region_Name ,ifnull(q.Property_Name,q.Ref_Unit_Property_Name) as 'Property_Name' , q.Unit_Property_Name , x.Meterno ,p.Utility_Name ,x.billdate ,x.billno ,sum(x.amount) as Billamount,x.duedate FROM tbl_eb_water_billentrydt x inner join tbl_eb_water_billentryhd x1 on x.refno = x1.refno inner join view_ebbill_payment_pending y on x.refno = y.refno and x.billno = y.billno and x.meterno = y.meterno and y.Region_Name = x.Region_Name inner join tbl_metermaster p on p.Meter_no = x.Meterno inner join tbl_propertiesmaster q on ifnull(q.Property_ID_Tawtheeq, Ref_unit_Property_ID_Tawtheeq) = ifnull(p.property_id, '') and ifnull(p.unit_id, '')= ifnull(q.Unit_ID_Tawtheeq, '') inner join tbl_caretaker z1 on z1.Caretaker_id = q.Caretaker_id  inner join tbl_emailtemplate z on z.templatename = 'eb water pending' where x.Region_Name = '"+ regnam + "' group by x.Refno,x.Meterno ,x.property_id ,q.Property_Name,q.Ref_Unit_Property_Name,x.Unit_id , q.Unit_Property_Name,x.Total_units ,x.Meterreadingno,x.Reading_date ,x.billdate ,x.billno ,x.duedate ,p.utility_id,p.Utility_Name,x.Region_Name,q.country,q.Caretaker_Name,q.Caretaker_ID,z1.Email,TemplateID , z.TemplateName,z.toid ,z.cc,z.bcc,Subject,Body,SubjectParameter,BodyParameter,toparameter,ccparameter , bccparameter, signature";
               

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
                regnam = HttpContext.Current.Session["Region"].ToString();
                List<NotificationAgreementApprovalModel> data = new List<NotificationAgreementApprovalModel>();
                //string query = "select ifnull(y1.Region_Name , '') as Region_Name,ifnull(x.Properties_Name, x.Unit_Property_Name) as Properties_Name, Ag_Tenant_Name,Agreement_No,Total_Rental_amount from tbl_agreement x inner join view_tenant y on x.Ag_Tenant_Name = y.First_Name inner join tbl_caretaker z1 on z1.Caretaker_id = x.Caretaker_id inner join tbl_propertiesmaster y1 on y1.Property_ID_Tawtheeq = ifnull(x.Property_ID_Tawtheeq, (select Ref_unit_Property_ID_Tawtheeq from tbl_propertiesmaster where x.Unit_ID_Tawtheeq = Unit_ID_Tawtheeq) ) inner join tbl_emailtemplate z on z.templatename = 'Agreement Approval' and ifnull(Approval_Flag,0)= 0;";
                string query = "select ifnull(x.Region_Name , '') as Region_Name,ifnull(x.Properties_Name,'') as Properties_Name,ifnull(x.Unit_Property_Name,'') as Unit_Property_Name, Ag_Tenant_Name,Agreement_No,Total_Rental_amount from tbl_agreement x  inner join tbl_caretaker z1 on z1.Caretaker_id = x.Caretaker_id  inner join tbl_emailtemplate z on z.templatename = 'Agreement Approval' where ifnull(x.Approval_Flag,0)= 0 and ifnull(x.Delmark,'') <> '*' and x.region_name ='" + regnam + "' order by agreement_no"; //and x.region_name ='"+ regname + "'
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
                regnam = HttpContext.Current.Session["Region"].ToString();
                List<NotificationContractApprovalModel> data = new List<NotificationContractApprovalModel>();
                if (db.tbl_approvalconfig.Any(x => x.Userid == HttpContext.Current.User.Identity.Name && x.Approval_flag=="Yes"))
                {
                    data =
                    await db.tbl_agreement.Where(w => w.Approval_Flag != 1)
                    .Select(s => new NotificationContractApprovalModel
                    {
                        Id = s.id,
                        AgreementNo = s.Agreement_No,
                        PropertyName = s.Properties_Name == null ? string.Empty : s.Properties_Name,
                        UnitName = s.Unit_Property_Name == null ? string.Empty : s.Unit_Property_Name,
                        TenantName = s.Ag_Tenant_Name == null ? string.Empty : s.Ag_Tenant_Name,
                    }).ToListAsync();
                }
                
                return data;
            }
            catch
            {
                throw;
            }
        }

        //public async Task<List<NotificationLicenseExpiryModel>> getNotificationLicenseExpiry()
        //{
        //    try
        //    {
        //        List<NotificationLicenseExpiryModel> data = new List<NotificationLicenseExpiryModel>();
        //        string query = "select * from view_expirydate_notifications";
        //        data = await db.Database.SqlQuery<NotificationLicenseExpiryModel>(query).ToListAsync();
        //        foreach (var obj in data)
        //        {
        //            obj.id = obj.id;
        //            obj.Name = string.IsNullOrEmpty(obj.Name) ? string.Empty : obj.Name;
        //            obj.issuedate = obj.issuedate;
        //            obj.Expirydate = obj.Expirydate;
        //            obj.Category = string.IsNullOrEmpty(obj.Category) ? string.Empty : obj.Category;
        //            obj.Status = string.IsNullOrEmpty(obj.Status) ? string.Empty : obj.Status;
        //            obj.Flag = string.IsNullOrEmpty(obj.Flag) ? string.Empty : obj.Flag;
        //        }
        //        return data;
        //    }
        //    catch(Exception ex)
        //    {
        //        //throw ex;
        //    }
        //}

        public async Task<List<NotificationLicenseExpiryModel>> getNotificationLicenseExpiry()
        {
            try
            {
                regnam = HttpContext.Current.Session["Region"].ToString();
                List<NotificationLicenseExpiryModel> data = new List<NotificationLicenseExpiryModel>();
                string query = "select * from view_expirydate_notifications where region_name ='" + regnam + "'";
                data = await db.Database.SqlQuery<NotificationLicenseExpiryModel>(query).ToListAsync();
                foreach (var obj in data)
                {
                    obj.id = obj.id;
                    obj.Name = string.IsNullOrEmpty(obj.Name) ? string.Empty : obj.Name;
                    obj.ISSDAT = obj.issuedate.ToString("dd/MM/yyyy");
                    obj.EXPDAT = obj.Expirydate.ToString("dd/MM/yyyy");
                    obj.Category = string.IsNullOrEmpty(obj.Category) ? string.Empty : obj.Category;
                    obj.Status = string.IsNullOrEmpty(obj.Status) ? string.Empty : obj.Status;
                    obj.Flag = string.IsNullOrEmpty(obj.Flag) ? string.Empty : obj.Flag;
                    //obj.BDate = obj.billdate.ToString("dd/MM/yyyy");
                    //obj.DDate = obj.duedate.ToString("dd/MM/yyyy");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}