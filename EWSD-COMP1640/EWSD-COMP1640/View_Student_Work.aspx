<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Student_Work.aspx.cs" Inherits="EWSD_COMP1640.View_Student_Work" %>

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

        .main {
            margin-left: 160px;
            font-size: 28px;
            padding: 0px 10px;
        }

        .main2 {
            margin-left: 160px;
            padding: 0px 10px;
        }

        #GridView3 {
            word-break: break-all;
            word-wrap: break-word;
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
                        <li class="active"><a href="Tutor_Home.aspx">Dashboard</a></li>
                        <li class="active"><a href="Tutor_Profile.aspx">Profile</a></li>
                        <li class="active"><a href="Learning_Material.aspx">Learning Material</a></li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Student_Work2.aspx">Student Work</a></li>
                        <li class="active"><a href="Arrange_Meeting.aspx">Meeting</a></li>
                        <li class="active"><a href="tutorchatroom.aspx">Messages</a></li>
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
                <legend>View Student Exercise</legend>
            </fieldset>
        </div>

        <div class="main2">
            <asp:GridView ID="GridView3" AutoGenerateColumns="False" runat="server" DataKeyNames="Id, filename"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                CssClass="display" OnRowCancelingEdit="GridView3_RowCancelingEdit" OnRowEditing="GridView3_RowEditing" OnRowUpdating="GridView3_RowUpdating" OnRowDataBound="GridView3_RowDataBound">
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
                    
                    <asp:TemplateField HeaderText="Upload Date">
                        <ItemStyle Width="200" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Work_Deadline") %>' ID="lbl_dead"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Work_Deadline") %>' ID="lbl_dead2"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Upload Date">
                        <ItemStyle Width="200" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Upload_Date") %>' ID="lbl_upload"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Upload_Date") %>' ID="lbl_upload2"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Student Name">
                        <ItemStyle Width="200" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Student_Name") %>' ID="lbl_name"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Student_Name") %>' ID="lbl_name2"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="filename">
                        <ItemStyle Width="200" Height="30" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text='<%# Eval("filename") %>' ForeColor="Black"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("filename") %>' ID="lbl_file2"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Comment">
                        <ItemStyle Width="200px" Height="30"/>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Comment") %>' ID="lbl_comment"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_comment" runat="server" Text='<%# Eval("Comment") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemStyle Width="150" Height="30" />
                        <ItemTemplate>
                            <asp:Button ID="btn_edit" runat="server" Text="Comment" CommandName="Edit" ToolTip="Edit" />
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:Button ID="btn_update" runat="server" Text="Update" CommandName="Update" ToolTip="Update" Width="150" Height="30" />
                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CommandName="Cancel" ToolTip="Cancel" Width="150" Height="30" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>

    </form>
</body>
</html>
