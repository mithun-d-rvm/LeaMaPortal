//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeaMaPortal.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_paymenthd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_paymenthd()
        {
            this.tbl_paymentdt = new HashSet<tbl_paymentdt>();
        }
    
        public int id { get; set; }
        public int PaymentNo { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> agreement_no { get; set; }
        public string Property_ID { get; set; }
        public string Property_Name { get; set; }
        public string Unit_ID { get; set; }
        public string unit_Name { get; set; }
        public Nullable<int> Supplier_id { get; set; }
        public string Supplier_Name { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<float> TotalAmount { get; set; }
        public string AmtInWords { get; set; }
        public string DDChequeNo { get; set; }
        public Nullable<System.DateTime> Cheqdate { get; set; }
        public string pdcstatus { get; set; }
        public string BankAcCode { get; set; }
        public string BankAcName { get; set; }
        public string AdvAcCode { get; set; }
        public string Narration { get; set; }
        public Nullable<int> Accyear { get; set; }
        public Nullable<System.DateTime> Createddatetime { get; set; }
        public string Createduser { get; set; }
        public string Delmark { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_paymentdt> tbl_paymentdt { get; set; }
    }
}
