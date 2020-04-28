<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="calender.aspx.cs" Inherits="EWSD_COMP1640.calender" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:ImageButton ID="ImageButton1" runat="server" Height="30px" ImageUrl="~/images/calendar.png" OnClick="ImageButton1_Click" Width="46px" CausesValidation="False" />
            <br />
             <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth"  Width="350px" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged">
                                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                    <TodayDayStyle BackColor="#CCCCCC" />
                                </asp:Calendar>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ForeColor="Red" ControlToValidate="TextBox1" ErrorMessage="date needed"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:TextBox ID="txt_datetime" runat="server" TextMode="Time" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ControlToValidate="txt_datetime" ErrorMessage="time needed"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:TextBox ID="txt_title" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />&nbsp;<asp:Button ID="Button1" runat="server" Text="add" OnClick="Button1_Click" />
        </div>



    </form>



     </body>
</html>
