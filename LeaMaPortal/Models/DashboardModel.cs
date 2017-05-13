using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class DashboardModel
    {
        [DisplayName("Category")]
        public string category { get; set; }
        [DisplayName("Total Value")]
        public string Total_Value { get; set; }
        [DisplayName("Residential")]
        public string Residential { get; set; }
        [DisplayName("Commercial")]
        public string Commercial { get; set; }
        [DisplayName("Water")]
        public string water { get; set; }
        [DisplayName("EB")]
        public string Eb { get; set; }
        [DisplayName("Others")]
        public string Others { get; set; }
        [DisplayName("overdues")]
        public string overdues { get; set; }
        [DisplayName("Pdc")]
        public string Pdc { get; set; }
        [DisplayName("Month")]
        public string Month { get; set; }

        [DisplayName("Year")]
        public string Year { get; set; }

    }

    public class DashboardEarningModel
    {
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string Property_Name { get; set; }
        public string unit_Name { get; set; }
        public string Tenant_Name { get; set; }
        public string TotalAmount { get; set; }
    }
    public class DashboardVacancyModel
    {
        public string Property_Name { get; set; }
        public string unit_Name { get; set; }
        public string Property_Usage { get; set; }
        public string Region_Name { get; set; }
        public string Caretaker_Name { get; set; }
        public string Country { get; set; }
        public string Rental_Rate_Month { get; set; }
        public string Vacant_Start_Date { get; set; }
        public string Aging_Days { get; set; }
    }
    public class DashboardVacancyLossModel
    {
        public string Property_Name { get; set; }
        public string unit_Name { get; set; }
        public string Property_Usage { get; set; }
        public string Region_Name { get; set; }
        public string Caretaker_Name { get; set; }
        public string Country { get; set; }
        public string Rental_Rate_Month { get; set; }
        public string Vacant_Start_Date { get; set; }
        public string Aging_Days { get; set; }
        public string Loss_Amt { get; set; }
    }
    public class DashboardRentalModel
    {
        public string Agreement_No { get; set; }
        public string Property_Name { get; set; }
        public string unit_Name { get; set; }
        public string Region_Name { get; set; }
        public string Caretaker_Name { get; set; }
        public string Country { get; set; }
        public string Ag_Tenanat_Name { get; set; }
        public string payment_mode { get; set; }
        public string totalamount { get; set; }
    }
    public class DashboardExpensesModel
    {
        public string PaymentNo { get; set; }
        public string PaymentDate { get; set; }
        public string Utility_name { get; set; }
        public string Supplier_name{ get; set; }
        public string PaymentType{ get; set; }
        public string PaymentMode { get; set; }
        public string TotalAmount{ get; set; }
    }
    public class DashboardUtilityModel
    {
        public string refno { get; set; }
        public string meterno { get; set; }
        public string billno { get; set; }
        public string amount { get; set; }
        public string utility_name{ get; set; }
    }
}