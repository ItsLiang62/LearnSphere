<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceDetails.aspx.cs" Inherits="LearnSphere.Forms.Resource.ResourceDetails" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Resource Details</title>
    <link href="~/CSS/Resource/resource-details.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="details-container">
            <div class="details-card">
                <h2><asp:Label ID="lblTitle" runat="server" /></h2>

                <p><strong>Author:</strong> <asp:Label ID="lblAuthor" runat="server" /></p>
                <p><strong>Publication Year:</strong> <asp:Label ID="lblYear" runat="server" /></p>
                <p><strong>Category:</strong> <asp:Label ID="lblCategory" runat="server" /></p>
                <p><strong>Domain:</strong> <asp:Label ID="lblDomain" runat="server" /></p>
                <p>
                    <strong>Locator:</strong>
                    <asp:HyperLink ID="lnkLocator" runat="server" Target="_blank" />
                </p>

                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn-secondary" OnClick="btnBack_Click" />
            </div>
        </div>
    </form>
</body>
</html>