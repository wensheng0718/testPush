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
using System.Web.Services;

namespace EWSD_COMP1640
{
    public partial class Home : System.Web.UI.Page
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
                ShowStudent();
                AutogenerateID();
                DropDownList1.SelectedValue = "0";
                DropDownList1.Items[0].Attributes.Add("disabled", "disabled");
            }

            if (IsPostBack)
            {
                DropDownList1.Items[0].Attributes.Add("disabled", "disabled");
            }

        }

        void ShowStudent()
        {
            DataTable dtbl = new DataTable();
            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT CONCAT(student_Fname ,' ', Student_Lname) AS Student_Name,Email_Address,Course,Student_Id FROM [Student] WHERE Student_Id NOT IN (SELECT Student_Id FROM [Group]) ", con);
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

        protected void Btn_assign_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(maincon);

            //for (int i = 0; i < GridView1.Rows.Count; i++)//loop the GridView Rows
            //{
            //    CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
            //    Label Student_Id = (Label)GridView1.Rows[i].Cells[0].FindControl("lbl_Id");
            //    Label Email_Address = (Label)GridView1.Rows[i].Cells[0].FindControl("lbl_email");//find the CheckBox

            //    if (cb != null)
            //    {
            //        if (cb.Checked)
            //        {
            //            string updatedata = "INSERT INTO [Group] (Group_Id,Student_Id,Student_Email) values" + "('" + Label1.Text + "','" + Student_Id.Text.ToString() + "','" + Email_Address.Text.ToString() + "')";
            //            con.Open();
            //            SqlCommand cmd = new SqlCommand();
            //            cmd.CommandText = updatedata;
            //            cmd.Connection = con;
            //            cmd.ExecuteNonQuery();
            //            con.Close();

            //            string assign = "UPDATE [Student] SET status='assign' WHERE Student_Id ='" + Student_Id.Text.ToString() + "'";
            //            con.Open();
            //            SqlCommand cmd1 = new SqlCommand();
            //            cmd1.CommandText = assign;
            //            cmd1.Connection = con;
            //            cmd1.ExecuteNonQuery();
            //            con.Close();

            //        }
            //    }

            //}
            //Session["GroupID"] = Label1.Text;
            //Server.Transfer("Tutor.aspx");

            foreach(GridViewRow grow in GridView1.Rows)
            {
                var checkboxselect = grow.FindControl("CheckBox1") as CheckBox;

                if (checkboxselect.Checked == false)
                {

                    Label3.Text = "Select Setudent";
                    Label3.ForeColor = System.Drawing.Color.Red;

                }
                else if (checkboxselect.Checked == true)
                {

                    string updatedata = "INSERT INTO [Group] (Group_Id,Student_Id,Student_Email) values" + "(@Group_Id,@Student_Id,@Student_Email)";
                    SqlCommand cmd = new SqlCommand(updatedata,con);
                    cmd.Parameters.AddWithValue("@Group_Id",Label1.Text);
                    cmd.Parameters.AddWithValue("@Student_Id", (grow.FindControl("lbl_Id") as Label).Text);
                    cmd.Parameters.AddWithValue("@Student_Email", (grow.FindControl("lbl_email") as Label).Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string assign = "UPDATE [Student] SET status='assign' WHERE Student_Id =@Student_Id";
                    SqlCommand cmd1 = new SqlCommand(assign, con);
                    cmd1.Parameters.AddWithValue("@Student_Id", (grow.FindControl("lbl_Id") as Label).Text);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }

            }

            bool exists;
            con.Open();
            string checkGroup = "select count (*) from [Group] where Group_Id = '" + Label1.Text + "'";
            SqlCommand cmdGroup = new SqlCommand(checkGroup, con);
            exists = (int)cmdGroup.ExecuteScalar() > 0;
            con.Close();

            if (exists)
            {

                Session["GroupID"] = Label1.Text;
                Response.Redirect("Tutor.aspx");

            }

        }

        public void AutogenerateID()
        {

            SqlConnection con = new SqlConnection(maincon);
            con.Open();
            string ss = "SELECT count(Group_Id) FROM [Group]";
            SqlCommand cmd = new SqlCommand(ss, con);
            int a = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            a++;
            Label1.Text = "Group" + a.ToString();

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (DropDownList1.SelectedValue == "0" && txtsearch.Text != "")
            {

                con.Open();
                SqlCommand sqlc = new SqlCommand();
                string query = "SELECT CONCAT(Student_Fname ,' ', Student_Lname) AS Student_Name,Email_Address,Course,Student_Id FROM [Student] WHERE Student_Id NOT IN (SELECT Student_Id FROM [Group]) AND Student_Id like '%" + txtsearch.Text + "%'";

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
            else if (DropDownList1.SelectedValue == "1" && txtsearch.Text =="")
            {

                con.Open();
                SqlCommand sqlc = new SqlCommand();
                string query = "SELECT CONCAT(Student_Fname ,' ', Student_Lname) AS Student_Name,Email_Address,Course,Student_Id FROM [Student] WHERE Student_Id NOT IN (SELECT Student_Id FROM [Group])";

                //string query = "Select * from [Student] where Concat (Concat(Student_Fname ,' ', Student_Lname )AS Student_Name,Student_Id,Email_Address,Course) like '%" + txtsearch.Text + "%'";
                sqlc.CommandText = query;
                sqlc.Connection = con;
                DataTable dtbl = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlc);
                sqlDa.Fill(dtbl);
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
                txtsearch.Text = string.Empty;
                con.Close();

            }
            else if(DropDownList1.SelectedValue == "1" && txtsearch.Text != "")
            {

                con.Open();
                SqlCommand sqlc = new SqlCommand();
                string query = "SELECT CONCAT(Student_Fname ,' ', Student_Lname) AS Student_Name,Email_Address,Course,Student_Id FROM [Student] WHERE Student_Id NOT IN (SELECT Student_Id FROM [Group]) AND Student_Id like '%" + txtsearch.Text + "%'";

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
                string query = "Select CONCAT(Student_Fname ,' ', Student_Lname) AS Student_Name,Student_Id,Email_address,Course from [Student]  WHERE CONCAT(Student_Id,Course,Email_Address) like '%" + txtsearch.Text + "%' AND Course='" + DropDownList1.SelectedItem.Text + "' AND Student_Id NOT IN (SELECT Student_Id FROM [Group])";

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

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(maincon);

            if (RadioButtonList1.SelectedValue == "0")
            {
                if (DropDownList1.SelectedValue == "0" || DropDownList1.SelectedValue == "1")
                {

                    con.Open();
                    SqlCommand sqlc2 = new SqlCommand();
                    string query2 = "Select CONCAT(Student_Fname ,' ', Student_Lname) AS Student_Name,Student_Id,Email_address,Course FROM Student WHERE Student_Id NOT IN (SELECT Student_Id FROM [Group]) ORDER BY Student_Name ASC";
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
                else if (DropDownList1.SelectedValue != "0")
                {
                    con.Open();
                    SqlCommand sqlc = new SqlCommand();
                    string query = "Select CONCAT(Student_Fname ,' ', Student_Lname) AS Student_Name,Student_Id,Email_address,Course FROM Student WHERE Student_Id like '%" + txtsearch.Text + "%' AND Course='" + DropDownList1.SelectedItem.Text + "' AND Student_Id NOT IN (SELECT Student_Id FROM [Group]) ORDER BY Student_Name ASC";
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
                    string query2 = "Select CONCAT(Student_Fname ,' ', Student_Lname) AS Student_Name,Student_Id,Email_address,Course FROM [Student] WHERE Student_Id NOT IN (SELECT Student_Id FROM [Group]) ORDER BY Course ASC";
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
                    string query = "Select CONCAT(Student_Fname ,' ', Student_Lname) AS Student_Name,Student_Id,Email_address,Course FROM [Student] WHERE Student_Id like '%" + txtsearch.Text + "%' AND Course='" + DropDownList1.SelectedItem.Text + "' AND Student_Id NOT IN (SELECT Student_Id FROM [Group]) ORDER BY Course ASC";
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