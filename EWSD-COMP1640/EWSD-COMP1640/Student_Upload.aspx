<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_Upload.aspx.cs" Inherits="EWSD_COMP1640.Student_Upload" %>

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
        .comment{
            word-wrap:break-word;
            word-break:break-all;
            width:100px;
        }
    </style>

        <script>

        function setUploadButtonState() {

            var maxFileSize = 4194304; // 4MB -> 4 * 1024 * 1024
            var fileUpload = $('#FileUpload1');

            if (fileUpload.val() == '') {
                return false;
            }
            else {
                if (fileUpload[0].files[0].size < maxFileSize) {
                    $('#label').text('')
                    $('#Button1').prop('disabled', false);
                    return true;
                } else {
                    $('#label').text('File size should not be greater than 4MB')
                    $("#label").css('backcolor', 'red');
                    $('#Button1').prop('disabled', true);
                    return false;
                }
            }
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
                    <a class="navbar-brand"><span />
                        <img alt="Logo" src="images/E-tutor system logo.png" height="150" /></a>
                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="Student_Home.aspx">Dashboard</a> </li>
                        <li class="active"><a href="Student_Profile.aspx">Profile</a></li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Student_Work.aspx">Exercises</a></li>
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
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />

            <fieldset>
                <legend>Upload Exercise</legend>
            </fieldset>
        </div>

        <div class="main2">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" />
            <br />
            <asp:Label ID="label" runat="server" ForeColor="Red"></asp:Label>
            <asp:CustomValidator ID="customValidatorUpload" runat="server" ErrorMessage="File size should not be greater than 2MB" ControlToValidate="FileUpload1" ClientValidationFunction="setUploadButtonState();" />

            <br />
            <br />
            <asp:GridView ID="GridView3" AutoGenerateColumns="False" runat="server" DataKeyNames="Id, filename"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                CssClass="display" OnRowDeleting="GridView3_RowDeleting" OnRowDataBound="GridView3_RowDataBound">
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
                            <asp:Label runat="server" Text='<%# Eval("Upload_Date") %>' ID="lbl_date"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="filename">
                        <ItemStyle Width="200" Height="30" />
                        <ItemTemplate>
                            <%--                                <asp:Label runat="server" Text='<%# Eval("filename") %>' ID="lbl_file"></asp:Label>--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text='<%# Eval("filename") %>' ForeColor="Black"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Comment">
                        <ItemStyle Width="200" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Comment") %>' ID="lbl_comment" CssClass="comment"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btn_delete" runat="server" Text="Delete" CommandName="Delete" ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>
    </form>
</body>
</html>
