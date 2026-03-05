<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForumDetails.aspx.cs" Inherits="LearnSphere.Forms.Forum.ForumDetails" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Forum Details</title>
    <link runat="server" href="~/CSS/Forum/forum-details.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">

    <div class="topbar">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="icon-btn" OnClick="btnBack_Click">
            <img runat="server" src="~/Images/back.png" alt="Back" />
        </asp:LinkButton>

        <div class="brand">LearnSphere</div>

        <div class="profile">
            <img runat="server" src="~/Images/profile.png" alt="Profile" />
        </div>
    </div>

    <div class="page">

        <div class="forum-meta">
            Forum · <asp:Label ID="lblDomain" runat="server" />
        </div>

        <div class="forum-title">
            <asp:Label ID="lblTopic" runat="server" />
        </div>

        <div class="forum-createdby">
            Created by <asp:Label ID="lblCreator" runat="server" />
        </div>

        <div class="tag-group">
            <asp:Repeater ID="rptTags" runat="server">
                <ItemTemplate>
                    <span class="tag"><%# Eval("TagName") %></span>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="posts">
            <asp:Repeater ID="rptPosts" runat="server" OnItemCommand="rptPosts_ItemCommand">
                <ItemTemplate>
                    <div class="post">

                        <div class="avatar">
                            <img runat="server" src="~/Images/profile.png" alt="User" />
                        </div>

                        <div class="post-body">
                            <div class="post-meta">
                                <%# Eval("Username") %> · <%# Eval("CreatedAt", "{0:HH:mm · dd/MM/yyyy}") %>
                            </div>
                            <div class="post-content">
                                <%# Eval("Content") %>
                            </div>
                        </div>

                        <asp:LinkButton
                            ID="btnRemove"
                            runat="server"
                            Text="Remove"
                            CommandName="Remove"
                            CommandArgument='<%# Eval("PostID") %>'
                            CssClass="remove-btn"
                            Visible="false" />

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="bottom-area">

            <asp:Panel ID="pnlComment" runat="server">
                <div class="comment-label">Say something...</div>

                <div class="comment-box">
                    <div class="comment-row">
                        <asp:TextBox
                            ID="txtComment"
                            runat="server"
                            CssClass="comment-input"
                            placeholder="Here is what I think..." />
                        <asp:Button
                            ID="btnSend"
                            runat="server"
                            Text="➤"
                            CssClass="send-btn"
                            OnClick="btnSend_Click" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlAdminRemoveForum" runat="server" Visible="false">
                <asp:Button
                    ID="btnRemoveForum"
                    runat="server"
                    Text="Remove Forum"
                    CssClass="admin-remove-forum"
                    OnClick="btnRemoveForum_Click" />
            </asp:Panel>

        </div>

    </div>

</form>
</body>
</html>