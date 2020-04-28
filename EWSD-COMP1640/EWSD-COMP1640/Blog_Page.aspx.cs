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
    public partial class Blog_Page : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_mesg.Attributes.Add("maxlength", txt_mesg.MaxLength.ToString());
                showblog();
                showblog2();

                foreach (DataListItem dli in DataList2.Items)
                {
                    ImageButton del = (ImageButton)dli.FindControl("ImageButton3");
                    ImageButton edit = (ImageButton)dli.FindControl("ImageButton1");
                    string lbl_name = ((Label)dli.FindControl("lbl_tutor")).Text;

                    if (lbl_name == Request.QueryString["lbl_name"].ToString())
                    {
                        del.Visible = true;
                        edit.Visible = true;

                    }
                    else
                    {
                        del.Visible = false;
                        edit.Visible = false;
                    }
                }

            }

            lbl_datetime.Text = DateTime.Now.ToString();



            if (Session["StudentID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id='" + Session["StudentID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                lbl_post.Text = Request.QueryString["lbl_tutor"];
                Label1.Text = Request.QueryString["lbl_name"];


            }

            else if (Session["TutorID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Tutor] WHERE Tutor_Id='" + Session["TutorID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                lbl_post.Text = Request.QueryString["lbl_publisher"];
                Label1.Text = Request.QueryString["lbl_name"];

            }
        }


        void showblog()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [blog] WHERE id='" + Request.QueryString["lbl_idb"] + "'", con);
            sqlDa.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {
                DataList1.DataSource = dtbl;
                DataList1.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                DataList1.DataSource = dtbl;
                DataList1.DataBind();

            }
        }

        void showblog2()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [blog] WHERE title='" + Request.QueryString["lbl_title"] + "' AND reply IS NOT NULL ORDER BY id desc", con);
            sqlDa.Fill(dtbl);
            con.Close();

            if (dtbl.Rows.Count > 0)
            {
                DataList2.DataSource = dtbl;
                DataList2.DataBind();
            }
            else
            {
                DataList2.Visible = false;

            }

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);

            con.Open();
            string write_student = "INSERT INTO [blog] (message,title,reply,date_time,Reply_Name) values" +
           "('" + Request.QueryString["lbl_blog"] + "' ,'" + Request.QueryString["lbl_title"] + "','" + txt_mesg.Text + "','" + lbl_datetime.Text + "','" + Label1.Text + "')";

            SqlCommand cmd = new SqlCommand(write_student, con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect(Request.Url.ToString(), false);

        }

        protected void DataList2_ItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "go_back")
            {
                DataList1.EditItemIndex = -1;
                showblog2();
                Response.Redirect(Request.Url.ToString(), false);
            }

            else if (e.CommandName == "publish")
            {

                DataList2.EditItemIndex = e.Item.ItemIndex;
                showblog2();

            }

            else if (e.CommandName == "edit")
            {
                ImageButton ImageButton2 = (ImageButton)e.Item.FindControl("ImageButton2");
                TextBox txt_msg = (TextBox)e.Item.FindControl("txt_msg");
                Label lbl_idb = (Label)e.Item.FindControl("lbl_idb");

                SqlConnection con = new SqlConnection(maincon);
                con.Open();
                string editblog = "UPDATE [blog] set reply= '" + txt_msg.Text + "' WHERE id='" + lbl_idb.Text + "'";
                SqlCommand cmd = new SqlCommand(editblog, con);

                DataList2.EditItemIndex = -1;
                showblog2();
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Redirect(Request.Url.ToString(), false);


            }
            else if (e.CommandName == "delete")
            {
                Label lbl_idb = (Label)e.Item.FindControl("lbl_idb");
                ImageButton ImageButton3 = (ImageButton)e.Item.FindControl("ImageButton3");

                SqlConnection con = new SqlConnection(maincon);
                con.Open();
                string delete = "DELETE  FROM  [blog] WHERE id='" + lbl_idb.Text + "'";
                SqlCommand cmd = new SqlCommand(delete, con);
                //cmd.Connection = con;
                showblog2();
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Redirect(Request.Url.ToString(), false);
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            bool existsstudent = false;

            con.Open();
            string check = "select count (*) from [Student] WHERE CONCAT(Student_Fname ,' ', Student_Lname) = '" + Request.QueryString["lbl_name"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(check, con);
            existsstudent = (int)cmd.ExecuteScalar() > 0;

            if (existsstudent)
            {

                Response.Redirect("Student_Blog.aspx");
            }
            else
            {
                Response.Redirect("Tutor_Blog.aspx");
            }
        }
    }
}