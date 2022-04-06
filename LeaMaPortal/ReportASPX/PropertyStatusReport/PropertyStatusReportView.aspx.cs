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

namespace LeaMaPortal.ReportASPX.PropertyStatusReport
{
    public partial class PropertyStatusReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegionLoad();
                ddlProperty.Items.Insert(0, "All");
                txt_CreatedUser.Text = System.Web.HttpContext.Current.User.Identity.Name;
                
            }
        }
        public void RegionLoad()
        {
            try
            {
                ddlRegion.Items.Clear();
                DataTable dt = new DataTable();
                string leemacn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(leemacn);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select Region_Name from tbl_Region", cn);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                ada.Fill(dt);

                ddlRegion.DataSource = cmd.ExecuteReader();
                ddlRegion.DataTextField = "Region_Name";
                ddlRegion.DataValueField = "Region_Name";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, "All");
                cn.Close();
            }
            catch (Exception ex)
            {

            }

        }

        public void PropertyLoad()
        {
            try
            {
                ddlProperty.Items.Clear();

                DataTable dt = new DataTable();
                string leemacn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(leemacn);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select distinct Property_Name from tbl_propertiesmaster where Region_name='" + ddlRegion.SelectedItem.Value + "' and Property_Name is not null", cn);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                ada.Fill(dt);
                ddlProperty.DataSource = cmd.ExecuteReader();
                ddlProperty.DataTextField = "Property_Name";
                ddlProperty.DataValueField = "Property_Name";
                ddlProperty.DataBind();
                ddlProperty.Items.Insert(0, "All");
                cn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        protected void filterbyChange(object sender, EventArgs e)
        {
          
        }

        protected void groupDropdownChange(object sender, EventArgs e)
        {
          
          
        }

        protected void agingDaysChange(object sender, EventArgs e)
        {
          
        }

        protected void valueRangeChange(object sender, EventArgs e)
        {
          
        }

        protected void btn_showReport_Click(object sender, EventArgs e)
        {
            try
            {
                DBContext.LeamaEntities entities = new DBContext.LeamaEntities();

                object[] parameters = {
                                 new MySqlParameter ("@PRegion_name", ddlRegion.SelectedItem.Value ),
                                 new MySqlParameter("@PProperty_name", ddlProperty.SelectedItem.Value  ),
                                 new MySqlParameter("@PCreateduser",string.IsNullOrWhiteSpace (txt_CreatedUser.Text )?null:txt_CreatedUser.Text),
                                 
                            };
                
                var PropertyStatus = entities.Database.SqlQuery<object>("call USP_Properties_view(@PRegion_name, @PProperty_name, @PCreateduser)", parameters).ToList();


                PropertyStatusReportViewer.Reset();
                PropertyStatusReport.Visible = true;
                ReportDataSource reportDataSource = new ReportDataSource();
                PropertyStatusReportViewer.LocalReport.ReportPath = "ReportRDLC\\PropertyStatus\\PropertyStatusNew.rdlc";
                //var property = entities.Database.SqlQuery<PDCReportModel>("Select id,Agreement_No,Property_id,Property_name,Unit_id,Unitname,Region_Name,Country,Caretaker_id,Caretaker_Name,Ag_Tenant_id,Ag_Tenant_Name,Agreement_Start_Date,Agreement_End_Date,Total_Rental_amount,outstanding_amt,cheque_amount,Cheque_No,cheque_date,pdcstatus,user from pdc_report where user='" + txt_CreatedUser.Text + "'").ToList();
                var property = entities.Database.SqlQuery<PropertyStatusReportModel>("Select * from result_view_properties_status_new where user='" + txt_CreatedUser.Text + "'").ToList();
                reportDataSource = new ReportDataSource
                {
                    // Must match the DataSource in the RDLC
                    Name = "DataSet1",
                    Value = property
                };

                PropertyStatusReportViewer.LocalReport.DataSources.Add(reportDataSource);
                PropertyStatusReportViewer.DataBind();
            }
            catch (Exception ex)
            {

            }

        }

        protected void dropDown_FilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dropDown_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLoad();
        }
    }
}