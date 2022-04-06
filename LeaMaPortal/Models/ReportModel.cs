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

    public class PropertyStatusReportModel
    {
        public int id { get; set; }
        public string Property_Flag { get; set; }
        public string Property_ID_Tawtheeq { get; set; }
        public int Property_ID { get; set; }
        public string Property_Name { get; set; }
        public string Compound { get; set; }
        public string Zone { get; set; }
        public string sector { get; set; }
        public string plotno { get; set; }
        public string ownedbyregistrant { get; set; }
        public string Property_Usage { get; set; }
        public string Property_Type { get; set; }
        public string Commercial_villa { get; set; }
        public string Street_Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }        
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Externalrefno { get; set; }
        public string Noofoffloors { get; set; }
        public string Noofunits { get; set; }
        public string Builtarea { get; set; }
        public string Plotarea { get; set; }
        public string Leasablearea { get; set; }
        public string commonarea { get; set; }
        public string completion_Date { get; set; }
        public string AEDvalue { get; set; }
        public string Purchased_date { get; set; }
        public string Valued_Date { get; set; }
        public string Status { get; set; }
        public string Vacant_Start_Date { get; set; }
        public string Caretaker_Name { get; set; }
        public int Cartaker_ID { get; set; }
        public string Rental_Rate_Month { get; set; }
        public string Comments { get; set; }
        public string Ref_unit_Property_id_Tawtheeq { get; set; }
        public string Ref_Unit_Property_ID { get; set; }
        public string Ref_Unit_Property_Name { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }
        public string Externalrefno_unit { get; set; }
        public string AEDvalue_unit { get; set; }
        public string Purchased_date_unit { get; set; }
        public string Valued_Date_unit { get; set; }
        public string Status_unit { get; set; }
        public string Vacant_Start_Date_Unit { get; set; }
        public string Rental_Rate_Month_unit { get; set; }
        public string Floorno { get; set; }
        public string Floorlevel { get; set; }
        public string Property_Usage_unit { get; set; }
        public string Property_Type_unit { get; set; }
        public string Total_Area { get; set; }
        public string Unit_Common_Area { get; set; }
        public string Common_Area { get; set; }
        public string Parkingno { get; set; }
        public string Unitcomments { get; set; }
        public string Company_occupied_Flag { get; set; }
        public string Accyear { get; set; }
        public string Createddatetime { get; set; }
        public string Createduser { get; set; }
        public string Delmark { get; set; }
        public string Agreement_No { get; set; }
        public string Agreement_Date { get; set; }
        public string Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public string Vacantstartdate { get; set; }
        public string Agreement_Start_Date { get; set; }
        public string Agreement_End_Date { get; set; }
        public string Total_Rental_amount { get; set; }
        public string user { get; set; }

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

    public class TallyReportModel
    {
        [DisplayName("ReportName:")]
        public string ReportName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("From Date:")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("To Date:")]
        public DateTime? ToDate { get; set; }
    }

    public class vacancy_report
    {
    public int id { get; set; }
    public string Property_Flag { get; set; }
    public string Property_id { get; set; }
    public string Property_Name { get; set; }
    public string Unit_id { get; set; }
    public string Unitname { get; set; }
    public string Region_Name { get; set; }
    public string Country { get; set; }
    public Nullable<int> Caretaker_id { get; set; }
    public string Caretaker_Name { get; set; }
    public Nullable<double> Rental_Rate_Month { get; set; }
    public Nullable<System.DateTime> Vacant_Start_Date { get; set; }
    public Nullable<int> Aging_Days { get; set; }
    public Nullable<double> Loss_Amt { get; set; }
    public string user { get; set; }
   }
}