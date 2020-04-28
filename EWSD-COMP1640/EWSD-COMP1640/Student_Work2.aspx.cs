using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace EWSD_COMP1640
{
    public partial class Student_Work2 : System.Web.UI.Page
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

                ShowWork();

            }

        }

         public void ShowWork()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT COUNT(mesg) AS Total_Message,Student_Id FROM [chat] WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Label1.Text + "') GROUP BY Student_Id", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT a.id,a.Work_Code,a.Work_Deadline FROM [Upload_Work] a LEFT JOIN [Tutor] b ON a.Tutor_Id=b.Tutor_Id WHERE b.Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Label1.Text + "') ", con);
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

            if (e.CommandName == "view")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[rowIndex];
                string Work_Code = (row.FindControl("lbl_code") as Label).Text;

                Session["WorkCode"] = Work_Code.ToString();
                Response.Redirect("View_Student_Work.aspx");

            }

        }
    }
}