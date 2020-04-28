using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace EWSD_COMP1640
{
    public partial class Student_Work : System.Web.UI.Page
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

                ShowWork();

            }
           
        }


        public void ShowWork()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT COUNT(mesg) AS Total_Message,Student_Id FROM [chat] WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Label1.Text + "') GROUP BY Student_Id", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT a.id,a.Work_Code,a.Work_Deadline FROM [Upload_Work] a LEFT JOIN [Tutor] b ON a.Tutor_Id=b.Tutor_Id LEFT JOIN [Group] c ON c.Tutor_Id=a.Tutor_Id WHERE c.Student_Id=(SELECT Student_Id FROM [Student] WHERE CONCAT(student_Fname ,' ', Student_Lname)='"+Label1.Text+"') ", con);
            sqlDa.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {
                GridView3.DataSource = dtbl;
                GridView3.DataBind();
            }


        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (e.CommandName == "upload")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[rowIndex];
                string Work_Code = (row.FindControl("lbl_code") as Label).Text;

                Session["WorkCode"] = Work_Code.ToString();
                Response.Redirect("Student_Upload.aspx");

            }

        }
    }
}