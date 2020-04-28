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
    public partial class Student_Home : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            if (Session["StudentID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Student] WHERE Student_Id='" + Session["StudentID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label1.Text = dt.Rows[0]["Student_Fname"].ToString() +" " + dt.Rows[0]["Student_Lname"].ToString();
            }

            if(!IsPostBack)
            {

                LoadMessage();
                BindNote();
                BindExercise();
                LoadAlert();

            }

        }

        private void LoadMessage()
        {
            SqlConnection conn = new SqlConnection(maincon);
            //string agent = Session["Admin"].ToString();
            conn.Open();
            string str = "SELECT DISTINCT(a.Meeting_Date),a.Title,a.Last_Update_Date,a.Content FROM [Meeting] a LEFT JOIN [Group] b ON a.Group_Id=b.Group_Id WHERE MONTH(a.Last_Update_Date) = MONTH(GetDate()) AND YEAR(a.Last_Update_Date) = YEAR(getdate()) AND b.Student_Id=(SELECT Student_Id FROM [Student] WHERE CONCAT(student_Fname ,' ', Student_Lname)='" + Label1.Text+"') ORDER BY a.Last_Update_Date DESC";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataList3.DataSource = ds;
            DataList3.DataBind();
            DataList3.DataSource = ds;
            DataList3.DataBind();
            conn.Close();
        }

        public void BindNote()
        {

            SqlConnection conn = new SqlConnection(maincon);
            //string agent = Session["Admin"].ToString();
            conn.Open();
            string str = "SELECT * FROM [Upload_Note] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE b.Student_Id='" + Session["StudentID"] + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.DataSource = ds;
            GridView1.DataBind();
            conn.Close();

        }

        public void BindExercise()
        {

            SqlConnection conn = new SqlConnection(maincon);
            //string agent = Session["Admin"].ToString();
            conn.Open();
            string str = "SELECT * FROM [Upload_Work] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE b.Student_Id='"+ Session["StudentID"] + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.DataSource = ds;
            GridView2.DataBind();
            GridView2.DataSource = ds;
            GridView2.DataBind();
            conn.Close();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            LinkButton linkbtn = sender as LinkButton;
            GridViewRow showfile = linkbtn.NamingContainer as GridViewRow;
            string dwn = GridView1.DataKeys[showfile.RowIndex].Value.ToString();
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

        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            LinkButton linkbtn = sender as LinkButton;
            GridViewRow showfile = linkbtn.NamingContainer as GridViewRow;
            string dwn = GridView2.DataKeys[showfile.RowIndex].Value.ToString();
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

        public void LoadAlert()
        {

            SqlConnection con = new SqlConnection(maincon);
            string check = "select count(*) from Student WHERE get_report = '1' AND Student_Id='" + Session["StudentID"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(check, con);

            con.Open();
            int counts = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            if (counts > 0)
            {
                Label2.Text = "You have 1 notification";
                ImageButton1.Visible = true;
            }
            else
            {
                Label2.Visible = false;
                ImageButton1.Visible = false;
            }

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            Response.Redirect("Not_Assign_Student_Report.aspx?STUDENT_NAME=" + Session["StudentID"].ToString());
        }
    }
}