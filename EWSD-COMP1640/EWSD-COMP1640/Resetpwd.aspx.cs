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
    public partial class Resetpwd : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {

            bool existsStudent = true;
            bool existsTutor = true;
            bool existStaff = true;

            SqlConnection con = new SqlConnection(maincon);

            con.Open();
            string checkTutor = "select count(*) from [Tutor] WHERE Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmd3 = new SqlCommand(checkTutor, con);
            existsTutor = (int)cmd3.ExecuteScalar() > 0;
            con.Close();

            con.Open();
            string checkStudent = "select count(*) from [Student] WHERE Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmd2 = new SqlCommand(checkStudent, con);
            existsStudent = (int)cmd2.ExecuteScalar() > 0;
            con.Close();

            con.Open();
            string checkStaff = "select count(*) from [Staff] WHERE Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmd7 = new SqlCommand(checkStaff, con);
            existStaff = (int)cmd7.ExecuteScalar() > 0;
            con.Close();


            if (txt_email.Text == "")
            {
                Label3.Text = "Please fill in your email";
                Label3.ForeColor = System.Drawing.Color.Red;
            }
            else if (existsStudent)
            {

                Random rnd = new Random();
                int rand = rnd.Next(10000, 99999);
                con.Open();
                string query = "UPDATE [User] SET Verification_Code='" + rand + "' WHERE Student_Id=(SELECT Student_Id FROM [Student] WHERE Email_Address='" + txt_email.Text + "')";
                SqlCommand cmd4 = new SqlCommand(query, con);
                cmd4.ExecuteNonQuery();
                con.Close();

                string queryString = "SELECT * FROM [Student] WHERE Email_Address = '" + txt_email.Text + "'";
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                cmd = new SqlCommand(queryString);
                cmd.Connection = con;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                    smtp.EnableSsl = true;
                    MailMessage msg = new MailMessage();
                    msg.Subject = "Reset Password";
                    msg.Body = "Hi " + (reader["Student_Fname"].ToString()) +" " + (reader["Student_Lname"].ToString()) + "," + "\n" + "we have receive your reset password request. This is your verification code" + "\n" + "Verification Code : " + rand + "\n" + "Student ID : " + (reader["Student_Id"].ToString()) + "\n" + "Please click the link below to change password \n " + Request.Url.AbsoluteUri.Replace("Resetpwd.aspx", "Resetpwd2.aspx?Student_Id=" + (reader["Student_Id"].ToString())) + "";
                    string toaddres = txt_email.Text;
                    msg.To.Add(toaddres);
                    string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                    msg.From = new MailAddress(fromaddress);
                    smtp.Send(msg);
                    Label3.Text = "A verify code is sent to your email";
                    Label3.ForeColor = System.Drawing.Color.Green;
                }
                reader.Close();
                con.Close();
            }
            else if (existsTutor)
            {

                Random rnd = new Random();
                int rand = rnd.Next(10000, 99999);
                con.Open();
                string query = "UPDATE [User] SET Verification_Code='" + rand + "' WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Email_Address='" + txt_email.Text + "')";
                SqlCommand cmd5 = new SqlCommand(query, con);
                cmd5.ExecuteNonQuery();
                con.Close();

                string queryString = "SELECT * FROM [Tutor] WHERE Email_Address = '" + txt_email.Text + "'";
                SqlCommand cmd6 = new SqlCommand(queryString, con);
                con.Open();
                cmd6 = new SqlCommand(queryString);
                cmd6.Connection = con;
                SqlDataReader reader2 = cmd6.ExecuteReader();

                if (reader2.Read())
                {

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                    smtp.EnableSsl = true;
                    MailMessage msg = new MailMessage();
                    msg.Subject = "Reset Password";
                    msg.Body = "Hi " + (reader2["Tutor_Name"].ToString()) + "," + "\n" + "we have receive your reset password request. This is your verification code" + "\n" + "Verification Code : " + rand + "\n" + "Tutor ID : " + (reader2["Tutor_Id"].ToString()) + "\n" + "Please click the link below to change password \n " + Request.Url.AbsoluteUri.Replace("Resetpwd.aspx", "Resetpwd2.aspx?Student_Id=" + (reader2["Tutor_Id"].ToString())) + "";
                    string toaddres = txt_email.Text;
                    msg.To.Add(toaddres);
                    string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                    msg.From = new MailAddress(fromaddress);
                    smtp.Send(msg);
                    Label3.Text = "A verify code is sent to your email";
                    Label3.ForeColor = System.Drawing.Color.Green;
                }
                reader2.Close();
                con.Close();
            }
            else if (existStaff)
            {

                Random rnd = new Random();
                int rand = rnd.Next(10000, 99999);
                con.Open();
                string query = "UPDATE [User] SET Verification_Code='" + rand + "' WHERE Staff_Id=(SELECT Staff_Id FROM [Staff] WHERE Email_Address='" + txt_email.Text + "')";
                SqlCommand cmd5 = new SqlCommand(query, con);
                cmd5.ExecuteNonQuery();
                con.Close();

                string queryString = "SELECT * FROM [Staff] WHERE Email_Address = '" + txt_email.Text + "'";
                SqlCommand cmd8 = new SqlCommand(queryString, con);
                con.Open();
                cmd8 = new SqlCommand(queryString);
                cmd8.Connection = con;
                SqlDataReader reader3 = cmd8.ExecuteReader();

                if (reader3.Read())
                {

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                    smtp.EnableSsl = true;
                    MailMessage msg = new MailMessage();
                    msg.Subject = "Reset Password";
                    msg.Body = "Hi " + (reader3["Staff_Name"].ToString()) + "," + "\n" + "we have receive your reset password request. This is your verification code" + "\n" + "Verification Code : " + rand + "\n" + "Tutor ID : " + (reader3["Staff_Id"].ToString()) + "\n" + "Please click the link below to change password \n " + Request.Url.AbsoluteUri.Replace("Resetpwd.aspx", "Resetpwd2.aspx?Student_Id=" + (reader3["Staff_Id"].ToString())) + "";
                    string toaddres = txt_email.Text;
                    msg.To.Add(toaddres);
                    string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                    msg.From = new MailAddress(fromaddress);
                    smtp.Send(msg);
                    Label3.Text = "A verify code is sent to your email";
                    Label3.ForeColor = System.Drawing.Color.Green;
                }
                reader3.Close();
                con.Close();
            }
            else
            {

                Label3.Text = "Email is not available";
                Label3.ForeColor = System.Drawing.Color.Red;

            }

        }
    }
}