<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_Profile.aspx.cs" Inherits="EWSD_COMP1640.Student_Profile" %>

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

        #DataList1 {
            margin: 0px auto;
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
                        <li class="active"><a href="Student_Home.aspx">Dashboard</a> </li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Student_Profile.aspx">Profile</a></li>
                        <li class="active"><a href="Student_Work.aspx">Exercises</a></li>
                        <li class="active"><a href="privatechatstudent.aspx">Messages</a></li>
                        <li class="active"><a href="Student_Blog.aspx">Blog</a></li>
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
                <legend>Profile</legend>

                <asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333" OnItemCommand="DataList1_ItemCommand">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemTemplate>
                        <table border="1" style="width: 800px;">
                            <tr style="height: 30px">
                                <td colspan="2" class="title">
                                    <asp:Label ID="Label5" runat="server" Text="Student Details"></asp:Label>
                                </td>

                            </tr>
                            <tr style="height: 30px">
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Student ID : "></asp:Label>
                                </td>
                                <td>

                                    <asp:Label ID="Label3" runat="server" Text=' <%# Eval("Student_Id") %>'></asp:Label>
                                </td>
                            </tr>

                            <tr style="height: 30px">
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Name : "></asp:Label>
                                </td>
                                <td>

                                    <asp:Label ID="lbl_name" runat="server" Text=' <%# Eval("Student_Name") %>'></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Email : "></asp:Label>
                                </td>
                                <td style="height: 30px">
                                    <asp:TextBox ID="txt_email" runat="server" Text=' <%# Eval("Email_Address") %>' Font-Size="Large" Width="250px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Course : "></asp:Label>
                                </td>
                                <td style="height: 30px">
                                    <asp:Label ID="Label7" runat="server" Text=' <%# Eval("Course") %>'></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Date of Birth : "></asp:Label>
                                </td>
                                <td style="height: 30px">
                                    <asp:Label ID="Label9" runat="server" Text=' <%# Eval("DOB") %>'></asp:Label>
                                </td>
                            </tr>

                            <tr style="height: 30px; text-align: center;">
                                <td colspan="2">
                                    <asp:Button ID="btn_save" runat="server" Text="Save" Width="100px" CommandName="save" />

                                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" Width="100px" CommandName="cancel" />
                                    <br />
                                    <asp:Label ID="Label12" runat="server" Text="Email Address already exist" Visible="False" ForeColor="Red"></asp:Label>
                                </td>

                            </tr>



                        </table>
                    </EditItemTemplate>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <ItemTemplate>
                        <table border="1" style="width: 800px;">
                            <tr style="height: 30px">
                                <td colspan="2" class="title">
                                    <asp:Label ID="Label5" runat="server" Text="Student Details"></asp:Label>
                                </td>

                            </tr>
                            <tr style="height: 30px">
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Student ID : "></asp:Label>
                                </td>
                                <td>

                                    <asp:Label ID="Label3" runat="server" Text=' <%# Eval("Student_Id") %>'></asp:Label>
                                </td>
                            </tr>

                            <tr style="height: 30px">
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Name : "></asp:Label>
                                </td>
                                <td>

                                    <asp:Label ID="lbl_name" runat="server" Text=' <%# Eval("Student_Name") %>'></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Email : "></asp:Label>
                                </td>
                                <td style="height: 30px">
                                    <asp:Label ID="lbl_email" runat="server" Text=' <%# Eval("Email_Address") %>'></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Course : "></asp:Label>
                                </td>
                                <td style="height: 30px">
                                    <asp:Label ID="Label7" runat="server" Text=' <%# Eval("Course") %>'></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Date of Birth : "></asp:Label>
                                </td>
                                <td style="height: 30px">
                                    <asp:Label ID="Label9" runat="server" Text=' <%# Eval("DOB") %>'></asp:Label>
                                </td>
                            </tr>

                            <tr style="height: 30px; text-align: center;">
                                <td colspan="2">
                                    <asp:Button ID="btn_edit" runat="server" Text="Edit" Width="100px" CommandName="edit" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:DataList>

            </fieldset>
        </div>

    </form>
</body>
</html>
