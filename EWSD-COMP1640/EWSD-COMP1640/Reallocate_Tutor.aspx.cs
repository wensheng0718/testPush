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
using System.Net.Mail;

namespace EWSD_COMP1640
{
    public partial class Reallocate_Tutor : System.Web.UI.Page
    {

        string maincon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["StaffID"] != null)
            {

                SqlConnection conn = new SqlConnection(maincon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Staff] WHERE Staff_Id='" + Session["StaffID"].ToString() + "' ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Label6.Text = dt.Rows[0]["Staff_Name"].ToString();
            }

            if (!IsPostBack)
            {

                ShowTutor();

            }
            
        }

        void ShowTutor()
        {
            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT a.Id,b.Group_Id,a.Tutor_Name,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE b.Group_Id !='"+ Session["GroupId"] .ToString()+ "' GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id,b.Group_Id", con);
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
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                GridView1.Rows[0].Cells[0].Text = "No Data Yet";
                GridView1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                GridView1.Width = 1030;

            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);


            if (e.CommandName == "assign")
            {


                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = GridView1.Rows[rowIndex];

                string GroupId = (row.FindControl("Group_Id") as Label).Text;
                string Tutor_Name = (row.FindControl("Tutor_Name") as Label).Text;

                bool exists = false;
                con.Open();
                string checkEmail = "select count(*) from [Group] WHERE Student_Id = '" + Session["StudentId"].ToString() + "'";
                SqlCommand cmdEmail = new SqlCommand(checkEmail, con);
                exists = (int)cmdEmail.ExecuteScalar() > 0;
                con.Close();

                if(exists)
                {

                    con.Open();
                    string query = "UPDATE [Group] SET Tutor_Id=(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Tutor_Name.ToString() + "'), Group_Id='" + GroupId.ToString() + "',Student_Id='" + Session["StudentId"].ToString() + "' WHERE Student_Id ='" + Session["StudentId"].ToString() + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string assign = "UPDATE [Student] SET status='Assign' WHERE Student_Id ='" + Session["StudentId"].ToString() + "'";
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = assign;
                    cmd1.Connection = con;
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    ShowTutor();

                }
                else
                {

                    string updatedata = "INSERT INTO [Group] (Group_Id,Student_Id,Student_Email,Tutor_Id) values" + "('" + GroupId.ToString() + "','" + Session["StudentId"].ToString() + "','" + Session["Email_Address"].ToString() + "',(SELECT Tutor_Id FROM [Tutor] WHERE Tutor_Name='" + Tutor_Name.ToString() + "'))";
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = updatedata;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string assign = "UPDATE [Student] SET status='Assign' WHERE Student_Id ='" + Session["StudentId"].ToString() + "'";
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = assign;
                    cmd2.Connection = con;
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    ShowTutor();

                }

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                smtp.EnableSsl = true;
                MailMessage mm = new MailMessage();
                mm.Subject = "Reassign Tutor";
                string body = " Hello your Tutor is change to:" + Tutor_Name;
                body += "\n\n Thank You";
                mm.Body = body;
                string toaddres = Session["Email_Address"].ToString(); 
                mm.To.Add(toaddres);
                string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                mm.From = new MailAddress(fromaddress);
                smtp.Send(mm);


                con.Open();
                string ss4 = "SELECT a.Email_Address FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE b.Group_Id='" + GroupId.ToString() + "' ";
                SqlCommand cmd4 = new SqlCommand(ss4, con);
                string email;
                SqlDataReader reader = cmd4.ExecuteReader();

                while (reader.Read())
                {
                    email = reader[0].ToString();

                    SmtpClient smtp2 = new SmtpClient();
                    smtp2.Host = "smtp.gmail.com";
                    smtp2.Port = 587;
                    smtp2.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                    smtp2.EnableSsl = true;
                    MailMessage mm2 = new MailMessage();
                    mm2.Subject = "Assign Student";
                    string body2 = "Hello you have new student join in "+ GroupId.ToString();
                    body2 += "\n" + Session["Student_Name"].ToString();
                    body2 += "\n\n Thank You";
                    mm2.Body = body2;
                    string toaddres2 = email;
                    mm2.To.Add(toaddres2);
                    string fromaddress2 = "E-Tutoring System <e_tutoring@gmail.com>";
                    mm2.From = new MailAddress(fromaddress2);
                    smtp2.Send(mm2);
                }
                reader.Close();
                con.Close();
            }

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (txtsearch.Text == "")
            {


            }
            else if (txtsearch.Text != "")
            {

                con.Open();
                SqlCommand sqlc = new SqlCommand();
                string query = "SELECT DISTINCT b.Tutor_Name,b.Course,a.Group_Id,COUNT(a.Student_Id) AS Total_Student FROM [Group] a LEFT JOIN [Tutor] b ON a.Tutor_Id=b.Tutor_Id WHERE b.Course like '%" + txtsearch.Text + "%' AND a.Group_Id !='" + Session["GroupId"].ToString() + "' GROUP BY b.Tutor_Name,b.Course,a.Group_Id";
                sqlc.CommandText = query;
                sqlc.Connection = con;
                //sqlc.Parameters.AddWithValue("Tutor_Name", txt_search.Text);
                DataTable dtbl = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlc);
                sqlDa.Fill(dtbl);
                GridView1.DataSource = dtbl;
                GridView1.DataBind();


            }

        }
    }
}