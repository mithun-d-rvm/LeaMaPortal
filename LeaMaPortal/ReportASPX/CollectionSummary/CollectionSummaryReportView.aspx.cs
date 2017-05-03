using LeaMaPortal.Models;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeaMaPortal.ReportASPX.CollectionSummary
{
    public partial class CollectionSummaryReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txt_CreatedUser.Text = System.Web.HttpContext.Current.User.Identity.Name;
        }
        protected void filterbyChange(object sender, EventArgs e)
        {
            if (dropDown_FilterBy.SelectedItem.Value == "--Select--")
            {
                txt_filterBy.Text = "";
                txt_filterBy.ReadOnly = true;
            }
            else
            {
                txt_filterBy.ReadOnly = false;
            }
        }
        protected void groupDropdownChange(object sender, EventArgs e)
        {
            dropDown_FilterBy.Items.Clear();
            if (dropDown_Group.SelectedItem.Value == "Property")
            {
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "--Select--", Value = "--Select--", Selected = true });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "id", Value = "id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Refno", Value = "Refno" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Meterno", Value = "Meterno" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "property_id", Value = "property_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Property_Name", Value = "Property_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unit_id", Value = "Unit_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unit_Property_Name", Value = "Unit_Property_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Total_units", Value = "Total_units" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Meterreadingno", Value = "Meterreadingno" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Reading_date", Value = "Reading_date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "billdate", Value = "billdate" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "duedate", Value = "duedate" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "utility_id", Value = "utility_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Utility_Name", Value = "Utility_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Region_Name", Value = "Region_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "country", Value = "country" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretaker_Name", Value = "Caretaker_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretaker_ID", Value = "Caretaker_ID" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Billamount", Value = "Billamount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Paidamount", Value = "Paidamount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "OutstandingAmt", Value = "OutstandingAmt" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Aging_Days", Value = "Aging_Days" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "user", Value = "user" });
            }
            else if (dropDown_Group.SelectedItem.Value == "Region")
            {
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "--Select--", Value = "--Select--", Selected = true });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "PropertyFlag", Value = "Property_Flag" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyid", Value = "Property_id" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyname", Value = "Property_Name" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitid", Value = "Unit_id" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitname", Value = "Unitname" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "RegionName", Value = "Region_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Country", Value = "Country" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakerid", Value = "Caretaker_id" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakername", Value = "Caretaker_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "No.of Properties", Value = "Noof_properties" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Billamount", Value = "Billamount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Paidamount", Value = "Paidamount" });
            }
            else if (dropDown_Group.SelectedItem.Value == "Caretaker")
            {
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "--Select--", Value = "--Select--", Selected = true });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyname", Value = "Property_Name" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitid", Value = "Unit_id" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitname", Value = "Unitname" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "RegionName", Value = "Region_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Country", Value = "Country" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakerid", Value = "Caretaker_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakername", Value = "Caretaker_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "No.of Properties", Value = "Noof_properties" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "totalrentalamount", Value = "totalrentalamount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Totalpaidamt", Value = "Totalpaidamt" });
            }
            txt_filterBy.Text = "";
            txt_filterBy.ReadOnly = true;
        }

        //protected void agingDaysChange(object sender, EventArgs e)
        //{
        //    if (dropDown_AgingDays.SelectedItem.Value != "others")
        //    {
        //        txt_fromAging.Visible = false;
        //        txt_toAging.Visible = false;
        //    }
        //    else
        //    {
        //        txt_fromAging.Visible = true;
        //        txt_toAging.Visible = true;
        //    }
        //}

        //protected void valueRangeChange(object sender, EventArgs e)
        //{
        //    if (dropDown_ValueRange.SelectedItem.Value != "others")
        //    {
        //        txt_fromValue.Visible = false;
        //        txt_toValue.Visible = false;
        //    }
        //    else
        //    {
        //        txt_fromValue.Visible = true;
        //        txt_toValue.Visible = true;
        //    }
        //}
        protected void btn_showReport_Click(object sender, EventArgs e)
        {
            try
            {
                DBContext.LeamaEntities entities = new DBContext.LeamaEntities();
                if (dropDown_Group.SelectedItem.Value != "--Select--")
                {
                    object[] parameters = {
                         new MySqlParameter("@Pgroup", dropDown_Group.SelectedItem.Value),
                         new MySqlParameter("@Pfromdate", string.IsNullOrWhiteSpace(txt_fromDate.Text)?null:txt_fromDate.Text),
                         new MySqlParameter("@Ptodate",string.IsNullOrWhiteSpace(txt_toDate.Text)?null:txt_toDate.Text),
                         new MySqlParameter("@Pfilter_field", dropDown_FilterBy.SelectedItem.Value=="--Select--"?null:dropDown_FilterBy.SelectedItem.Value),
                         new MySqlParameter("@Pfilter_value", txt_filterBy.Text),
                         new MySqlParameter("@PCreateduser", string.IsNullOrWhiteSpace(txt_CreatedUser.Text)?null:txt_CreatedUser.Text)
                    };
                    var vacantReportData = entities.Database.SqlQuery<object>("CALL Usp_Collectionsummary_Report_all(@Pgroup, @Pfromdate, @Ptodate, @Pfilter_field, @Pfilter_value, @PCreateduser)", parameters).ToList();
                }

                CollectionSummaryReportViewer.Reset();
                CollectionSummaryReport.Visible = true;
                ReportDataSource reportDataSource = new ReportDataSource();
                if (dropDown_Group.SelectedItem.Value == "Property")
                {
                    CollectionSummaryReportViewer.LocalReport.ReportPath = "ReportRDLC\\CollectionSummary\\summarycollectionprop.rdlc";
                    var property = entities.Database.SqlQuery<CollectionSummaryReportModel>("Select id,property_id,Property_ID_Tawtheeq,Properties_Name,Unit_ID_Tawtheeq,Unit_Property_Name,Caretaker_id,Caretaker_Name,Ag_Tenant_id,Ag_Tenant_Name,country,region_name,Reccategory,RecpType,pdcstatus,DDChequeNo,DDChequeDate,Total_Rental_amount,totalamount,user from summarycollection_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = property
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Region")
                {
                    CollectionSummaryReportViewer.LocalReport.ReportPath = "ReportRDLC\\CollectionSummary\\summarycollectionregion.rdlc";
                    var region = entities.Database.SqlQuery<CollectionSummaryRegionReportModel>("Select Region_Name,Country,Noof_properties,Billamount,Paidamount,user from summaryebwater_region_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = region
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Caretaker")
                {
                    CollectionSummaryReportViewer.LocalReport.ReportPath = "ReportRDLC\\CollectionSummary\\summarycollectioncaretaker.rdlc";
                    var caretaker = entities.Database.SqlQuery<CollectionSummaryCaretakerReportModel>("Select Caretaker_id,Caretaker_Name,Region_Name,Country,Noof_properties,totalrentalamount,Totalpaidamt,user from summarycollection_caretaker_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = caretaker
                    };
                }
                //ReportDataSource reportDataSource = new ReportDataSource
                //{
                //    // Must match the DataSource in the RDLC
                //    Name = "DataSet1",
                //    Value = region
                //};
                CollectionSummaryReportViewer.LocalReport.DataSources.Add(reportDataSource);
                CollectionSummaryReportViewer.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}