<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LearnSphere.Forms.Forum.Home" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="DomainFiltersColumn" ContentPlaceHolderID="DomainFiltersColumnPlaceholder" runat="server">
    <asp:Panel ID="colDomainFilters" CssClass="dropdown-column" runat="server">
        <asp:Repeater ID="rptDomain" runat="server">
            <ItemTemplate>
                <asp:LinkButton
                    ID="lnkDomain"
                    CssClass="dropdown-item"
                    Text='<%# Eval("DomainFilter") %>'
                    OnCommand="DomainFilter_Command"
                    CommandArgument='<%# Eval("DomainFilter") %>'
                    runat="server" />
            </ItemTemplate>
        </asp:Repeater>
        <asp:LinkButton
            ID="lnkDomainNone"
            CssClass="dropdown-item"
            Text="None"
            OnCommand="DomainFilter_Command"
            CommandArgument="None"
            runat="server">
        </asp:LinkButton>
    </asp:Panel>
</asp:Content>

<asp:Content ID="MineFiltersColumn" ContentPlaceHolderID="MineFiltersColumnPlaceholder" runat="server">
    <asp:Panel ID="colMineFilters" runat="server" CssClass="dropdown-column">
        <asp:LinkButton 
            ID="lnkMine"
            CssClass="dropdown-item" 
            Text="Mine"
            OnCommand="MineFilter_Command"
            CommandArgument="Mine"
            runat="server"/>
        <asp:LinkButton
            ID="lnkMineNone"
            CssClass="dropdown-item"
            Text="None"
            OnCommand="MineFilter_Command"
            CommandArgument="None"
            runat="server"/>
    </asp:Panel>
</asp:Content>

<asp:Content ID="SortsColumm" ContentPlaceHolderID="SortsColumnPlaceholder" runat="server">
    <asp:Panel ID="colSorts" runat="server" CssClass="dropdown-column">
        <asp:LinkButton 
            ID="TopicSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="Topic (A-Z)" 
            OnCommand="Sort_Command"
            CommandArgument="Topic (A-Z)"/>
        <asp:LinkButton 
            ID="NoneSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="None" 
            OnCommand="Sort_Command"
            CommandArgument="None"/>
    </asp:Panel>
</asp:Content>

<asp:Content ID="CreateForum" ContentPlaceHolderID="ActionGroupPlaceholder" runat="server">
    <asp:LinkButton 
    ID="lnkCreateForum" 
    runat="server" 
    CssClass="rightmost-action-group"
    PostBackUrl="~/Forms/Forum/CreateForum.aspx">
    <img src="~/Images/add.png" runat="server" class="action-group-component"/>
    <asp:Label ID="lblCreateForum" runat="server" Text="Create Forum"/>
</asp:LinkButton>
</asp:Content>

<asp:Content ID="Forums" ContentPlaceHolderID="BoxesPlaceholder" runat="server">
    <asp:Repeater ID="BoxRepeater" runat="server">
        <ItemTemplate>
            <asp:LinkButton ID="lnkBox" runat="server" CssClass="box" CommandArgument='<%# Eval("ID") %>' OnCommand="Box_Command">
                <label class="box-secondary-text">
                    <%# Eval("Domain") %>
                </label>
                <label class="box-primary-text">
                    <%# Eval("Topic") %>
                </label>
                <div class="tag-group">
                    <asp:Repeater ID="rptTag" runat="server" DataSource='<%# Eval("Tags") %>'>
                        <ItemTemplate>
                            <span class="tag"><%# Container.DataItem %></span>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
