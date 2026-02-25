<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LearnSphere.Forms.Profile.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile</title>
    <link href="~/CSS/Profile/Profile.css" rel="stylesheet" type="text/css" runat="server"/>

    <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link rel="preconnect" href="https://fonts.gstatic.com"/>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0" />
</head>
<body>
    <form id="frmProfile" runat="server">
        <asp:ScriptManager runat="server"/>
        <div class="master-container" runat="server">
            <div class="header" runat="server">
                <asp:ImageButton OnClick="btnBack_Click" ID="btnBack" runat="server" ImageUrl="~/Images/back.png" CssClass="back-button" />
                <img class="logo" runat="server" src="~/Images/logo-dark-mode.png" />
            </div>
            <div class="body-container">
                <div class="image-container">
                    <img runat="server" src="~/Images/learning.png" />
                </div>
                <div class="info-container">
                    <div class="info-user-type-body">
                        <asp:Label ID="lblUserType" CssClass="user-type" runat="server"></asp:Label>
                        <div class="info-body" runat="server">
                            <asp:Label Text="Username" runat="server" CssClass="field" />
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="value text-box" />

                            <asp:Label Text="Email" runat="server" CssClass="field" />
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="value text-box" />

                            <asp:Label ID="lblFieldDomain" Text="Domain" runat="server" CssClass="field" />
                            <asp:Label ID="lblValueDomain" runat="server" CssClass="value" />

                            <asp:Label Text="Account ID" runat="server" CssClass="field" />
                            <asp:Label ID="lblAccountID" runat="server" CssClass="value" />

                            <asp:Label ID="lblFieldUserTypeID" runat="server" CssClass="field" />
                            <asp:Label ID="lblValueUserTypeID" runat="server" CssClass="value" />
                        </div>
                        <asp:Label ID="lblSaveStatus" runat="server" />
                        <asp:Button 
                            ID="btnSave" 
                            Text="Save"
                            CssClass="action-button save-button" 
                            OnClick="btnSave_Click"
                            runat="server"/>
                        <asp:Button 
                            ID="btnLogOut" 
                            Text="Log Out"
                            CssClass="action-button log-out-button" 
                            OnClick="btnLogOut_Click"
                            runat="server"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
