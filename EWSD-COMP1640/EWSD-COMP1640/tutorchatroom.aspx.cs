using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data;

namespace EWSD_COMP1640
{

    public partial class tutorchatroom : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

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
                showMessage();
            }

        }

        public void showMessage()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT COUNT(mesg) AS Total_Message,Student_Id FROM [chat] WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Label1.Text + "') GROUP BY Student_Id", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT COUNT(a.mesg) AS Total_Message,CONCAT(c.student_Fname ,' ', c.Student_Lname) AS Student_Name,b.Student_Id FROM [Group] b LEFT JOIN [chat] a ON a.Student_Id=b.Student_Id LEFT JOIN [Student] c ON b.Student_Id=c.Student_Id WHERE b.Tutor_Id=(SELECT Tutor_Id FROM Tutor WHERE Tutor_Name='" + Label1.Text+ "') GROUP BY b.Student_Id,c.Student_Fname,c.Student_Lname", con);
            sqlDa.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {
                GridView2.DataSource = dtbl;
                GridView2.DataBind();
            }


        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[rowIndex];
                string Student_Id = (row.FindControl("lbl_id") as Label).Text;

                con.Open();
                string up = "UPDATE [chat] SET reply='Recived' WHERE Student_Id='" + Student_Id.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(up, con);
                cmd1.ExecuteNonQuery();
                con.Close();

                Session["StudentID"] = Student_Id.ToString();
                Response.Redirect("private.aspx");



            }

        }
    }
}