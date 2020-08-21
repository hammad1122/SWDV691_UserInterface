<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Queue_Manager.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center;">
                    <asp:Panel ID="Panel1" class="panel-body" runat="server" BackColor="Orange">    
            Queue Manager<br />
                </asp:Panel>
            </div>
        <div style="text-align:center;">
            <asp:Label ID="lblUserID" runat="server" Text="USER ID"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtboxUser" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblPass" runat="server" Text="PASSWORD"></asp:Label>
            <asp:TextBox ID="txtboxPass" runat="server"></asp:TextBox>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" Width="128px" />
            <br />
            <asp:Label ID="lblMsg" runat="server" BackColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
