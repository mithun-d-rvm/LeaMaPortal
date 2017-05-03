using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class VacancyReportModel
    {

        [DisplayName("Group:")]
        public string Group { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("From Date:")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("To Date:")]
        public DateTime? ToDate { get; set; }

        [DisplayName("Filter by:")]
        public string Filterby { get; set; }

        [DisplayName("Value:")]
        public string Value { get; set; }

        [DisplayName("Aging Filter:")]
        public string AgingFilter { get; set; }

        [DisplayName("From:")]
        public int AgingFrom { get; set; }

        [DisplayName("To:")]
        public int AgingTo { get; set; }

        [DisplayName("Rental Amount Filter:")]
        public string RentalAmountFilter { get; set; }

        [DisplayName("From:")]
        public int RentalAmountFrom { get; set; }

        [DisplayName("To:")]
        public int RentalAmountTo { get; set; }

        [DisplayName("Created User")]
        public string CreatedUser { get; set; }
    }
    public class VacantRegionReportModel
    {
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public long Noof_properties { get; set; }
        public int Aging_days { get; set; }
        public string Aging_Range { get; set; }
        public string Amount_Range { get; set; }
        public double Loss_Amt { get; set; }
    }

    public class VacantCaretakerReportModel
    {
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public long Noof_properties { get; set; }
        public int Aging_days { get; set; }
        public string Aging_Range { get; set; }
        public string Amont_Range { get; set; }
        public double Loss_Amt { get; set; }
        public string user { get; set; }
    }

    public class VacantPropertyReportModel
    {
        public int id { get; set; }
        public string Property_Flag { get; set; }
        public string Property_id { get; set; }
        public string Property_Name { get; set; }
        public string Unit_id { get; set; }
        public string Unitname { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public double Rental_Rate_Month { get; set; }
        public DateTime Vacant_Start_Date { get; set; }
        public int Aging_Days { get; set; }
        public double Loss_Amt { get; set; }
        public string user { get; set; }
    }

    public class ContractPropertyReportModel
    {
        public int id { get; set; }
        public int Agreement_No { get; set; }
        public string Property_id { get; set; }
        public string Property_name { get; set; }
        public string Unit_id { get; set; }
        public string Unitname { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public Nullable<System.DateTime> Agreement_End_Date { get; set; }
        public Nullable<int> Notice_Period { get; set; }
        public Nullable<float> Perday_Rental { get; set; }
        public Nullable<int> Remaining_Days { get; set; }
        public Nullable<double> Contract_Value { get; set; }
        public string user { get; set; }
    }
    public class ContractCaretakerReportModel
    {
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Noof_properties { get; set; }
        public int Remaining_Days { get; set; }
        public string Remaining_Range { get; set; }
        public string Amont_Range { get; set; }
        public float Contract_Value { get; set; }
        public string user { get; set; }
    }
    public class ContractRegionReportModel
    {
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public long Noof_properties { get; set; }
        public int Remaining_Days { get; set; }
        public string Remaining_Range { get; set; }
        public string Amont_Range { get; set; }
        public float Contract_Value { get; set; }
        public string user { get; set; }
    }
    public class EBWaterPropertyReportModel
    {
        public int id { get; set; }
        public Nullable<int> Refno { get; set; }
        public string Meterno { get; set; }
        public string property_id { get; set; }
        public string Property_Name { get; set; }
        public string Unit_id { get; set; }
        public string Unit_Property_Name { get; set; }
        public Nullable<float> Total_units { get; set; }
        public string Meterreadingno { get; set; }
        public Nullable<System.DateTime> Reading_date { get; set; }
        public Nullable<System.DateTime> billdate { get; set; }
        public Nullable<int> billno { get; set; }
        public Nullable<System.DateTime> duedate { get; set; }
        public string utility_id { get; set; }
        public string Utility_Name { get; set; }
        public string Region_Name { get; set; }
        public string country { get; set; }
        public string Caretaker_Name { get; set; }
        public Nullable<int> Caretaker_ID { get; set; }
        public Nullable<double> Billamount { get; set; }
        public Nullable<double> Paidamount { get; set; }
        public Nullable<double> OutstandingAmt { get; set; }
        public Nullable<int> Aging_Days { get; set; }
        public string user { get; set; }
    }
    public class OutstandingPropertyReportModel
    {
        public int id { get; set; }
        public int Agreement_No { get; set; }
        public string Property_id { get; set; }
        public string Property_name { get; set; }
        public string Unit_id { get; set; }
        public string Unitname { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public Nullable<System.DateTime> Agreement_Start_Date { get; set; }
        public Nullable<System.DateTime> Agreement_End_Date { get; set; }
        public Nullable<float> Total_Rental_amount { get; set; }
        public Nullable<double> outstanding_amt { get; set; }
        public Nullable<float> Perday_Rental { get; set; }
        public Nullable<int> Remaining_Days { get; set; }
        public Nullable<double> Contract_Value { get; set; }
        public string user { get; set; }
    }
    public class OutstandingRegionReportModel
    {
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public long Noof_properties { get; set; }
        public int Remaining_Days { get; set; }
        public string Remaining_Range { get; set; }
        public string Amont_Range { get; set; }
        public float outstanding_amt { get; set; }
        public string user { get; set; }
    }
    public class OutstandingCaretakerReportModel
    {
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Noof_properties { get; set; }
        public int Remaining_Days { get; set; }
        public string Remaining_Range { get; set; }
        public string Amont_Range { get; set; }
        public float outstanding_amt { get; set; }
        public string user { get; set; }
    }
    public class EBWaterRegionReportModel
    {
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public long Noof_properties { get; set; }
        public int Aging_days { get; set; }
        public string Aging_Range { get; set; }
        public string Amount_Range { get; set; }
        public float OutstandingAmt { get; set; }
        public string user { get; set; }
        public string utility_id { get; set; }
        public string Utility_Name { get; set; }
    }
    public class EBWaterCaretakerReportModel
    {
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Noof_properties { get; set; }
        public int Aging_days { get; set; }
        public string Aging_Range { get; set; }
        public string Amont_Range { get; set; }
        public float OutstandingAmt { get; set; }
        public string user { get; set; }
        public string utility_id { get; set; }
        public string Utility_Name { get; set; }
    }
    public class CollectionSummaryReportModel
    {
        public int id { get; set; }
        public int property_id { get; set; }
        public string Property_ID_Tawtheeq { get; set; }
        public string Properties_Name { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public string country { get; set; }
        public string region_name { get; set; }
        public string Reccategory { get; set; }
        public string RecpType { get; set; }
        public string pdcstatus { get; set; }
        public string DDChequeNo { get; set; }
        public Nullable<System.DateTime> DDChequeDate { get; set; }
        public Nullable<float> Total_Rental_amount { get; set; }
        public Nullable<float> totalamount { get; set; }
        public string user { get; set; }
    }
    public class CollectionSummaryRegionReportModel
    {
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Noof_properties { get; set; }
        public float Billamount { get; set; }
        public float Paidamount { get; set; }
        public string user { get; set; }
    }
    public class CollectionSummaryCaretakerReportModel
    {
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Noof_properties { get; set; }
        public float totalrentalamount { get; set; }
        public float Totalpaidamt { get; set; }
        public string user { get; set; }
    }
    public class SummaryEBWaterpropertyReportModel
    {
        public int id { get; set; }
        public Nullable<int> Refno { get; set; }
        public string Meterno { get; set; }
        public string property_id { get; set; }
        public string Property_Name { get; set; }
        public string Unit_id { get; set; }
        public string Unit_Property_Name { get; set; }
        public Nullable<float> Total_units { get; set; }
        public string Meterreadingno { get; set; }
        public Nullable<System.DateTime> Reading_date { get; set; }
        public Nullable<System.DateTime> billdate { get; set; }
        public Nullable<int> billno { get; set; }
        public Nullable<System.DateTime> duedate { get; set; }
        public string utility_id { get; set; }
        public string Utility_Name { get; set; }
        public string Region_Name { get; set; }
        public string country { get; set; }
        public string Caretaker_Name { get; set; }
        public Nullable<int> Caretaker_ID { get; set; }
        public Nullable<double> Billamount { get; set; }
        public Nullable<double> Paidamount { get; set; }
        public Nullable<int> Aging_Days { get; set; }
        public string user { get; set; }
    }
    public class SummaryEBWaterRegionReportModel
    {
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Noof_properties { get; set; }
        public float Billamount { get; set; }
        public float Paidamount { get; set; }
        public string user { get; set; }
    }
    public class SummaryEBWaterCaretakerReportModel
    {
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Noof_properties { get; set; }
        public float Billamount { get; set; }
        public float Paidamount { get; set; }
        public string user { get; set; }
    }
    public class PDCReportModel
    {
        public int id { get; set; }
        public int Agreement_No { get; set; }
        public string Property_id { get; set; }
        public string Property_name { get; set; }
        public string Unit_id { get; set; }
        public string Unitname { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public Nullable<System.DateTime> Agreement_Start_Date { get; set; }
        public Nullable<System.DateTime> Agreement_End_Date { get; set; }
        public Nullable<float> Total_Rental_amount { get; set; }
        public Nullable<double> outstanding_amt { get; set; }
        public Nullable<float> cheque_amount { get; set; }
        public string Cheque_No { get; set; }
        public Nullable<System.DateTime> cheque_date { get; set; }
        public string pdcstatus { get; set; }
        public string user { get; set; }
    }
}