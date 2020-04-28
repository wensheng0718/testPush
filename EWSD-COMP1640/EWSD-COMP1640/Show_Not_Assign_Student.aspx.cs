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
using System.Net.Mail;

namespace EWSD_COMP1640
{
    public partial class Show_Not_Assign_Student : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (Session["StaffID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Staff] WHERE Staff_Id='" + Session["StaffID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label1.Text = dt.Rows[0]["Staff_Name"].ToString();
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
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT CONCAT(student_Fname ,' ', Student_Lname) AS Student_Name,Email_Address,Course,Student_Id ,first_dateline,final_dateline FROM [Student] WHERE status ='Have Not Assign' AND get_report='0' ", con);
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

        protected void btn_send_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                SqlConnection con = new SqlConnection(maincon);
                CheckBox status = (CheckBox)row.FindControl("chc_send");
                Label Student_Id = (Label)row.FindControl("lbl_Id");
                Label Email_Address = (Label)row.FindControl("lbl_email");
                Label Student_Name = (Label)row.FindControl("lbl_name");

                if (status.Checked == true)
                {

                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("UPDATE [Student] set get_report='1' WHERE Student_Id='" + Student_Id.Text + "'", con);
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                    smtp.EnableSsl = true;
                    MailMessage mm = new MailMessage();
                    mm.Subject = "Report";
                    string body = "\n\n '" + Request.Url.AbsoluteUri.Replace("Show_Not_Assign_Student.aspx", "Not_Assign_Student_Report.aspx?STUDENT_NAME=" + Student_Id.Text) + "'";
                    body += "\n\n We Have Send A Report to :" + Student_Id.Text + "Please click the link";
                    body += "\n\n Please click the link";
                    mm.Body = body;
                    string toaddres = Email_Address.Text;
                    mm.To.Add(toaddres);
                    string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                    mm.From = new MailAddress(fromaddress);
                    smtp.Send(mm);

                }

            }
            //Session["GroupID"] = Label1.Text;
            //Response.Redirect("Tutor.aspx");
        }
    }
}

