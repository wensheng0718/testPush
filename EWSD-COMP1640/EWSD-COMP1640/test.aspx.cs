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
using System.Web.UI.DataVisualization.Charting;

namespace EWSD_COMP1640
{
    public partial class test : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Chart2.Visible = false;

                //BindChart();
                //   test();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Chart2.Visible)
            {
                Chart2.Visible = false;

            }
            else
            {
                Chart2.Visible = true;
            }

        }

        protected void Chart2_Load(object sender, EventArgs e)
        {
            Chart2.Visible = false;
        }
        //protected void BindChart()
        //{
        //    SqlConnection con = new SqlConnection(maincon);
        //DataSet ds = new DataSet();

        //    DataTable dt = new DataTable();

        //   con.Open();
        //    //Fetch the Statistical data from database.
        //    //  string cmdstr = "Select status, COUNT(Student_Id) as Total_assigned FROM [Student] where status='assign' Group by status ";
        //    string cmdstr = "SELECT a.status, COUNT(a.Student_Id) AS count " +
        //        ", 'Assigned_Student' as Student " +
        //        "FROM [Student] a LEFT JOIN[User] b on a.Student_Id = b.Student_Id WHERE a.status LIKE 'assign' Group by a.status UNION ALL SELECT b.Role, COUNT(b.Student_Id) AS count , 'Total_Student'" +
        //        "FROM[User] b LEFT JOIN[Student] a on a.Student_Id = b.Student_Id " +
        //        "WHERE b.Role  LIKE 'student'  Group by b.Role";
        //    // DataTable dt = GetData(query);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

        //    adp.Fill(ds);

        //    dt = ds.Tables[0];

        //    //Get the DISTINCT Countries.
        //    List<string> student = (from p in dt.AsEnumerable()
        //                              select p.Field<string>("Student_Id")).Distinct().ToList();

        //    //Loop through the Countries.
        //    foreach (string country in student)
        //    {

        //        //Get the Year for each Country.
        //        int[] x = (from p in dt.AsEnumerable()
        //                   where p.Field<string>("Student_Id") == student
        //                   orderby p.Field<int>("Student_Id") ascending
        //                   select p.Field<int>("Student_Id")).ToArray();

        //        //Get the Total of Orders for each Country.
        //        int[] y = (from p in dt.AsEnumerable()
        //                   where p.Field<string>("Student_Id") == student
        //                   orderby p.Field<int>("Student_Id") ascending
        //                   select p.Field<int>("Student_Id")).ToArray();

        //        //Add Series to the Chart.
        //        Chart1.Series.Add(new Series(student));
        //        Chart1.Series[student].IsValueShownAsLabel = true;
        //        Chart1.Series[student].ChartType = SeriesChartType.Column;
        //        Chart1.Series[student].Points.DataBindXY(x, y);
        //    }

        //    Chart1.Legends[0].Enabled = true;
        //}
    }


        //protected void BindChart()

        //{

        //    SqlConnection con = new SqlConnection(maincon);
        //    DataSet ds = new DataSet();

        //    DataTable dt = new DataTable();

        //    con.Open();

        //    //  string cmdstr = "Select status, COUNT(Student_Id) as Total_assigned FROM [Student] where status='assign' Group by status ";
        //    string cmdstr = "SELECT a.status, COUNT(a.Student_Id) AS count " +
        //        ", 'Assigned_Student' as Student " +
        //        "FROM [Student] a LEFT JOIN[User] b on a.Student_Id = b.Student_Id WHERE a.status LIKE 'assign' Group by a.status UNION ALL SELECT b.Role, COUNT(b.Student_Id) AS count , 'Total_Student'" +
        //        "FROM[User] b LEFT JOIN[Student] a on a.Student_Id = b.Student_Id " +
        //        "WHERE b.Role  LIKE 'student'  Group by b.Role";
            
        //    SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

        //    adp.Fill(ds);

        //    dt = ds.Tables[0];

        //    string[] x = new string[dt.Rows.Count];
        //    //int[] x = new int[dt.Rows.Count];
        //    int[] y = new int[dt.Rows.Count];

        //    for (int i = 0; i < dt.Rows.Count; i++)

        //    {

        //        x[i] = dt.Rows[i][0].ToString();
        //       // x[i] = Convert.ToInt32(dt.Rows[i][0]);
        //        y[i] = Convert.ToInt32(dt.Rows[i][1]);

        //    }

        //    Chart1.Series[0].Points.DataBindXY(x, y);

        //    Chart1.Series[0].ChartType = SeriesChartType.Pie;

        //   

        //    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

        //   // Chart1.Legends[0].Enabled = true;
        //  
        //}






    }



