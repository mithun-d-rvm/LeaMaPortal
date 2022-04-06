using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace LeaMaPortal
{
    public partial class GetRegions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["leamaConnectionString"].ToString());
                    MySqlCommand cmd = new MySqlCommand("select * from tbl_region where ifnull(delmark,'')<>'*'", con);
                    con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    con.Close();
                    da.Fill(dt);
                    dropdownregion.DataSource = dt;
                    dropdownregion.DataTextField = "Region_Name";
                    dropdownregion.DataValueField = "Region_Name";
                    dropdownregion.DataBind();
                    dropdownregion.Items.Insert(0, new ListItem("--Select Region--", "-1"));
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            
        }

        protected void dropdownregion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(dropdownregion.SelectedValue != "-1")
                {
                    Session["Region"] = dropdownregion.SelectedItem.Text;
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["leamaConnectionString"].ToString());
                    MySqlCommand cmd = new MySqlCommand("select * from tbl_region where ifnull(delmark,'')<>'*' and Region_Name='" + dropdownregion.SelectedItem.Text + "'", con);
                    con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    con.Close();
                    da.Fill(dt);
                    
                    Session["Country"] = dt.Rows[0]["Country"];
                    //string tst = dt.Rows[0]["Country"].ToString();
                    //string url = HttpContext.Current.Request.Url.AbsoluteUri;
                    //string path = HttpContext.Current.Request.Url.AbsolutePath;
                    //string host = HttpContext.Current.Request.Url.Host;
                    //string finpath = host + "/LeaMaPortal/Dashboard/Index";
                    //Response.Redirect(finpath);
                }
                
                            
            }
            catch (Exception ex)
            {

            }
        }
    }
}