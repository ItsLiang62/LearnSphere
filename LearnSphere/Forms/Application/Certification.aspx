<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Certification.aspx.cs" Inherits="LearnSphere.Forms.Application.Certification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Certification</title>

    <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link rel="preconnect" href="https://fonts.gstatic.com"/>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0" />

    <link href="~/CSS/Application/Certification.css" rel="stylesheet" type="text/css" runat="server"/>
</head>
<body>
    <form id="frmCertification" runat="server">
        <asp:ScriptManager runat="server"/>
        <div class="master-container">
            <div class="header">
                <asp:ImageButton OnClick="btnBack_Click" ID="btnBack" runat="server" ImageUrl="~/Images/back.png" CssClass="header-button" />
                <img src="~/Images/logo-dark-mode.png" class="logo" runat="server" />
                <asp:ImageButton OnClick="btnProfile_Click" ID="btnProfile" runat="server" ImageUrl="~/Images/profile.png" CssClass="header-button" />
            </div>

            <asp:FormView ID="fvApplicationInfo" runat="server">
                <ItemTemplate>
                    <div class="application-info-container">
                        <asp:Label ID="lblDomain" runat="server" 
                            CssClass="application-info-secondary" 
                            Text='<%# Eval("DomainName") %>' />

                        <asp:Label ID="lblUsername" runat="server" 
                            CssClass="application-info-primary" 
                            Text='<%# Eval("Username") %>' />

                        <asp:Label ID="lblEmail" runat="server" 
                            CssClass="application-info-secondary" 
                            Text='<%# Eval("Email") %>' />

                        <span class="application-status-tag">
                            <%# Eval("Status") %>
                        </span>
                    </div>
                </ItemTemplate>
            </asp:FormView>
            <div class="certification-container">
                <asp:Literal ID="ltCert" runat="server" />
            </div>
            <div id="actionContainer" class="action-container" runat="server">
                <asp:Button 
                    ID="btnApprove" 
                    CssClass="action-button approve-button" 
                    Text="Approve"
                    OnClick="btnApprove_Click"
                    runat="server"/>
                <asp:Button 
                    ID="btnReject" 
                    CssClass="action-button reject-button" 
                    Text="Reject"
                    OnClick="btnReject_Click"
                    runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
