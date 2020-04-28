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
    public partial class Studentchatroom : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["StudentID"] != null)
            {
                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id = '" + Session["StudentID"].ToString() + "'", conn);
                DataTable dt = new DataTable(); sda.Fill(dt);
                Label1.Text = dt.Rows[0]["Student_FName"].ToString() + dt.Rows[0]["Student_LName"].ToString();
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
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT COUNT(b.mesg) AS Total_Message,a.Tutor_Id  FROM [Group] a LEFT JOIN [chat] b ON a.Student_Id=b.Student_Id WHERE a.Student_Id='"+Session["StudentID"].ToString()+"' GROUP BY a.Tutor_Id", con);
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

            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[rowIndex];
                string Tutor_Id = (row.FindControl("lbl_id") as Label).Text;

                con.Open();
                string up = "UPDATE [chat] SET reply='Recived' WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] Where Tutor_Id = '" + Tutor_Id.ToString() + "')";
                SqlCommand cmd1 = new SqlCommand(up, con);
                cmd1.ExecuteNonQuery();
                con.Close();

                Session["TutorID"] = Tutor_Id.ToString();
                Session["StudentName"] = Label1.Text;
                Response.Redirect("privatechatstudent.aspx");

            }
        }

    }
}