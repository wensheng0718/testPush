<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard_Viewer.aspx.cs" Inherits="EWSD_COMP1640.Dashboard_Viewer" %>

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

        .main2 {
            margin-left: 100px;
            padding: 0px 10px;
            margin-top: 10px;
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

        .welcome {
            margin-left: 160px;
            font-size: 28px;
            padding: 0px 10px;
        }

        .display{
            width:auto;
        }
    </style>

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
            <asp:Label ID="Label6" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <fieldset>
                <legend>View Dashboard</legend>
            </fieldset>
        </div>
        <div class="main2">
            <fieldset>
                <legend>Student</legend>
                <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" runat="server"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeys="Student_Id"
                    DataKeyNames="Student_Id" CssClass="display" OnRowCommand="GridView1_RowCommand">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                    <Columns>
                        <asp:TemplateField HeaderText="Student ID">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label ID="lbl_Id" runat="server" Text='<%# Eval("Student_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Student Name">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Student_Name") %>' ID="lbl_name"></asp:Label>
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

                        <asp:TemplateField HeaderText="Option">
                            <ItemStyle Width="100" Height="30" />
                            <ItemTemplate>
                                <asp:Button ID="btn_view" runat="server" Text="View Dashboard" Width="150px" CommandName="view" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>

        <br/>

        <div class="main2">
            <fieldset>
                <legend>Tutor</legend>
                <asp:GridView ID="GridView2" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" runat="server"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeys="Tutor_Id"
                    DataKeyNames="Tutor_Id" CssClass="display" OnRowCommand="GridView2_RowCommand">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                    <Columns>
                        <asp:TemplateField HeaderText="Tutor ID">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label ID="lbl_TutorId" runat="server" Text='<%# Eval("Tutor_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tutor Name">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Tutor_Name") %>' ID="lbl_Tutorname"></asp:Label>
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

                        <asp:TemplateField HeaderText="Option">
                            <ItemStyle Width="100" Height="30" />
                            <ItemTemplate>
                                <asp:Button ID="btn_view" runat="server" Text="View Dashboard" Width="150px" CommandName="view2" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>

    </form>
</body>
</html>
