using LeaMaPortal.Models;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeaMaPortal.ReportASPX.Utility
{
    public partial class EBWaterReportView : System.Web.UI.Page
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Aging_days", Value = "Aging_days" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Aging_Range", Value = "Aging_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Amount_Range", Value = "Amount_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "OutstandingAmt", Value = "OutstandingAmt" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "utility_id", Value = "utility_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Utility_Name", Value = "Utility_Name" });
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Aging_days", Value = "Aging_days" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Aging_Range", Value = "Aging_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Amont_Range", Value = "Amont_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "OutstandingAmt", Value = "OutstandingAmt" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "utility_id", Value = "utility_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Utility_Name", Value = "Utility_Name" });
            }
            txt_filterBy.Text = "";
            txt_filterBy.ReadOnly = true;
        }

        protected void agingDaysChange(object sender, EventArgs e)
        {
            if (dropDown_AgingDays.SelectedItem.Value != "others")
            {
                txt_fromAging.ReadOnly = true;
                txt_toAging.ReadOnly = true;
            }
            else
            {

                txt_fromAging.ReadOnly = false;
                txt_toAging.ReadOnly = false;
            }
        }

        protected void valueRangeChange(object sender, EventArgs e)
        {
            if (dropDown_ValueRange.SelectedItem.Value != "others")
            {
                txt_fromValue.ReadOnly = true;
                txt_toValue.ReadOnly = true;
            }
            else
            {
                txt_fromValue.ReadOnly = false;
                txt_toValue.ReadOnly = false;
            }
        }
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
                         new MySqlParameter("@Pagin_Filter", dropDown_AgingDays.SelectedItem.Value=="--Select--"?null:dropDown_AgingDays.SelectedItem.Value),
                         new MySqlParameter("@Pagin_Filter_From", string.IsNullOrWhiteSpace(txt_fromAging.Text)?0:Convert.ToInt32(txt_fromAging.Text)),
                         new MySqlParameter("@Pagin_Filter_To", string.IsNullOrWhiteSpace(txt_toAging.Text)?0:Convert.ToInt32(txt_toAging.Text)),
                         new MySqlParameter("@Prentalamt_Filter", dropDown_ValueRange.SelectedItem.Value=="--Select--"?null:dropDown_ValueRange.SelectedItem.Value),
                         new MySqlParameter("@Prentalamt_Filter_From",string.IsNullOrWhiteSpace(txt_fromValue.Text)?0:Convert.ToInt32(txt_fromValue.Text)),
                         new MySqlParameter("@Prentalamt_Filter_To", string.IsNullOrWhiteSpace(txt_toValue.Text)?0:Convert.ToInt32(txt_toValue.Text)),
                         new MySqlParameter("@PCreateduser", string.IsNullOrWhiteSpace(txt_CreatedUser.Text)?null:txt_CreatedUser.Text)
                    };
                    var vacantReportData = entities.Database.SqlQuery<object>("CALL Usp_Ebwater_Report_all(@Pgroup, @Pfromdate, @Ptodate, @Pfilter_field, @Pfilter_value, @Pagin_Filter, @Pagin_Filter_From, @Pagin_Filter_To, @Prentalamt_Filter, @Prentalamt_Filter_From, @Prentalamt_Filter_To, @PCreateduser)", parameters).ToList();
                }

                EBWaterReportViewer.Reset();
                EBWaterReport.Visible = true;
                ReportDataSource reportDataSource = new ReportDataSource();
                if (dropDown_Group.SelectedItem.Value == "Property")
                {
                    EBWaterReportViewer.LocalReport.ReportPath = "ReportRDLC\\Utility\\ebwaterprop.rdlc";
                    var property = entities.Database.SqlQuery<EBWaterPropertyReportModel>("Select id,Refno,Meterno,property_id,Property_Name,Unit_id,Unit_Property_Name,Total_units,Meterreadingno,Reading_date,billdate,billno,duedate,utility_id,Utility_Name,Region_Name,country,Caretaker_Name,Caretaker_ID,Billamount,Paidamount,OutstandingAmt,Aging_Days,user from ebwater_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = property
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Region")
                {
                    EBWaterReportViewer.LocalReport.ReportPath = "ReportRDLC\\Utility\\ebwaterregion.rdlc";
                    var region = entities.Database.SqlQuery<EBWaterRegionReportModel>("Select Region_Name,Country,Noof_properties,Aging_days,Aging_Range,Amount_Range,OutstandingAmt,user,utility_id,Utility_Name from ebwater_region_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = region
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Caretaker")
                {
                    EBWaterReportViewer.LocalReport.ReportPath = "ReportRDLC\\Utility\\ebwatercaretaker.rdlc";
                    var caretaker = entities.Database.SqlQuery<EBWaterCaretakerReportModel>("Select Caretaker_id,Caretaker_Name,Region_Name,Country,Noof_properties,Aging_days,Aging_Range,Amont_Range,OutstandingAmt,user,utility_id,Utility_Name from ebwater_caretaker_report").ToList();
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
                EBWaterReportViewer.LocalReport.DataSources.Add(reportDataSource);
                EBWaterReportViewer.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}