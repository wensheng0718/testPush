using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using System.Text;

namespace EWSD_COMP1640
{
    public partial class Resetpwd2 : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string encrypwd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {

            encryption();
            bool exists = false;
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            string check = "SELECT count(*) from [User] WHERE Verification_Code = '" + txt_verify.Text + "'";
            SqlCommand cmd = new SqlCommand(check, con);
            exists = (int)cmd.ExecuteScalar() == 0;
            con.Close();

            if (txt_verify.Text == "" || txt_cpassword.Text == "" || txt_nPassword.Text == "")
            {
                Label4.Text = "Please fill in all the field!";
            }
            else if (exists)
            {
                Label4.Text = "Invalid verification code!";
            }
            else if (txt_cpassword.Text == "" || txt_nPassword.Text == "")
            {
                Label4.Text = "Please fill in all the field!";
            }
            else if (txt_cpassword.Text != txt_nPassword.Text)
            {
                Label4.Text = "Password not match!";
            }
            else 
            {
                string ss = "UPDATE [User] SET Password='" + encrypwd + "' WHERE Verification_Code='" + txt_verify.Text + "'";
                SqlCommand cmd2 = new SqlCommand(ss, con);
                con.Open();
                cmd2.ExecuteNonQuery();

                Response.Write("<script>alert('Password Change Successfully !');</script>");
                Server.Transfer("Login.aspx");

            }

        }

        public void encryption()
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[txt_nPassword.Text.ToString().Length];
            encode = Encoding.UTF8.GetBytes(txt_nPassword.Text);
            strmsg = Convert.ToBase64String(encode);
            encrypwd = strmsg;
        }

    }
}