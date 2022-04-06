using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class TcaStatusViewModel
    {
        
        public int Id { get; set; }
        public int Agreement_No { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public int Properties_ID { get; set; }
        public string Properties_Name { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Renewal_Close_Flag { get; set; }
        public string Createduser { get; set; }

    }

    public class TcaStatusDisplayModel
    {

        public int Id { get; set; }
        public int Agreement_No { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public int Properties_ID { get; set; }
        public string Properties_Name { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Renewal_Close_Flag { get; set; }
        public string Createduser { get; set; }

        public string AgreementDate { get; set; }
        public string Tenant_Type { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }

    }
    public class TcaPrintModel
    {

        public int Id { get; set; }
        public int Agreement_No { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public string Ag_Tenant_Faxno { get; set; }
        public string Ag_Tenant_Address{ get; set; }
        public string Ag_Tenant_Telephone { get; set; }

        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string CompanyFax { get; set; }

        public int Properties_ID { get; set; }
        public string Properties_Name { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Renewal_Close_Flag { get; set; }
        public string Createduser { get; set; }
        public float? SecurityDeposit { get; set; }
        public string AgreementDate { get; set; }
        public string Tenant_Type { get; set; }
        public string PropertyDet { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }

        public string Agreement_Start_Date { get; set; }
        public string Agreement_End_Date { get; set; }
        public string IssueDate { get; set; }
        public string ContractYearsAndMonths { get; set; }
        public Nullable<float> Total_Rental_amount { get; set; }
        public string TotalAmountInWords { get; set; }
        public string Property_Usage { get; set; }
        public string OtherTerms { get; set; }
        public string PaymentMode { get; set; }
        public string Paymentcount { get; set; }
        public string Region_Name { get; set; }

    }


    public class InvoicePrintModel
    {
        public int Id { get; set; }

        public string invtype { get; set; }
        public string Agreementdate { get; set; }
        public string invdate { get; set; }
        public String PropertyAddress { get; set; }
        public string invno { get; set; }

        public DateTime? date { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public string Ag_Tenant_Faxno { get; set; }
        public string Ag_Tenant_Address { get; set; }
        public string Ag_Tenant_Telephone { get; set; }

        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string CompanyFax { get; set; }

        public string Properties_ID { get; set; }
        public int? Propertyid { get; set; }
        public string Properties_Name { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Renewal_Close_Flag { get; set; }
        public string Createduser { get; set; }
        public float? SecurityDeposit { get; set; }
        public DateTime? AgreementDate { get; set; }
        public string Tenant_Type { get; set; }
        public string PropertyDet { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }

        public string Agreement_Start_Date { get; set; }
        public string Agreement_End_Date { get; set; }
        public string IssueDate { get; set; }
        public string ContractYearsAndMonths { get; set; }

        public int? Tenant_id { get; set; }

        public string Tenant_Name { get; set; }

        public int? Agreement_No { get; set; }

        public string Property_ID { get; set; }
        public string amtinwords { get; set; }
        public string Property_Name { get; set; }

        public string Unit_ID { get; set; }

        public string unit_Name { get; set; }

        public string item { get; set; }
        public string description { get; set; }
        public float qty { get; set; }
        public int amount { get; set; }
        public int? month { get; set; }

        public int? year { get; set; }

        public float? totalamt { get; set; }

        public DateTime? duedate { get; set; }

        public string bank_details { get; set; }

        public string remarks { get; set; }
        public int? incno { get; set; }

       // public string qts { get; set; }


    }


    public class PaymentPrintModel
    {
        public int Id { get; set; }
        public int PaymentNo { get; set; }
        public string Paymenttype { get; set; }
        public string Dhirams { get; set; }
        public string Fils { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Amountinwords { get; set; }
        public string PaymentMode { get; set; }
        public string DDChequeNo { get; set; }
        public string Cheqdate { get; set; }
        public string pdcstatus { get; set; }
        public string BankAcCode { get; set; }
        public string BankAcName { get; set; }
        public string AdvAcCode { get; set; }
        public string Narration { get; set; }

        public string Paymentdate { get; set; }
        public int Agreement_No { get; set; }
        public string Agreementdate { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public string Ag_Tenant_Faxno { get; set; }
        public string Ag_Tenant_Address { get; set; }
        public string Ag_Tenant_Telephone { get; set; }

        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string CompanyFax { get; set; }

        public string Properties_ID { get; set; }
        public string Properties_Name { get; set; }
        public string Properties_Address { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public string Renewal_Close_Flag { get; set; }
        public string Createduser { get; set; }
        public float? SecurityDeposit { get; set; }
        public string AgreementDate { get; set; }
        public string Tenant_Type { get; set; }
        public string PropertyDet { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }

        public string Agreement_Start_Date { get; set; }
        public string Agreement_End_Date { get; set; }
        public string IssueDate { get; set; }
        public string ContractYearsAndMonths { get; set; }

        public int? Tenant_id { get; set; }

        public string Tenant_Name { get; set; }



        public string Property_ID { get; set; }

        public string Property_Name { get; set; }

        public string Unit_ID { get; set; }

        public string unit_Name { get; set; }

        public string item { get; set; }
        public string description { get; set; }
        public float qty { get; set; }
        public int amount { get; set; }
        public int? month { get; set; }

        public int? year { get; set; }

        public float? totalamt { get; set; }

        public DateTime? duedate { get; set; }

        public string bank_details { get; set; }

        public string remarks { get; set; }
        public int? incno { get; set; }


    }


    public class ReceiptPrintModel
    {
        public int id { get; set; }
        public string Reccategory { get; set; }
        public string RecpType { get; set; }
        public int ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public int agreement_no { get; set; }
        public string Property_id { get; set; }
        public string Property_Name { get; set; }
        public string Dhirams { get; set; }
        public string Fills { get; set; }
        public string Unit_ID { get; set; }
        public string unit_Name { get; set; }
        public int Tenant_id { get; set; }
        public string Tenant_Name { get; set; }
        public float TotalAmount { get; set; }

        public string AmtInWords { get; set; }
        public string DDChequeNo { get; set; }
        public string PDCstatus { get; set; }
        public string BankAcCode { get; set; }
        public string BankAcName { get; set; }
        public string AdvAcCode { get; set; }
        public string DDChequeDate { get; set; }
        public string Narration { get; set; }
        public string AgreementDate { get; set; }
        public string Tenant_Type { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public string Ag_Tenant_Faxno { get; set; }
        public string Ag_Tenant_Address { get; set; }
        public string Ag_Tenant_Telephone { get; set; }

    }

    public class EBWaterPrintModel
    {
        public int? id { get; set; }
        public int Refno { get; set; }
        public string refdate { get; set; }
        public string Utility_id { get; set; }
        public string utility_name { get; set; }
        public int? Supplier_id { get; set; }
        public string Supplier_name { get; set; }
        public string Supplier_address { get; set; }
        public string Meterno { get; set; }
        public string property_id { get; set; }
        public string property_name { get; set; }
        public string Unit_id { get; set; }
        public string unit_name { get; set; }
        public decimal? Total_units { get; set; }
        public string Meterreadingno { get; set; }
        public DateTime? Reading_date { get; set; }
        public DateTime? billdate { get; set; }
        public int? billno { get; set; }
        public DateTime? duedate { get; set; }
        public int? daysofuse { get; set; }
        public float? rate { get; set; }
        public float? amount { get; set; }
        public string Delmark { get; set; }

    }

    public class UtilityPaymentPrintModel
    {
        public int? id { get; set; }
        public int PaymentNo { get; set; }
        public string PaymentDate { get; set; }
        public string Utility_id { get; set; }
        public string Utiltiy_name { get; set; }
        public int? Supplier_id { get; set; }
        public string Supplier_name { get; set; }
        public string Supplier_address { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMode { get; set; }
        public float? TotalAmount { get; set; }
        public string AmtInWords { get; set; }
        public string DDChequeNo { get; set; }
        public string Cheqdate { get; set; }
        public string pdcstatus { get; set; }
        public string BankAcCode { get; set; }
        public string BankAcName { get; set; }
        public string AdvAcCode { get; set; }
        public string Narration { get; set; }
        public string Meterno { get; set; }
        public string OtherTerms { get; set; }
        public string Dhirams { get; set; }
        public string Fils { get; set; }
        public DateTime? billdate { get; set; }
        public int? billno { get; set; }
        public float? billamount { get; set; }
        public float? PaidAmount { get; set; }
        public float? Debitamt { get; set; }
        public string Remarks { get; set; }
    }







}