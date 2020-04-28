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
    public partial class View_Student_Work : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (Session["TutorID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Tutor] WHERE Tutor_Id='" + Session["TutorID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label1.Text = dt.Rows[0]["Tutor_Name"].ToString();
            }
            if (!IsPostBack)
            {

                ShowWork();
               
            }

        }


        public void ShowWork()
        {

            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT COUNT(mesg) AS Total_Message,Student_Id FROM [chat] WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Label1.Text + "') GROUP BY Student_Id", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT d.Work_Deadline,a.id,a.Comment,a.Upload_Date,a.filename,CONCAT(b.student_Fname ,' ', b.Student_Lname) AS Student_Name FROM [Student_Work] a LEFT JOIN [Student] b ON a.Student_Id=b.Student_Id LEFT JOIN [Group] c ON c.Student_Id=a.Student_Id LEFT JOIN [Upload_Work] d ON a.Work_Code=d.Work_Code WHERE c.Tutor_Id='" + Session["TutorID"].ToString() + "' AND a.Work_Code='" + Session["WorkCode"].ToString() + "' ORDER BY a.Upload_Date DESC", con);
            sqlDa.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {
                GridView3.DataSource = dtbl;
                GridView3.DataBind();
            }


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

        protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView3.EditIndex = -1;
            ShowWork();
        }

        protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView3.EditIndex = e.NewEditIndex;
            ShowWork();

        }

        protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            DateTime now = DateTime.Now;
            string Comment = (GridView3.Rows[e.RowIndex].FindControl("txt_comment") as TextBox).Text.Trim();

            string valuestr;
            valuestr = Comment;

        

            if (valuestr.Length > 150)
            {
                Response.Write("<script>alert('Maximum Character should not be more than 150');</script>");
            }
            

           else if (Comment == "" )
            {

                Response.Write("<script>alert('Please write down a comment !');</script>");

            }
            else 
             {

                con.Open();
                string query = "UPDATE [Student_Work] SET Comment=@comment WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@comment", (GridView3.Rows[e.RowIndex].FindControl("txt_comment") as TextBox).Text.Trim());
               
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView3.DataKeys[e.RowIndex].Value.ToString()));
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Update Successfully !');</script>");
                Server.Transfer("View_Student_Work.aspx");

            }


        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime Deadline = (DateTime)DataBinder.Eval(e.Row.DataItem, "Work_Deadline");
                DateTime UploadDate = (DateTime)DataBinder.Eval(e.Row.DataItem, "Upload_Date");

                if (Deadline < UploadDate)
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;

                }

            }
        }

    }
}