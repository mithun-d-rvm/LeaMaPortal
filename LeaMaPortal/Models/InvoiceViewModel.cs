using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Invoice Type")]
        public string invtype { get; set; }
        [DisplayName("Invoice Number")]
        public string invno { get; set; }

        [DisplayName("Invoice Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        [DisplayName("Tenant Id")]
        public int? Tenant_id { get; set; }
        [DisplayName("Tenant Name")]
        public string Tenant_Name { get; set; }
        [DisplayName("Agreement No")]
        public int? Agreement_No { get; set; }
        [DisplayName("Property ID")]
        public string Property_ID { get; set; }
        [DisplayName("Property Name")]
        public string Property_Name { get; set; }
        [DisplayName("Unit ID")]
        public string Unit_ID { get; set; }
        [DisplayName("Unit Name")]
        public string unit_Name { get; set; }
        [DisplayName("Month")]
        public int? month { get; set; }
        [DisplayName("Year")]
        public int? year { get; set; }
        [DisplayName("Total Amount")]
        public float? totalamt { get; set; }
        [DisplayName("Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? duedate { get; set; }
        [DisplayName("Bank Details")]
        public string bank_details { get; set; }
        [DisplayName("Remarks")]
        public string remarks { get; set; }
        public int? incno { get; set; }
        public List<InvoiceDetailsViewModel> InvoiceDetails { get; set; }

    }

    public class InvoiceDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Item Type")]
        public string item { get; set; }
        [DisplayName("Item Description")]
        public string description { get; set; }
        [DisplayName("Quantity")]
        public float? qty { get; set; }
        [DisplayName("Amount")]
        public float? amount { get; set; }

    }
    public class TenantDropdown
    {
        public int Tenantid { get; set; }
        public string TenantName { get; set; }
    }
    public class PropertyDropdown
    {
        public string Propertyid { get; set; }
        public string PropertyName { get; set; }
    }
    public class UnitDropdown
    {
        public string Unitid { get; set; }
        public string unitName { get; set; }
    }
    public class ChequeDropdown
    {
        public string Cheque_No { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? cheque_date { get; set; }
    }
    
    public class InvoiceDropdown
    {
        public InvoiceDropdown()
        {
            Tenants = new List<TenantDropdown>();
            Units = new List<UnitDropdown>();
            Properties = new List<PropertyDropdown>();
            Cheques = new List<ChequeDropdown>();
        }
        public List<TenantDropdown> Tenants { get; set; }
        public List<UnitDropdown> Units { get; set; }
        public List<PropertyDropdown> Properties { get; set; }
        public List<ChequeDropdown> Cheques { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AgreementDate { get; set; }
    }
}