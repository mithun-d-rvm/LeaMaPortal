using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using System.Data;
using System.Data.Entity;

namespace LeaMaPortal
{
     
    public static class Common
    {
       public static LeamaEntities db = new LeamaEntities();
        public static List<string> Title { get
            {
               return GetTitle();
            }
        }
        public static List<string> TcaTenantType
        {
            get
            {
                return GetTcaTenantType();
            }
        }
        public static List<string> SecurityFlag
        {
            get
            {
                return GetSecurityFlag();
            }
        }
        public static List<string> PaymentMode
        {
            get
            {
                return GetPaymentMode();
            }
        }
        public static List<string> TenantType
        {
            get
            {
                return GetTenantType();
            }
        }
        public static List<string> Emirate
        {
            get
            {
                return GetEmirate();
            }
        }
        public static List<string> ComapanyActivity
        {
            get
            {
                return GetComapanyActivity();
            }
        }

        public static List<string> Nationality
        {
            get
            {
                return GetNationality();
            }
        }
        public static List<string> InvoiceType
        {
            get
            {
                return GetInvoiceType();
            }
        }
        public static List<string> Reccategory
        {
            get
            {
                return GetReccategory();
            }
        }
        public static List<string> ReceiptMode
        {
            get
            {
                return GetReceiptMode();
            }
        }
        public static List<string> Receipts_PDCStatus
        {
            get
            {
                return GetReceipts_PDCStatus();
            }
        }

