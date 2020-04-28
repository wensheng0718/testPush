<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create_Coursework.aspx.cs" Inherits="EWSD_COMP1640.Create_Coursework" %>

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
                    $('#Label5').text('')
                    $('#btn_upload').prop('disabled', false);
                    return true;
                } else {
                    $('#Label5').text('File size should not be greater than 4MB')
                    $("#Label5").css('backcolor', 'red');
                    $('#btn_upload').prop('disabled', true);
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
                        <li class="active"><a href="Tutor_Home.aspx">Dashboard</a></li>
                        <li class="active"><a href="Tutor_Profile.aspx">Profile</a></li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Learning_Material.aspx">Learning Material</a></li>
                        <li class="active"><a href="Student_Work2.aspx">Student Work</a></li>
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

            <asp:Label ID="Label6" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label7" runat="server"></asp:Label>
            <br />
            <fieldset>
                <legend>Add Exercise</legend>
            </fieldset>
        </div>

        <div class="main2">
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <table border="1" class="table1">

                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Exercise Code : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_code" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Deadline : "></asp:Label>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" OnDayRender="Calendar1_DayRender">
                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                            <TodayDayStyle BackColor="#CCCCCC" />
                        </asp:Calendar>
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="File : "></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>

                <tr style="height: 30px; text-align: center;">
                    <td colspan="2">
                        <asp:Button ID="btn_upload" runat="server" Text="Upload" OnClick="btn_upload_Click" Width="85px" /></td>
                </tr>
            </table>
            <asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:CustomValidator ID="customValidatorUpload" runat="server" ErrorMessage="File size should not be greater than 2MB" ControlToValidate="FileUpload1" ClientValidationFunction="setUploadButtonState();" />
        </div>
        <br />
        <div class="main2">
            <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" runat="server"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating1"
                DataKeyNames="id" CssClass="display" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
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

                    <asp:TemplateField HeaderText="Exercise Code">
                        <ItemStyle Width="180" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Work_Code") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_code" runat="server" Text='<%# Eval("Work_Code") %>'></asp:TextBox>
                            <asp:TextBox ID="txt_id" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Deadline">
                        <ItemStyle Width="200" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Work_Deadline") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_date" runat="server" TextMode="Date" Text='<%# Eval("Work_Deadline") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="File">
                        <ItemStyle Width="300" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload ID="FileUpload2" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemStyle Width="150" Height="30" />
                        <ItemTemplate>
                            <asp:Button ID="btn_edit" runat="server" Text="Edit" CommandName="Edit" ToolTip="Edit" />
                            <asp:Button ID="btn_delete" runat="server" Text="Delete" CommandName="Delete" ToolTip="Delete" />
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
