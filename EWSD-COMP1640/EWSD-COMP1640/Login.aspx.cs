using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace EWSD_COMP1640
{
    public partial class Login : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string encrypwd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);

            encryption();

            bool exists = false;
            
            con.Open();
            string StudentStatus = "select count (*) from [User] WHERE Student_Id = '" + txt_id.Text + "' AND Status='Inactive'";
            SqlCommand cmd = new SqlCommand(StudentStatus, con);
            exists = (int)cmd.ExecuteScalar() > 0;
            con.Close();

            string CheckStatus = "select count (*) from [User] WHERE Tutor_Id = '" + txt_id.Text + "' AND Status='Inactive'";
            SqlCommand cmd3 = new SqlCommand(CheckStatus, con);
            con.Open();
            int TutorStatus = Convert.ToInt32(cmd3.ExecuteScalar().ToString());
            con.Close();

            string checkStudent = "select count(*) from [User] WHERE Student_Id ='" + txt_id.Text + "' AND Password= '" +  encrypwd + "' AND Status='Active' AND Role='student'";
            SqlCommand com = new SqlCommand(checkStudent, con);
            con.Open();
            int existStudent = Convert.ToInt32(com.ExecuteScalar().ToString());
            con.Close();

            string checkTutor = "select count(*) from [User] WHERE Tutor_Id ='" + txt_id.Text + "' AND Password= '" + encrypwd + "' AND Status='Active' AND Role='tutor'";
            SqlCommand com2 = new SqlCommand(checkTutor, con);
            con.Open();
            int existTutor = Convert.ToInt32(com2.ExecuteScalar().ToString());
            con.Close();

            string checkStaff = "select count(*) from [User] WHERE Staff_Id ='" + txt_id.Text + "' AND Password= '" + encrypwd + "' AND Status='Active' AND Role='staff'";
            SqlCommand cmd4 = new SqlCommand(checkStaff, con);
            con.Open();
            int existStaff = Convert.ToInt32(cmd4.ExecuteScalar().ToString());
            con.Close();

            if (exists)
            {
                lbl_msg.Text = "Account is not activate. Please check your email";
                lbl_msg.ForeColor = System.Drawing.Color.Red;
            }
            else if (TutorStatus ==1)
            {
                lbl_msg.Text = "Account is not activate. Please check your email";
                lbl_msg.ForeColor = System.Drawing.Color.Red;
            }
            else if (existStudent == 1)
            {
                //textbox value is stored in Session  
                Session["StudentID"] = txt_id.Text;
                //Session["password"] = txtpassword.Text;
                //Response.Redirect("WebForm2.aspx");
                Response.Redirect("Student_Home.aspx");
            }
            else if (existTutor == 1)
            {
                //textbox value is stored in Session  
                Session["TutorID"] = txt_id.Text;
                //Session["password"] = txtpassword.Text;
                //Response.Redirect("WebForm2.aspx");
                Response.Redirect("Tutor_Home.aspx");
            }
            else if (existStaff == 1)
            {
                //textbox value is stored in Session  
                Session["StaffID"] = txt_id.Text;
                //Session["password"] = txtpassword.Text;
                //Response.Redirect("WebForm2.aspx");
                Response.Redirect("Admin_Dashboard.aspx");
            }
            else
            {
                lbl_msg.Text = "Wrong Login Name or Password";
                lbl_msg.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void encryption()
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[txt_password.Text.ToString().Length];
            encode = Encoding.UTF8.GetBytes(txt_password.Text);
            strmsg = Convert.ToBase64String(encode);
            encrypwd = strmsg;
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {

            Response.Redirect("Resetpwd.aspx");

        }
    }
}