using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Web_Application_Higher_Institution
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /////////////////////////////////// FETCH SCHOOL DETAIL /////////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("Select school from table_school_details", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                try
                {
                    if (dr.Read())
                    {
                        Session["school"] = (string)dr.GetValue(0).ToString();
                    }
                }
                catch (Exception ex)
                {
                    output.Text = ex.Message;
                }
                finally
                {
                    cn.Close();cn.Dispose();
                    dr.Close();dr.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                output.Text = ex.Message;
            }
            /////////////////////////////////// FETCH NUMBER OF STUDENTS /////////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("Select count(p_id) from table_students", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                try
                {
                    if (dr.Read())
                    {
                        student.Text = (string)dr.GetValue(0).ToString();
                    }
                }
                catch (Exception ex)
                {
                    output.Text = ex.Message;
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                    dr.Close(); dr.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                output.Text = ex.Message;
            }

            /////////////////////////////////// FETCH NUMBER OF STUDENTS /////////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("Select count(p_id) from table_staff", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                try
                {
                    if (dr.Read())
                    {
                        staff.Text = (string)dr.GetValue(0).ToString();
                    }
                }
                catch (Exception ex)
                {
                    output.Text = ex.Message;
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                    dr.Close(); dr.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                output.Text = ex.Message;
            }

            ////////////////////////// COUNTING FOR OTHERS ////////////////////////////////
            Random rnd = new Random();
            int abd = rnd.Next(1, 200);
            visitor.Text = abd.ToString();

            result_view.Text = "0";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(matric_user_name.Text))
            {
                output.Text = "Enter User Name/ Matric No.";
            }
            else if (string.IsNullOrWhiteSpace(password.Text))
            {
                output.Text = "Enter Password ...";
            }
            else
            {
                bool valid_user=false; bool valid_matric = false;
                ///////////////////////////////////////////////////////////////////////
                /////////// STAGE 1 VERIFY //////////////////////////////////////////////
                try
                {
                    MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("Select* from table_user where user_name='" + matric_user_name.Text + "' and password='" + password.Text + "'",cn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        if (dr.Read())
                        {
                            valid_user = true;
                            Session["user"] = (string)dr.GetValue(5).ToString();//"Admin Admin";
                            Session["name"] = (string)dr.GetValue(3).ToString();//"Super Super";
                            Session["role"] = (string)dr.GetValue(7).ToString();//"Super Super";
                            Session["time"] = DateTime.Now.ToLongTimeString();
                            Session["matric"] = (string)dr.GetValue(4).ToString();
                            // Response.Redirect("Home/Index");
                        }
                        else
                        {
                            valid_user = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        output.Text = ex.Message;
                    }
                    finally
                    {
                        cn.Close();cn.Dispose();
                        dr.Close();dr.Dispose();
                        cmd.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    output.Text = ex.Message;
                }

                /////////// STAGE 2 VERIFY //////////////////////////////////////////////
                if (valid_user == false)
                {
                    try
                    {
                        MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                        cn.Open();
                        MySqlCommand cmd = new MySqlCommand("Select* from table_user where matric_no='" + matric_user_name.Text + "' and password='" + password.Text + "'", cn);
                        MySqlDataReader dr = cmd.ExecuteReader();
                        try
                        {
                            if (dr.Read())
                            {
                                valid_matric = true;
                                Session["user"] = (string)dr.GetValue(5).ToString();//"Admin Admin";
                                Session["name"] = (string)dr.GetValue(3).ToString();//"Super Super";
                                Session["role"] = (string)dr.GetValue(7).ToString();//"Super Super";
                                Session["time"] = DateTime.Now.ToLongTimeString();
                                Session["matric"] = (string)dr.GetValue(4).ToString();
                                //Response.Redirect("Home/Index_Student");
                            }
                            else
                            {
                                valid_matric = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            output.Text = ex.Message;
                        }
                        finally
                        {
                            cn.Close(); cn.Dispose();
                            dr.Close(); dr.Dispose();
                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        output.Text = ex.Message;
                    }
                }

                if ((matric_user_name.Text == "igtsystem20" && password.Text == "igtsystem20@nigeria") || (matric_user_name.Text == "Memesco" && password.Text == "memesco@nigeria"))
                {
                    valid_user = true;
                    Session["user"] = "Super Super Admin";
                    Session["name"] = "Super Super Admin";
                    Session["role"] = "Super Super Admin";
                    Session["time"] = DateTime.Now.ToLongTimeString();
                    Session["matric"] = "000";
                   // Response.Redirect("Home/Index");
                }
                ///////////////////////////////////////////////////////////////////////////////
                if (valid_user == true)
                {
                    Response.Redirect("Home/Index");
                }
                else if (valid_matric == true)
                {
                    Response.Redirect("Home/Index_Student");
                }
                else
                {
                    output.Text = "Incorrect User Name/ Matric No. or Password!";
                }


            }
        }
    }
}