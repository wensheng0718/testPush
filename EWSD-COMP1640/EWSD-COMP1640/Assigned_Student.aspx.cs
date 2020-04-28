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
    public partial class Assigned_Student : System.Web.UI.Page
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

            }
            
        }

        void ShowStudent()
        {
            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT CONCAT(a.student_Fname ,' ', a.Student_Lname) AS Student_Name,a.Email_Address,a.Course,a.Student_Id,b.Group_Id,c.Tutor_Name FROM [Student] a LEFT JOIN [Group] b ON a.Student_Id=b.Student_Id LEFT JOIN [Tutor] c ON b.Tutor_Id=c.Tutor_Id ORDER BY b.Group_Id", con);
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);

            if (e.CommandName == "remove")
            {
                DateTime first = DateTime.Now;
                DateTime after2days = first.AddDays(2);
                after2days.ToString();

                DateTime final = DateTime.Now;
                DateTime after28days = final.AddDays(28);
                after28days.ToString();

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                string StudentId = (row.FindControl("lbl_Id") as Label).Text;
                string GroupId = (row.FindControl("lbl_group") as Label).Text;

                con.Open();
                string query = "DELETE FROM [Group] WHERE Student_Id='" + StudentId.ToString() + "' AND Group_Id='" + GroupId.ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                ShowStudent();

                string assign = "UPDATE [Student] SET status='Have Not Assign',get_report='0',first_dateline='" + after2days.ToString() + "',final_dateline='" + after28days.ToString() + "' WHERE Student_Id ='" + StudentId.ToString() + "'";
                con.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = assign;
                cmd1.Connection = con;
                cmd1.ExecuteNonQuery();
                con.Close();


            }
            else if(e.CommandName == "reassign")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                string StudentId = (row.FindControl("lbl_Id") as Label).Text;
                string GroupId = (row.FindControl("lbl_group") as Label).Text;
                string Email_Address = (row.FindControl("lbl_email") as Label).Text;
                string Student_Name = (row.FindControl("lbl_name") as Label).Text;

                Session["StudentId"] = StudentId.ToString();
                Session["GroupId"] = GroupId.ToString();
                Session["Email_Address"] = Email_Address.ToString();
                Session["Student_Name"] = Student_Name.ToString();
                Response.Redirect("Reallocate_Tutor.aspx");

            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {



        }
    }
}