using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace EWSD_COMP1640
{
    public partial class Student_Upload : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, typeof(Panel), "PanelChatContent", ";setUploadButtonState();", true);
            SqlConnection con = new SqlConnection(maincon);
            if (Session["StudentID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id='" + Session["StudentID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label2.Text = dt.Rows[0]["Student_Fname"].ToString() + " " + dt.Rows[0]["Student_Lname"].ToString();
            }

            if (!IsPostBack)
            {

                ShowWork();

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool exists = false;
            SqlConnection con = new SqlConnection(maincon);

            con.Open();
            string CheckWork = "select count (*) from [Student_Work] WHERE Student_Id =(SELECT Student_Id FROM [Student] WHERE CONCAT(student_Fname ,' ', Student_Lname) = '" + Label2.Text + "') AND Work_Code='" + Session["WorkCode"].ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(CheckWork, con);
            exists = (int)cmd2.ExecuteScalar() > 0;
            con.Close();

            if(exists)
            {
                label.Text = "Please delete the previous file and reupload again.";
                label.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

                if (FileUpload1.HasFile)
                {

                    FileUpload1.SaveAs(Server.MapPath("~/file/") + Path.GetFileName(FileUpload1.FileName));
                    String link = "file/" + Path.GetFileName(FileUpload1.FileName);
                    String query = "Insert into [Student_Work] (filename,Student_Id,Upload_Date,Work_Code) values('" + link + "','" + Session["StudentId"].ToString() + "',GETDATE(),'" + Session["WorkCode"].ToString() + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    Response.Redirect(Request.Url.ToString(), false);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    label.Text = "Upload Successfully";

                }
                else
                {

                    label.Text = "Please select a file you want to upload.";
                    label.ForeColor = System.Drawing.Color.Red;

                }

            }


        }


        public void ShowWork()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);

            con.Open();
            //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT COUNT(mesg) AS Total_Message,Student_Id FROM [chat] WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Label1.Text + "') GROUP BY Student_Id", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Student_Work] WHERE Student_Id=(SELECT Student_Id FROM [Student] WHERE CONCAT(student_Fname ,' ', Student_Lname)='" + Label2.Text + "') AND Work_Code='" + Session["WorkCode"].ToString() + "' ORDER BY Upload_Date DESC", con);
            sqlDa.Fill(dtbl);

            GridView3.DataSource = dtbl;
            GridView3.DataBind();
            con.Close();

            
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            LinkButton linkbtn = sender as LinkButton;
            GridViewRow showfile = linkbtn.NamingContainer as GridViewRow;
            string dwn = GridView3.DataKeys[showfile.RowIndex].Values[1].ToString();
            //int fileid = Convert.ToInt32(GridView3.DataKeys[showfile.RowIndex].Value.ToString());
            //string name, type;
            //SqlConnection con = new SqlConnection(maincon);

            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "select filenamefrom [upload] where Id = @Id";
            //cmd.Parameters.AddWithValue("@id", fileid);
            //cmd.Connection = con;
            //con.Open();
            //SqlDataReader dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{
            Response.ContentType = "file/";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + dwn + "\"");
            Response.TransmitFile(Server.MapPath(dwn));
            //  Response.BinaryWrite((byte[])dr["filename"]);
            Response.End();
            //}

        }

        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            string query = "DELETE FROM [Student_Work] WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView3.DataKeys[e.RowIndex].Value.ToString()));
            cmd.ExecuteNonQuery();
            Response.Redirect("Student_Upload.aspx");

        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);
            foreach (GridViewRow row in GridView3.Rows)
            {
                Label date = (Label)row.FindControl("lbl_date");
                bool exists = false;
                bool exists2 = false;

                con.Open();
                string checkDate = "SELECT Count(*) FROM Student_Work a LEFT JOIN Upload_Work b ON a.Work_Code=b.Work_Code WHERE a.Upload_Date > b.Work_Deadline AND a.Work_Code='" + Session["WorkCode"].ToString() + "' AND a.Student_Id=(SELECT Student_Id FROM [Student] WHERE CONCAT(student_Fname ,' ', Student_Lname)='" + Label2.Text + "')";
                SqlCommand cmdEmail = new SqlCommand(checkDate, con);
                exists = (int)cmdEmail.ExecuteScalar() > 0;
                con.Close();

                con.Open();
                string checkDate2 = "SELECT Count(*) FROM Student_Work a LEFT JOIN Upload_Work b ON a.Work_Code=b.Work_Code WHERE a.Upload_Date < b.Work_Deadline AND a.Work_Code='" + Session["WorkCode"].ToString() + "' AND a.Student_Id=(SELECT Student_Id FROM [Student] WHERE CONCAT(student_Fname ,' ', Student_Lname)='" + Label2.Text + "')";
                SqlCommand cmdEmail2 = new SqlCommand(checkDate2, con);
                exists2 = (int)cmdEmail2.ExecuteScalar() > 0;
                con.Close();

                if (exists)
                {

                    date.ForeColor = System.Drawing.Color.Red;

                }
                else if (exists2)
                {

                    date.ForeColor = System.Drawing.Color.Green;

                }

            }

        }
    }
}