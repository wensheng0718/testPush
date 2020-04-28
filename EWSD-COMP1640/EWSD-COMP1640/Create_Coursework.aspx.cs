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
using System.IO;

namespace EWSD_COMP1640
{
    public partial class Create_Coursework : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Panel), "PanelChatContent", ";setUploadButtonState();", true);
            if (Session["TutorID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Tutor] WHERE Tutor_Id='" + Session["TutorID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label2.Text = dt.Rows[0]["Tutor_Name"].ToString();
            }

            if(!IsPostBack)
            {

                ShowWork();

            }

        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            bool exists = false;
            var maxFileSize = 4194304;

            con.Open();
            string checkDate = "SELECT Count(*) FROM [Upload_Work] WHERE Work_Code='" + txt_code.Text + "'";
            SqlCommand cmdEmail = new SqlCommand(checkDate, con);
            exists = (int)cmdEmail.ExecuteScalar() > 0;
            con.Close();

            if (txt_code.Text == "")
            {

                Label5.Text = "Work Code is require";
                Label5.ForeColor = System.Drawing.Color.Red;

            }
            else if (exists)
            {

                Label5.Text = "Work Code is already exist";
                Label5.ForeColor = System.Drawing.Color.Red;

            }
            else if(Calendar1.SelectedDate.Date == DateTime.MinValue.Date)
            {

                Label5.Text = "Please select a deadline";
                Label5.ForeColor = System.Drawing.Color.Red;

            }
            else if (FileUpload1.HasFile)
            {
                //bool exists = false;

                //con.Open();
                //string checkDate = "SELECT Count(*) FROM [Upload_Work] WHERE Work_Code='" + txt_code.Text + "'";
                //SqlCommand cmdEmail = new SqlCommand(checkDate, con);
                //exists = (int)cmdEmail.ExecuteScalar() > 0;
                //con.Close();

                //if(exists)
                //{

                //    Label5.Text = "Work Code is already exist";
                //    Label5.ForeColor = System.Drawing.Color.Red;

                //}
                //else
                //{

                    FileUpload1.SaveAs(Server.MapPath("~/file/") + Path.GetFileName(FileUpload1.FileName));
                    String link = "file/" + Path.GetFileName(FileUpload1.FileName);
                    String query = "Insert INTO [Upload_Work] (filename,Tutor_Id,Upload_Date,Work_Deadline,Work_Code) values('" + link + "','" + Session["TutorID"].ToString() + "', GETDATE(),'" + Calendar1.SelectedDate.ToString("yyyy-MM-dd") + "','" + txt_code.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    Response.Redirect(Request.Url.ToString(), false);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

            }
            else
            {

                Label5.Text = "Please Select File to upload";
                Label5.ForeColor = System.Drawing.Color.Red;

            }

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {

            if (e.Day.Date < DateTime.Now)
            {

                e.Cell.BackColor = System.Drawing.Color.LightGray;

                e.Day.IsSelectable = false;

            }

        }

        void ShowWork()
        {

            SqlConnection con = new SqlConnection(maincon);

            DataTable dtbl = new DataTable();
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Upload_Work] WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_name='"+ Label2.Text + "')", con);
            sqlDa.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                GridView1.Rows[0].Cells[0].Text = "No Data Yet";
                GridView1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                GridView1.Width = 1030;
            }

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridView1.EditIndex = -1;
            ShowWork();

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            string query = "DELETE FROM [Upload_Work] WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()));
            cmd.ExecuteNonQuery();
            ShowWork();

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {



        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {



        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;
            ShowWork();

        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            FileUpload filename = (GridView1.Rows[e.RowIndex].FindControl("FileUpload2") as FileUpload);
            string code = (GridView1.Rows[e.RowIndex].FindControl("txt_code") as TextBox).Text.Trim();
            string Date = (GridView1.Rows[e.RowIndex].FindControl("txt_date") as TextBox).Text.Trim();
            string id = (GridView1.Rows[e.RowIndex].FindControl("txt_id") as TextBox).Text.Trim();

            bool exists = false;
            bool exists2 = false;

            con.Open();
            string checkDate = "SELECT Count(*) FROM [Upload_Work] WHERE Work_Code='" + code + "'";
            SqlCommand cmdEmail = new SqlCommand(checkDate, con);
            exists = (int)cmdEmail.ExecuteScalar() > 0;
            con.Close();

            con.Open();
            string check2 = "SELECT Count(*) FROM [Upload_Work] WHERE Work_Code='" + code + "' AND Id='"+id+"'";
            SqlCommand cmd4 = new SqlCommand(check2, con);
            exists2 = (int)cmd4.ExecuteScalar() > 0;
            con.Close();

            if (filename.HasFile)
            {


                if(exists2)
                {

                    filename.SaveAs(Server.MapPath("~/file/") + Path.GetFileName(filename.FileName));
                    String link = "file/" + Path.GetFileName(filename.FileName);
                    String query = "UPDATE [Upload_Work] SET filename='" + link + "',Upload_Date=GETDATE(),Work_Deadline='" + Date + "',Work_Code='" + code + "' WHERE Id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()));
                    Response.Redirect(Request.Url.ToString(), false);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                else if (exists)
                {

                    Label5.Text = "Work Code already exists";

                }

            }
            else
            {

                if(exists2)
                {

                    String query2 = "UPDATE [Upload_Work] SET Upload_Date=GETDATE(),Work_Deadline='" + Date + "',Work_Code='" + code + "' WHERE Id=@id";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()));
                    Response.Redirect(Request.Url.ToString(), false);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();

                }
                else if (exists)
                {

                    Label5.Text = "Work Code already exists";

                }

            }

        }

    }
}