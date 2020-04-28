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
    public partial class Tutor_Profile : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            if (Session["TutorID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Tutor] WHERE Tutor_Id='" + Session["TutorID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label1.Text = dt.Rows[0]["Tutor_Name"].ToString();
            }

            if (!IsPostBack)
            {

                ShowData();

            }

        }

        private void ShowData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Tutor_Name,Course,Email_Address,Tutor_Id FROM [Tutor] WHERE Tutor_Id='" + Session["TutorID"].ToString() + "'", maincon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataList1.DataSource = ds;
            DataList1.DataBind();
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
                string check = "SELECT count(*) FROM [Tutor] WHERE Email_Address = '" + Email + "'";
                SqlCommand cmd = new SqlCommand(check, con);
                exists = (int)cmd.ExecuteScalar() > 0;
                con.Close();

                con.Open();
                string check2 = "SELECT count(*) FROM [Tutor] WHERE Email_Address = '" + Email + "' AND Tutor_Name='" + Label1.Text + "'";
                SqlCommand cmd4 = new SqlCommand(check2, con);
                exists2 = (int)cmd4.ExecuteScalar() > 0;
                con.Close();

                if (exists2)
                {

                    SqlCommand cmd3 = new SqlCommand("UPDATE [Tutor] SET Email_Address = '" + Email + "' WHERE Tutor_Id='" + Session["TutorID"].ToString() + "'", con);
                    con.Open();
                    cmd3.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("Tutor_Profile.aspx");

                }
                else if (exists)
                {

                    message.Visible = true;

                }
                else
                {

                    SqlCommand cmd2 = new SqlCommand("UPDATE [Tutor] SET Email_Address = '" + Email + "' WHERE Tutor_Id='" + Session["TutorID"].ToString() + "'", con);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("Tutor_Profile.aspx");

                }

            }
            else if (e.CommandName == "cancel")
            {
                DataList1.EditItemIndex = -1;
                ShowData();
            }

        }
    }
}