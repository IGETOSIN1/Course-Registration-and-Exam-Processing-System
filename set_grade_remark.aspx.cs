using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Web_Application_Higher_Institution
{
    public partial class set_grade_remark : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            try
            {
                cn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from table_grading_system", cn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn.Close(); cn.Dispose();
            }
            ///////////////////////////////////////////////////////////////////////////////////
            MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            try
            {
                cn1.Open();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    MySqlCommand cmd = new MySqlCommand("update table_result set grade='" + GridView1.Rows[i].Cells[3].Text + "',gp='" + GridView1.Rows[i].Cells[4].Text + "',remark='" + GridView1.Rows[i].Cells[6].Text + "' where score>='" + GridView1.Rows[i].Cells[1].Text + "' and score<='" + GridView1.Rows[i].Cells[2].Text + "';", cn1);
                    cmd.ExecuteNonQuery();
                    Response.Write("Success ...");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn1.Close();
            }
        }
    }
}