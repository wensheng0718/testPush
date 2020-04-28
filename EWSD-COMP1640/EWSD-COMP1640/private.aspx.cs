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
    public partial class _private : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            ScriptManager.RegisterStartupScript(Page, typeof(Panel), "PanelChatContent", ";ScrollToBottom();", true);

            if (Session["TutorID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Tutor] WHERE Tutor_Id='" + Session["TutorID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label1.Text = dt.Rows[0]["Tutor_Name"].ToString();
            }

            if (Session["StudentID"] != null)
            {
                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id = '" + Session["StudentID"].ToString() + "'", conn);
                DataTable dt = new DataTable(); sda.Fill(dt);
                lbl_student.Text = dt.Rows[0]["Student_FName"].ToString() + dt.Rows[0]["Student_LName"].ToString();
            }

            if (!IsPostBack)
            {

                ShowChat();
                txtmesg.Attributes.Add("maxlength", txtmesg.MaxLength.ToString());
            }

        }

        public void ShowChat()
        {

            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            string cmdstr = "select * from [chat] WHERE Student_Id='" + Session["StudentID"].ToString() + "' AND Tutor_Id='" + Session["TutorID"].ToString() + "'";

            SqlCommand cmd = new SqlCommand(cmdstr, con);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataList1.DataSource = ds;
            DataList1.DataBind();
            con.Close();

        }

        protected void btn_send_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            con.Open();

            string ss = "INSERT INTO [chat] (mesg,Tutor_Id,Student_Id,reply) values " + "('" + txtmesg.Text + "',(SELECT Tutor_Id FROM [Tutor] Where Tutor_Id = '" + Session["TutorID"].ToString() + "'),(SELECT Student_Id FROM [Student] Where Student_Id = '" + Session["StudentID"].ToString() + "'),'sent')";

            SqlCommand cmd = new SqlCommand(ss, con);
            cmd.ExecuteNonQuery();
            cmd.Connection = con;
            Response.Redirect(Request.Url.AbsoluteUri);
            txtmesg.Text = "";
            con.Close();

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmesg.Text))// I added && to all the text box, but it still gave me the same result.
            {
                btn_send.Enabled = false;
            }
            else
            {
                btn_send.Enabled = true;
            }
        }

    }
}