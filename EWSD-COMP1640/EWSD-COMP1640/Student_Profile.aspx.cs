using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EWSD_COMP1640
{
    public partial class Student_Profile : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            if (Session["StudentID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id='" + Session["StudentID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label1.Text = dt.Rows[0]["Student_Fname"].ToString() + " " + dt.Rows[0]["Student_Lname"].ToString();
            }

            if (!IsPostBack)
            {

                ShowData();

            }

        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (e.CommandName == "edit")
            {
                DataList1.EditItemIndex = e.Item.ItemIndex;
                ShowData();
            }
            else if (e.CommandName == "save")
            {
                SqlConnection conn = new SqlConnection(maincon);

                bool exists = false;
                bool exists2 = false;
                string Email = ((TextBox)e.Item.FindControl("txt_email")).Text;
                Label message = (Label)DataList1.Items[e.Item.ItemIndex].FindControl("Label12");

                con.Open();
                string check = "SELECT count (*) FROM [Student] WHERE Email_Address = '" + Email + "'";
                SqlCommand cmd = new SqlCommand(check, con);
                exists = (int)cmd.ExecuteScalar() > 0;
                con.Close();

                con.Open();
                string check2 = "SELECT count(*) FROM [Student] WHERE Email_Address = '" + Email + "' AND Student_Id='" + Session["StudentID"].ToString() + "'";
                SqlCommand cmd4 = new SqlCommand(check2, con);
                exists2 = (int)cmd4.ExecuteScalar() > 0;
                con.Close();

                if (exists2)
                {

                    SqlCommand cmd3 = new SqlCommand("UPDATE [Student] SET Email_Address = '" + Email + "' WHERE Student_Id='" + Session["StudentID"].ToString() + "'", con);
                    con.Open();
                    cmd3.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("Student_Profile.aspx");

                }
                else if (exists)
                {

                    message.Visible = true;

                }
                else
                {

                    SqlCommand cmd2 = new SqlCommand("UPDATE [Student] SET Email_Address = '" + Email + "' WHERE Student_Id='" + Session["StudentID"].ToString() + "'", con);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("Student_Profile.aspx");

                }

            }
            else if (e.CommandName == "cancel")
            {
                DataList1.EditItemIndex = -1;
                ShowData();
            }

        }

        private void ShowData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT CONCAT(student_Fname ,' ', Student_Lname) AS Student_Name,Course,FORMAT(DOB,'dd/MM/yyyy') as [DOB],Email_Address,Student_Id FROM [Student] WHERE Student_Id='" + Session["StudentID"].ToString() + "'", maincon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataList1.DataSource = ds;
            DataList1.DataBind();
        }

    }
}