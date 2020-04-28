<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reallocate_Tutor.aspx.cs" Inherits="EWSD_COMP1640.Reallocate_Tutor" %>

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

        .welcome {
            margin-left: 160px;
            font-size: 28px;
            padding: 0px 10px;
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
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Assigned_Student.aspx">Re-assigned Student</a></li>
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
            <asp:Label ID="Label4" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label6" runat="server"></asp:Label>
            <br />
            <fieldset>
                <legend>Re-Assign Tutor</legend>
            </fieldset>
        </div>

<div class="main2">
            <fieldset>

                <div>
                    <br />
                    <asp:TextBox ID="txtsearch" runat="server" placeholder="Course"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" />
                </div>
                <br />
                <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" runat="server"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    CssClass="display" OnRowCommand="GridView1_RowCommand">
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

                        <asp:TemplateField HeaderText="Group">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Group_Id") %>' ID="Group_Id"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tutor Name">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Tutor_Name") %>' ID="Tutor_Name"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Course">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Course") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Student">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Total_Student") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Option">
                            <ItemStyle Width="100" Height="30" />
                            <ItemTemplate>
                                <asp:Button ID="btn_assign" runat="server" Text="Assign" CommandName="assign" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>

                </asp:GridView>
            </fieldset>
        </div>
    </form>
</body>
</html>
