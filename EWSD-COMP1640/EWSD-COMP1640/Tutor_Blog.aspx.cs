using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace EWSD_COMP1640
{
    public partial class Tutor_Blog : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txt_title.Attributes.Add("maxlength", txt_title.MaxLength.ToString());
                txt_mesg.Attributes.Add("maxlength", txt_mesg.MaxLength.ToString());
                showblog();
                publish();

            }

            lbltimedate.Text = DateTime.Now.ToString();

            SqlConnection con = new SqlConnection(maincon);

            if (Session["TutorID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Tutor] WHERE Tutor_Id='" + Session["TutorID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                lbl_tutor.Text = dt.Rows[0]["Tutor_Name"].ToString();
            }
        }

        protected void DataList1_ItemCommand1(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                Button btn_cmnt = (Button)e.Item.FindControl("btn_cmnt");
                Label lbl_idb = (Label)e.Item.FindControl("lbl_idb");
                Label lbl_blog = (Label)e.Item.FindControl("lbl_blog");
                Label lbl_publisher = (Label)e.Item.FindControl("lbl_tutor");
                Label lbl_title = (Label)e.Item.FindControl("lbl_title");
               


                Response.Redirect("Blog_Page.aspx?lbl_blog=" + lbl_blog.Text + "&lbl_publisher=" + lbl_publisher.Text + "&lbl_title=" + lbl_title.Text + "&lbl_idb=" + lbl_idb.Text + "&lbl_name=" + lbl_tutor.Text );

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
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM blog a LEFT JOIN Tutor b on a.Post_Name=b.Tutor_Name WHERE a.Post_Name=(SELECT Tutor_Name FROM Tutor WHERE Tutor_Id='" + Session["TutorID"].ToString() + "')", con);
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
                showblog();
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
                Label lbl_blog = (Label)e.Item.FindControl("lbl_blog");
                Label lbl_title = (Label)e.Item.FindControl("lbl_titleB");
                ImageButton ImageButton3 = (ImageButton)e.Item.FindControl("ImageButton3");

                SqlConnection con = new SqlConnection(maincon);
                con.Open();
                string delete = "DELETE  FROM  [blog] WHERE title='" + lbl_title.Text + "' AND message='" + lbl_blog.Text + "'";
                SqlCommand cmd = new SqlCommand(delete, con);
                //cmd.Connection = con;
                showblog();
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
                        string post = "INSERT INTO [blog] (title,message,reply,date_time,blog_pic,Post_Name) VALUES ('" + txt_title.Text + "','" + txt_mesg.Text + "','NOT YET REPLY','" + lbltimedate.Text + "','" + blogpost + "','" + lbl_tutor.Text + "')";
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
                string post = "INSERT INTO [blog] (title,message,reply,date_time,Post_Name) VALUES ('" + txt_title.Text + "','" + txt_mesg.Text + "','NO REPLY YET','" + lbltimedate.Text + "','" + lbl_tutor.Text + "')";
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
            //    string post = "INSERT INTO [blog] (title,message,date_time,blog_pic,Post_Name) VALUES ('" + txt_title.Text + "','" + txt_mesg.Text + "','" + lbltimedate.Text + "','" + blogpost + "','" + lbl_tutor.Text + "')";
            //    SqlCommand cmd1 = new SqlCommand(post, con);
            //    cmd1.ExecuteNonQuery();

            //    con.Close();

            //    Response.Redirect(Request.Url.AbsoluteUri);

            //}
            //else
            //{

            //    con.Open();
            //    string post = "INSERT INTO [blog] (title,message,date_time,Post_Name) VALUES ('" + txt_title.Text + "','" + txt_mesg.Text + "','" + lbltimedate.Text + "','" + lbl_tutor.Text + "')";
            //    SqlCommand cmd1 = new SqlCommand(post, con);
            //    cmd1.ExecuteNonQuery();

            //    con.Close();

            //    Response.Redirect(Request.Url.AbsoluteUri);

            //}

        }

        protected void DataList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}