        public static List<string> GetTitle()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Tenant individual" && x.comboname == "Title")?.combovalue.Split(',').ToList();
        }
        public static List<string> GetTcaTenantType()
        {
            return new List<string> { "Company", "Individual" };
        }
        public static List<string> GetSecurityFlag()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Tenancy Contract Agreement" && x.comboname == "Security_Flag")?.combovalue.Split(',').ToList();
        }
        public static List<string> GetPaymentMode()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Tenancy Contract Agreement" && x.comboname == "Security_Flag")?.combovalue.Split(',').ToList();
        }
        public static List<string> GetTenantType()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Tenant Master" && x.comboname == "Type")?.combovalue.Split(',').ToList();
        }

        public static List<string> GetEmirate()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Tenant Company" && x.comboname == "Emirate")?.combovalue.Split(',').ToList();
        }

        public static List<string> GetComapanyActivity()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Tenant Company" && x.comboname == "Actitvity")?.combovalue.Split(',').ToList();
        }
        public static List<string> GetNationality()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Slab Master" && x.comboname == "Residence_type")?.combovalue.Split(',').ToList();
        }
        public static List<string> GetInvoiceType()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Invoice" && x.comboname == "invtype")?.combovalue.Split(',').ToList();
        }
        public static List<string> GetReccategory()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Receipts" && x.comboname == "Reccategory")?.combovalue.Split(',').ToList();
        }
        public static List<string> GetReceiptMode()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Receipts" && x.comboname == "RecpType")?.combovalue.Split(',').ToList();
        }
        public static List<string> GetReceipts_PDCStatus()
        {
            return db.tbl_combo_master.FirstOrDefault(x => x.screen_name == "Receipts" && x.comboname == "PDCstatus")?.combovalue.Split(',').ToList();
        }

        //public static List<string> Title = new List<string>() { "MR.", "MRS.", "MS." };
        //public static List<string> TcaTenantType = new List<string>() { "Company", "Individual" };
        //public static List<string> SecurityFlag = new List<string>() { "Cash", "Cheque", "Online", "Cheque without date", "DD" };

        public static List<string> Months= new List<string>() { "Jan", "Feb", "Mar", "April", "May","June","July","Aug","Sep","Oct","Nov","Dec" };
        //public static List<string> PaymentMode = new List<string>() { "Cash", "Cheque", "Online", "Cheque without date", "DD" };
        //public const string DefaultTitle = "Mr.";
        public static List<string> Role = new List<string>() { "Admin", "Management", "Caretaker" };
        //public static List<string> City = new List<string>() { "Abudhabi", "Sharja" };
        public static List<string> Profession = new List<string>() { "Engineer", "Teacher", "Shop keeper", "Doctor", "Farmer" };

        public const string INSERT = "INSERT", UPDATE = "UPDATE", DELETE = "DELETE", SELECT = "SELECT", View = "View";
        public const int DefaultMaster = 9;
        public const string Bank_number = "XXXXYYYYZZZZ";
        public const string TenantIndividualDocumentContainer = "Documents/TenantIndividual/";
        public const string TenantIndividualDocumentDirectoryName = "TenantIndividual";

        public static List<FormMaster> FormMasterList = new List<FormMaster>()
        {
           //new FormMaster() {Id=1,FormName="" },
           new FormMaster() {Id=2,MenuName="User Rights" },
           //new FormMaster() {Id=3,FormName="" },
           new FormMaster() {Id=4,MenuName="Approval" },
           new FormMaster() {Id=5,MenuName="Country Master" },
           new FormMaster() {Id=6,MenuName="Region Master" },
           new FormMaster() {Id=7,MenuName="Caretaker Master" },
           new FormMaster() {Id=8,MenuName="Property Type Master" },
           new FormMaster() {Id=9,MenuName="Property Master" },
           new FormMaster() {Id=10,MenuName="Tenant Master - Company" },
           new FormMaster() {Id=11,MenuName="Tenant Master - Individual" },
           new FormMaster() {Id=12,MenuName="Checklist Master" },
           new FormMaster() {Id=13,MenuName="Facility Master" },
           new FormMaster() {Id=14,MenuName="Utility Master" },
           new FormMaster() {Id=15,MenuName="Supplier Master" },
           new FormMaster() {Id=16,MenuName="Slab Master" },
           new FormMaster() {Id=17,MenuName="Meter Master" },
           new FormMaster() {Id=18,MenuName="Email Template" },
           //new FormMaster() {Id=19,FormName="" },
           //new FormMaster() {Id=20,FormName="" }

        };
        public const int DefaultReport = 1;
        public static List<ReportListModel> ReportList = new List<ReportListModel>()
        {
           new ReportListModel() {Id=1,ReportName="Vacancy Report" },
           new ReportListModel() {Id=2,ReportName="Tenancy Contract Report" },
           new ReportListModel() {Id=3,ReportName="Outstanding Report" },
           new ReportListModel() {Id=4,ReportName="Utility Outstanding Payments Report" },
           new ReportListModel() {Id=5,ReportName="PDC Report" },
           new ReportListModel() {Id=6,ReportName="Collection Report Summary" },
           new ReportListModel() {Id=7,ReportName="Summary Ebwater Report" },
        };
        //tenant company
        //public static List<string> TenantType = new List<string>() { "Government", "Person", "Company" };
        //public static List<string> Emirate = new List<string>() { "Default" };
        //public static List<string> ComapanyActivity = new List<string>() { "Activity1" };
        public static List<string> Issuance_authority = new List<string>() { "List-1" };
        public const string TenantCompanyDocumentContainer = "Documents/TenantCompany/";
        public const string TenantCompanyDocumentDirectoryName = "TenantCompany";
        
        //public static List<string> Nationality = new List<string>() { "UAE", "Non-UAE" };
        public static string DefaultNationality = "UAE";
        public static string DefaultMaridalStatus = "Family";
        //public static List<string> InvoiceType = new List<string>() { "Rental", "Others" };

        //agreement
        public const string AgreementDocumentContainer = "Documents/AgreementDocument/";
        public const string AgreementDocumentDirectoryName = "AgreementDocument";
        
        public const string AgreementCheck_type= "New Contract";
        public const string NewAgreement = "New";
        public const string Renewal = "Renewal";
        
        public static List<MonthField> Month = new List<MonthField>()
        {
            new MonthField
            {
                Text = "January",
                Value = 1
            },
            new MonthField
            {
                Text = "February",
                Value = 2
            },
            new MonthField
            {
                Text = "March",
                Value = 3
            },
            new MonthField
            {
                Text = "Apirl",
                Value = 4
            },
            new MonthField
            {
                Text = "May",
                Value = 5
            },
            new MonthField
            {
                Text = "June",
                Value = 6
            },
            new MonthField
            {
                Text = "July",
                Value = 7
            },
            new MonthField
            {
                Text = "August",
                Value = 8
            },
            new MonthField
            {
                Text = "September",
                Value = 9
            },
            new MonthField
            {
                Text = "October",
                Value = 10
            },
            new MonthField
            {
                Text = "November",
                Value = 11
            },
            new MonthField
            {
                Text = "December",
                Value = 12
            }
        };
        

        //public static List<string> Reccategory = new List<string>() { "advance", "against", "invoice", "others", "security", "deposit" };
        //public static List<string> ReceiptMode = new List<string>() { "cheque", "cash", "online", "Pdc", "advance adjustment" };

        //public static List<string> BankAccountNumber = new List<string>() { "Account 1", "Account 2", "Account 3", "Account 4" };
        //public static List<string> BankAccountName = new List<string>() { "Axis Bank", "ICICI Bank", "HDFC Bank", "SBI Bank" };

        //public static List<string> Receipts_PDCStatus = new List<string>() { "Received", "Cleared", "Bounced", "Cancelled" };

        public static List<BankDetails> BankDetails = new List<BankDetails>()
        {
            new BankDetails()
            {
                BankName="Axis",
                AccountNumber="Account1"
            },
            new BankDetails()
            {
                BankName="ICCI",
                AccountNumber="Account 2"
            }
        };
        //public static List<string> Receipts_PDCStatus = new List<string>() { "Received", "Cleared", "Bounced", "Cancelled" };

        public static List<string> ReportGroup = new List<string>() { "Property", "Region", "Caretaker" };
        public static List<string> AgingDaysFilter = new List<string>() { "All", "Others" };
        public static List<string> RentalAmountFilter = new List<string>() { "All", "Others" };
    }
    public class FormMaster
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
    }
    public class MonthField
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
    public class BankDetails
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }


    public class ReportListModel
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
    }
}