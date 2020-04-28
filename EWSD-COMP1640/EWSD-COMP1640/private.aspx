<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="private.aspx.cs" Inherits="EWSD_COMP1640._private" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta http-equiv="refresh" content="20;" />
    <meta charset="utf-8" />
    
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <style>
        div {
            resize: both;
        }

        .main {
            margin-left: 160px;
            font-size: 28px;
            padding: 0px 10px;
        }

        .content1 {
            margin: 0px auto;
        }

        .lv-body {
            font-size: 20px;
            margin: 0px auto;
        }

        .chat {
            font-size: 20px;
            width: 800px;
            height:500px;
            background: white;
            border:1px solid;
            margin:0 auto;
            align-content:center;
            overflow:auto;
        }
        .subchat{
            margin:0 auto;
            width: 800px;
            align-content:center;
        }
        
        .message {
            word-break: break-all;
            word-wrap: break-word;
            width:700px;
        }
    </style>
        <script>
        function ScrollToBottom() {
            var objDiv = document.getElementById("chat");

            if (objDiv.scrollHeight > objDiv.clientHeight) {
                objDiv.scrollTop = objDiv.scrollHeight - objDiv.clientHeight;
            }
        }
    </script>
     <script type="text/javascript"> 
         var idleInterval = setInterval("reloadPage()", 20000);
         function reloadPage() {
             location.reload();
         }
    </script>
</head>
<body>
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
                        <img alt="Logo" src="images/E-tutor system logo.png" height="150"/></a>
                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="Tutor_Home.aspx">Dashboard</a></li>
                        <li class="active"><a href="Tutor_Profile.aspx">Profile</a></li>
                        <li class="active"><a href="Learning_Material.aspx">Learning Material</a></li>
                        <li class="active"><a href="Student_Work2.aspx">Student Work</a></li>
                        <li class="active"><a href="Arrange_Meeting.aspx">Meeting</a></li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="tutorchatroom.aspx">Messages</a></li>
                        <li class="active"><a href="Tutor_Blog.aspx">Blog</a></li>
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

            <asp:Label ID="Label3" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <fieldset>
                <legend>Chat Room</legend>
            </fieldset>
        </div>

        <div class="chat" id="chat">
            <asp:Label ID="lbl_student" runat="server"></asp:Label>
            <br />

            <asp:DataList ID="DataList1" runat="server" CellPadding="4" DataKeyField="Id" ForeColor="#333333">
                <AlternatingItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <ItemStyle />
                <ItemTemplate>
                    Student_Id:
                    <asp:Label ID="Student_IdLabel" runat="server" Text='<%# Eval("Student_Id") %>' />
                    <br />
                    Tutor_Id:
                    <asp:Label ID="Tutor_IdLabel" runat="server" Text='<%# Eval("Tutor_Id") %>' />
                    <br />
                    mesg:
                    <asp:Label ID="mesgLabel" runat="server" Text='<%# Eval("mesg") %>' CssClass="message" />
                    <br />
                    <br />
                </ItemTemplate>
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataList>
            <asp:Label ID="lbltutor" runat="server" Visible="false"></asp:Label>
        </div>
                <div class="subchat">
            <asp:TextBox ID="txtmesg" runat="server"  Height="80px" Width="700px" MaxLength="500" OnTextChanged="TextBox1_TextChanged" AutoPostBack="True"></asp:TextBox>
            <asp:Button ID="btn_send" runat="server" OnClick="btn_send_Click" Text="Send" Width="128px" Enabled="False" />
        </div>
    </form>
</body>
</html>
