<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EWSD_COMP1640.Register" %>


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
            margin: 0;
            font-family: Arial;
            background-color: #f2f2f2;
        }

        .container {
            width: 600px;
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
            width: 500px;
            height: 50px;
            border: 0;
            border-bottom: 1px solid #4b4040;
        }

        input[placeholder] {
            font-size: 1.2vw;
            font-family: 'Crete Round';
        }

        section {
            width: 600px;
            height: 80%;
            overflow:auto;
        }

        .textfield {
            margin-left: 40px;
        }

        div {
            resize: both;
        }
    </style>
</head>
<body class="has-header">
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
      <a class="navbar-brand"><span/><img alt="Logo" src="images/E-tutor system logo.png" height="150" /></a>
    </div>
      <div class="collapse navbar-collapse" id="myNavbar">
          <ul class="nav navbar-nav">
                            <li class="active"><a  href="Login.aspx">Login</a></li>
                            <li class="active"><a href="Register.aspx" style="background-color: mediumblue; color: white">Signup</a></li>

          </ul>
          <ul class="nav navbar-nav navbar-right">

          </ul>
      </div>
  </div>
</div>
        <br/><br/>
        <h2>Register</h2>
     <h2>
                <asp:Label ID="lbl_msg" runat="server" Font-Size="Large"></asp:Label>
            </h2>

        <div class="container">
            <asp:Label ID="lbldate" runat="server" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txt_fname" placeholder="First Name" runat="server" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_fname_TextChanged"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_fname" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
            <br />

            <asp:TextBox ID="txt_lname" placeholder="Last Name" runat="server" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_lname_TextChanged"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_lname" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
            <br />

            <asp:TextBox ID="txt_email" placeholder="Email" runat="server" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_email_TextChanged"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_email" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
            <br />

            <asp:TextBox ID="txt_course" placeholder="Course" runat="server" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_course_TextChanged"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_course" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
            <br />

            <div class="textfield">
                <asp:TextBox ID="txt_dob" placeholder="Date of Birth" runat="server" AutoPostBack="True" OnTextChanged="txt_dob_TextChanged"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png" ImageAlign="AbsBottom" Width="30px" Height="30px" OnClick="ImageButton1_Click" />
                <asp:Calendar ID="Calendar1" runat="server" Height="253px" Width="361px" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
            </div>
            <asp:Label ID="lbl_dob" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
            <br />

            <asp:TextBox ID="txt_password" placeholder="Password" runat="server" TextMode="Password" CssClass="textfield" AutoPostBack="True" OnTextChanged="txt_password_TextChanged"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_password" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
            <br />

            <asp:TextBox ID="txt_cpassword" placeholder="Confirm Password" runat="server" TextMode="Password" CssClass="textfield" ControlToCompare="txt_cpassword" CausesValidation="False" Enabled="True"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_cpassword" runat="server" CssClass="textfield" ForeColor="Red"></asp:Label>
            <br />

            <div style="text-align: center;">
                <asp:Button ID="btn_register" runat="server" CssClass="btn" Text="Register" Height="40px" BackColor="#12a312" ForeColor="White" BorderColor="#12A312" Width="160px" Font-Size="Large" OnClick="btn_register_Click" />
                <br />
            </div>
        </div>


    </form>
</body>
</html>
