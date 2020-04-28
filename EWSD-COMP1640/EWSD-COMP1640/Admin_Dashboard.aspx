    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Dashboard.aspx.cs" Inherits="EWSD_COMP1640.Admin_Dashboard" %>

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

        .main {
            margin-left: 160px;
            font-size: 28px;
            padding: 0px 10px;
        }

        .welcome {
            margin-left: 160px;
            font-size: 28px;
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
                    <ul class="nav navbar-nav">
                        <li class="active"><a style="background-color: mediumblue; color: white" href="Admin_Dashboard.aspx">Dashboard</a></li>
                        <li class="active"><a href="Add_Tutor.aspx">Add Tutor</a></li>
                        <li class="active"><a href="Student.aspx">Assign Student</a></li>
                        <li class="active"><a href="Assigned_Student.aspx">Re-assigned Student</a></li>
                        <li class="active"><a href="Dashboard_Viewer.aspx">View Dashboard</a></li>
                        <li class="active"><a href="Show_Not_Assign_Student.aspx">Generate Report</a></li>
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
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <fieldset>
                <legend>Dashboard</legend>
            </fieldset>
        </div>

        <div class="main">

            <fieldset>
                <legend>Students</legend><br/>
           <table>
              <asp:Chart ID="Chart2" runat="server" CssClass="auto-style2" Height="291px" Width="556px"  >
            <Series>
                <asp:Series ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1" Name="Series1" XValueMember="status" YValueMembers="count" Font="Microsoft Sans Serif, 9pt, style=Bold" >
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" Title="Student" Font="Microsoft Sans Serif, 10.8pt, style=Bold">
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
                 
               </tr>
           </table>
            </fieldset>
            <br />
            <br />
            <fieldset>
                <legend>Tutors</legend>
                <br />
                <table>
                    <asp:Chart ID="Chart1" runat="server" CssClass="auto-style2"  Height="291px" Width="556px" Palette="SemiTransparent">
            <Series>
                <asp:Series ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1" Name="Series1" XValueMember="status" YValueMembers="count" Font="Microsoft Sans Serif, 9pt, style=Bold">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea2">
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" Title="Tutor" Font="Microsoft Sans Serif, 10.8pt, style=Bold" IsTextAutoFit="False">
                </asp:Legend>
            </Legends>
        </asp:Chart>
                    <tr>
                        <td style="text-align: right;">Number of Tutors :			
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">Tutor without student : 		
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>

        </div>

    </form>

</body>
</html>
