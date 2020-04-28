using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace EWSD_COMP1640
{
    public partial class Add_Tutor : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string encrypwd;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (Session["StaffID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Staff] WHERE Staff_Id='" + Session["StaffID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label6.Text = dt.Rows[0]["Staff_Name"].ToString();
            }

            if (IsPostBack)
            {

                txt_password.Attributes["value"] = txt_password.Text;
                DropDownList1.Items[0].Attributes.Add("disabled", "disabled");

            }

            if(!IsPostBack)
            {

                DisableCourse();
                DropDownList1.SelectedValue = "0";
                DropDownList1.Items[0].Attributes.Add("disabled", "disabled");
            }

        }


        protected void btn_register_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);

            bool existsTutor = false;
            bool existsStaff = false;

            string activationCode = Guid.NewGuid().ToString();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            var random = new Random();

            con.Open();
            string generateTutorID = "SELECT COUNT(Tutor_Id) FROM [Tutor]";
            SqlCommand cmd2 = new SqlCommand(generateTutorID, con);
            int a = Convert.ToInt32(cmd2.ExecuteScalar());
            con.Close();
            a++;
            string TutorID = "L00" + a.ToString();

            con.Open();
            string generateStaffID = "SELECT COUNT(Staff_Id) FROM [Staff]";
            SqlCommand cmd4 = new SqlCommand(generateStaffID, con);
            int b = Convert.ToInt32(cmd4.ExecuteScalar());
            con.Close();
            b++;
            string StaffID = "staff00" + a.ToString();

            con.Open();
            string check = "select count (*) from [Tutor] where Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmdTutor = new SqlCommand(check, con);
            existsTutor = (int)cmdTutor.ExecuteScalar() > 0;
            con.Close();

            con.Open();
            string checkStaff = "select count (*) from [Staff] where Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmdStaff = new SqlCommand(checkStaff, con);
            existsStaff = (int)cmdStaff.ExecuteScalar() > 0;
            con.Close();


            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var rand = new String(stringChars);

            encryption();

            string password = txt_password.Text;

            if (DropDownList1.SelectedValue == "0")
            {
                lbl_msg.Text = "Please Select a role";
                lbl_msg.ForeColor = System.Drawing.Color.Red;
            }
            else if (DropDownList1.SelectedValue == "1")
            {
                if (txt_name.Text == "" || txt_email.Text == "" || txt_password.Text == "" || txt_course.Text == "")
                {
    
                    lbl_name.Text = "Please enter your Name";
                    lbl_password.Text = "Please enter Password";
                    lbl_email.Text = "Please enter Email";
                    lbl_course.Text = "Please enter Course";
                    lbl_msg.Text = string.Empty;


                }
                else if (txt_password.Text != txt_cpassword.Text)
                {
    
                    lbl_cpassword.Text = "Password not match";
    
                }
                else if(existsTutor)
                {
                    lbl_email.Text = "Email is already exist";
                }
                else
                {
                    string ss = "INSERT INTO [Tutor] (Tutor_Name,Tutor_Id,Email_Address,Course) values" +
                    "('" + txt_name.Text + "','" + TutorID + "','" + txt_email.Text + "','" + txt_course.Text + "')";
                    SqlCommand cmd = new SqlCommand(ss, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Session["email"] = txt_email.Text;
                    con.Close();

                    string ss3 = "INSERT INTO [User] (Tutor_Id,Password,Activation_Code,Status,Role) values" +
                    "('" + TutorID + "','" + encrypwd + "','" + activationCode + "','Inactive','Tutor')";
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
                    string body = " Hello " + txt_name.Text + ",";
                    body += "\n\n This is your Tutor ID :" + TutorID;
                    body += "\n\n Password :" + txt_password.Text;
                    body += "\n\n Please click the following link to activate your account";
                    body += "\n\n '" + Request.Url.AbsoluteUri.Replace("Add_Tutor.aspx", "Activate_Account.aspx?ActivationCode=" + activationCode) + "'";
                    body += "\n\n Thanks";
                    mm.Body = body;
                    string toaddres = txt_email.Text;
                    mm.To.Add(toaddres);
                    string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                    mm.From = new MailAddress(fromaddress);
                    smtp.Send(mm);
    
                    lbl_msg.Text = "You are Successfully Registered...!";
                    lbl_msg.ForeColor = System.Drawing.Color.Green;
                    txt_name.Text = string.Empty;
                    txt_password.Attributes["value"] = string.Empty;
                    txt_cpassword.Text = string.Empty;
                    txt_email.Text = string.Empty;
                    txt_course.Text = string.Empty;
                    DropDownList1.SelectedValue = "0";
                }

            }
            else if (DropDownList1.SelectedValue == "2")
            {

                if (txt_name.Text == "" || txt_email.Text == "" || txt_password.Text == "")
                {

                    lbl_name.Text = "Please enter your Name";
                    lbl_password.Text = "Please enter Password";
                    lbl_email.Text = "Please enter Email";
                    lbl_msg.Text = string.Empty;

                }
                else if (txt_password.Text != txt_cpassword.Text)
                {

                    lbl_cpassword.Text = "Password not match";

                }
                else if (existsStaff)
                {
                    lbl_email.Text = "Email is already exist";
                }
                else
                {

                    txt_course.Visible = false;
                    string ss = "INSERT INTO [Staff] (Staff_Id,Staff_Name,Email_Address) values" +
                    "('" + StaffID + "','" + txt_name.Text + "','" + txt_email.Text + "')";
                    SqlCommand cmd10 = new SqlCommand(ss, con);
                    con.Open();
                    cmd10.ExecuteNonQuery();
                    Session["email"] = txt_email.Text;
                    con.Close();

                    string ss3 = "INSERT INTO [User] (Staff_Id,Password,Activation_Code,Status,Role) values" +
                    "('" + StaffID + "','" + encrypwd + "','" + activationCode + "','Inactive','staff')";
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
                    string body = " Hello " + txt_name.Text + ",";
                    body += "\n\n This is your Staff ID :" + StaffID;
                    body += "\n\n Password :" + txt_password.Text;
                    body += "\n\n Please click the following link to activate your account";
                    body += "\n\n '" + Request.Url.AbsoluteUri.Replace("Add_Tutor.aspx", "Activate_Account.aspx?ActivationCode=" + activationCode) + "'";
                    body += "\n\n Thanks";
                    mm.Body = body;
                    string toaddres = txt_email.Text;
                    mm.To.Add(toaddres);
                    string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                    mm.From = new MailAddress(fromaddress);
                    smtp.Send(mm);

                    lbl_msg.Text = "You are Successfully Registered...!";
                    lbl_msg.ForeColor = System.Drawing.Color.Green;
                    txt_name.Text = string.Empty;
                    txt_password.Attributes["value"] = string.Empty;
                    txt_cpassword.Text = string.Empty;
                    txt_email.Text = string.Empty;
                    lbl_email.Text = string.Empty;
                    lbl_password.Text = string.Empty;
                    DropDownList1.SelectedValue = "0";

                   }
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

        protected void txt_name_TextChanged(object sender, EventArgs e)
        {

            if (txt_name.Text != "")
            {
                lbl_name.Text = "";
            }

        }

        protected void txt_email_TextChanged(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            bool existsTutor = false;
            bool existsStaff = false;
            string email = txt_email.Text;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            con.Open();
            string check = "select count (*) from [Tutor] where Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmd = new SqlCommand(check, con);
            existsTutor = (int)cmd.ExecuteScalar() > 0;
            con.Close();

            con.Open();
            string checkStaff = "select count (*) from [Staff] where Email_Address = '" + txt_email.Text + "'";
            SqlCommand cmd2 = new SqlCommand(checkStaff, con);
            existsStaff = (int)cmd2.ExecuteScalar() > 0;
            con.Close();

            if (existsTutor && DropDownList1.SelectedValue == "1")
            {
                lbl_email.Text = "Email is already exist";
            }
            else if (existsStaff && DropDownList1.SelectedValue == "2")
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

        protected void txt_password_TextChanged(object sender, EventArgs e)
        {

            string passwordText = txt_password.Text;
            bool result = passwordText.Any(c => char.IsLetter(c)) && passwordText.Any(c => char.IsDigit(c));

            if (Regex.IsMatch(txt_password.Text, @"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_-]") && txt_password.Text.Any(char.IsDigit) && txt_password.Text.Any(char.IsLetter))
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

        protected void txt_cpassword_TextChanged(object sender, EventArgs e)
        {

            if (txt_password.Text == txt_cpassword.Text )
            {
                lbl_cpassword.Text = "";
            }

        }


        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {

            if (DropDownList1.SelectedItem.Text == "Staff")
            {

                txt_course.Visible = false;

            }
            else
            {

                txt_course.Visible = true;

            }

        }

        public void DisableCourse()
        {
            if(DropDownList1.SelectedValue =="2")
            {
                txt_course.Visible = false;
                lbl_course.Visible = false;

                lbl_cpassword.Text = string.Empty;
                lbl_email.Text = string.Empty;
                lbl_msg.Text = string.Empty;
                lbl_name.Text = string.Empty;
                lbl_password.Text = string.Empty;

                txt_email.Text = string.Empty;
                txt_name.Text = string.Empty;
                txt_password.Text = string.Empty;
                txt_cpassword.Text = string.Empty;

            }
            else
            {

                txt_course.Visible = true;
                lbl_course.Visible = true;

                lbl_cpassword.Text = string.Empty;
                lbl_email.Text = string.Empty;
                lbl_msg.Text = string.Empty;
                lbl_name.Text = string.Empty;
                lbl_password.Text = string.Empty;

                txt_email.Text = string.Empty;
                txt_name.Text = string.Empty;
                txt_password.Text = string.Empty;
                txt_cpassword.Text = string.Empty;

            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableCourse();
        }
    }
}