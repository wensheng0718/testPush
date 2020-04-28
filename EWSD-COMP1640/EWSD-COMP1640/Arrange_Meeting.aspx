<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Arrange_Meeting.aspx.cs" Inherits="EWSD_COMP1640.Arrange_Meeting" %>

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

        .table1 {
            margin: 0px auto;
        }
    </style>
    <script type="text/javascript">
    $(function () {
        var today = new Date();
        var month = ('0' + (today.getMonth() + 1)).slice(-2);
        var day = ('0' + today.getDate()).slice(-2);
        var year = today.getFullYear();
        var date = year + '-' + month + '-' + day;
        $('[id*=txt_date]').attr('min', date);
    });
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
                        <li class="active"><a href="Learning_Material.aspx">Learning Material</a></li>
                        <li class="active"><a href="Student_Work2.aspx">Student Work</a></li>
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Arrange_Meeting.aspx">Meeting</a></li>
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

            <asp:Label ID="Label4" runat="server" Text="Welcome"></asp:Label>
            &nbsp;
            <asp:Label ID="Label5" runat="server"></asp:Label>
            <br />
            <fieldset>
                <legend>Arrange Meeting</legend>
            </fieldset>
        </div>

        <div class="main2">
                <div class="notice">
       <table border="1" class="table1">

                        <tr style="height: 30px">
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Meeting Date : "></asp:Label>
                            </td>
                            <td>
                                <span />
                                <asp:TextBox ID="txt_calender" runat="server"></asp:TextBox>
                             <asp:ImageButton ID="ImageButton2" runat="server" Height="27px" ImageUrl="~/images/calendar.png" OnClick="ImageButton1_Click" Width="26px" CausesValidation="False" />
                                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="189px" NextPrevFormat="FullMonth" OnSelectionChanged="Calendar1_SelectionChanged" Width="298px" OnDayRender="Calendar1_DayRender">
                                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                    <TodayDayStyle BackColor="#CCCCCC" />
                                </asp:Calendar>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_calender" ErrorMessage="Date Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr style="height: 30px">
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Title: "></asp:Label>
                            </td>
                            <td><span />
                                <asp:TextBox ID="txt_title" runat="server" Height="20px" MaxLength="50"  Width="200px"></asp:TextBox>
                                </span>
                                <br />
                                <span />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ControlToValidate="txt_title"  ErrorMessage="Title Required"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr style="height: 30px">
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Meeting Time : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_time" runat="server" TextMode="Time" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ControlToValidate="txt_time"  ErrorMessage="Time Required"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr style="height: 30px">
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Meeting Type : "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Select Type</asp:ListItem>
                                    <asp:ListItem Value="1">Real</asp:ListItem>
                                    <asp:ListItem Value="2">Virtual</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr style="height: 30px">
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Group : "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" DataTextField="Group_Id" DataValueField="Group_Id">
                                    <asp:ListItem Value="0">Select Group</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Content : "></asp:Label>
                            </td>
                            <td style="width: 400px; height: 200px;">
                                <asp:TextBox ID="txt_content" runat="server" MaxLength="100" Height="180px" Width="400px"></asp:TextBox>
                            &nbsp;&nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red" ControlToValidate="txt_content"  ErrorMessage="Content Required"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr style="height: 30px; text-align: center;">
                            <td colspan="2">
                                <asp:Button ID="btn_upload" runat="server" Text="Upload" OnClick="btn_upload_Click" Width="85px" /></td>
                        </tr>
                    </table>
                </div>
                <br />
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

                        <asp:TemplateField HeaderText="Group">
                            <ItemStyle Width="100" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Group_Id") %>'></asp:Label>
                                <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true" DataTextField="Group_Id" DataValueField="Group_Id" Visible="false"></asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true" DataTextField="Group_Id" DataValueField="Group_Id"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Title">
                            <ItemStyle Width="180" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_title" runat="server" Text='<%# Eval("Title") %>'></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="validation_title" runat="server" ForeColor="Red" ControlToValidate="txt_title"  ErrorMessage="Title Required"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Content">
                            <ItemStyle Width="300" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Content") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_content" runat="server" Text='<%# Eval("Content") %>'></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="validation_content" runat="server" ForeColor="Red" ControlToValidate="txt_content"  ErrorMessage="Content Required"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Meeting Date and Time">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Meeting_Date") %>'></asp:Label>
                                 <asp:Label runat="server" Text='<%# Eval("time") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_date" runat="server" TextMode="Date" Text='<%# Eval("Meeting_Date") %>'></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="validation_date" runat="server" ForeColor="Red" ControlToValidate="txt_date"  ErrorMessage="Date Required"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txt_time" runat="server" TextMode="Time" Text='<%# Eval("time") %>'></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="validation_time" runat="server" ForeColor="Red" ControlToValidate="txt_time"  ErrorMessage="Time Required"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Meeting Type">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Meeting_Type") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="dpl_type" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Value="1">Real</asp:ListItem>
                                    <asp:ListItem Value="2">Virtual</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Last Update">
                            <ItemStyle Width="200" Height="30" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Last_Update_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Last_Update_Date") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField>
                            <ItemStyle Width="150" Height="30" />
                            <ItemTemplate>
                                <asp:Button ID="btn_edit" runat="server" Text="Edit" CommandName="Edit" ToolTip="Edit" CausesValidation="False" />
                                <asp:Button ID="btn_delete" runat="server" Text="Delete" CommandName="Delete" ToolTip="Delete" CausesValidation="False" />
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:Button ID="btn_update" runat="server" Text="Update" CommandName="Update" ToolTip="Update" Width="150" CausesValidation="False"  Height="30" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CommandName="Cancel" ToolTip="Cancel" Width="150" CausesValidation="False"  Height="30" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>

    </form>
</body>
</html>
