<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="privatechatstudent.aspx.cs" Inherits="EWSD_COMP1640.privatechatstudent" %>

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
        body {
            background-color: #f2f2f2;
        }

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
        #lbltutor{
            text-align:left;
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
      <a class="navbar-brand"><span/><img alt="Logo" src="images/E-tutor system logo.png" height="150" /></a>
    </div>
      <div class="collapse navbar-collapse" id="myNavbar">
          <ul class="nav navbar-nav">
              <li class="active"><a href="Student_Home.aspx">Dashboard</a> </li>
              <li class="active"><a href="Student_Profile.aspx">Profile</a></li>
              <li class="active"><a href="Student_Work.aspx">Exercises</a></li>
              <li class="active"><a style="background-color: mediumblue; color: white" href="privatechatstudent.aspx">Messages</a></li>
              <li class="active"><a href="Student_Blog.aspx">Blog</a></li>

          </ul>
          <ul class="nav navbar-nav navbar-right">
              <li><a href="Login.aspx"><span class="glyphicon glyphicon-user"></span>Log Out</a></li>
          </ul>
      </div>
  </div>
</div>
<br/>
	<br/>
	<br/>
        <div class="main">

            <asp:Label ID="Label3" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br/>

            <fieldset>
                <legend>Chat Room</legend>
            </fieldset>
        </div>
         
<div class="chatbody">
        <div class="chat" id="chat">
            <asp:Label ID="lbltutor" runat="server"></asp:Label>
            <br />
            <asp:DataList ID="DataList2" runat="server" CellPadding="4" DataKeyField="Id" ForeColor="#333333">
                <AlternatingItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <ItemStyle />
                <ItemTemplate>
                    Student Name:
                    <asp:Label ID="Student_IdLabel" runat="server" Text='<%# Eval("Student_Name") %>' />
                    <br />
                    Tutor Name:
                    <asp:Label ID="Tutor_IdLabel" runat="server" Text='<%# Eval("Tutor_Name") %>' />
                    <br />
                    mesg:
                    <asp:Label ID="mesgLabel" runat="server" Text='<%# Eval("mesg") %>' />
                    <br />
                    <br />
                </ItemTemplate>
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataList>
        </div>
        <div class="subchat">
            <asp:TextBox ID="txtmesg" runat="server" TextMode="MultiLine" MaxLength="500" Height="80px" Width="700px"  AutoPostBack="True"></asp:TextBox>
            <asp:Button ID="btnenter" runat="server" OnClick="btnenter_Click" Text="Send" Width="131px" />
        </div>
    </div>
        
    </form>
</body>
</html>
