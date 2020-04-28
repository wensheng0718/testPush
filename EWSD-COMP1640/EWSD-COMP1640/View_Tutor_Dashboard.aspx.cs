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
    public partial class View_Tutor_Dashboard : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (Session["TutorName"] != null)
            {

                Label1.Text = Session["TutorName"].ToString();
            }

            if (!IsPostBack)
            {

                LoadMessage();
                ShowStudent();

            }
            
        }

        private void LoadMessage()
        {
            SqlConnection conn = new SqlConnection(maincon);
            //string agent = Session["Admin"].ToString();
            conn.Open();
            string str = "SELECT DISTINCT(a.Group_Id),a.Meeting_Date FROM [Meeting] a LEFT JOIN [Group] b ON a.Group_Id=b.Group_Id WHERE MONTH(a.Last_Update_Date) = MONTH(GetDate()) AND YEAR(a.Last_Update_Date) = YEAR(getdate()) AND b.Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Session["TutorName"].ToString() + "')  ORDER BY a.Meeting_Date DESC";
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

        void ShowStudent()
        {
            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT CONCAT(b.student_Fname ,' ', b.Student_Lname) AS Student_Name,b.Email_Address,b.Course,b.Student_Id,a.Group_Id FROM [Group] a LEFT JOIN [Student] b ON a.Student_Id=b.Student_Id WHERE a.Tutor_Id=(SELECT Tutor_Id FROM Tutor WHERE Tutor_Name='" + Session["TutorName"].ToString() + "')", con);
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

            }

        }

    }
}