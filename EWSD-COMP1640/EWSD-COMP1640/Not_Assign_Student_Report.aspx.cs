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
    public partial class Not_Assign_Student_Report : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            SqlConnection conn = new SqlConnection(maincon);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id='" + Request.QueryString["STUDENT_NAME"].ToString() + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            lblstudent.Text = dt.Rows[0]["Student_Fname"].ToString() + " " + dt.Rows[0]["Student_Lname"].ToString();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("UPDATE [Student] set get_report='0' WHERE Student_Id='" + Request.QueryString["STUDENT_NAME"].ToString() + "'", con);
            cmd2.ExecuteNonQuery();
            con.Close();

        }

        protected void btn_back_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            bool existsstudent = false;

            con.Open();
            string check = "select count (*) from [Student] WHERE Student_Id = '" + Request.QueryString["STUDENT_NAME"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(check, con);
            existsstudent = (int)cmd.ExecuteScalar() > 0;

            if (existsstudent)
            {

                Response.Redirect("Student_Home.aspx");
            }

        }
    }
}