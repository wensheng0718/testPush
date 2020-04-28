<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_Home.aspx.cs" Inherits="EWSD_COMP1640.Student_Home" %>

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

        div {
            resize: both;
        }

        .sidenav {
            height: 100%;
            width: 160px;
            position: fixed;
            z-index: 1;
            top: 0;
            left: 0;
            background-color: #111;
            padding-top: 20px;
        }

            .sidenav a {
                padding: 6px 8px 6px 16px;
                text-decoration: none;
                font-size: 28px;
                color: #FFFFFF;
                display: block;
            }

                .sidenav a:hover {
                    color: #F0FF00;
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

        .content {
            height: 3px;
        }

        .main2 {
            margin-left: 160px;
            padding: 0px 10px;
        }
                .u_img2 {
            Height: 50px;
            Width: 50px;
            border-radius: 50%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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
              <li class="active"><a style="background-color: mediumblue; color: white" href="Student_Home.aspx">Dashboard</a> </li>
              <li class="active"><a href="Student_Profile.aspx">Profile</a></li>
              <li class="active"><a href="Student_Work.aspx">Exercises</a></li>
              <li class="active"><a href="privatechatstudent.aspx">Messages</a></li>
              <li class="active"><a href="Student_Blog.aspx">Blog</a></li>
              <li class="active">
                  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/notification.png" Visible="false" CssClass="u_img2" OnClick="ImageButton1_Click" />
                  <asp:Label ID="Label2" runat="server" visble="false" ForeColor="Blue" Font-Underline="true"></asp:Label>
              </li>

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
            <br />
            <fieldset>
                <legend>Announcement</legend>
                <div class="lv-body" id="ms-scrollbar" style="overflow-x: hidden; height: 300px; width: 800px; margin-top: 5px; background-color: grey;">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DataList ID="DataList3" runat="server" CssClass="notice">

                                <ItemTemplate>

                                    <div class="notice" id="notice">
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Title") %>' Font-Bold="True" ForeColor="White" Font-Size="Large" CssClass="content"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label4" runat="server" ForeColor="White" Text=" Update on : " CssClass="content"></asp:Label>
                                        <asp:Label ID="Date" runat="server" ForeColor="White" Text='<%# Bind("Last_Update_Date") %>'></asp:Label>
                                        <br />
                                        <table class="auto-style1" style="width: 798px; height: 100px; background: white; text-align: center;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Your tutor has arrange a meeting on "></asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Font-Size="Medium" Text='<%# Bind("Meeting_Date") %>'></asp:Label>
                                            </tr>
                                            <tr class="content">
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Font-Size="Medium" Text='<%# Bind("Content") %>'></asp:Label>
                                            </tr>
                                        </table>
                                        </img>
                    <br />
                                    </div>

                                </ItemTemplate>
                            </asp:DataList>



                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
            </div>
        <div class="main2">
            <br />
            <fieldset>
                <legend>Notes</legend>
                <div style="overflow:auto">
                <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" runat="server" ShowHeader="False"
                    BorderStyle="None" DataKeyNames="filename">

                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="800px" Height="30" BorderColor="white" />
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text='<%# Eval("filename") %>' ForeColor="Black"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
            </fieldset>

            <fieldset>

                <legend>Exercises</legend>
                <div style="overflow:auto">
                    <asp:GridView ID="GridView2" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" runat="server" ShowHeader="False"
                        BorderStyle="None" DataKeyNames="filename">

                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="800px" Height="30" BorderColor="white" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Text='<%# Eval("filename") %>' ForeColor="Black"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </fieldset>



        </div>
    </form>
</body>
</html>
