using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace EWSD_COMP1640
{
    public partial class Student_Blog : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                showblog();
                publish();
                txt_title.Attributes.Add("maxlength", txt_title.MaxLength.ToString());
                txt_mesg.Attributes.Add("maxlength", txt_mesg.MaxLength.ToString());
            }
            lbltimedate.Text = DateTime.Now.ToString();

            if (Session["StudentID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id='" + Session["StudentID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                lbl_student.Text = dt.Rows[0]["Student_Fname"].ToString() + " " + dt.Rows[0]["Student_Lname"].ToString();
            }
        }

        protected void DataList1_ItemCommand1(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                Button btn_cmnt = (Button)e.Item.FindControl("btn_cmnt");
                Label lbl_idb = (Label)e.Item.FindControl("lbl_idb");
                Label lbl_blog = (Label)e.Item.FindControl("lbl_blog");
                Label lbl_publisher = (Label)e.Item.FindControl("lbl_publisher");
                Label lbl_title = (Label)e.Item.FindControl("lbl_title");

                
                Response.Redirect("Blog_Page.aspx?lbl_blog=" + lbl_blog.Text + "&lbl_publisher=" + lbl_publisher.Text + "&lbl_title=" + lbl_title.Text + "&lbl_idb=" + lbl_idb.Text + "&lbl_name=" + lbl_student.Text);

            }


        }

        void showblog()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [blog]", con);
            sqlDa.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {
                DataList1.DataSource = dtbl;
                DataList1.DataBind();
            }
            else
            {

                DataList1.Visible = false;

            }
        }

        void publish()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM blog WHERE Post_Name=(SELECT CONCAT(student_Fname ,' ', Student_Lname) FROM [Student] WHERE Student_Id='" + Session["StudentID"].ToString() + "')", con);
            sqlDa.Fill(dtbl);

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

        protected void DataList2_ItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "go_back")
            {
                DataList1.EditItemIndex = -1;
                publish();

                Response.Redirect(Request.Url.ToString(), false);
            }
            else if (e.CommandName == "publish")
            {

                DataList2.EditItemIndex = e.Item.ItemIndex;
                publish();

            }

            else if (e.CommandName == "edit")
            {
                ImageButton ImageButton2 = (ImageButton)e.Item.FindControl("ImageButton2");
                TextBox txt_msg = (TextBox)e.Item.FindControl("txt_msg");
                Label lbl_idb = (Label)e.Item.FindControl("lbl_idb");


                SqlConnection con = new SqlConnection(maincon);
                con.Open();
                string editblog = "UPDATE [blog] set message= '" + txt_msg.Text + "' WHERE id='" + lbl_idb.Text + "'";
                SqlCommand cmd = new SqlCommand(editblog, con);
                DataList1.EditItemIndex = -1;
                publish();
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Redirect(Request.Url.ToString(), false);


            }

            else if (e.CommandName == "delete")
            {
                ImageButton ImageButton3 = (ImageButton)e.Item.FindControl("ImageButton3");
                Label lbl_blog = (Label)e.Item.FindControl("lbl_blog");
                Label lbl_title = (Label)e.Item.FindControl("lbl_titleB");

                SqlConnection con = new SqlConnection(maincon);
                con.Open();
                string deletepic = "DELETE  FROM  [blog] WHERE title='" + lbl_title.Text + "' AND message='"+ lbl_blog.Text +"'";
                SqlCommand cmd = new SqlCommand(deletepic, con);
                //cmd.Connection = con;
                publish();
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Redirect(Request.Url.ToString(), false);
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);

            if (file_uploadblog.HasFile)
            {
                string fileExtwnsion = Path.GetExtension(file_uploadblog.FileName);

                if (fileExtwnsion.ToLower() != ".jpg" && fileExtwnsion.ToLower() != ".png" && fileExtwnsion.ToLower() != ".jpeg")
                {
                    lbl_status.Text = "Only jpg,jpeg,png file allowed";
                    lbl_status.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    int fileSize = file_uploadblog.PostedFile.ContentLength;
                    if (fileSize > 2097152)
                    {
                        lbl_status.Text = "Maximum size 2(MB) exceeded ";
                        lbl_status.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {

                        con.Open();
                        file_uploadblog.SaveAs(Server.MapPath("~/images/") + Path.GetFileName(file_uploadblog.FileName));
                        string blogpost = "images/" + Path.GetFileName(file_uploadblog.FileName);
                        string post = "INSERT INTO [blog] (title,message,reply,date_time,blog_pic,Post_Name) VALUES ('" + txt_title.Text + "','" + txt_mesg.Text + "','NO REPLY YET','" + lbltimedate.Text + "','" + blogpost + "','" + lbl_student.Text + "')";
                        SqlCommand cmd1 = new SqlCommand(post, con);
                        lbl_status.Text = "Saved with File Uploaded successfully";
                        lbl_status.ForeColor = System.Drawing.Color.Green;
                        cmd1.ExecuteNonQuery();

                        con.Close();

                        Response.Redirect(Request.Url.AbsoluteUri);



                        //  file_uploadblog.SaveAs(Server.MapPath("~/images/" + file_uploadblog.FileName));

                    }
                }
            }
            else
            {
                con.Open();
                string post = "INSERT INTO [blog] (title,message,reply,date_time,Post_Name) VALUES ('" + txt_title.Text + "','" + txt_mesg.Text + "','NO REPLY YET','" + lbltimedate.Text + "','" + lbl_student.Text + "')";
                SqlCommand cmd1 = new SqlCommand(post, con);
                lbl_status.Text = "Save successfully";
                lbl_status.ForeColor = System.Drawing.Color.Green;
                cmd1.ExecuteNonQuery();

                con.Close();

                Response.Redirect(Request.Url.AbsoluteUri);
            }

            //if (file_uploadblog.HasFile)
            //{

            //    con.Open();
            //    file_uploadblog.SaveAs(Server.MapPath("~/images/") + Path.GetFileName(file_uploadblog.FileName));
            //    string blogpost = "images/" + Path.GetFileName(file_uploadblog.FileName);
            //    string post = "INSERT INTO [blog] (title,message,date_time,blog_pic,Post_Name) VALUES ('" + txt_title.Text + "','" + txt_mesg.Text + "','" + lbltimedate.Text + "','" + blogpost + "','" + lbl_student.Text + "')";
            //    SqlCommand cmd1 = new SqlCommand(post, con);
            //    cmd1.ExecuteNonQuery();

            //    con.Close();

            //    Response.Redirect(Request.Url.AbsoluteUri);

            //}
            //else
            //{

            //    con.Open();
            //    string post = "INSERT INTO [blog] (title,message,date_time,Post_Name) VALUES ('" + txt_title.Text + "','" + txt_mesg.Text + "','" + lbltimedate.Text + "','" + lbl_student.Text + "')";
            //    SqlCommand cmd1 = new SqlCommand(post, con);
            //    cmd1.ExecuteNonQuery();

            //    con.Close();

            //    Response.Redirect(Request.Url.AbsoluteUri);

            //}


        }
    }
}