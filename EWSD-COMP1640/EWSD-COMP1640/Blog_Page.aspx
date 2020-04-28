<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blog_Page.aspx.cs" Inherits="EWSD_COMP1640.Blog_Page" %>

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
            width: 100%;
        }

        .reply {
            overflow: auto;
            height: 500px;
            width: 590px;
            margin: 0 auto;
        }

        .lbl_reply {
            word-wrap: break-word;
            word-break: break-all;
            width: 500px;
        }
        .back{
            padding: 0px 10px;
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
                    <ul class="nav navbar-nav navbar-right">
                        <li><asp:Button ID="btn_back" runat="server" Text="Back" OnClick="btn_back_Click" BorderStyle="None" Font-Bold="True" Font-Size="Medium" CssClass="back" BackColor="transparent" CausesValidation="False" ></asp:Button></li>
                        
                    </ul>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />

        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
        <div>
            <asp:Panel ID="Panel1" runat="server">
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_post" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_datetime" runat="server" Visible="false"></asp:Label>
                            <asp:DataList ID="DataList1" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" GridLines="Both" Style="text-align: center" HorizontalAlign="Center" Width="563px">
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <ItemStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_idb" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                    <asp:Image ID="Image1" ImageUrl='<%# Eval("blog_pic") %>' runat="server" Height="48px" Width="60px" />
                                    <br />
                                    <br />
                                    <asp:Label ID="Label2" Text="Post By : " runat="server"></asp:Label>
                                    <asp:Label ID="lbl_studentid" Text='<%# Eval("Post_Name") %>' runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label3" Text="Message : " runat="server"></asp:Label><br />
                                    <asp:Label ID="Label4" Text='<%# Eval("message") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2" style="text-align: center;">
                            <asp:TextBox ID="txt_mesg" runat="server" Height="30px" TextMode="MultiLine" MaxLength="100" Width="287px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="txt_mesg"  ErrorMessage="Input Required"></asp:RequiredFieldValidator>

                            <asp:Button ID="Button1" runat="server" Height="37px" Text="Comment It" Width="148px" OnClick="Button1_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <br />
        <div class="reply">
            <table class="auto-style4">
                <tr>
                    <td class="auto-style2">
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:DataList ID="DataList2" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" GridLines="Both" Style="text-align: Left" HorizontalAlign="Center" Width="563px" OnItemCommand="DataList2_ItemCommand">

                                <EditItemTemplate>
                                    Message :<asp:TextBox ID="txt_msg" runat="server" Height="41px" Text='<%# Eval("reply") %>' Width="212px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lbl_idb" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                    <asp:Label ID="reply_name" runat="server" Text='<%# Eval("Reply_Name") %>'></asp:Label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="edit" Height="44px" ImageUrl="~/images/11_Completed_check_tick_verified_approved-512.png" Width="49px" />
                                    <asp:ImageButton ID="ImageButton4" CommandName="go_back" runat="server" Height="37px" ImageUrl="~/images/unnamed.png" Width="38px" />
                                </EditItemTemplate>

                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <ItemStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_idb" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label5" Text="Reply By : " runat="server"></asp:Label>
                                    <asp:Label ID="lbl_tutor" Text='<%# Eval("Reply_Name") %>' runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    Reply message:
                                    <asp:Label ID="lbl_reply" Text='<%# Eval("reply") %>' runat="server" CssClass="lbl_reply"></asp:Label>
                                    <br />
                                    <br />
                                    Time Date :<asp:Label ID="lbl_datetime" Text='<%# Eval("date_time") %>' runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:ImageButton ID="ImageButton3" runat="server" Height="34px" CommandName="delete" ImageUrl="~/images/delete_286553.png" Width="45px" Visible="false" />
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="publish" Height="28px" ImageUrl="~/images/2773833_button-icon-icon-button-edit-png-hd-png.png" Width="57px" Visible="false" />
                                </ItemTemplate>
                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            </asp:DataList>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <table>
        </table>
    </form>
</body>
</html>