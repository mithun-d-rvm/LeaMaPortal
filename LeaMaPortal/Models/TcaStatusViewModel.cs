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

    }
}