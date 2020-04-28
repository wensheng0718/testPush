<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Not_Assign_Student_Report.aspx.cs" Inherits="EWSD_COMP1640.Not_Assign_Student_Report" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

        <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <style type="text/css">
        div {
            resize: both;
        }

        .auto-style2 {
            font-size: x-large;
        }

        .auto-style3 {
            text-align: center;
            margin: 0px auto;
            margin-top: 100px;
            align-content: center;
        }

        .auto-style4 {
            font-size: xx-large;
        }

        span {
            font-weight: bold;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server">

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
                    <ul class="nav navbar-nav navbar-right">
                        <li>
                            <asp:Button ID="btn_back" runat="server" Text="Back" OnClick="btn_back_Click" BorderStyle="None" Font-Bold="True" Font-Size="Medium" CssClass="back" BackColor="transparent"></asp:Button></li>

                    </ul>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="auto-style3" style="height: 429px; width: 1100px">
            <span class="auto-style2">
            <br />
            <br />
            </span><span class="auto-style4">STUDENT</span><br class="auto-style4" />
            <br class="auto-style4" />
            <asp:Label ID="lblstudent" runat="server" CssClass="auto-style4"></asp:Label>
            <br class="auto-style4" />
            <br class="auto-style4" /> <span class="auto-style4">&nbsp;At&nbsp; moment you are not assigned to any tutor and pls hold on as we are doing our best to assign you to the tutor for your course </span>
            <br class="auto-style4" />
            <asp:Label ID="Label1" class="auto-style4" runat="server" Text="THANK YOU !!!!"></asp:Label>
    </div>
    </form>
</body>
</html>
