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
    public partial class Dashboard_Viewer : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["StaffID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Staff] WHERE Staff_Id='" + Session["StaffID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label6.Text = dt.Rows[0]["Staff_Name"].ToString();
            }

            if (!IsPostBack)
            {

                ShowStudent();
                ShowTutor();

            }

        }



        void ShowStudent()
        {
            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT CONCAT(student_Fname ,' ', Student_Lname) AS Student_Name,Email_Address,Course,Student_Id FROM [Student]", con);
            sqlDa.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                GridView1.DataSource = dtbl;
                GridView1.DataBind();

            }

        }

        void ShowTutor()
        {
            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT Tutor_Id,Tutor_Name,Email_address,Course FROM [Tutor]", con);
            sqlDa.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {
                GridView2.DataSource = dtbl;
                GridView2.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                GridView2.DataSource = dtbl;
                GridView2.DataBind();
                GridView2.Rows[0].Cells.Add(new TableCell());
                GridView2.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                GridView2.Rows[0].Cells[0].Text = "No Data Yet";
                GridView2.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                GridView2.Width = 1030;

            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (e.CommandName == "view")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                string StudentId = (row.FindControl("lbl_Id") as Label).Text;
                string StudentName = (row.FindControl("lbl_name") as Label).Text;

                Session["StudentId"] = StudentId.ToString();
                Response.Redirect("View_Student_Dashboard.aspx");

            }

        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            SqlConnection con = new SqlConnection(maincon);

            if (e.CommandName == "view2")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[rowIndex];
                string TutorName = (row.FindControl("lbl_Tutorname") as Label).Text;

                Session["TutorName"] = TutorName.ToString();
                Response.Redirect("View_Tutor_Dashboard.aspx");

            }

        }
    }
}