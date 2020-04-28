<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tutor_Blog.aspx.cs" Inherits="EWSD_COMP1640.Tutor_Blog" %>

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
        .auto-style1 {
            width: 85%;
        }

        .auto-style2 {
            width: 268px;
            margin-top:0;
        }

        .auto-style3 {
            height: 700px;
            width: 1013px;
            margin-left: 160px;
            margin-top:100px;
        }

        .auto-style4 {
            width: 100%;
            height: 209px;
        }

        .auto-style5 {
            height: 75px;
            text-align: justify;
        }

        .auto-style6 {
            width: 675px;
        }

        .auto-style7 {
            text-align: center;
            height: 95px;
        }

        .auto-style8 {
            background-color: #E7E7FF;
        }

        .main {
            margin-left: 160px;
            font-size: 28px;
            width: 100%;
            padding: 0px 10px;
            position: fixed;
            background-color: white;
        }
        table{
            border:1px solid;
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
                        <li class="active"><a href="Student_Work2.aspx">Student Work</a></li>
                        <li class="active"><a href="Arrange_Meeting.aspx">Meeting</a></li>
                        <li class="active"><a href="tutorchatroom.aspx">Messages</a></li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Tutor_Blog.aspx">Blog</a></li>

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
            <asp:Label ID="lbl_tutor" runat="server"></asp:Label>
            <asp:Label ID="lbltimedate" runat="server" Visible="false"></asp:Label>

            <fieldset>
                <legend>My Blog</legend>
            </fieldset>
        </div>

        <div class="auto-style3">
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">
                        <asp:Panel ID="Panel1" runat="server" Height="700px" ScrollBars="Vertical" Width="266px">
                            <asp:DataList ID="DataList1" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" GridLines="Both" Style="text-align: right" OnItemCommand="DataList1_ItemCommand1" Height="292px" Width="284px">
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <ItemStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_idb" Text='<%# Eval("id") %>' Visible="false" runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Image ID="Image2" ImageUrl='<%# Eval("blog_pic") %>' runat="server" Height="48px" Width="60px" />
                                    <br />
                                    <br />
                                    Title :
                                    <asp:Label ID="lbl_title" Text='<%# Eval("title") %>' runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    Message :
                                    <asp:Label ID="lbl_blog" Text='<%# Eval("message") %>' runat="server"></asp:Label>
                                    <br />
                                    Reply :
                                    <asp:Label ID="lbl_reply" runat="server" Text='<%# Eval("reply") %>'></asp:Label>
                                    <br />
                                    Reply/Post By :
                                    <asp:Label ID="lbl_replyby" runat="server" Text='<%# Eval("Reply_Name") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_tutor" Text='<%# Eval("Post_Name") %>' runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    Time Date:
                                    <asp:Label ID="lbl_timedate" Text='<%# Eval("date_time") %>' runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Button ID="btn_cmnt" runat="server" Text="comment" CommandName="select" CausesValidation="False" />
                                </ItemTemplate>
                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            </asp:DataList>
                        </asp:Panel>
                    </td>

                    <td class="auto-style6">
                        <asp:Panel ID="Panel2" runat="server" Height="700px" Width="744px">
                            <asp:DataList ID="DataList2" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Height="175px" OnItemCommand="DataList2_ItemCommand" Width="356px" OnSelectedIndexChanged="DataList2_SelectedIndexChanged">
                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                <EditItemTemplate>
                                    Message :<asp:TextBox ID="txt_msg" Text='<%# Eval("message") %>' runat="server" Height="41px" Width="212px"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="lbl_idb" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="edit" Height="34px" ImageUrl="~/images/btn_confirm.png" Width="40px" CausesValidation="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="ImageButton4" CommandName="go_back" runat="server" Height="44px" ImageUrl="~/images/unnamed.png" Width="38px" CausesValidation="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </EditItemTemplate>
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                <ItemTemplate>
                                    Title :<asp:Label ID="lbl_titleB" Text='<%# Eval("title") %>' runat="server"></asp:Label>
                                    <br />
                                    <span class="auto-style8">Message</span> :
                                    <asp:Label ID="lbl_blog" runat="server" Text='<%# Eval("message") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_idb" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                    <br />
                                    Post By :
                                    <asp:Label ID="lbl_post" Text='<%# Eval("Post_Name") %>' runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    Date Time Post:
                                    <asp:Label ID="lbl_DT" Text='<%# Eval("date_time") %>' runat="server"></asp:Label>
                                    <br />
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/images/2773833_button-icon-icon-button-edit-png-hd-png.png" CommandName="publish" Width="57px" CausesValidation="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="ImageButton3" runat="server" Height="27px" CommandName="delete" ImageUrl="~/images/delete_286553.png" Width="38px" CausesValidation="False" />
                                    <br />
                                </ItemTemplate>
                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            </asp:DataList>
                            <asp:Label ID="lbl_status" runat="server"></asp:Label>
                            <br />
                            <table class="auto-style4">
                                <tr>
                                    <td class="auto-style5">Title:
                                        <asp:TextBox ID="txt_title" runat="server" Height="22px" Width="353px" MaxLength="50"></asp:TextBox>
                                        &nbsp;<span /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_title" ErrorMessage="Title Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <span /><br />
                                        Image:<asp:FileUpload ID="file_uploadblog" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">Message :<asp:TextBox ID="txt_mesg" runat="server" Height="63px" TextMode="MultiLine" MaxLength="200" Width="557px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="Button2" runat="server" Height="81px" OnClick="Button2_Click" Text="Publish" Width="734px" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
