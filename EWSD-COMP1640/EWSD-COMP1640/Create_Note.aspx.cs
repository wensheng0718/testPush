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
    public partial class Create_Note : System.Web.UI.Page
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

            if (!IsPostBack)
            {

                ShowNote();

            }

        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if(txt_desc.Text =="")
            {

                Label5.Text = "Please Write an description";
                Label5.ForeColor = System.Drawing.Color.Red;

            }
            else if (FileUpload1.HasFile)
            {


                    FileUpload1.SaveAs(Server.MapPath("~/file/") + Path.GetFileName(FileUpload1.FileName));
                    String link = "file/" + Path.GetFileName(FileUpload1.FileName);
                    String query = "Insert INTO [Upload_Note] (filename,Tutor_Id,Upload_Date,Description) values('" + link + "',(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='"+ Label2.Text+ "'), GETDATE(),'"+txt_desc.Text+"')";
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


        void ShowNote()
        {

            SqlConnection con = new SqlConnection(maincon);

            DataTable dtbl = new DataTable();
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Upload_Note] WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_name='" + Label2.Text + "')", con);
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
            ShowNote();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            string query = "DELETE FROM [Upload_Note] WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()));
            cmd.ExecuteNonQuery();
            ShowNote();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            ShowNote();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            FileUpload filename = (GridView1.Rows[e.RowIndex].FindControl("FileUpload2") as FileUpload);
            string desc = (GridView1.Rows[e.RowIndex].FindControl("txt_desc2") as TextBox).Text.Trim();

            if (filename.HasFile)
            {

                filename.SaveAs(Server.MapPath("~/file/") + Path.GetFileName(filename.FileName));
                String link = "file/" + Path.GetFileName(filename.FileName);
                String query = "UPDATE [Upload_Note] SET filename='" + link + "',Upload_Date=GETDATE(),Description='" + desc + "' WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()));
                Response.Redirect(Request.Url.ToString(), false);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            else
            {

                String query = "UPDATE [Upload_Note] SET Upload_Date=GETDATE(),Description='" + desc + "' WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()));
                Response.Redirect(Request.Url.ToString(), false);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }
    }
}