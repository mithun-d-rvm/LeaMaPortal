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
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["leamaConnectionString"].ToString());
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
        private DataTable GetData()
        {
            ds = new DataSet();
            dt = new DataTable();
            cmd = new MySqlCommand("select * from vacancy_caretaker_report", con);
            // cmd.CommandType = CommandType.Text;
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("pregion", null);
            // cmd.Parameters.AddWithValue("pcaretaker_name", null);

            con.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            cmd.Dispose();

            return dt;
        }

        protected void btn_showReport_Click(object sender, EventArgs e)
        {
            VacancyReportViewer.Reset();
            vacancyReport.Visible = true;
            DBContext.LeamaEntities entities = new DBContext.LeamaEntities();
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
            else
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