<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Tutor.aspx.cs" Inherits="EWSD_COMP1640.Add_Tutor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <style>
        body {
            background-color: #f2f2f2;
        }

        .main {
            margin-left: 160px;
            font-size: 28px;
            padding: 0px 10px;
        }

        .container {
            width: 50%;
            background: white;
            margin: 0 auto;
            border: 1px solid;
            margin-top: 30px;
        }

        h2 {
            font-family: 'Bitter';
            text-align: center;
        }

        input[type=text], input[type=password] {
            width: 80%;
            height: 10%;
            margin-bottom: 2%;
            border: 0;
            border-bottom: 1px solid #4b4040;
        }

        input[placeholder] {
            font-size: 1.2vw;
            padding-left: 3%;
            font-family: 'Crete Round';
            margin-top: -3px;
        }

        section {
            width: 50%;
            height: 80%;
            margin: 0 auto;
            margin-top: 20px;
        }

        .textfield {
            margin-left: 40px;
        }

        .welcome {
            margin-left: 160px;
            font-size: 28px;
            padding: 0px 10px;
        }

        .main2 {
            margin-left: 160px;
            padding: 0px 10px;
            margin-top:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <div class="navbar navbar-default navbar-fixed-top" role="navigation">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand"><span />
                        <img alt="Logo" src="images/E-tutor system logo.png" height="150" /></a>
                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="Admin_Dashboard.aspx">Dashboard</a></li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Add_Tutor.aspx">Add Tutor</a></li>
                        <li class="active"><a href="Student.aspx">Assign Student</a></li>
                        <li class="active"><a href="Assigned_Student.aspx">Re-assigned Student</a></li>
                        <li class="active"><a href="Dashboard_Viewer.aspx">View Dashboard</a></li>
                        <li class="active"><a href="Show_Not_Assign_Student.aspx">Generate Report</a></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="Login.aspx"><span class="glyphicon glyphicon-user"></span>Log Out</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />

        <div class="main">
            <asp:Label ID="Label1" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label6" runat="server"></asp:Label>
            <br />
            <fieldset>
                <legend>Register Tutor/Staff</legend>
            </fieldset>
        </div>

        <div class="main2">
            <fieldset>
                <div class="container">
                    <br />
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textfield" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--Select Role--</asp:ListItem>
                        <asp:ListItem Value="1">Tutor</asp:ListItem>
                        <asp:ListItem Value="2">Staff</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:TextBox ID="txt_name" placeholder="Name" runat="server" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_name_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="lbl_name" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
                    <br />

                    <asp:TextBox ID="txt_email" placeholder="Email" runat="server" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_email_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="lbl_email" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
                    <br />

                    <asp:TextBox ID="txt_course" placeholder="Course" runat="server" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_course_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="lbl_course" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
                    <br />

                    <asp:TextBox ID="txt_password" placeholder="Password" runat="server" TextMode="Password" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_password_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="lbl_password" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
                    <br />

                    <asp:TextBox ID="txt_cpassword" placeholder="Confirm Password" runat="server" TextMode="Password" CssClass="textfield" ControlToCompare="txt_cpassword" CausesValidation="False" Enabled="True" OnTextChanged="txt_cpassword_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="lbl_cpassword" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
                    <br />

                    <div style="text-align: center;">
                        <asp:Button ID="btn_register" runat="server" CssClass="btn" Text="Register" Height="40px" BackColor="#12a312" ForeColor="White" BorderColor="#12A312" Width="160px" Font-Size="Large" OnClick="btn_register_Click" />
                        <br />
                        <asp:Label ID="lbl_msg" runat="server" Font-Size="Large"></asp:Label>
                    </div>

                </div>
            </fieldset>
        </div>

    </form>
</body>
</html>