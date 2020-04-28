<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Tutor_Dashboard.aspx.cs" Inherits="EWSD_COMP1640.View_Tutor_Dashboard" %>

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

        div {
            resize: both;
        }

        .fieldset {
            font-size: 25px;
        }

        .lv-body {
            font-size: 20px;
            margin: 0px auto;
        }

        .welcome {
            font-size: 25px;
        }

        .notice {
            resize: both;
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
                    <a class="navbar-brand"><span />
                        <img alt="Logo" src="images/E-tutor system logo.png" height="150" /></a>
                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="Admin_Dashboard.aspx">Dashboard</a></li>
                        <li class="active"><a href="Add_Tutor.aspx">Add Tutor</a></li>
                        <li class="active"><a href="Student.aspx">Assign Student</a></li>
                        <li class="active"><a href="Assigned_Student.aspx">Re-assigned Student</a></li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Dashboard_Viewer.aspx">View Dashboard</a></li>
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
                <asp:Label ID="Label2" runat="server" Text="Welcome to "></asp:Label>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Label ID="Label7" runat="server" Text=" Dashboard"></asp:Label>
            <br />
            <div class="fieldset">
                <fieldset>

                    <div class="lv-body" id="ms-scrollbar" style="overflow-x: hidden; height: 300px; width: 800px; margin-top: 10px; background-color: grey;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DataList ID="DataList3" runat="server" CssClass="notice">

                                    <ItemTemplate>

                                        <div class="notice" id="notice">
                                            <asp:Label ID="Label3" runat="server" Text="Notice" Font-Bold="True" ForeColor="White" Font-Size="Large" CssClass="content"></asp:Label>
                                            <br />
                                            <table class="auto-style1" style="width: 800px; height: 10px; background: white;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="You have arrange a meeting with "></asp:Label>
                                                        <asp:Label ID="Label5" runat="server" Font-Size="Medium" Text='<%# Bind("Group_Id") %>'></asp:Label>
                                                        <asp:Label ID="Label6" runat="server" Text="On"></asp:Label>
                                                        <asp:Label ID="Message" runat="server" Font-Size="Medium" Text='<%# Bind("Meeting_Date") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <legend>Latest Updates</legend>
                    <br />
                    <br />

                    <fieldset>
                        <legend><strong>Student List</strong></legend>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">

                            <Columns>

                                <asp:TemplateField HeaderText="Group">
                                    <ItemStyle Width="200" Height="30" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Group_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student Name">
                                    <ItemStyle Width="200" Height="30" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Student_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student ID">
                                    <ItemStyle Width="200" Height="30" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Id" runat="server" Text='<%# Eval("Student_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email">
                                    <ItemStyle Width="300" Height="30" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Email_Address") %>' ID="lbl_email"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course">
                                    <ItemStyle Width="200" Height="30" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Course") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>
                    </fieldset>

                </fieldset>
            </div>
        </div>

    </form>
</body>
</html>
