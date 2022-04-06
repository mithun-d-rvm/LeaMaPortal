using LeaMaPortal.Models;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using System.Configuration;
using System.Data;

namespace LeaMaPortal.ReportASPX.PDC
{
    public partial class PDCReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Countryconnect();
                txt_CreatedUser.Text = System.Web.HttpContext.Current.User.Identity.Name;
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "--Select--", Value = "--Select--", Selected = true });

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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_Start_Date", Value = "Agreement_Start_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_End_Date", Value = "Agreement_End_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Total_Rental_amount", Value = "Total_Rental_amount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "outstanding_amt", Value = "outstanding_amt" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "cheque_amount", Value = "cheque_amount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Cheque_No", Value = "Cheque_No" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "cheque_date", Value = "cheque_date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "pdcstatus", Value = "pdcstatus" });
            }
        }

        //public void Regionconnect()
        //{
        //    string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
        //    //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
        //    MySqlConnection cn = new MySqlConnection(leemaconn);
        //    cn.Open();
        //    DataTable dt = new DataTable();
        //    MySqlCommand cmd = new MySqlCommand("select distinct Id,Region_Name,Country from tbl_region where country = '" + dropdowncountry.SelectedItem.Text + "'", cn);
        //    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
        //    ada.Fill(dt);
        //    dropdownregion.DataSource = dt;
        //    dropdownregion.DataTextField = "Region_Name";
        //    dropdownregion.DataValueField = "Id";
        //    dropdownregion.DataBind();
        //    //if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report" || dropDown_Group.SelectedItem.Text == "RAG Report")
        //    //{
        //    //    dropdownregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
        //    //    dropdownregion.Items.Insert(1, new ListItem("All", "1"));
        //    //}
        //    //else
        //    //{
        //    //    dropdownregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
        //    //}
        //    dropdownregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
        //    cn.Close();
        //}
        //public void Countryconnect()
        //{
        //    string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
        //    //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
        //    MySqlConnection cn = new MySqlConnection(leemaconn);
        //    cn.Open();
        //    DataTable dt = new DataTable();
        //    MySqlCommand cmd = new MySqlCommand("select distinct Country from tbl_region", cn);
        //    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
        //    ada.Fill(dt);
        //    dropdowncountry.DataSource = dt;
        //    dropdowncountry.DataTextField = "Country";
        //    dropdowncountry.DataValueField = "Country";
        //    dropdowncountry.DataBind();
        //    //if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report" || dropDown_Group.SelectedItem.Text == "RAG Report")
        //    //{
        //    //    dropdowncountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
        //    //    dropdowncountry.Items.Insert(1, new ListItem("All", "1"));
        //    //}
        //    //else
        //    //{
        //    //    dropdowncountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
        //    //}
        //    dropdowncountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
        //    cn.Close();
        //}
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
        protected void groupDropdownChange(object sender, EventArgs e)
        {
            dropDown_FilterBy.Items.Clear();
            if (dropDown_Group.SelectedItem.Value == "Property")
            {
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "--Select--", Value = "--Select--", Selected = true });
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_Start_Date", Value = "Agreement_Start_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_End_Date", Value = "Agreement_End_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Total_Rental_amount", Value = "Total_Rental_amount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "outstanding_amt", Value = "outstanding_amt" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "cheque_amount", Value = "cheque_amount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Cheque_No", Value = "Cheque_No" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "cheque_date", Value = "cheque_date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "pdcstatus", Value = "pdcstatus" });
            }
            else if (dropDown_Group.SelectedItem.Value == "Region")
            {
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "--Select--", Value = "--Select--", Selected = true });
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_Start_Date", Value = "Agreement_Start_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_End_Date", Value = "Agreement_End_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Total_Rental_amount", Value = "Total_Rental_amount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "outstanding_amt", Value = "outstanding_amt" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "cheque_amount", Value = "cheque_amount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Cheque_No", Value = "Cheque_No" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "cheque_date", Value = "cheque_date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "pdcstatus", Value = "pdcstatus" });
            }
            else if (dropDown_Group.SelectedItem.Value == "Caretaker")
            {
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "--Select--", Value = "--Select--", Selected = true });
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
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_Start_Date", Value = "Agreement_Start_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Agreement_End_Date", Value = "Agreement_End_Date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Total_Rental_amount", Value = "Total_Rental_amount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "outstanding_amt", Value = "outstanding_amt" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "cheque_amount", Value = "cheque_amount" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "Cheque_No", Value = "Cheque_No" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "cheque_date", Value = "cheque_date" });
                dropDown_FilterBy.Items.Add(new ListItem() { Text = "pdcstatus", Value = "pdcstatus" });
            }
            txt_filterBy.Text = "";
            txt_filterBy.ReadOnly = true;
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
                object[] parameters = {
                         new MySqlParameter("@Pfromdate", string.IsNullOrWhiteSpace(txt_fromDate.Text)?null:txt_fromDate.Text),
                         new MySqlParameter("@Ptodate",string.IsNullOrWhiteSpace(txt_toDate.Text)?null:txt_toDate.Text),
                         new MySqlParameter("@Pfilter_field", dropDown_FilterBy.SelectedItem.Value=="--Select--"?null:dropDown_FilterBy.SelectedItem.Value),
                         new MySqlParameter("@Pfilter_value", txt_filterBy.Text),
                         new MySqlParameter("@Prentalamt_Filter", dropDown_ValueRange.SelectedItem.Value=="--Select--"?null:dropDown_ValueRange.SelectedItem.Value),
                         new MySqlParameter("@Prentalamt_Filter_From",string.IsNullOrWhiteSpace(txt_fromValue.Text)?0:Convert.ToInt32(txt_fromValue.Text)),
                         new MySqlParameter("@Prentalamt_Filter_To", string.IsNullOrWhiteSpace(txt_toValue.Text)?0:Convert.ToInt32(txt_toValue.Text)),
                         new MySqlParameter("@PCreateduser", string.IsNullOrWhiteSpace(txt_CreatedUser.Text)?null:txt_CreatedUser.Text)
                    };
                var vacantReportData = entities.Database.SqlQuery<object>("CALL Usp_pdc_Report_all(@pfromdate, @ptodate, @Pfilter_field, @Pfilter_value, @Prentalamt_Filter, @Prentalamt_Filter_From, @Prentalamt_Filter_To, @PCreateduser)", parameters).ToList();

                PDCReportViewer.Reset();
                PDCReport.Visible = true;
                ReportDataSource reportDataSource = new ReportDataSource();
                PDCReportViewer.LocalReport.ReportPath = "ReportRDLC\\PDC\\PDCreport.rdlc";
                var property = entities.Database.SqlQuery<PDCReportModel>("Select id,Agreement_No,Property_id,Property_name,Unit_id,Unitname,Region_Name,Country,Caretaker_id,Caretaker_Name,Ag_Tenant_id,Ag_Tenant_Name,Agreement_Start_Date,Agreement_End_Date,Total_Rental_amount,outstanding_amt,cheque_amount,Cheque_No,cheque_date,pdcstatus,user from pdc_report where user='" + txt_CreatedUser.Text + "'").ToList();
                //reportDataSource = new ReportDataSource
                //{
                //    // Must match the DataSource in the RDLC
                //    Name = "DataSet1",
                //    Value = property
                //};
                if (dropDown_Group.SelectedItem.Value == "Property")
                {
                    PDCReportViewer.LocalReport.ReportPath = "ReportRDLC\\PDC\\PDCreport.rdlc";
                    var propertyreport = entities.Database.SqlQuery<PDCReportModel>("Select id,Agreement_No,Property_id,Property_name,Unit_id,Unitname,Region_Name,Country,Caretaker_id,Caretaker_Name,Ag_Tenant_id,Ag_Tenant_Name,Agreement_Start_Date,Agreement_End_Date,Total_Rental_amount,outstanding_amt,cheque_amount,Cheque_No,cheque_date,pdcstatus,user from pdc_report where user='" + txt_CreatedUser.Text + "'").ToList();

                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = propertyreport
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Region")
                {
                    PDCReportViewer.LocalReport.ReportPath = "ReportRDLC\\PDC\\PDCreport - Regionwise.rdlc";
                    var region = entities.Database.SqlQuery<PDCReportModel>("Select id,Agreement_No,Property_id,Property_name,Unit_id,Unitname,Region_Name,Country,Caretaker_id,Caretaker_Name,Ag_Tenant_id,Ag_Tenant_Name,Agreement_Start_Date,Agreement_End_Date,Total_Rental_amount,outstanding_amt,cheque_amount,Cheque_No,cheque_date,pdcstatus,user from pdc_report where user='" + txt_CreatedUser.Text + "'").ToList();
                    reportDataSource = new ReportDataSource
                    {
                        // Must match the DataSource in the RDLC
                        Name = "DataSet1",
                        Value = region
                    };
                }
                else if (dropDown_Group.SelectedItem.Value == "Caretaker")
                {
                    PDCReportViewer.LocalReport.ReportPath = "ReportRDLC\\PDC\\PDCreport - Caretakerwise.rdlc";
                    var caretaker = entities.Database.SqlQuery<PDCReportModel>("Select id,Agreement_No,Property_id,Property_name,Unit_id,Unitname,Region_Name,Country,Caretaker_id,Caretaker_Name,Ag_Tenant_id,Ag_Tenant_Name,Agreement_Start_Date,Agreement_End_Date,Total_Rental_amount,outstanding_amt,cheque_amount,Cheque_No,cheque_date,pdcstatus,user from pdc_report where user='" + txt_CreatedUser.Text + "'").ToList();
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
                PDCReportViewer.LocalReport.DataSources.Add(reportDataSource);
                PDCReportViewer.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void dropdowncountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (dropdowncountry.SelectedItem.Text != "")
            //{
            //    Regionconnect();
            //}
            //else
            //{

            //}
        }

        protected void dropDown_Group_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}