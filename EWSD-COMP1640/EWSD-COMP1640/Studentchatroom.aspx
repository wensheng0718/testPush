<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Studentchatroom.aspx.cs" Inherits="EWSD_COMP1640.Studentchatroom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style>
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

        .content1
        {
            margin:0px auto;
        }

                .lv-body
        {
            font-size:20px;
            margin:0px auto;
        }
                .content
                {
                    height:3px;
                }
                </style>

</head>
<body>
    <form id="form1" runat="server">

        <div class="sidenav">
            <a href="Student_Home.aspx">Dashboard</a>
            <a href="Student_Profile.aspx">Profile</a>
            <a href="#stuexercises">Exercises</a>
            <a href="Studentchatroom.aspx">Messages</a>
            <a href="Login.aspx">Logout</a>
        </div>

        <asp:Label ID="Label2" runat="server" Text="Chat Room"></asp:Label>

        <div class="main">

            <asp:Label ID="Label3" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />

            <asp:GridView ID="GridView3" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" runat="server"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                CssClass="display" OnRowCommand="GridView3_RowCommand">
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

                    <asp:TemplateField HeaderText="Total Message">
                        <ItemStyle Width="200" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Total_Message") %>' ID="Tutor_Name"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tutor ID">
                        <ItemStyle Width="300" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Tutor_Id") %>' ID="lbl_Id"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Student ID">
                        <ItemStyle Width="300" Height="30" />
                        <ItemTemplate>
                            <asp:Button ID="btn_select" runat="server" Text="Select" CommandName="select" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>
    </form>
</body>
</html>
