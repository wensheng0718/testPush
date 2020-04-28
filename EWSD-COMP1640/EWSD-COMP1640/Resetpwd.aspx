<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resetpwd.aspx.cs" Inherits="EWSD_COMP1640.Resetpwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
                input[type=text]{
            width: 80%;
            border: 0;
            border-bottom: 1px solid #4b4040;
        }
                div{
                    resize:both;
                }
        </style>
</head>
<body>
    <form id="form1" runat="server">

         <div style="background:white; width:500px; height:160px; border:1px solid; margin:0 auto; margin-top:200px;">
                <br/>
                   <asp:Label ID="Label1" runat="server" Text="Recover account" CssClass="label_header" Font-Bold="True" Font-Size="Medium"></asp:Label>
                  
                    <hr/>
                <div style="height:50px; margin-top:20px; padding:3px;">
             <a style="Font-Size:Medium;">Please enter your email to recover your account.</a>
            <asp:TextBox ID="txt_email" runat="server" placeholde="Email" placeholder="Email" Height="20px" CssClass="txt_email" ></asp:TextBox>
                    <br/>
                    <asp:Label ID="Label3" runat="server" ForeColor="green"></asp:Label>
                      </div>
                   
                   <br/>
                   <div style="background:lightgray; height:25px; text-align:right; padding-top:7px; margin-top:10px;">
            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" BackColor="#12a312" ForeColor="White" Width="60px" BorderColor="#12A312" Height="25px" /> &nbsp;
                       <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_cancel_Click" Width="60px" Height="25px" />
                       &nbsp;
                      <div style="background:grey; height:5px;" >
                          </div>
                       </div>
                 <br/>
        </div>

    </form>
</body>
</html>
