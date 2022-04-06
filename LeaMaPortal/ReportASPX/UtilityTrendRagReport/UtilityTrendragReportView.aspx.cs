using LeaMaPortal.Models;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace LeaMaPortal.ReportASPX.UtilityTrendRagReport
{
    public partial class UtilityTrendragReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                utilityconnect();
                Countryconnect();
                Propertyconnect();

                txt_CreatedUser.Text = System.Web.HttpContext.Current.User.Identity.Name;
            }
        }
        protected void filterbyChange(object sender, EventArgs e)
        {

        }
        public void shownhide()
        {
            if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report")
            {
                
            }
            else if (dropDown_Group.SelectedItem.Text == "Yearly Trend Report")
            {
                

            }
            else if (dropDown_Group.SelectedItem.Text == "Property wise Summary Report" || dropDown_Group.SelectedItem.Text == "Utility Payments RAG property Report")
            {
                

            }
            else if (dropDown_Group.SelectedItem.Text == "RAG Report")
            {
               

            }
            else if (dropDown_Group.SelectedItem.Text == "Utility Payments RAG property Report")
            {
         
            }




        }
        public void utilityconnect()
        {
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("select distinct Utility_id, Utility_Name from tbl_utilitiesmaster", cn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            ada.Fill(dt);
            dropdown_utility.DataSource = dt;
            dropdown_utility.DataTextField = "Utility_Name";
            dropdown_utility.DataValueField = "Utility_id";
            dropdown_utility.DataBind();
            dropdown_utility.Items.Insert(0, new ListItem("--Select Utility--", "-1"));
           // dropdown_utility.Items.Insert(1, new ListItem("--All--", "1"));
            cn.Close();
        }
        public void Countryconnect()
        {
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("select distinct Country from tbl_region", cn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            ada.Fill(dt);
            dropdowncountry.DataSource = dt;
            dropdowncountry.DataTextField = "Country";
            dropdowncountry.DataValueField = "Country";
            dropdowncountry.DataBind();
            if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report" || dropDown_Group.SelectedItem.Text == "RAG Report")
            {
                dropdowncountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
                dropdowncountry.Items.Insert(1, new ListItem("All", "1"));
            }
          else
            {
                dropdowncountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
            }
            cn.Close();
        }
        public void Regionconnect()
        {
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("select distinct Id,Region_Name,Country from tbl_region where country = '" + dropdowncountry.SelectedItem.Text + "'", cn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            ada.Fill(dt);
            dropdownregion.DataSource = dt;
            dropdownregion.DataTextField = "Region_Name";
            dropdownregion.DataValueField = "Id";
            dropdownregion.DataBind();
            if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report" || dropDown_Group.SelectedItem.Text == "RAG Report")
            {
                dropdownregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
                dropdownregion.Items.Insert(1, new ListItem("All", "1"));
            }
            else
            {
                dropdownregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
            }
            cn.Close();
        }
        public void Propertyconnect()
        {
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("select distinct Property_Id,Property_Name,Property_ID_Tawtheeq from tbl_propertiesmaster where property_flag = 'property'", cn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            ada.Fill(dt);
            dropdownproperty.DataSource = dt;
            dropdownproperty.DataTextField = "Property_Name";
            dropdownproperty.DataValueField = "Property_ID_Tawtheeq";
            dropdownproperty.DataBind();
            if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report" || dropDown_Group.SelectedItem.Text == "RAG Report"  || dropDown_Group.SelectedItem.Text == "Property wise Summary Report")
            {
                dropdownproperty.Items.Insert(0, new ListItem("Select Property", "-1"));
                dropdownproperty.Items.Insert(1, new ListItem("All", "0"));
            }
            else
            {
                dropdownproperty.Items.Insert(0, new ListItem("--Select Property--", "0"));
            }
              
            cn.Close();
        }
        protected void btn_showReport_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {

            }
        }

        protected void dropdowncountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdowncountry.SelectedItem.Text != "")
            {
                Regionconnect();
            }
            else
            {

            }
        }
        public void exportrdlc(string flag, string utilityname, string country, string region, string property, string fromdt, string todt, string cuser)
        {
            DataTable dt = new DataTable();
            //string leemaconn = ConfigurationManager.ConnectionStrings["Leamareport"].ConnectionString;

            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("Usp_Ebwater_Trend_Report", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Pflag", flag);
            cmd.Parameters.AddWithValue("Pfromdate", fromdt);
            cmd.Parameters.AddWithValue("Ptodate", todt);
            cmd.Parameters.AddWithValue("Putility", utilityname);
            cmd.Parameters.AddWithValue("Pcountry", country);
            cmd.Parameters.AddWithValue("Pregion", region);
            cmd.Parameters.AddWithValue("Pproperty", property);
            cmd.Parameters.AddWithValue("PCreateduser", cuser);
            // MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();

            //  ada.Fill(dt);
            // return dt;
            cn.Close();
        }

        private DataTable GetData()
        {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = new DataSet();
                dt = new DataTable();
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            //  string leemaconn = ConfigurationManager.ConnectionStrings["Leamareport"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
                cn.Open();

                MySqlCommand cmd;
                cmd = new MySqlCommand("Select * from summaryebwater_treand where user='" + txt_CreatedUser.Text + "'", cn);

                cmd.CommandType = CommandType.Text;
                // cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("pregion", null);
                // cmd.Parameters.AddWithValue("pcaretaker_name", null);


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cn.Close();
                cmd.Dispose();

                return dt;
            
        }

        private DataTable GetDataYear()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = new DataSet();
            dt = new DataTable();
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            // string leemaconn = ConfigurationManager.ConnectionStrings["Leamareport"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();

            MySqlCommand cmd;
            cmd = new MySqlCommand("Select * from Summaryebwater_Treand_Year where user='" + txt_CreatedUser.Text + "'", cn);

            cmd.CommandType = CommandType.Text;
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("pregion", null);
            // cmd.Parameters.AddWithValue("pcaretaker_name", null);


            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();
            cmd.Dispose();

            return dt;
            
        }

        private DataTable GetPropertySummary()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = new DataSet();
            dt = new DataTable();
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            //  string leemaconn = ConfigurationManager.ConnectionStrings["Leamareport"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();

            MySqlCommand cmd;
            cmd = new MySqlCommand("Select * from Summaryebwater_Treand_property where user='" + txt_CreatedUser.Text + "'", cn);

            cmd.CommandType = CommandType.Text;
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("pregion", null);
            // cmd.Parameters.AddWithValue("pcaretaker_name", null);


            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();
            cmd.Dispose();

            return dt;

        }

        private DataTable GetRag()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = new DataSet();
            dt = new DataTable();
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            //  string leemaconn = ConfigurationManager.ConnectionStrings["Leamareport"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();

            MySqlCommand cmd;
            cmd = new MySqlCommand("Select * from Summaryebwater_Colour where user='" + txt_CreatedUser.Text + "'", cn);

            cmd.CommandType = CommandType.Text;
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("pregion", null);
            // cmd.Parameters.AddWithValue("pcaretaker_name", null);


            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();
            cmd.Dispose();

            return dt;

        }

        private DataTable GetutilitypaymentRag()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = new DataSet();
            dt = new DataTable();
            string leemaconn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
            //string leemaconn = ConfigurationManager.ConnectionStrings["leamatest"].ConnectionString;
            //  string leemaconn = ConfigurationManager.ConnectionStrings["Leamareport"].ConnectionString;
            MySqlConnection cn = new MySqlConnection(leemaconn);
            cn.Open();

            MySqlCommand cmd;
            cmd = new MySqlCommand("Select * from Summaryebwater_Colour_Prop where user='" + txt_CreatedUser.Text + "'", cn);

            cmd.CommandType = CommandType.Text;
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("pregion", null);
            // cmd.Parameters.AddWithValue("pcaretaker_name", null);


            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();
            cmd.Dispose();

            return dt;

        }

        protected void btn_showReport_Click1(object sender, EventArgs e)
        {

            try
            {
                //var vacantReportData = ;

                UtilityTrendRagReportViewer.Reset();
                DataTable dt = new DataTable();
                dt = new DataTable();
                string propertyval = "";
                string group = "",utility="",country = "", region = "";

                if (dropdownproperty.SelectedItem.Text == "Select Property")
                {
                    //dropdownproperty.SelectedItem.Text = null;
                    propertyval = null;
                }
                else
                {
                    propertyval= string.IsNullOrEmpty( dropdownproperty.SelectedItem.Text)? null : dropdownproperty.SelectedItem.Text;
                }
                if (dropdown_utility.SelectedItem.Text == "Select Utility")
                {
                    utility = null;
                }
                else
                {
                    utility = string.IsNullOrEmpty(dropdown_utility.SelectedItem.Text) ? null : dropdown_utility.SelectedItem.Text;
                }
                    exportrdlc(dropDown_Group.SelectedItem.Text, utility, dropdowncountry.SelectedItem.Text,
                    dropdownregion.SelectedItem.Text, dropdownproperty.SelectedItem.Value, string.IsNullOrWhiteSpace(txt_fromDate.Text) ? null : txt_fromDate.Text, string.IsNullOrWhiteSpace(txt_toDate.Text) ? null : txt_toDate.Text ,
                    txt_CreatedUser.Text);
                if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report")
                {
                    dt = GetData();
                    UtilityTrendRagReportViewer.Reset();
                    UtilityTrenddiv.Visible = true;

                    UtilityTrendRagReportViewer.LocalReport.ReportPath = "ReportRDLC\\UtilityTrendReport\\EbwatertrendAll.rdlc";

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    UtilityTrendRagReportViewer.LocalReport.DataSources.Add(rds);
                    UtilityTrendRagReportViewer.DataBind();
              
                    UtilityTrendRagReportViewer.LocalReport.Refresh();
                }
                else if (dropDown_Group.SelectedItem.Text == "Yearly Trend Report")
                {
                    dt = GetDataYear();
                    UtilityTrendRagReportViewer.Reset();
                    UtilityTrenddiv.Visible = true;

                    UtilityTrendRagReportViewer.LocalReport.ReportPath = "ReportRDLC\\UtilityTrendReport\\Ebwatertrendyearnew.rdlc";

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    UtilityTrendRagReportViewer.LocalReport.DataSources.Add(rds);
                    UtilityTrendRagReportViewer.DataBind();
        
                    UtilityTrendRagReportViewer.LocalReport.Refresh();

                }
                else if (dropDown_Group.SelectedItem.Text == "Property wise Summary Report")
                {
                    dt = GetPropertySummary();
                    UtilityTrendRagReportViewer.Reset();
                    UtilityTrenddiv.Visible = true;

                    UtilityTrendRagReportViewer.LocalReport.ReportPath = "ReportRDLC\\UtilityTrendReport\\EBWATERtrendproperty.rdlc";

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    UtilityTrendRagReportViewer.LocalReport.DataSources.Add(rds);
                    UtilityTrendRagReportViewer.DataBind();
                    UtilityTrendRagReportViewer.LocalReport.Refresh();

                }
                else if (dropDown_Group.SelectedItem.Text == "RAG Report")
                {
                    dt = GetRag();
                    UtilityTrendRagReportViewer.Reset();
                    UtilityTrenddiv.Visible = true;

                    UtilityTrendRagReportViewer.LocalReport.ReportPath = "ReportRDLC\\UtilityTrendReport\\EBWATERcolour.rdlc";

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    UtilityTrendRagReportViewer.LocalReport.DataSources.Add(rds);
                    UtilityTrendRagReportViewer.DataBind();
                    UtilityTrendRagReportViewer.LocalReport.Refresh();

                }
                else if (dropDown_Group.SelectedItem.Text == "Utility Payments RAG property Report")
                {
                    dt = GetutilitypaymentRag();
                    UtilityTrendRagReportViewer.Reset();
                    UtilityTrenddiv.Visible = true;

                    UtilityTrendRagReportViewer.LocalReport.ReportPath = "ReportRDLC\\UtilityTrendReport\\EBWATERcolourprop.rdlc";

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    UtilityTrendRagReportViewer.LocalReport.DataSources.Add(rds);
                    UtilityTrendRagReportViewer.DataBind();
                    UtilityTrendRagReportViewer.LocalReport.Refresh();

                }
               


            }
            catch (Exception ex)
            {

            }
          
           
        }

        protected void dropDown_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            utilityconnect();
            Countryconnect();
            Propertyconnect();
        }
    }
}