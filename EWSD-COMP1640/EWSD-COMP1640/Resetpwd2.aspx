<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resetpwd2.aspx.cs" Inherits="EWSD_COMP1640.Resetpwd2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        div, tr,td{
            resize:both;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background:white; width:500px; height:220px; margin:0 auto; margin-top:200px;  border:1px solid;">
                <div style="margin-left:3px; margin-top:10px;">
                    <asp:Label ID="Label5" runat="server" Text="Reset Password" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <hr/>
                    </div>
               <table style="width:100%; margin-left:3%; margin-top:5px; resize:both;">
                   <tr style="height:40px;">
                       <td>
                           <asp:Label ID="Label1" runat="server" Text="Verification Code"></asp:Label>
                        </td>
                         <td>
                           <asp:TextBox ID="txt_verify" runat="server" Height="20px" Width="70%"></asp:TextBox>
                        </td>
                       </tr>

                   <tr style="height:40px;">
                       <td class="auto-style1">
                           <asp:Label ID="Label2" runat="server" Text="New Password"></asp:Label>
                        </td>
                         <td class="auto-style1">
                           <asp:TextBox ID="txt_nPassword" runat="server" Height="20px" Width="70%" TextMode="Password"></asp:TextBox>
                         
                        </td>
                       </tr>
                   <tr style="height:40px;">
                       <td>
                           <asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label>
                        </td>
                         <td>
                           <asp:TextBox ID="txt_cpassword" runat="server" Height="20px" Width="70%" TextMode="Password"></asp:TextBox>
                        </td>
                       </tr>
                   </table>

                <table style="width:100%">
                     <tr style="height:18px; text-align:center;">
                       <td colspan="2">
                           <asp:RegularExpressionValidator display="Dynamic" id="RegularExpressionValidator1" Font-Size="Medium" errormessage="Password must be 8-10 characters long with at least one numeric, one upper case character and one special character." forecolor="Red" runat="server" ControlToValidate="txt_nPassword" ValidationExpression="(?=^.{8,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{&quot;:;'?/>.<,])(?!.*\s).*$"></asp:RegularExpressionValidator>
                           <asp:Label ID="Label4" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                       </tr>
               </table>
                <div style="text-align:center; width:100%; margin-top:3px; background-color:lightgray; height:34px; border:1px solid;">
                    <asp:Button ID="btn_confirm" runat="server" Text="Confirm" OnClick="btn_confirm_Click"  BackColor="#12a312" ForeColor="White" Width="80px" BorderColor="#12A312" Height="27px" />
                    </div>
        </div>
    </form>
</body>
</html>
