using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace LeaMaPortal.ReportASPX.VacancyReport
{
    public partial class VacancyRegionReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var reportDataSource = new ReportDataSource
            {
                // Must match the DataSource in the RDLC
                Name = "Dataset1",
                Value = Session["ReportData"]
            };
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
            ReportViewer1.DataBind();
            //if (!IsPostBack)
            //{
            //    var reportDataSource = new ReportDataSource
            //    {
            //        // Must match the DataSource in the RDLC
            //        Name = "Dataset1",
            //        Value = Session["ReportData"]
            //    };
            //    ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
            //    ReportViewer1.DataBind();
            //}
        }
    }
}