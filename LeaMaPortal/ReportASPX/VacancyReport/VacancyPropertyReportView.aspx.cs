using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace LeaMaPortal.Views.Report.VacancyReport
{
    public partial class VacancyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using(LeaMaPortal.DBContext.LeamaEntities db =new LeaMaPortal.DBContext.LeamaEntities())
                {
                    ReportViewer1.Visible = true;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = "ReportRDLC/Vacancy/vacancycaretaker.rdlc";
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", db.vacancy_caretaker_report.ToList());
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                //var reportDataSource = new ReportDataSource
                //{
                //    // Must match the DataSource in the RDLC
                //    Name = "Dataset1",
                //    Value = Session["ReportData"]
                //};
                //ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
                //ReportViewer1.DataBind();
            }
        }
    }
}