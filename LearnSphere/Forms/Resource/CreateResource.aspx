<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateResource.aspx.cs" Inherits="LearnSphere.Forms.Resource.CreateResource" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Create Resource</title>
    <link href="~/CSS/Resource/resource-create.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="modal-overlay">
            <div class="modal">
                <h2>Share a Learning Resource</h2>

                <div class="form-row">
                    <label>Title</label>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input" />
                </div>

                <div class="form-row">
                    <label>Author</label>
                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="input" />
                </div>

                <div class="form-row">
                    <label>Publication Year</label>
                    <asp:TextBox ID="txtYear" runat="server" CssClass="input" TextMode="Number" />
                </div>

                <div class="form-row">
                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="select">
                        <asp:ListItem Text="Lecture" Value="Lecture" />
                        <asp:ListItem Text="Article" Value="Article" />
                        <asp:ListItem Text="Book" Value="Book" />
                        <asp:ListItem Text="Other" Value="Other" />
                    </asp:DropDownList>
                </div>

                <div class="form-row">
                    <label>Domain</label>
                    <asp:DropDownList ID="ddlDomain" runat="server" CssClass="select" />
                </div>

                <div class="form-row">
                    <label>Locator</label>
                    <asp:TextBox ID="txtLocator" runat="server" CssClass="input" />
                </div>

                <asp:Label ID="lblMessage" runat="server" CssClass="message" />

                <asp:Button ID="btnCreate" runat="server"
                    Text="Create"
                    CssClass="btn-primary"
                    OnClick="btnCreate_Click" />
            </div>
        </div>
    </form>
</body>
</html>