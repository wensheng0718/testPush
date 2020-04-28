using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace EWSD_COMP1640
{
    public partial class Register : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string encrypwd;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                lbldate.Text = DateTime.Now.ToString();
                Calendar1.Visible = false;
 
            }

            if(IsPostBack)
            {

                    txt_password.Attributes["value"] = txt_password.Text;
                
            }

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            if(Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
            Calendar1.Attributes.Add("style", "position:absolute");
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txt_dob.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            Calendar1.Visible = false;
        }

        //If the days is other month is unable to select
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {

            if(e.Day.IsOtherMonth)
            {

                e.Day.IsSelectable = false;

            }

        }

        protected void btn_register_Click(object sender, EventArgs e)
        {

           SqlConnection con = new SqlConnection(maincon);

            DateTime first = Convert.ToDateTime(lbldate.Text);
            DateTime after2days = first.AddDays(2);
            after2days.ToString();

            DateTime final = Convert.ToDateTime(lbldate.Text);
            DateTime after28days = final.AddDays(28);
            after28days.ToString();

            bool exists;
            string activationCode = Guid.NewGuid().ToString();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            var random = new Random();

            con.Open();
            string ss2 = "SELECT count(Student_Id) FROM [Student]";
            SqlCommand cmd2 = new SqlCommand(ss2, con);
            int a = Convert.ToInt32(cmd2.ExecuteScalar());
            con.Close();
            a++;
            string StudentID = "s00" + a.ToString();

            con.Open();
            string checkEmail = "select count (*) from [Student] where Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmdEmail = new SqlCommand(checkEmail, con);
            exists = (int)cmdEmail.ExecuteScalar() > 0;
            con.Close();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var rand = new String(stringChars);

            encryption();

            string password = txt_password.Text;

            if (txt_fname.Text == "" || txt_lname.Text == "" || txt_email.Text == "" || txt_dob.Text == "" || txt_password.Text == "" || txt_course.Text == "")
            {

                lbl_fname.Text = "Please enter your First Name";
                lbl_lname.Text = "Please enter your Last Name";
                lbl_password.Text = "Please enter your Password";
                lbl_dob.Text = "Please enter your Date of Birth";
                lbl_email.Text = "Please enter your Email";
                lbl_course.Text = "Please enter your Course";

            }
            else if (txt_password.Text != txt_cpassword.Text)
            {

                lbl_cpassword.Text = "Password not match";

            }
            else if (exists)
            {
                lbl_email.Text = "Email is already exist";
            }
            else
            {

                string ss = "INSERT INTO [Student] (Student_Fname,Student_Lname,Email_Address,DOB,Student_Id,Course,status,first_dateline,final_dateline) values" +
            "('" + txt_fname.Text + "','" + txt_lname.Text + "','" + txt_email.Text + "','" + txt_dob.Text + "','" + StudentID + "','" + txt_course.Text + "','Have Not Assign','" + after2days.ToString() + "','" + after28days.ToString() + "')";
                SqlCommand cmd = new SqlCommand(ss, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                string ss3 = "INSERT INTO [User] (Student_Id,Password,Activation_Code,Status,Role) values" +
            "('" + StudentID + "','" + encrypwd + "','" + activationCode + "','Inactive','Student')";
                SqlCommand cmd3 = new SqlCommand(ss3, con);
                con.Open();
                cmd3.ExecuteNonQuery();
                con.Close();

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                smtp.EnableSsl = true;
                MailMessage mm = new MailMessage();
                mm.Subject = "Account Activation";
                string body = " Hello " + txt_fname.Text + txt_lname.Text + ",";
                body += "\n\n This is your Student_Id :" + StudentID;
                body += "\n\n Please click the following link to activate your account";
                body += "\n\n '" + Request.Url.AbsoluteUri.Replace("Register.aspx", "Activate_Account.aspx?ActivationCode=" + activationCode) + "'";
                body += "\n\n Thanks";
                mm.Body = body;
                string toaddres = txt_email.Text;
                mm.To.Add(toaddres);
                string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                mm.From = new MailAddress(fromaddress);
                smtp.Send(mm);

                lbl_msg.Text = "You are Successfully Registered...!";
                lbl_msg.ForeColor = System.Drawing.Color.Green;
                txt_fname.Text = string.Empty;
                txt_password.Attributes["value"] = string.Empty;
                txt_cpassword.Text = string.Empty;
                txt_email.Text = string.Empty;
                txt_dob.Text = string.Empty;
                txt_lname.Text = string.Empty;
                txt_course.Text = string.Empty;

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

        protected void txt_fname_TextChanged(object sender, EventArgs e)
        {
            if(txt_fname.Text !="")
            {
                lbl_fname.Text = "";
            }
        }

        protected void txt_lname_TextChanged(object sender, EventArgs e)
        {

            if (txt_lname.Text != "")
            {
                lbl_lname.Text = "";
            }

        }

        protected void txt_email_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);

            bool exists = false;
            string email = txt_email.Text;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            con.Open();
            string check = "select count (*) from [Student] where Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmd = new SqlCommand(check, con);
            exists = (int)cmd.ExecuteScalar() > 0;

            if (exists)
            {
                lbl_email.Text = "Email is already exist";
            }
            else if (match.Success)
            {
                lbl_email.Text = "";
            }
            else
            {
                lbl_email.Text = "Invalid Email Address";
            }
        }

        protected void txt_dob_TextChanged(object sender, EventArgs e)
        {

            if (txt_dob.Text != "")
            {
                lbl_dob.Text = "";
            }

        }

        protected void txt_password_TextChanged(object sender, EventArgs e)
        {
            string passwordText = txt_password.Text;
            bool result = passwordText.Any(c => char.IsLetter(c)) && passwordText.Any(c => char.IsDigit(c));

            if(Regex.IsMatch(txt_password.Text, @"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_-]") && txt_password.Text.Any(char.IsDigit) && txt_password.Text.Any(char.IsLetter))
            {

                lbl_password.Text = "";

            }
            else
            {

                lbl_password.Text = "Password should contain 1 alphabet character , 1 number and 1 special character";

            }

        }

        protected void txt_course_TextChanged(object sender, EventArgs e)
        {

            if (txt_course.Text != "")
            {
                lbl_course.Text = "";
            }

        }
    }
}