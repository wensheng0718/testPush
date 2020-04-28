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

namespace EWSD_COMP1640
{
    public partial class calender : System.Web.UI.Page
    {
        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar1.SelectedDate = DateTime.Now;
                Calendar1.Visible = false;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Calendar1.Visible = true;
            TextBox1.Text = "";
            txt_datetime.Text = "";
            txt_title.Text = "";

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
           txt_datetime.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            Calendar1.Visible = false;
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now)
            {

                e.Cell.BackColor = System.Drawing.Color.LightGray;

                e.Day.IsSelectable = false;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime start = Convert.ToDateTime("9:00:00 AM");
            DateTime end = Convert.ToDateTime("3:00:00 PM");
            DateTime t1 = Convert.ToDateTime(txt_datetime.Text);

            if ((t1 < start) || (t1 > end))
            {
                Response.Write("<script>alert('Meeting time is between 9:00 AM - 3:00 PM !');</script>");
            }
            else
            {
                SqlConnection con = new SqlConnection(maincon);
                string ss = "INSERT INTO [test] (title,Meeting_Date,time) values" +
                 "('" + txt_title.Text + "','" + Calendar1.SelectedDate.Year + "-" + Calendar1.SelectedDate.Month + "-" + Calendar1.SelectedDate.Day + " " + TextBox1.Text + "','" + txt_datetime.Text + "')";
                SqlCommand cmd = new SqlCommand(ss, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ShowMeeting();
            }
            txt_title.Text = string.Empty;
            txt_datetime.Text = string.Empty;
            TextBox1.Text = string.Empty;
            Calendar1.SelectedDate = DateTime.Now;

        }
    }
}