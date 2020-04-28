<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create_Note.aspx.cs" Inherits="EWSD_COMP1640.Create_Note" %>

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

            var maxFileSize = 4000000; // 4MB -> 4 * 1024 * 1024
            var fileUpload = $(`#FileUpload1`);

            if (fileUpload.val() == '') {
                return false;
            }
            else {

                if (fileUpload[0].files[0].size > maxFileSize) {
                    $('#Label5').text('File size should not be greater than 4MB')
                    $("#Label5").css('backcolor', 'red');
                    $(`#btn_upload`).prop(`disabled`, true);
                    return false;
                }
                else {
                    $(`#Label5`).text('')
                    $(`#btn_upload`).prop(`disabled`, false);
                    return true;
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

            <asp:Label ID="Label3" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            <fieldset>
                <legend>Add Note</legend>
            </fieldset>
        </div>

        <div class="main2">

            <table border="1" class="table1">

                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Note Description : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_desc" runat="server" Height="135px" TextMode="MultiLine" Width="293px"></asp:TextBox>
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

                <tr style="height: 30px; text-align: center">
                    <td colspan="2">
                        <asp:Button ID="btn_upload" runat="server" Text="Upload" OnClick="btn_upload_Click" Width="85px" />
                    </td>
                </tr>
            </table>
             <asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label>
            <asp:CustomValidator ID="customValidatorUpload" runat="server" ErrorMessage="File size should not be greater than 2MB" ControlToValidate="FileUpload1" ClientValidationFunction="setUploadButtonState();" />
        </div>
       
        <br />
        <div class="main2">
            <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" runat="server"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                DataKeyNames="id" CssClass="display" OnRowDeleting="GridView1_RowDeleting">
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

                    <asp:TemplateField HeaderText="File">
                        <ItemStyle Width="300" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload ID="FileUpload2" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemStyle Width="300" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_desc2" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Width="221px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Upload">
                        <ItemStyle Width="300" Height="30" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Upload_Date") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Upload_Date") %>'></asp:Label>
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
