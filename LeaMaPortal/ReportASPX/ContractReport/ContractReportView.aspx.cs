﻿using LeaMaPortal.Models;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeaMaPortal.ReportASPX.ContractReport
{
    public partial class ContractReportView : System.Web.UI.Page
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_No", Value = "Agreement_No" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyid", Value = "Property_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyname", Value = "Property_name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitid", Value = "Unit_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitname", Value = "Unitname" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "RegionName", Value = "Region_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Country", Value = "Country" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakerid", Value = "Caretaker_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakername", Value = "Caretaker_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Ag_Tenant_id", Value = "Ag_Tenant_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Ag_Tenant_Name", Value = "Ag_Tenant_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_End_Date", Value = "Agreement_End_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Notice_Period", Value = "Notice_Period" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Perday_Rental", Value = "Perday_Rental" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Remaining_Days", Value = "Remaining_Days" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Contract_Value", Value = "Contract_Value" });
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Remaining_Days", Value = "Remaining_Days" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Amount_Range", Value = "Amount_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Remaining_Range", Value = "Remaining_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Contract_Value", Value = "Contract_Value" });
            }
            else if (dropDown_Group.SelectedItem.Value == "Caretaker")
            {
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "--Select--", Value = "--Select--", Selected = true });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "PropertyFlag", Value = "Property_Flag" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyid", Value = "Property_id" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyname", Value = "Property_Name" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitid", Value = "Unit_id" });
                //dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitname", Value = "Unitname" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "RegionName", Value = "Region_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Country", Value = "Country" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakerid", Value = "Caretaker_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakername", Value = "Caretaker_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "No.of Properties", Value = "Noof_properties" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Remaining_Days", Value = "Remaining_Days" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Amount_Range", Value = "Amount_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Remaining_Range", Value = "Remaining_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Contract_Value", Value = "Contract_Value" });
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
                    var vacantReportData = entities.Database.SqlQuery<object>("CALL Usp_REnewal_Report_all(@Pgroup, @Pfromdate, @Ptodate, @Pfilter_field, @Pfilter_value, @Pagin_Filter, @Pagin_Filter_From, @Pagin_Filter_To, @Prentalamt_Filter, @Prentalamt_Filter_From, @Prentalamt_Filter_To, @PCreateduser)", parameters).ToList();
                }

                ContractReportViewer.Reset();
                ContractReport.Visible = true;
                ReportDataSource reportDataSource = new ReportDataSource();
                if (dropDown_Group.SelectedItem.Value == "Property")
                {
                    ContractReportViewer.LocalReport.ReportPath = "ReportRDLC\\Contract\\Renewalprop.rdlc";
                    var property = entities.Database.SqlQuery<ContractPropertyReportModel>("Select id,Agreement_No,Property_id,Property_name,Unit_id,Unitname,Region_Name,Country,Caretaker_id,Caretaker_Name,Ag_Tenant_id,Ag_Tenant_Name,Agreement_End_Date,Notice_Period,Perday_Rental,Remaining_Days,Contract_Value,user from renewal_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = property
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Region")
                {
                    ContractReportViewer.LocalReport.ReportPath = "ReportRDLC\\Contract\\renewal_region_report.rdlc";
                    var region = entities.Database.SqlQuery<ContractRegionReportModel>("Select Region_Name,Country,Noof_properties,Remaining_Days,Remaining_Range,Amount_Range,Contract_Value,user from renewal_region_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = region
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Caretaker")
                {
                    ContractReportViewer.LocalReport.ReportPath = "ReportRDLC\\Contract\\renewalcaretaker.rdlc";
                    var caretaker = entities.Database.SqlQuery<ContractCaretakerReportModel>("Select Caretaker_id,Caretaker_Name,Region_Name,Country,Noof_properties,Remaining_Days,Remaining_Range,Amont_Range,Contract_Value,user from renewal_caretaker_report").ToList();
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
                ContractReportViewer.LocalReport.DataSources.Add(reportDataSource);
                ContractReportViewer.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}