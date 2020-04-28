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
    public partial class Arrange_Meeting : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["TutorID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Tutor] WHERE Tutor_Id='" + Session["TutorID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label5.Text = dt.Rows[0]["Tutor_Name"].ToString();
            }

            if (!IsPostBack)
            {
                Calendar1.SelectedDate = DateTime.Now;
                ShouwGroup();
                ShowMeeting();
                DropDownList1.SelectedValue = "0";
                DropDownList1.Items[0].Attributes.Add("disabled", "disabled");
                DropDownList3.SelectedValue = "0";
                DropDownList3.Items[0].Attributes.Add("disabled", "disabled");
                Calendar1.Visible = false;
                txt_title.Attributes.Add("maxlength", txt_title.MaxLength.ToString());
                txt_content.Attributes.Add("maxlength", txt_content.MaxLength.ToString());
            }

        }

        void ShowMeeting()
        {
            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT DISTINCT(a.Group_Id),a.Id,a.Title,a.Meeting_Type,a.Content,a.Meeting_Date,a.time,a.Last_Update_Date FROM [Meeting] a LEFT JOIN [Group] b ON a.Group_Id=b.Group_Id WHERE b.Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Label5.Text + "')", con);
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
       
        void ShouwGroup()
        {

            SqlConnection con = new SqlConnection(maincon);
            string com = "SELECT DISTINCT[GROUP_Id],Tutor_Id FROM [Group] WHERE Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Label5.Text + "')";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "Group_Id";
            DropDownList1.DataValueField = "Group_Id";
            DropDownList1.DataBind();

        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            DateTime start = Convert.ToDateTime("9:00:00 AM");
            DateTime end = Convert.ToDateTime("3:00:00 PM");
             DateTime t1 = Convert.ToDateTime(txt_time.Text);

           
          if ((t1 < start) || (t1 > end))
            {
                Response.Write("<script>alert('Meeting time is between 9:00 AM - 3:00 PM !');</script>");
            }
            else
            {

                if (DropDownList3.SelectedValue == "1")
                {

                    SqlConnection con = new SqlConnection(maincon);
                    // string ss = "INSERT INTO [Meeting] (Title,Content,Meeting_Date,time,Last_Update_Date,Group_Id,Meeting_Type) values" +
                    // "('" + txt_title.Text + "','" + txt_content.Text + "','" + Calendar1.SelectedDate.Year + "-" + Calendar1.SelectedDate.Month + "-" + Calendar1.SelectedDate.Day + " " + txt_calender.Text + "','" + txt_time.Text + "',GETDATE(),'" + DropDownList1.Text + "','" + DropDownList3.SelectedItem.Text + "')";
                    string ss = "INSERT INTO [Meeting] (Title,Content,Meeting_Date,time,Last_Update_Date,Group_Id,Meeting_Type) values" +
                     "('" + txt_title.Text + "','" + txt_content.Text + "','" + txt_calender.Text + "','" + txt_time.Text + "',GETDATE(),'" + DropDownList1.Text + "','" + DropDownList3.SelectedItem.Text + "')";
                     SqlCommand cmd = new SqlCommand(ss, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ShowMeeting();
                    Response.Redirect(Request.Url.ToString(), false);

                }
                else if (DropDownList3.SelectedValue == "2")
                {

                    SqlConnection con = new SqlConnection(maincon);
                    // string ss2 = "INSERT INTO [Meeting] (Title,Content,Meeting_Date,time,Last_Update_Date,Group_Id,Meeting_Type) values" +
                    //  "('" + txt_title.Text + "','" + txt_content.Text + "','" + Calendar1.SelectedDate.Year + "-" + Calendar1.SelectedDate.Month + "-" + Calendar1.SelectedDate.Day + " " + txt_calender.Text + "','" + txt_time.Text + "',GETDATE(),'" + DropDownList1.Text + "','" + DropDownList3.SelectedItem.Text + "')";
                    string ss2 = "INSERT INTO [Meeting] (Title,Content,Meeting_Date,time,Last_Update_Date,Group_Id,Meeting_Type) values" +
                     "('" + txt_title.Text + "','" + txt_content.Text + "','" + txt_calender.Text + "','" + txt_time.Text + "',GETDATE(),'" + DropDownList1.Text + "','" + DropDownList3.SelectedItem.Text + "')";
                    SqlCommand cmd2 = new SqlCommand(ss2, con);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    ShowMeeting();

                    Response.Redirect(Request.Url.ToString(), false);
                }

                txt_title.Text = string.Empty;
                txt_content.Text = string.Empty;
                txt_time.Text = string.Empty;
                Calendar1.SelectedDate = DateTime.Now;
                DropDownList1.SelectedValue = "0";
                DropDownList1.Items[0].Attributes.Add("disabled", "disabled");
                DropDownList3.SelectedValue = "0";
                DropDownList3.Items[0].Attributes.Add("disabled", "disabled");
            }

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {

            if (e.Day.Date.CompareTo(DateTime.Today) < 0)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.LightGray;
            }

            //if (e.Day.Date < DateTime.Now)
            //{

            //    e.Cell.BackColor = System.Drawing.Color.LightGray;

            //    e.Day.IsSelectable = false;

            //}

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txt_calender.Text = Calendar1.SelectedDate.ToString("D");
            Calendar1.Visible = false;
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;

            }
            else
            {
                Calendar1.Visible = true;
            }

            txt_calender.Text = "";
            txt_time.Text = "";
            txt_title.Text = "";
            txt_title.Text = "";
            txt_content.Text = "";
            

        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridView1.EditIndex = -1;
            ShowMeeting();

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            string query = "DELETE FROM [Meeting] WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()));
            cmd.ExecuteNonQuery();
            ShowMeeting();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;
            ShowMeeting();

        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);
            DateTime now = DateTime.Now;
            string Date = (GridView1.Rows[e.RowIndex].FindControl("txt_date") as TextBox).Text.Trim();
            string Time = (GridView1.Rows[e.RowIndex].FindControl("txt_time") as TextBox).Text.Trim();

            string title = (GridView1.Rows[e.RowIndex].FindControl("txt_title") as TextBox).Text;
            string content = (GridView1.Rows[e.RowIndex].FindControl("txt_content") as TextBox).Text;

            string type = (GridView1.Rows[e.RowIndex].FindControl("dpl_type") as DropDownList).Text;


            DateTime start = Convert.ToDateTime("9:00:00 AM");
            DateTime end = Convert.ToDateTime("3:00:00 PM");
            DateTime t1 = Convert.ToDateTime(Time);


            if ((t1 < start) || (t1 > end))
            {
                Response.Write("<script>alert('Meeting time is between 9:00 AM - 3:00 PM !');</script>");
            }

           else if (title == "" || content == "")
            {

                Response.Write("<script>alert('Please Fill in all the field !');</script>");

            }
            else
            {

                    con.Open();
                    string query = "UPDATE Meeting SET Title=@title,Group_Id=@group,Meeting_Type=@type, Content=@content, Meeting_Date='" + Date + "', time='" + Time + "', Last_Update_Date=GETDATE() WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@title", (GridView1.Rows[e.RowIndex].FindControl("txt_title") as TextBox).Text.Trim());
                    cmd.Parameters.AddWithValue("@content", (GridView1.Rows[e.RowIndex].FindControl("txt_content") as TextBox).Text.Trim());
                    cmd.Parameters.AddWithValue("@group", (GridView1.Rows[e.RowIndex].FindControl("DropDownList2") as DropDownList).Text.Trim());
                    cmd.Parameters.AddWithValue("@type", (GridView1.Rows[e.RowIndex].FindControl("dpl_type") as DropDownList).SelectedItem.ToString());
                    //cmd.Parameters.AddWithValue("@date", (GridView1.Rows[e.RowIndex].FindControl("txt_date") as TextBox).Text.Trim());
                    //cmd.Parameters.AddWithValue("@time", (GridView1.Rows[e.RowIndex].FindControl("txt_time") as TextBox).Text.Trim());
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Update Successfully !');</script>");
                    Server.Transfer("Arrange_Meeting.aspx");

            }
            

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                SqlConnection con = new SqlConnection(maincon);

                con.Open();
                var Group = (DropDownList)e.Row.FindControl("DropDownList2");

                string ss = "SELECT DISTINCT(a.GROUP_Id),a.Tutor_Id FROM [Group] a LEFT JOIN Meeting b ON a.Group_Id=b.Group_Id WHERE a.Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='"+Label5.Text+"')";
                SqlCommand cmd = new SqlCommand(ss, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Group.DataSource = dt;
                con.Close();
                Group.DataTextField = "Group_Id";
                Group.DataValueField = "Group_Id";
                Group.DataBind();
                Group.Items.Insert(0, new ListItem("Select Group"));
                Group.Items[0].Attributes.Add("disabled", "disabled");

            }

         }
    }
}