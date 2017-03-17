namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<tbl_agreement> tbl_agreement { get; set; }
        public virtual DbSet<tbl_agreement_checklist> tbl_agreement_checklist { get; set; }
        public virtual DbSet<tbl_agreement_closure> tbl_agreement_closure { get; set; }
        public virtual DbSet<tbl_agreement_closure_checklist> tbl_agreement_closure_checklist { get; set; }
        public virtual DbSet<tbl_agreement_closure_facility> tbl_agreement_closure_facility { get; set; }
        public virtual DbSet<tbl_agreement_closure_pdc> tbl_agreement_closure_pdc { get; set; }
        public virtual DbSet<tbl_agreement_closure_utility> tbl_agreement_closure_utility { get; set; }
        public virtual DbSet<tbl_agreement_doc> tbl_agreement_doc { get; set; }
        public virtual DbSet<tbl_agreement_facility> tbl_agreement_facility { get; set; }
        public virtual DbSet<tbl_agreement_pdc> tbl_agreement_pdc { get; set; }
        public virtual DbSet<tbl_agreement_status> tbl_agreement_status { get; set; }
        public virtual DbSet<tbl_agreement_unit_inner> tbl_agreement_unit_inner { get; set; }
        public virtual DbSet<tbl_agreement_utility> tbl_agreement_utility { get; set; }
        public virtual DbSet<tbl_approvalconfig> tbl_approvalconfig { get; set; }
        public virtual DbSet<tbl_caretaker> tbl_caretaker { get; set; }
        public virtual DbSet<tbl_checklistmaster> tbl_checklistmaster { get; set; }
        public virtual DbSet<tbl_combo_master> tbl_combo_master { get; set; }
        public virtual DbSet<tbl_country> tbl_country { get; set; }
        public virtual DbSet<tbl_eb_water_billentrydt> tbl_eb_water_billentrydt { get; set; }
        public virtual DbSet<tbl_eb_water_billentryhd> tbl_eb_water_billentryhd { get; set; }
        public virtual DbSet<tbl_eb_water_paymentdt> tbl_eb_water_paymentdt { get; set; }
        public virtual DbSet<tbl_eb_water_paymenthd> tbl_eb_water_paymenthd { get; set; }
        public virtual DbSet<tbl_emailconfigurationmaster> tbl_emailconfigurationmaster { get; set; }
        public virtual DbSet<tbl_emailtemplate> tbl_emailtemplate { get; set; }
        public virtual DbSet<tbl_errorlog> tbl_errorlog { get; set; }
        public virtual DbSet<tbl_facilitymaster> tbl_facilitymaster { get; set; }
        public virtual DbSet<tbl_formmaster> tbl_formmaster { get; set; }
        public virtual DbSet<tbl_invoicedt> tbl_invoicedt { get; set; }
        public virtual DbSet<tbl_invoicehd> tbl_invoicehd { get; set; }
        public virtual DbSet<tbl_log> tbl_log { get; set; }
        public virtual DbSet<tbl_metermaster> tbl_metermaster { get; set; }
        public virtual DbSet<tbl_paymentdt> tbl_paymentdt { get; set; }
        public virtual DbSet<tbl_paymenthd> tbl_paymenthd { get; set; }
        public virtual DbSet<tbl_propertiesdt> tbl_propertiesdt { get; set; }
        public virtual DbSet<tbl_propertiesdt1> tbl_propertiesdt1 { get; set; }
        public virtual DbSet<tbl_propertiesmaster> tbl_propertiesmaster { get; set; }
        public virtual DbSet<tbl_propertytypemaster> tbl_propertytypemaster { get; set; }
        public virtual DbSet<tbl_receiptdt> tbl_receiptdt { get; set; }
        public virtual DbSet<tbl_receipthd> tbl_receipthd { get; set; }
        public virtual DbSet<tbl_region> tbl_region { get; set; }
        public virtual DbSet<tbl_slabmaster> tbl_slabmaster { get; set; }
        public virtual DbSet<tbl_supplierdt> tbl_supplierdt { get; set; }
        public virtual DbSet<tbl_supplierdt1> tbl_supplierdt1 { get; set; }
        public virtual DbSet<tbl_suppliermaster> tbl_suppliermaster { get; set; }
        public virtual DbSet<tbl_tenant_company> tbl_tenant_company { get; set; }
        public virtual DbSet<tbl_tenant_company_government_doc> tbl_tenant_company_government_doc { get; set; }
        public virtual DbSet<tbl_tenant_companydt> tbl_tenant_companydt { get; set; }
        public virtual DbSet<tbl_tenant_companydt1> tbl_tenant_companydt1 { get; set; }
        public virtual DbSet<tbl_tenant_individual> tbl_tenant_individual { get; set; }
        public virtual DbSet<tbl_tenant_individual_doc> tbl_tenant_individual_doc { get; set; }
        public virtual DbSet<tbl_userrights> tbl_userrights { get; set; }
        public virtual DbSet<tbl_utilitiesmaster> tbl_utilitiesmaster { get; set; }
        public virtual DbSet<test_report1> test_report1 { get; set; }
        public virtual DbSet<test_report2> test_report2 { get; set; }
        public virtual DbSet<view_auto_receipt> view_auto_receipt { get; set; }
        public virtual DbSet<view_find_pdcstatus> view_find_pdcstatus { get; set; }
        public virtual DbSet<view_invoice_agreement> view_invoice_agreement { get; set; }
        public virtual DbSet<view_invoice_receipt_pending> view_invoice_receipt_pending { get; set; }
        public virtual DbSet<view_tenant> view_tenant { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Single_Multiple_Flag)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.New_Renewal_flag)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Ag_Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Property_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Properties_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Unit_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Unit_Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Caretaker_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Security_Flag)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Security_chequeno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Approved_By)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Tenant_Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement>()
                .HasOptional(e => e.tbl_agreement_status)
                .WithRequired(e => e.tbl_agreement);

            modelBuilder.Entity<tbl_agreement_checklist>()
                .Property(e => e.Checklist_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_checklist>()
                .Property(e => e.Checklist_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_checklist>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_checklist>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure>()
                .Property(e => e.Advance_Security_Amount_Paid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure>()
                .Property(e => e.Less_any_damanges)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_checklist>()
                .Property(e => e.Checklist_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_checklist>()
                .Property(e => e.Checklist_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_checklist>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_checklist>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_facility>()
                .Property(e => e.Facility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_facility>()
                .Property(e => e.Facility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_facility>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_pdc>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_pdc>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_pdc>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_pdc>()
                .Property(e => e.Cheque_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_pdc>()
                .Property(e => e.Cheque_Date)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_pdc>()
                .Property(e => e.Payment_Mode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_pdc>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_pdc>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_utility>()
                .Property(e => e.Utility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_utility>()
                .Property(e => e.Utility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_utility>()
                .Property(e => e.Payable)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_utility>()
                .Property(e => e.Amount_Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_closure_utility>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_doc>()
                .Property(e => e.Doc_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_doc>()
                .Property(e => e.Doc_Path)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_doc>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_facility>()
                .Property(e => e.Facility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_facility>()
                .Property(e => e.Facility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_facility>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_pdc>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_pdc>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_pdc>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_pdc>()
                .Property(e => e.Cheque_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_pdc>()
                .Property(e => e.Cheque_Date)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_pdc>()
                .Property(e => e.Payment_Mode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_pdc>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .Property(e => e.Ag_Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .Property(e => e.Properties_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .Property(e => e.Caretaker_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .Property(e => e.Unit_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .Property(e => e.Unit_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .Property(e => e.Renewal_Close_Flag)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_status>()
                .HasOptional(e => e.tbl_agreement_closure)
                .WithRequired(e => e.tbl_agreement_status);

            modelBuilder.Entity<tbl_agreement_unit_inner>()
                .Property(e => e.Property_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_unit_inner>()
                .Property(e => e.Properties_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_unit_inner>()
                .Property(e => e.Unit_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_unit_inner>()
                .Property(e => e.Unit_Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_unit_inner>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_utility>()
                .Property(e => e.Utility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_utility>()
                .Property(e => e.Utility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_utility>()
                .Property(e => e.Payable)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_utility>()
                .Property(e => e.Amount_Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_agreement_utility>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_approvalconfig>()
                .Property(e => e.Approval_flag)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_approvalconfig>()
                .Property(e => e.Userid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_approvalconfig>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_approvalconfig>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Caretaker_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Region_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Pincode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Phoneno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_caretaker>()
                .HasMany(e => e.tbl_agreement)
                .WithRequired(e => e.tbl_caretaker)
                .HasForeignKey(e => new { e.Caretaker_id, e.Caretaker_Name })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_caretaker>()
                .HasMany(e => e.tbl_propertiesmaster)
                .WithOptional(e => e.tbl_caretaker)
                .HasForeignKey(e => new { e.Caretaker_ID, e.Caretaker_Name });

            modelBuilder.Entity<tbl_checklistmaster>()
                .Property(e => e.Checklist_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_checklistmaster>()
                .Property(e => e.Checklist_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_checklistmaster>()
                .Property(e => e.Check_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_checklistmaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_checklistmaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_checklistmaster>()
                .HasMany(e => e.tbl_agreement_checklist)
                .WithOptional(e => e.tbl_checklistmaster)
                .HasForeignKey(e => new { e.Checklist_id, e.Checklist_Name });

            modelBuilder.Entity<tbl_combo_master>()
                .Property(e => e.screen_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_combo_master>()
                .Property(e => e.comboname)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_combo_master>()
                .Property(e => e.combovalue)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_country>()
                .Property(e => e.Country_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_country>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_country>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_country>()
                .HasMany(e => e.tbl_region)
                .WithRequired(e => e.tbl_country)
                .HasForeignKey(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_eb_water_billentrydt>()
                .Property(e => e.Meterno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentrydt>()
                .Property(e => e.property_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentrydt>()
                .Property(e => e.Unit_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentrydt>()
                .Property(e => e.Meterreadingno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentrydt>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentryhd>()
                .Property(e => e.Utility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentryhd>()
                .Property(e => e.Utiltiy_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentryhd>()
                .Property(e => e.Supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentryhd>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_billentryhd>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymentdt>()
                .Property(e => e.Meterno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymentdt>()
                .Property(e => e.billNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymentdt>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymentdt>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.Utility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.Utiltiy_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.Supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.PaymentType)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.PaymentMode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.AmtInWords)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.DDChequeNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.pdcstatus)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.BankAcCode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.BankAcName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.AdvAcCode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.Narration)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_eb_water_paymenthd>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.MailServerName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.AttachmentDownloadPath)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.AliasName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.OutFromId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.Mode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.OutMailServerName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.OutEmailId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.OutPassword)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.InMailID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.ServerTimeZone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.IncNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailconfigurationmaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.TemplateName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.Bodytext)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.SubjectParameter)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.BodyParameter)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.UpdateUser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.SubjectText)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.BodyTextParam)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.SubjectParameterText)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.BodyParameterText)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_emailtemplate>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_errorlog>()
                .Property(e => e.Procedure_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_errorlog>()
                .Property(e => e.Error_Text)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_errorlog>()
                .Property(e => e.User)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_errorlog>()
                .Property(e => e.Machine_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_facilitymaster>()
                .Property(e => e.Facility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_facilitymaster>()
                .Property(e => e.Facility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_facilitymaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_facilitymaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_facilitymaster>()
                .HasMany(e => e.tbl_agreement_facility)
                .WithOptional(e => e.tbl_facilitymaster)
                .HasForeignKey(e => new { e.Facility_id, e.Facility_Name });

            modelBuilder.Entity<tbl_facilitymaster>()
                .HasMany(e => e.tbl_propertiesdt)
                .WithOptional(e => e.tbl_facilitymaster)
                .HasForeignKey(e => new { e.Facility_id, e.Facility_Name });

            modelBuilder.Entity<tbl_formmaster>()
                .Property(e => e.Menutype)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_formmaster>()
                .Property(e => e.MenuName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_formmaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_formmaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicedt>()
                .Property(e => e.invno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicedt>()
                .Property(e => e.item)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicedt>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicedt>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.invno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.invtype)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.Property_ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.Unit_ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.unit_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.bank_details)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_invoicehd>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_log>()
                .Property(e => e.Table_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_log>()
                .Property(e => e.Operations)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_log>()
                .Property(e => e.Login_username)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_log>()
                .Property(e => e.Keyfield)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_log>()
                .Property(e => e.Keyfieldvalue)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_log>()
                .Property(e => e.Machine_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_log>()
                .Property(e => e.Procedure_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .Property(e => e.Utility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .Property(e => e.Utility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .Property(e => e.Meter_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .Property(e => e.Accno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .Property(e => e.Property_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .Property(e => e.unit_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_metermaster>()
                .HasMany(e => e.tbl_eb_water_billentrydt)
                .WithOptional(e => e.tbl_metermaster)
                .HasForeignKey(e => e.Meterno);

            modelBuilder.Entity<tbl_paymentdt>()
                .Property(e => e.Invtype)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymentdt>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymentdt>()
                .Property(e => e.InvoiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymentdt>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymentdt>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.Property_ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.Unit_ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.unit_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.Supplier_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.PaymentType)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.PaymentMode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.AmtInWords)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.DDChequeNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.pdcstatus)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.BankAcCode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.BankAcName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.AdvAcCode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.Narration)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_paymenthd>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt>()
                .Property(e => e.Property_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt>()
                .Property(e => e.Unit_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt>()
                .Property(e => e.Facility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt>()
                .Property(e => e.Facility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt>()
                .Property(e => e.Numbers_available)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt1>()
                .Property(e => e.Property_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt1>()
                .Property(e => e.Unit_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt1>()
                .Property(e => e.Utility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt1>()
                .Property(e => e.Utility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesdt1>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Property_Flag)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Property_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Compound)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Zone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.sector)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.plotno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Property_Usage)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Property_Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Street_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Region_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Externalrefno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Caretaker_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Ref_unit_Property_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Ref_Unit_Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Unit_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Unit_Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Externalrefno_unit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Status_unit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Floorno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Floorlevel)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Property_Usage_unit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Property_Type_unit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Unitcomments)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertiesmaster>()
                .HasMany(e => e.tbl_agreement)
                .WithRequired(e => e.tbl_propertiesmaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_propertytypemaster>()
                .Property(e => e.Type_Flag)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertytypemaster>()
                .Property(e => e.Type_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertytypemaster>()
                .Property(e => e.Usage_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertytypemaster>()
                .Property(e => e.Accyear)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertytypemaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_propertytypemaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receiptdt>()
                .Property(e => e.Invno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receiptdt>()
                .Property(e => e.Invtype)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receiptdt>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receiptdt>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receiptdt>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.Reccategory)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.RecpType)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.Property_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.Unit_ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.unit_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.AmtInWords)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.DDChequeNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.PDCstatus)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.BankAcCode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.BankAcName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.AdvAcCode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.Narration)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_receipthd>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_region>()
                .Property(e => e.Region_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_region>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_region>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_region>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_region>()
                .HasMany(e => e.tbl_caretaker)
                .WithOptional(e => e.tbl_region)
                .HasForeignKey(e => new { e.Region_Name, e.Country });

            modelBuilder.Entity<tbl_region>()
                .HasMany(e => e.tbl_propertiesmaster)
                .WithOptional(e => e.tbl_region)
                .HasForeignKey(e => new { e.Region_Name, e.Country });

            modelBuilder.Entity<tbl_slabmaster>()
                .Property(e => e.Utility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_slabmaster>()
                .Property(e => e.Utility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_slabmaster>()
                .Property(e => e.Colour)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_slabmaster>()
                .Property(e => e.Residence_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_slabmaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_slabmaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.Branch)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.BranchAdd)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.Branchadd1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.Pincode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.EMailID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.FaxNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.Cper)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.Cpercode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.Desig)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.Dept)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.Ext)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.MobNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.Salutations)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_supplierdt1>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Supplier_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Marital_Status)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Middle_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Locationlink)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Emirate)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.PostboxNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Mobile_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Mobile_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Mobile_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Landline_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Landline_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Landline_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Fax_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Fax_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Fax_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Nationality)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Actitvity)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Cocandindustryuid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.TradelicenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Issuance_authority)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.ADWEA_Regid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_suppliermaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Marital_Status)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Middle_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Emirate)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.PostboxNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Mobile_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Mobile_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Mobile_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Landline_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Landline_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Landline_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Fax_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Fax_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Fax_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Nationality)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Actitvity)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Cocandindustryuid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.TradelicenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Issuance_authority)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.ADWEA_Regid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company_government_doc>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company_government_doc>()
                .Property(e => e.Doc_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company_government_doc>()
                .Property(e => e.Doc_Path)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_company_government_doc>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.Branch)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.BranchAdd)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.Branchadd1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.Pincode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.EMailID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.FaxNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt1>()
                .Property(e => e.Cper)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt1>()
                .Property(e => e.Desig)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt1>()
                .Property(e => e.Dept)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt1>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt1>()
                .Property(e => e.Ext)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt1>()
                .Property(e => e.MobNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt1>()
                .Property(e => e.Salutations)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_companydt1>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Middle_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Company_Educational)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Profession)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Marital_Status)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Emirate)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.PostboxNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Mobile_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Mobile_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Mobile_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Landline_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Landline_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Landline_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Fax_Countrycode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Fax_Areacode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Fax_No)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Nationality)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Emiratesid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Passportno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Placeofissuance)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.VisaType)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Visano)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Familyno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Familybookcity)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.ADWEA_Regid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual_doc>()
                .Property(e => e.Doc_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual_doc>()
                .Property(e => e.Doc_Path)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tenant_individual_doc>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Userid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Pwd)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Cnfpwd)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Phoneno)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.MenuConfig)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_userrights>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_utilitiesmaster>()
                .Property(e => e.Utility_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_utilitiesmaster>()
                .Property(e => e.Utility_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_utilitiesmaster>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_utilitiesmaster>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_utilitiesmaster>()
                .HasMany(e => e.tbl_metermaster)
                .WithOptional(e => e.tbl_utilitiesmaster)
                .HasForeignKey(e => new { e.Utility_id, e.Utility_Name });

            modelBuilder.Entity<tbl_utilitiesmaster>()
                .HasMany(e => e.tbl_propertiesdt1)
                .WithOptional(e => e.tbl_utilitiesmaster)
                .HasForeignKey(e => new { e.Utility_id, e.Utility_Name });

            modelBuilder.Entity<tbl_utilitiesmaster>()
                .HasMany(e => e.tbl_slabmaster)
                .WithOptional(e => e.tbl_utilitiesmaster)
                .HasForeignKey(e => new { e.Utility_id, e.Utility_Name });

            modelBuilder.Entity<test_report1>()
                .Property(e => e.Prperty_id)
                .IsUnicode(false);

            modelBuilder.Entity<test_report1>()
                .Property(e => e.Property_name)
                .IsUnicode(false);

            modelBuilder.Entity<test_report1>()
                .Property(e => e.Unit_id)
                .IsUnicode(false);

            modelBuilder.Entity<test_report1>()
                .Property(e => e.Unitname)
                .IsUnicode(false);

            modelBuilder.Entity<test_report1>()
                .Property(e => e.Region_Name)
                .IsUnicode(false);

            modelBuilder.Entity<test_report1>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<test_report1>()
                .Property(e => e.Caretaker_Name)
                .IsUnicode(false);

            modelBuilder.Entity<test_report1>()
                .Property(e => e.Ag_Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<test_report2>()
                .Property(e => e.Prperty_id)
                .IsUnicode(false);

            modelBuilder.Entity<test_report2>()
                .Property(e => e.Property_name)
                .IsUnicode(false);

            modelBuilder.Entity<test_report2>()
                .Property(e => e.Unit_id)
                .IsUnicode(false);

            modelBuilder.Entity<test_report2>()
                .Property(e => e.Unitname)
                .IsUnicode(false);

            modelBuilder.Entity<test_report2>()
                .Property(e => e.Region_Name)
                .IsUnicode(false);

            modelBuilder.Entity<test_report2>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<test_report2>()
                .Property(e => e.Caretaker_Name)
                .IsUnicode(false);

            modelBuilder.Entity<test_report2>()
                .Property(e => e.Ag_Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_auto_receipt>()
                .Property(e => e.invno)
                .IsUnicode(false);

            modelBuilder.Entity<view_auto_receipt>()
                .Property(e => e.Property_ID)
                .IsUnicode(false);

            modelBuilder.Entity<view_auto_receipt>()
                .Property(e => e.Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_auto_receipt>()
                .Property(e => e.Unit_ID)
                .IsUnicode(false);

            modelBuilder.Entity<view_auto_receipt>()
                .Property(e => e.unit_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_auto_receipt>()
                .Property(e => e.Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.Reccategory)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.RecpType)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.Property_id)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.Unit_ID)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.unit_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.AmtInWords)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.DDChequeNo)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.PDCstatus)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.BankAcCode)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.BankAcName)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.AdvAcCode)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.Narration)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.Createduser)
                .IsUnicode(false);

            modelBuilder.Entity<view_find_pdcstatus>()
                .Property(e => e.Delmark)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_agreement>()
                .Property(e => e.Ag_Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_agreement>()
                .Property(e => e.Property_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_agreement>()
                .Property(e => e.Properties_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_agreement>()
                .Property(e => e.Unit_ID_Tawtheeq)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_agreement>()
                .Property(e => e.Unit_Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_receipt_pending>()
                .Property(e => e.invno)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_receipt_pending>()
                .Property(e => e.Property_ID)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_receipt_pending>()
                .Property(e => e.Property_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_receipt_pending>()
                .Property(e => e.Unit_ID)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_receipt_pending>()
                .Property(e => e.unit_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_receipt_pending>()
                .Property(e => e.Tenant_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_invoice_receipt_pending>()
                .Property(e => e.invtype)
                .IsUnicode(false);

            modelBuilder.Entity<view_tenant>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_tenant>()
                .Property(e => e.Middle_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_tenant>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<view_tenant>()
                .Property(e => e.type)
                .IsUnicode(false);
        }
    }
}
