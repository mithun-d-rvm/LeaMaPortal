using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using LeaMaPortal.Models;

namespace LeaMaPortal.ReportASPX.VacancyReport
{
    public partial class VacancyCaretakerReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_CreatedUser.Text = System.Web.HttpContext.Current.User.Identity.Name;
                //VacancyRegionReport.Reset();
                //VacancyRegion.Visible = true;
                //LeaMaPortal.DBContext.LeamaEntities entities = new DBContext.LeamaEntities();
                //VacancyRegionReport.LocalReport.ReportPath = "ReportRDLC\\Vacancy\\vacancyregion.rdlc";
                //var region = entities.Database.SqlQuery<VacantRegionReportModel>("Select Region_Name,Country,Noof_properties,Aging_days,Aging_Range,Amount_Range,Loss_Amt,user from vacancy_region_report").ToList();
                //var reportDataSource = new ReportDataSource
                //{
                //    // Must match the DataSource in the RDLC
                //    Name = "DataSet1",
                //    Value = region
                //};
                //VacancyRegionReport.LocalReport.DataSources.Add(reportDataSource);
                //VacancyRegionReport.DataBind();
                //VacancyRegionReport.LocalReport.Refresh();
                //ReportViewer1.Visible = true;

                //ReportViewer1.Reset();
                //ReportViewer1.LocalReport.DataSources.Clear();
                //dt = new DataTable();
                //dt = GetData();
                //ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ////ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ////ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportRDLC/Vacancy/vacancycaretaker.rdlc");
                //ReportViewer1.LocalReport.ReportPath = "ReportRDLC/Vacancy/vacancycaretaker.rdlc";
                //ReportViewer1.LocalReport.DataSources.Clear();
                //ReportViewer1.LocalReport.DataSources.Add(rds);
                //ReportViewer1.DataBind();
                ////ReportViewer1.LocalReport.ReportPath = "Report5.rdlc";
                //ReportViewer1.LocalReport.Refresh();
            }
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "PropertyFlag", Value = "Property_Flag" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyid", Value = "Property_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Propertyname", Value = "Property_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitid", Value = "Unit_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Unitname", Value = "Unitname" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "RegionName", Value = "Region_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Country", Value = "Country" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakerid", Value = "Caretaker_id" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Caretakername", Value = "Caretaker_Name" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "RentalRate", Value = "Rental_Rate_Month" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "VacantStartDate", Value = "Vacant_Start_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "AgingDays", Value = "Aging_Days" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "LossAmount", Value = "Loss_Amt" });
            }
            else if(dropDown_Group.SelectedItem.Value == "Region")
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "AgingRange", Value = "Aging_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "AmountRange", Value = "Amount_range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "LossAmount", Value = "Loss_Amt" });
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "AgingDays", Value = "Aging_days" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "AgingRange", Value = "Aging_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "AmountRange", Value = "Amount_Range" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "LossAmount", Value = "Loss_Amt" });
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
                    var vacantReportData = entities.Database.SqlQuery<object>("CALL Usp_Vacant_Report_all(@Pgroup, @Pfromdate, @Ptodate, @Pfilter_field, @Pfilter_value, @Pagin_Filter, @Pagin_Filter_From, @Pagin_Filter_To, @Prentalamt_Filter, @Prentalamt_Filter_From, @Prentalamt_Filter_To, @PCreateduser)", parameters).ToList();
                }

                VacancyReportViewer.Reset();
                vacancyReport.Visible = true;
                ReportDataSource reportDataSource = new ReportDataSource();
                if (dropDown_Group.SelectedItem.Value == "Property")
                {
                    VacancyReportViewer.LocalReport.ReportPath = "ReportRDLC\\Vacancy\\vacancyprop.rdlc";
                    var property = entities.Database.SqlQuery<VacantPropertyReportModel>("Select id,Property_Flag,Property_id,Property_Name,Unit_id,Unitname,Region_Name,Country,Caretaker_id,Caretaker_Name,Rental_Rate_Month,Vacant_Start_Date,Aging_Days,Loss_Amt,user from vacancy_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = property
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Region")
                {
                    VacancyReportViewer.LocalReport.ReportPath = "ReportRDLC\\Vacancy\\vacancyregion.rdlc";
                    var region = entities.Database.SqlQuery<VacantRegionReportModel>("Select Region_Name,Country,Noof_properties,Aging_days,Aging_Range,Amount_Range,Loss_Amt,user from vacancy_region_report").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = region
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Caretaker")
                {
                    VacancyReportViewer.LocalReport.ReportPath = "ReportRDLC\\Vacancy\\vacancycaretaker.rdlc";
                    var caretaker = entities.Database.SqlQuery<VacantCaretakerReportModel>("Select Caretaker_id,Caretaker_Name,Region_Name,Country,Noof_properties,Aging_days,Aging_Range,Amont_Range,Loss_Amt,user from vacancy_caretaker_report").ToList();
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
                VacancyReportViewer.LocalReport.DataSources.Add(reportDataSource);
                VacancyReportViewer.DataBind();
            }
            catch(Exception ex)
            {

            }
            
            //if (!IsPostBack)
            //{
            //    VacancyReportViewer.Reset();
            //    vacancyReport.Visible = true;
            //    DBContext.LeamaEntities entities = new DBContext.LeamaEntities();
            //    ReportDataSource reportDataSource = new ReportDataSource();
            //    if (dropDown_Group.SelectedItem.Value == "Property")
            //    {
            //        VacancyReportViewer.LocalReport.ReportPath = "ReportRDLC\\Vacancy\\vacancyprop.rdlc";
            //    }
            //    else if (dropDown_Group.SelectedItem.Value == "Region")
            //    {
            //        VacancyReportViewer.LocalReport.ReportPath = "ReportRDLC\\Vacancy\\vacancyregion.rdlc";
            //        var region = entities.Database.SqlQuery<VacantRegionReportModel>("Select Region_Name,Country,Noof_properties,Aging_days,Aging_Range,Amount_Range,Loss_Amt,user from vacancy_region_report").ToList();
            //        reportDataSource = new ReportDataSource
            //        {
            //            // Must match the DataSource in the RDLC
            //            Name = "DataSet1",
            //            Value = region
            //        };
            //    }
            //    else
            //    {
            //        VacancyReportViewer.LocalReport.ReportPath = "ReportRDLC\\Vacancy\\vacancycaretaker.rdlc";
            //        var caretaker = entities.Database.SqlQuery<VacantCaretakerReportModel>("Select Caretaker_id,Caretaker_Name,Region_Name,Country,Noof_properties,Aging_days,Aging_Range,Amont_Range,Loss_Amt,user from vacancy_caretaker_report").ToList();
            //        reportDataSource = new ReportDataSource
            //        {
            //            // Must match the DataSource in the RDLC
            //            Name = "DataSet1",
            //            Value = caretaker
            //        };
            //    }
            //    //ReportDataSource reportDataSource = new ReportDataSource
            //    //{
            //    //    // Must match the DataSource in the RDLC
            //    //    Name = "DataSet1",
            //    //    Value = region
            //    //};
            //    VacancyReportViewer.LocalReport.DataSources.Add(reportDataSource);
            //    VacancyReportViewer.DataBind();
            //}
        }
    }
}