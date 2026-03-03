<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateForum.aspx.cs" Inherits="LearnSphere.Forms.Forum.CreateForum" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Create Forum</title>
    <link href="~/CSS/Forum/forum-create.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="modal-overlay">
            <div class="modal">
                <h2>Create a Forum</h2>

                <div class="form-row">
                    <label>Title</label>
                    <asp:TextBox ID="txtTopic" runat="server" CssClass="input" />
                </div>

                <div class="form-row">
                    <label>Domain</label>
                    <asp:DropDownList ID="ddlDomain" runat="server" CssClass="select" />
                </div>

                <div class="form-row">
                    <label>Tags (comma separated)</label>
                    <asp:TextBox ID="txtTags" runat="server" CssClass="input" />
                </div>

                <asp:Button ID="btnCreate" runat="server"
                    Text="Create"
                    CssClass="btn-primary"
                    OnClick="btnCreate_Click" />
            </div>
        </div>

    </form>
</body>
</html>