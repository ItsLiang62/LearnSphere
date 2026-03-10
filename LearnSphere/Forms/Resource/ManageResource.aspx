<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageResource.aspx.cs" Inherits="LearnSphere.Forms.Resource.ManageResource" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Manage Resources</title>
    <link href="~/CSS/Resource/resource-manage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="manage-container">
            <h2>Manage Learning Resources</h2>

            <asp:GridView ID="gvResources" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID"
                CssClass="grid"
                OnRowEditing="gvResources_RowEditing"
                OnRowCancelingEdit="gvResources_RowCancelingEdit"
                OnRowUpdating="gvResources_RowUpdating"
                OnRowDeleting="gvResources_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Author" HeaderText="Author" />
                    <asp:BoundField DataField="PublicationYear" HeaderText="Year" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="Locator" HeaderText="Locator" />
                    <asp:BoundField DataField="DomainName" HeaderText="Domain" />
                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblMessage" runat="server" CssClass="message" />
        </div>
    </form>
</body>
</html>