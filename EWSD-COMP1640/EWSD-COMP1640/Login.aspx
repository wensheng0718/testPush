<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EWSD_COMP1640.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <style>
        body {
            margin: 0;
            font-family: Arial;
            background-color: #f2f2f2;
        }

        .container {
            width: 500px;
            background: white;
            border: 1px solid;
            margin: 0 auto;
            margin-top: 50px;
            align-content: center;
            text-align: center;
        }

        .sub_container {
            text-align: center;
        }

        h2 {
            font-family: 'Bitter';
            text-align: center;
            margin-top: 80px;
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

        .btn_reset {
            border: none;
            background: none;
        }

        div {
            resize: both;
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
                            <li class="active"><a style="background-color: mediumblue; color: white" href="Login.aspx">Login</a></li>
                            <li class="active"><a href="Register.aspx">Signup</a></li>
                        </ul>
                    </div>
                </div>
        </div>
            <br/><br/>
                <h2>Login</h2>

                <div class="container">

                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txt_id" placeholder="ID" runat="server" CssClass="textfield"></asp:TextBox>
                    <br />
                    <br />

                    <asp:TextBox ID="txt_password" placeholder="Password" runat="server" TextMode="Password" CssClass="textfield"></asp:TextBox>
                    <br />
                    <br />

                    <br />
                    <div class="sub_container">
                        <asp:Button ID="btn_register" runat="server" CssClass="btn_register" Text="Log In" Height="40px" BackColor="#12a312" ForeColor="White" BorderColor="#12A312" Width="160px" Font-Size="Large" OnClick="btn_register_Click" />
                        <br />
                        <asp:Button ID="btn_reset" runat="server" CssClass="btn_reset" Text="Forgot Password?" Height="40px" ForeColor="gray" Width="160px" Font-Size="medium" OnClick="btn_reset_Click" />

                        <br />
                        <asp:Label ID="lbl_msg" runat="server" Font-Size="Large"></asp:Label>
                    </div>
                </div>
    </form>
</body>
</html>
