using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace EWSD_COMP1640
{
    public partial class privatechatstudent : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Panel), "PanelChatContent", ";ScrollToBottom();", true);
            if (Session["StudentID"] != null)
            {
                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id = '" + Session["StudentID"].ToString() + "'", conn);
                DataTable dt = new DataTable(); sda.Fill(dt);
                Label1.Text = dt.Rows[0]["Student_FName"].ToString() + dt.Rows[0]["Student_LName"].ToString();
            }
            if (!IsPostBack)
            {
                ShowTutor();
                ShowChat();
                
                txtmesg.Attributes.Add("maxlength", txtmesg.MaxLength.ToString());
            }



        }
        public void ShowChat()
        {

            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            string cmdstr = "select a.Id,a.mesg,CONCAT(b.student_Fname ,' ', b.Student_Lname) AS Student_Name,c.Tutor_Name FROM [chat] a LEFT JOIN [Student] b ON a.Student_Id=b.Student_Id LEFT JOIN [Tutor] c ON a.Tutor_Id=c.Tutor_Id WHERE b.Student_Id='" + Session["StudentID"].ToString() + "' AND c.Tutor_Name='"+ lbltutor.Text + "'";

            SqlCommand cmd = new SqlCommand(cmdstr, con);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataList2.DataSource = ds;
            DataList2.DataBind();
            con.Close();

        }

        protected void btnenter_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            string strValue;
            strValue = txtmesg.Text;

            if (strValue.Length > 200)
            {
                Response.Write("<script>alert('Maxixmum character should not more than 200');</script>");
            }
            else
            {
                con.Open();
                string ss = "INSERT INTO [chat] (mesg,Tutor_Id,Student_Id,reply) values " + "('" + txtmesg.Text + "',(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + lbltutor.Text + "'),'" + Session["StudentID"].ToString() + "','sent')";

                SqlCommand cmd = new SqlCommand(ss, con);
                cmd.ExecuteNonQuery();
                cmd.Connection = con;
                Response.Redirect(Request.Url.AbsoluteUri);
                txtmesg.Text = "";
                con.Close();
            }

        }

        public void ShowTutor()
        {

                SqlConnection con = new SqlConnection(maincon);
                bool exists;
                con.Open();
                string checkEmail = "select count (*) from [Group] a LEFT JOIN [Student] b ON a.Student_Id=b.Student_Id WHERE a.Student_Id IN (SELECT Student_Id FROM [Group] WHERE Student_Id='" + Session["StudentID"].ToString() + "')";
                SqlCommand cmdEmail = new SqlCommand(checkEmail, con);
                exists = (int)cmdEmail.ExecuteScalar() > 0;
                con.Close();

            if (exists)
            {

                SqlDataAdapter sda = new SqlDataAdapter("SELECT a.Tutor_Id,c.Tutor_Name  FROM [Group] a LEFT JOIN [chat] b ON a.Student_Id=b.Student_Id LEFT JOIN [Tutor] c ON a.Tutor_Id=c.Tutor_Id  WHERE a.Student_Id='" + Session["StudentID"].ToString() + "' GROUP BY a.Tutor_Id,c.Tutor_Name", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                lbltutor.Text = dt.Rows[0]["Tutor_Name"].ToString();

            }
            else
            {
                Response.Write("<script>alert('You do not have any tutor yet !');</script>");
                Server.Transfer("Student_Home.aspx");
            }

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmesg.Text))// I added && to all the text box, but it still gave me the same result.
            {
                btnenter.Enabled = false;
            }
            else
            {
                btnenter.Enabled = true;
            }
        }

    }
}