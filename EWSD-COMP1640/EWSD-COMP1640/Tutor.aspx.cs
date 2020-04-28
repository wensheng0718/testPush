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
    public partial class Tutor : System.Web.UI.Page
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

            if (Session["GroupID"] != null)
            {
                Label1.Text = Session["GroupID"].ToString();
            }

            if (!IsPostBack)
            {
                ShowTutor();
                DropDownList1.SelectedValue = "0";
                DropDownList1.Items[0].Attributes.Add("disabled", "disabled");
            }

            if(IsPostBack)
            {
                DropDownList1.Items[0].Attributes.Add("disabled", "disabled");
            }

        }

        void ShowTutor()
        {
            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id", con);
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

                //var Tutor_Id = e.CommandArgument;

                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

               
                GridViewRow row = GridView1.Rows[rowIndex];


              
                string idtuto = (row.FindControl("lbl_Id") as Label).Text;
                string Tutor_Name = (row.FindControl("Tutor_Name")as Label).Text;
             

              Label Email_Address = (Label)row.FindControl("lbl_email");
                //Label Tutor_Name = (Label)row.FindControl("Tutor_Name");
              //  string lbl_Id = ((Label)row.FindControl("Tutor_Id")).Text;
              //  string Tutor_Name = ((Label)row.FindControl("Tutor_Name")).Text;

                    con.Open();
                    string query = "UPDATE [Group] SET Tutor_Id='" + idtuto.ToString() + "' WHERE Group_Id ='" + Session["GroupID"].ToString() + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    string ss = "SELECT STUFF((SELECT ',' + b.Student_Email FROM [Group] b WHERE b.Group_Id = '" + Session["GroupID"].ToString() + "' FOR XML PATH('')), 1, 1, '') FROM [Group] a WHERE a.Group_Id = '" + Session["GroupID"].ToString() + "' GROUP BY a.Group_Id";
                    SqlCommand cmd3 = new SqlCommand(ss, con);
                    string email;
                    SqlDataReader reader = cmd3.ExecuteReader();
                     
                    while (reader.Read())
                    {
                        email = reader[0].ToString();

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                        smtp.EnableSsl = true;
                        MailMessage mm = new MailMessage();
                        mm.Subject = "Assign Tutor";
                        string body = " Hello your Tutor is:" + Tutor_Name;
                        body += "\n\n Thank You";
                        mm.Body = body;
                        string toaddres = email;
                        mm.To.Add(toaddres);
                        string fromaddress = "E-Tutoring System <e_tutoring@gmail.com>";
                        mm.From = new MailAddress(fromaddress);
                        smtp.Send(mm);

                    }
                    reader.Close();
                    con.Close();

                    con.Open();
                    string ss4 = "SELECT STUFF((SELECT ',' + CONCAT(a.Student_Fname ,' ', a.Student_Lname) FROM [Student] a LEFT JOIN [Group] b ON a.Student_Id=b.Student_Id WHERE b.Group_Id = '"+Label1.Text+"' FOR XML PATH('')), 1, 1, '') ";
                    SqlCommand cmd4 = new SqlCommand(ss4, con);
                    string studentList;
                    SqlDataReader reader2 = cmd4.ExecuteReader();
                    while (reader2.Read())
                    {
                        studentList = reader2[0].ToString();

                        SmtpClient smtp2 = new SmtpClient();
                        smtp2.Host = "smtp.gmail.com";
                        smtp2.Port = 587;
                        smtp2.Credentials = new System.Net.NetworkCredential("testingemailabc557@gmail.com", "testing123!");
                        smtp2.EnableSsl = true;
                        MailMessage mm2 = new MailMessage();
                        mm2.Subject = "Assign Student";
                        string body2 = " Hello the list below is yout student";
                        body2 += "\n" + studentList;
                        body2 += "\n\n Thank You";
                        mm2.Body = body2;
                        string toaddres2 = Email_Address.Text;
                        mm2.To.Add(toaddres2);
                        string fromaddress2 = "E-Tutoring System <e_tutoring@gmail.com>";
                        mm2.From = new MailAddress(fromaddress2);
                        smtp2.Send(mm2);
                    }
                    reader2.Close();
                    con.Close();
                }

            ShowTutor();
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (DropDownList1.SelectedValue == "0" && txtsearch.Text != "")
            {

                con.Open();
                SqlCommand sqlc = new SqlCommand();
                string query = "Select a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE a.Tutor_Id like '%" + txtsearch.Text + "%' GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id";

                //string query = "Select * from [Student] where Concat (Concat(Student_Fname ,' ', Student_Lname )AS Student_Name,Student_Id,Email_Address,Course) like '%" + txtsearch.Text + "%'";
                sqlc.CommandText = query;
                sqlc.Connection = con;
                DataTable dtbl = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlc);
                sqlDa.Fill(dtbl);
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
                con.Close();

            }
            else if (DropDownList1.SelectedValue == "1" && txtsearch.Text == "")
            {
                con.Open();
                SqlCommand sqlc = new SqlCommand();
                string query = "SELECT a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id";
                sqlc.CommandText = query;
                sqlc.Connection = con;
                //sqlc.Parameters.AddWithValue("Tutor_Name", txt_search.Text);
                DataTable dtbl = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlc);
                sqlDa.Fill(dtbl);
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
                txtsearch.Text = string.Empty;
            }
            else if (DropDownList1.SelectedValue == "1" && txtsearch.Text != "")
            {

                con.Open();
                SqlCommand sqlc = new SqlCommand();
                string query = "Select a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE a.Tutor_Id like '%" + txtsearch.Text + "%' GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id";

                //string query = "Select * from [Student] where Concat (Concat(Student_Fname ,' ', Student_Lname )AS Student_Name,Student_Id,Email_Address,Course) like '%" + txtsearch.Text + "%'";
                sqlc.CommandText = query;
                sqlc.Connection = con;
                DataTable dtbl = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlc);
                sqlDa.Fill(dtbl);
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
                con.Close();

            }
            else 
            {

                con.Open();
                SqlCommand sqlc = new SqlCommand();
                string query = "Select a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE a.Tutor_Id like '%" + txtsearch.Text + "%' AND a.Course='" + DropDownList1.SelectedItem.Text + "' GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id";
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

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);

            if (RadioButtonList1.SelectedValue=="0")
            {
                 if(DropDownList1.SelectedValue == "0" || DropDownList1.SelectedValue == "1")
                {

                    con.Open();
                    SqlCommand sqlc2 = new SqlCommand();
                    string query2 = "SELECT a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id ORDER BY Tutor_Name ASC";
                    sqlc2.CommandText = query2;
                    sqlc2.Connection = con;
                    //sqlc.Parameters.AddWithValue("Tutor_Name", txt_search.Text);
                    DataTable dtbl2 = new DataTable();
                    SqlDataAdapter sqlDa2 = new SqlDataAdapter(sqlc2);
                    sqlDa2.Fill(dtbl2);
                    GridView1.DataSource = dtbl2;
                    GridView1.DataBind();
                    con.Close();

                }
                  else if (DropDownList1.SelectedValue !="0" )
                {
                    con.Open();
                    SqlCommand sqlc = new SqlCommand();
                    string query = "SELECT a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE a.Tutor_Id like '%" + txtsearch.Text + "%' AND a.Course='" + DropDownList1.SelectedItem.Text + "' GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id ORDER BY Tutor_Name ASC";
                    sqlc.CommandText = query;
                    sqlc.Connection = con;
                    //sqlc.Parameters.AddWithValue("Tutor_Name", txt_search.Text);
                    DataTable dtbl = new DataTable();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlc);
                    sqlDa.Fill(dtbl);
                    GridView1.DataSource = dtbl;
                    GridView1.DataBind();
                    con.Close();
                }

            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                if (DropDownList1.SelectedValue == "0" || DropDownList1.SelectedValue == "1")
                {

                    con.Open();
                    SqlCommand sqlc2 = new SqlCommand();
                    string query2 = "SELECT a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) as Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id ORDER BY Course ASC";
                    sqlc2.CommandText = query2;
                    sqlc2.Connection = con;
                    //sqlc.Parameters.AddWithValue("Tutor_Name", txt_search.Text);
                    DataTable dtbl2 = new DataTable();
                    SqlDataAdapter sqlDa2 = new SqlDataAdapter(sqlc2);
                    sqlDa2.Fill(dtbl2);
                    GridView1.DataSource = dtbl2;
                    GridView1.DataBind();
                    con.Close();

                }
                else if (DropDownList1.SelectedValue != "")
                {
                    con.Open();
                    SqlCommand sqlc = new SqlCommand();
                    string query = "SELECT a.Id,a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,COUNT(b.Student_Id) AS Total_Student FROM [Tutor] a LEFT JOIN [Group] b ON a.Tutor_Id=b.Tutor_Id WHERE a.Tutor_Id like '%" + txtsearch.Text + "%' AND a.Course='" + DropDownList1.SelectedItem.Text + "' GROUP BY a.Tutor_Id,a.Tutor_Name,a.Email_address,a.Course,a.Id ORDER BY Course ASC";
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
}