using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public DateTime date { get; set; }
        [DisplayName("Tenant Id")]
        public int Tenant_id { get; set; }
        [DisplayName("Tenant Name")]
        public string Tenant_Name { get; set; }
        [DisplayName("Agreement No")]
        public int Agreement_No { get; set; }
        [DisplayName("Property ID")]
        public string Property_ID { get; set; }
        [DisplayName("Property Name")]
        public string Property_Name { get; set; }
        [DisplayName("Unit ID")]
        public string Unit_ID { get; set; }
        [DisplayName("Unit Name")]
        public string unit_Name { get; set; }
        [DisplayName("Month")]
        public int month { get; set; }
        [DisplayName("Year")]
        public int year { get; set; }
        [DisplayName("Subtotal")]
        public decimal totalamt { get; set; }
        [DisplayName("Due Date")]
        public DateTime duedate { get; set; }
        [DisplayName("Bank Details")]
        public string bank_details { get; set; }
        [DisplayName("Remarks")]
        public string remarks { get; set; }
      
    }

    public class InvoiceDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Item Type")]
        public string item { get; set; }
        [DisplayName("Item Description")]
        public string description { get; set; }
        [DisplayName("Quantity")]
        public DateTime qty { get; set; }
        [DisplayName("Amount")]
        public int amount { get; set; }
        
    }
}