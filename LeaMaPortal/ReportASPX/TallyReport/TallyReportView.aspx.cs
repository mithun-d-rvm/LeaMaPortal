using LeaMaPortal.Models;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML;
using ClosedXML.Excel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using System.Configuration;


namespace LeaMaPortal.ReportASPX.TallyReport
{
    public partial class TallyReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //Countryconnect();
                //txt_CreatedUser.Text = System.Web.HttpContext.Current.User.Identity.Name;
            }
        }

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
        //    if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report" || dropDown_Group.SelectedItem.Text == "RAG Report")
        //    {
        //        dropdowncountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
        //        dropdowncountry.Items.Insert(1, new ListItem("All", "1"));
        //    }
        //    else
        //    {
        //        dropdowncountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
        //    }
        //    cn.Close();
        //}
        protected void groupDropdownChange(object sender, EventArgs e)
        {
            
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
                //MySqlConnection cn = new MySqlConnection("server = 192.168.1.40; user id = msqladmin; password = Admin@123; persistsecurityinfo = True; database = leama_new; ");
                string leemacn = ConfigurationManager.ConnectionStrings["leamaConnectionString"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(leemacn);

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                cn.Open();
                if (dropDown_Group .Text == "Customer")
                { 
                string query = "";
                query = "select 'Ledgername','Sub Group 3','Sub Group 2','Sub  Group 1','Primary Group','Agreement_No','Property_ID','Property_Name','Unit_ID','unit_Name','Tenant_Name' union all select concat(ifnull(x.Ag_Tenant_id, ''), ' ') as LedgerName, ";
                query = query + " concat('Property id:', ifnull(x.Property_ID_Tawtheeq, ''), ' Property Name:', ifnull(x.Properties_Name, ''), ' Unit Id:', ifnull(x.Unit_ID_Tawtheeq, '')";
                query = query + "  , ' Unit Nmae:', ifnull(x.Unit_Property_Name, '')) as SubGroup3,y.SubGroup2,y.SubGroup1, y.PrimaryGroup,ifnull(x.Agreement_no, '') as Agreement_no ,ifnull(x.Property_ID_Tawtheeq, '') as Property_ID,ifnull(x.Properties_Name, '') as Property_Name ,ifnull(x.Unit_ID_Tawtheeq, '') as Unit_ID";
                query = query + ",ifnull(x.Unit_Property_Name, '') as Unit_Name,ifnull(x.Ag_Tenant_Name, '') as Tenant_Name from tbl_agreement x inner join view_tenant y on y.tenant_id = x.Ag_Tenant_id where ifnull(x.Delmark, '') <> '*' and ifnull(x.Approval_Flag, 0)= 1 and ifnull(x.status, '')= '' ";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    dt.TableName = "TallydataCustomer";
                    ada.Fill(dt);
                    //dt.Clear();
                    //  exportexcel(dt);
                    ExportGridToExcel(dt);
                }
                else if (dropDown_Group .Text == "Invoice")
                {

                    string qryinvoice = "";
                    qryinvoice = "select   concat(invno,'/',month,'-',year) as Invno,Date,concat(ifnull(Tenant_id,''),'') as LedgerName";
                    qryinvoice = qryinvoice + " ,Month,Year,concat('Property_Name:', ifnull(property_name, ''), 'Property_id:', ifnull(property_id, '')";
                    qryinvoice = qryinvoice + " , 'Unit_ID:', ifnull(unit_id, ''), 'unit_Name:', ifnull(unit_name, ''), ' Renatl for the month of ', month, ' ', year) as Description,totalamt as Amount,totalamt as Totalamount  ";
                    qryinvoice = qryinvoice + "   from tbl_invoicehd  where date between '"+txt_fromDate .Text +"' and '"+ txt_toDate .Text  +"' order by id";
                    MySqlCommand cmd1 = new MySqlCommand(qryinvoice, cn);
                    MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1 );
                    dt1.TableName = "TallydataInvoice";
                    ada1.Fill(dt1);
                    //exportexcel(dt1);
                    ExportGridToExcel(dt1);
                }
                else if (dropDown_Group.Text == "Receipt")
                {
                    string qryReceipt = "";
                    qryReceipt = "SELECT x.Reccategory,x.RecpType,x.Receiptno,x.ReceiptDate,concat(ifnull(x.Tenant_id,''),'')as LedgerName, ";
                    qryReceipt = qryReceipt + " ifnull(concat(ifnull(y.invno, ''), '/', month(y.InvoiceDate), '-', year(y.InvoiceDate)), '') as 'Invno ref' ,ifnull(x.AdvAcCode, '') as 'Adv Rct Ref',x.TotalAmount as CreditAmt,ifnull(x.DDChequeNo, '') as DDChequeNo,";
                    qryReceipt = qryReceipt + " ifnull(x.DDChequeDate, '') as DDChequeDate,ifnull(x.BankAcCode, '') as BankAcCode,ifnull(x.BankAcName, '') as BankAcName FROM tbl_receipthd x  left join tbl_receiptdt y on x.ReceiptNo = y.receiptno where x.ReceiptDate between '" + txt_fromDate.Text  +"' and '"+ txt_toDate.Text  +"'; ";
                    MySqlCommand cmd2 = new MySqlCommand(qryReceipt , cn);
                    MySqlDataAdapter ada2 = new MySqlDataAdapter(cmd2);
                    dt2.TableName = "TallydataReceipt";
                    ada2.Fill(dt2);
                    //exportexcel(dt2);
                    ExportGridToExcel(dt2);
                }
                else if (dropDown_Group.Text == "Payment")
                {
                    string qryPayment = "";
                    qryPayment = "select x.PaymentNo,x.PaymentDate,x.Utiltiy_name as Billtype,x.Supplier_name,x.PaymentType,x.PaymentMode,ifnull(y.Billno, '') as Billno,ifnull(y.Billdate, '') as Billdate,ifnull(x.advaccode, '') as Advpayno";
                    qryPayment = qryPayment + " ,ifnull(y.paidamount + ifnull(y.debitamt, 0), 0) as Totalamount ,ifnull(x.DDChequeNo, '') as DDChequeNo,ifnull(x.Cheqdate, '') as Chequedate,ifnull(x.BankAcCode, '') as BankAcCode,ifnull(x.BankAcName, '') as BankAcName,ifnull(x.Narration, '') as Narration";
                    qryPayment = qryPayment + "  from tbl_eb_water_paymenthd x left  join tbl_eb_water_paymentdt y on x.PaymentNo = y.PaymentNo union all select x.PaymentNo,x.PaymentDate,'others' as Billtype,x.Supplier_name ,x.PaymentType,x.PaymentMode,ifnull(y.InvoiceNo, '') as InvoiceNo ,ifnull(y.InvoiceDate, '') as InvoiceDate,ifnull(x.advaccode, '') as Advpayno ";
                    qryPayment = qryPayment + " ,ifnull(y.paidamount + ifnull(y.debitamt, 0), x.totalamount) as Totalamount ,ifnull(x.DDChequeNo, '') as DDChequeNo, ifnull(x.Cheqdate, '') as Chequedate,ifnull(x.BankAcCode, '') as BankAcCode,ifnull(x.BankAcName, '') as BankAcName,";
                    qryPayment = qryPayment + " ifnull(x.Narration, '') as Narration from tbl_paymenthd x left join tbl_paymentdt y on x.PaymentNo = y.PaymentNo where x.PaymentDate between '"+ txt_fromDate .Text  +"' and '"+ txt_toDate .Text  +"' ;";
                    MySqlCommand cmd3 = new MySqlCommand(qryPayment, cn);
                    MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
                    dt3.TableName = "TallydataPayment";
                    ada3.Fill(dt3);
                    //exportexcel(dt3);
                    ExportGridToExcel(dt3);

                }
                cn.Close();

                
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void ExportGridToExcel(DataTable dt)
        {
            try
            {
                GridView g = new GridView();
                g.DataSource = dt;
                g.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = dropDown_Group.Text + " " + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                g.GridLines = GridLines.Both;
                g.HeaderStyle.Font.Bold = true;
                g.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();
                

            }
            catch (Exception ex)
            {
                //throw ex;
            }

        }
        private void exporttocsv(DataTable dt)
        {
            try
            {
                
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                DataTable datatable = new DataTable();
                datatable = dt;
                char seperator = ',';
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    sb.Append(datatable.Columns[i]);
                    if (i < datatable.Columns.Count - 1)
                        sb.Append(seperator);
                }
                sb.AppendLine();
                foreach (DataRow dr in datatable.Rows)
                {
                    for (int i = 0; i < datatable.Columns.Count; i++)
                    {
                        sb.Append(dr[i].ToString());

                        if (i < datatable.Columns.Count - 1)
                            sb.Append(seperator);
                    }
                    sb.AppendLine();
                }
                //string FileName = dropDown_Group.Text + " " + DateTime.Now + ".csv";
                //StringWriter strwritter = new StringWriter();

                // HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                //  Response.Cache.SetCacheability(HttpCacheability.NoCache);
                // Response.ContentType = "application/vnd.ms-excel";
                string FileName = dropDown_Group.Text + " " + DateTime.Now + ".csv";
                Response.ContentType = "text/csv";
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                //Response.AddHeader("Content-Disposition", "text;filename=" + FileName);
               
                //gvHiringStages.GridLines = GridLines.Both;
                //gvHiringStages.HeaderStyle.Font.Bold = true;
                //gvHiringStages.RenderControl(htmltextwrtter);

                Response.Write(sb.ToString());
                Response.End();

            }
            catch (Exception ex)
            {
                //throw ex;
            }

        }

        

        private void exporttoworkbook(DataTable dt)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= TallyReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        

        protected void btn_loadReport_Click(object sender, EventArgs e)
        {
            try
            {
                // string constr = ConfigurationManager.ConnectionStrings["LeamaEntities"].ConnectionString;
                MySqlConnection cn = new MySqlConnection("server = 192.168.1.40; user id = msqladmin; password = Admin@123;  database = leama_new; ");
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                cn.Open();
                if (dropDown_Group.Text == "Customer")
                {
                    string query = "";
                    query = "select 'Ledgername','Sub Group 3','Sub Group 2','Sub  Group 1','Primary Group','Agreement_No','Property_ID','Property_Name','Unit_ID','unit_Name','Tenant_Name' union all select concat(ifnull(x.Property_ID_Tawtheeq, ''), ' ', ifnull(x.Unit_ID_Tawtheeq, '')) as LedgerName, ";
                    query = query + " concat('Property id:', ifnull(x.Property_ID_Tawtheeq, ''), ' Property Name:', ifnull(x.Properties_Name, ''), ' Unit Id:', ifnull(x.Unit_ID_Tawtheeq, '')";
                    query = query + "  , ' Unit Nmae:', ifnull(x.Unit_Property_Name, '')) as SubGroup3,y.SubGroup2,y.SubGroup1, y.PrimaryGroup,ifnull(x.Agreement_no, '') as Agreement_no ,ifnull(x.Property_ID_Tawtheeq, '') as Property_ID,ifnull(x.Properties_Name, '') as Property_Name ,ifnull(x.Unit_ID_Tawtheeq, '') as Unit_ID";
                    query = query + ",ifnull(x.Unit_Property_Name, '') as Unit_Name,ifnull(x.Ag_Tenant_Name, '') as Tenant_Name from tbl_agreement x inner join view_tenant y on y.tenant_id = x.Ag_Tenant_id where ifnull(x.Delmark, '') <> '*' and ifnull(x.Approval_Flag, 0)= 1 and ifnull(x.status, '')= '' ";
                    //MySqlCommand cmd = new MySqlCommand("usp_tally_customer",cn);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Type", dropDown_Group.Text);
                    //cmd.Parameters.AddWithValue("@Fromdt", txt_fromDate.Text);
                    //cmd.Parameters.AddWithValue("@Todt", txt_toDate.Text);
                    MySqlCommand cmd = new MySqlCommand(query, cn);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    dt.TableName = "TallydataCustomer";
                    ada.Fill(dt);

                    gvtally.DataSource = dt;
                    gvtally.DataBind();

                }
                else if (dropDown_Group.Text == "Invoice")
                {

                    string qryinvoice = "";
                    qryinvoice = "select   concat(invno,'/',month,'-',year) as Invno,Date,concat(ifnull(property_id,''),ifnull(Unit_id,'')) as LedgerName";
                    qryinvoice = qryinvoice + " ,Month,Year,concat('Property_Name:', ifnull(property_name, ''), 'Property_id:', ifnull(property_id, '')";
                    qryinvoice = qryinvoice + " , 'Unit_ID:', ifnull(unit_id, ''), 'unit_Name:', ifnull(unit_name, ''), ' Renatl for the month of ', month, ' ', year) as Description,totalamt as Amount,totalamt as Totalamount  ";
                    qryinvoice = qryinvoice + "   from leama_new.tbl_invoicehd  where date between '" + txt_fromDate.Text + "' and '" + txt_toDate.Text + "' order by id";
                    MySqlCommand cmd1 = new MySqlCommand(qryinvoice, cn);
                    MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
                    dt1.TableName = "TallydataInvoice";
                    ada1.Fill(dt1);
                    gvtally.DataSource = dt1;
                    gvtally.DataBind();

                }
                else if (dropDown_Group.Text == "Receipt")
                {
                    string qryReceipt = "";
                    qryReceipt = "SELECT x.Reccategory,x.RecpType,x.Receiptno,x.ReceiptDate,concat(ifnull(x.unit_id,''),ifnull(x.property_id,''))as LedgerName, ";
                    qryReceipt = qryReceipt + " ifnull(concat(ifnull(y.invno, ''), '/', month(y.InvoiceDate), '-', year(y.InvoiceDate)), '') as 'Invno ref' ,ifnull(x.AdvAcCode, '') as 'Adv Rct Ref',x.TotalAmount as CreditAmt,ifnull(x.DDChequeNo, '') as DDChequeNo,";
                    qryReceipt = qryReceipt + " ifnull(x.DDChequeDate, '') as DDChequeDate,ifnull(x.BankAcCode, '') as BankAcCode,ifnull(x.BankAcName, '') as BankAcName FROM tbl_receipthd x  left join tbl_receiptdt y on x.ReceiptNo = y.receiptno where x.ReceiptDate between '" + txt_fromDate.Text + "' and '" + txt_toDate.Text + "'; ";
                    MySqlCommand cmd2 = new MySqlCommand(qryReceipt, cn);
                    MySqlDataAdapter ada2 = new MySqlDataAdapter(cmd2);
                    dt2.TableName = "TallydataReceipt";
                    ada2.Fill(dt2);
                    gvtally.DataSource = dt2;
                    gvtally.DataBind();

                }
                else if (dropDown_Group.Text == "Payment")
                {
                    string qryPayment = "";
                    qryPayment = "select x.PaymentNo,x.PaymentDate,x.Utiltiy_name as Billtype,x.Supplier_name,x.PaymentType,x.PaymentMode,ifnull(y.Billno, '') as Billno,ifnull(y.Billdate, '') as Billdate,ifnull(x.advaccode, '') as Advpayno";
                    qryPayment = qryPayment + " ,ifnull(y.paidamount + ifnull(y.debitamt, 0), 0) as Totalamount ,ifnull(x.DDChequeNo, '') as DDChequeNo,ifnull(x.Cheqdate, '') as Chequedate,ifnull(x.BankAcCode, '') as BankAcCode,ifnull(x.BankAcName, '') as BankAcName,ifnull(x.Narration, '') as Narration";
                    qryPayment = qryPayment + "  from tbl_eb_water_paymenthd x left  join tbl_eb_water_paymentdt y on x.PaymentNo = y.PaymentNo union all select x.PaymentNo,x.PaymentDate,'others' as Billtype,x.Supplier_name ,x.PaymentType,x.PaymentMode,ifnull(y.InvoiceNo, '') as InvoiceNo ,ifnull(y.InvoiceDate, '') as InvoiceDate,ifnull(x.advaccode, '') as Advpayno ";
                    qryPayment = qryPayment + " ,ifnull(y.paidamount + ifnull(y.debitamt, 0), x.totalamount) as Totalamount ,ifnull(x.DDChequeNo, '') as DDChequeNo, ifnull(x.Cheqdate, '') as Chequedate,ifnull(x.BankAcCode, '') as BankAcCode,ifnull(x.BankAcName, '') as BankAcName,";
                    qryPayment = qryPayment + " ifnull(x.Narration, '') as Narration from tbl_paymenthd x left join tbl_paymentdt y on x.PaymentNo = y.PaymentNo where x.PaymentDate between '" + txt_fromDate.Text + "' and '" + txt_toDate.Text + "' ;";
                    MySqlCommand cmd3 = new MySqlCommand(qryPayment, cn);
                    MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
                    dt3.TableName = "TallydataPayment";
                    ada3.Fill(dt3);
                    gvtally.DataSource = dt3;
                    gvtally.DataBind();


                }
                cn.Close();


            }
            catch (Exception ex)
            {

            }

        }

        public void exportexcel(DataTable dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
            Response.ContentType = "application/ms-excel";
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();

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
        //    if (dropDown_Group.SelectedItem.Text == "Monthly Trend Report" || dropDown_Group.SelectedItem.Text == "RAG Report")
        //    {
        //        dropdownregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
        //        dropdownregion.Items.Insert(1, new ListItem("All", "1"));
        //    }
        //    else
        //    {
        //        dropdownregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
        //    }
        //    cn.Close();
        //}

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
    }
}