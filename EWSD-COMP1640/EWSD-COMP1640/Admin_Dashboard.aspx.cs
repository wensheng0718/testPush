using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;

namespace EWSD_COMP1640
{
    public partial class Admin_Dashboard : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (Session["StaffID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Staff] WHERE Staff_Id='" + Session["StaffID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label1.Text = dt.Rows[0]["Staff_Name"].ToString();
            }

            if (!IsPostBack)
            {

                LoadStudent();
                LoadAssignedStudent();
                LoadUnassignedStudent();
                LoadTutor();
                LoadEmptyTutor();
                chartStudent();
                charttutor();
            }

        }

        protected void chartStudent()

        {

            SqlConnection con = new SqlConnection(maincon);
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            con.Open();

            string cmdstr = "SELECT a.status, COUNT(a.Student_Id) AS count, 'Assigned_Student' as Student FROM[Student] a LEFT JOIN[User] b on a.Student_Id = b.Student_Id  WHERE a.status LIKE 'assign' Group by a.status   UNION ALL  SELECT b.Role, COUNT(b.Student_Id) AS count , 'Total_Student'  FROM[User] b LEFT JOIN[Student] a on a.Student_Id = b.Student_Id WHERE b.Role LIKE 'student'  Group by b.Role UNION ALL SELECT a.status, COUNT(a.Student_Id) AS count  , 'Assigned_Student' as Student FROM[Student] a LEFT JOIN[User] b on a.Student_Id = b.Student_Id WHERE a.status LIKE 'Have Not Assign' Group by a.status";

            SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

            adp.Fill(ds);

            dt = ds.Tables[0];

            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)

            {

                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }

            Chart2.Series[0].Points.DataBindXY(x, y);

            Chart2.Series[0].ChartType = SeriesChartType.Pie;



            Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;


        }

        protected void charttutor()

        {

            SqlConnection con = new SqlConnection(maincon);
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            con.Open();

            string cmdstr = "SELECT b.Role, COUNT(a.Tutor_Id) AS count, 'Total_Tutor' as Tutor FROM [Tutor] a LEFT JOIN [User] b on a.Tutor_Id=b.Tutor_Id WHERE b.Role LIKE 'tutor' Group by b.Role  UNION ALL SELECT a.status, COUNT(b.Student_Id) AS count, 'Total_Tutor_without_Student' FROM[User] b LEFT JOIN[Student] a on a.Student_Id = b.Student_Id WHERE a.status LIKE 'Have Not Assign'  Group by a.status";

            SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

            adp.Fill(ds);

            dt = ds.Tables[0];

            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)

            {

                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }

            Chart1.Series[0].Points.DataBindXY(x, y);

            Chart1.Series[0].ChartType = SeriesChartType.Pie;



            Chart1.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = false;


        }
        public void LoadStudent()
        {

            SqlConnection con = new SqlConnection(maincon);
            string check = "select count(*) from [Student]";
            SqlCommand cmd = new SqlCommand(check, con);

            con.Open();
            int counts = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            if (counts > 0)
            {
                Label12.Text = counts.ToString();
            }
            else
            {
                
            }

        }

        public void LoadUnassignedStudent()
        {

            SqlConnection con = new SqlConnection(maincon);
            string check = "select count(*) from [Student] WHERE Student_Id IN (SELECT Student_Id FROM [Group])";
            SqlCommand cmd = new SqlCommand(check, con);

            con.Open();
            int counts = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            if (counts > 0)
            {
                Label2.Text = counts.ToString();
            }
            else
            {

            }

        }

        public void LoadAssignedStudent()
        {

            SqlConnection con = new SqlConnection(maincon);
            string check = "select count(*) from [Student] WHERE Student_Id NOT IN (SELECT Student_Id FROM [Group])";
            SqlCommand cmd = new SqlCommand(check, con);

            con.Open();
            int counts = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            if (counts > 0)
            {
                Label3.Text = counts.ToString();
            }
            else
            {

            }

        }

        public void LoadTutor()
        {

            SqlConnection con = new SqlConnection(maincon);
            string check = "select count(*) from [Tutor] ";
            SqlCommand cmd = new SqlCommand(check, con);

            con.Open();
            int counts = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            if (counts > 0)
            {
                Label4.Text = counts.ToString();
            }
            else
            {

            }

        }

        public void LoadEmptyTutor()
        {

            SqlConnection con = new SqlConnection(maincon);
            string check = "select count(*) from [Tutor] WHERE Tutor_Id NOT IN (SELECT Tutor_Id FROM [Group])";
            SqlCommand cmd = new SqlCommand(check, con);

            con.Open();
            int counts = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            if (counts > 0)
            {
                Label5.Text = counts.ToString();
            }
            else
            {

            }

        }

    }
}