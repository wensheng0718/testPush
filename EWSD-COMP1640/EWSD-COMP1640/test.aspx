<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="EWSD_COMP1640.test" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>ASP.NET chart</title>

    <style type="text/css">
        .auto-style1 {
            width: 886px;
            height: 667px;
        }
    </style>

</head>

<body style="height: 359px">

    <form id="form1" runat="server">

    <div style="padding-left:200px" class="auto-style1">

        <br />
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT a.status, COUNT(a.Student_Id) AS count, 'Assigned_Student' AS Student FROM Student AS a LEFT OUTER JOIN [User] AS b ON a.Student_Id = b.Student_Id WHERE (a.status LIKE 'assign') GROUP BY a.status UNION ALL SELECT b.Role, COUNT(b.Student_Id) AS count, 'Total_Student' AS Expr1 FROM [User] AS b LEFT OUTER JOIN Student AS a ON a.Student_Id = b.Student_Id WHERE (b.Role LIKE 'student') GROUP BY b.Role UNION ALL SELECT a.status, COUNT(a.Student_Id) AS count, 'Assigned_Student' AS Student FROM Student AS a LEFT OUTER JOIN [User] AS b ON a.Student_Id = b.Student_Id WHERE (a.status LIKE 'Have Not Assign') GROUP BY a.status"></asp:SqlDataSource>
  <table>
       <asp:Chart ID="Chart2" runat="server" CssClass="auto-style2" DataSourceID="SqlDataSource1" Height="291px" Width="556px" OnLoad="Chart2_Load" Visible="False" Palette="SemiTransparent">
            <Series>
                <asp:Series ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1" Name="Series1" XValueMember="status" YValueMembers="count" Font="Microsoft Sans Serif, 9pt, style=Bold">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" Title="Tutor" Font="Microsoft Sans Serif, 10.8pt, style=Bold" IsTextAutoFit="False">
                </asp:Legend>
            </Legends>
        </asp:Chart>
               <tr>
                   <td style="text-align: right;">Number of Students :</td>
                   <td>
                       <asp:Label ID="Label12" runat="server"></asp:Label>
                   </td>
               </tr>

               <tr>
                   <td style="text-align: right;">Assigned students :			
                   </td>
                   <td>
                       <asp:Label ID="Label2" runat="server"></asp:Label>
                   </td>
               </tr>


               <tr>
                   <td style="text-align: right;">Unassigned Students :			
                   </td>
                   <td>
                       <asp:Label ID="Label3" runat="server"></asp:Label>
                   </td>
                    <td>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                   </td>
               </tr>
           </table>
    </div>

    </form>

    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>

</body>

</html>