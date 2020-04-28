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
    public partial class View_Student_Dashboard : System.Web.UI.Page
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

                Label1.Text = dt.Rows[0]["Student_Fname"].ToString() + " " + dt.Rows[0]["Student_Lname"].ToString();
            }

            if (!IsPostBack)
            {

                LoadMessage();
                BindNote();
                BindExercise();

            }

        }

        private void LoadMessage()
        {
            SqlConnection conn = new SqlConnection(maincon);
            //string agent = Session["Admin"].ToString();
            conn.Open();
            string str = "SELECT DISTINCT(a.Meeting_Date),a.Title,a.Last_Update_Date,a.Content FROM [Meeting] a LEFT JOIN [Group] b ON a.Group_Id=b.Group_Id WHERE MONTH(a.Last_Update_Date) = MONTH(GetDate()) AND YEAR(a.Last_Update_Date) = YEAR(getdate()) AND b.Student_Id=(SELECT Student_Id FROM [Student] WHERE CONCAT(Student_Fname ,' ', Student_Lname)='" + Label1.Text + "') ORDER BY a.Last_Update_Date DESC";
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
            string str = "SELECT * FROM [Upload_Work] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE b.Student_Id='" + Session["StudentID"] + "'";
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

    }
}