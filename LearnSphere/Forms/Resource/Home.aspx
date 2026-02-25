<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LearnSphere.Forms.Resource.Home" %>
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

<asp:Content ID="CategoryFiltersColumn" ContentPlaceHolderID="CategoryFiltersColumnPlaceholder" runat="server">
    <asp:Panel ID="colCategoryFilters" CssClass="dropdown-column" runat="server">
        <asp:LinkButton
            ID="lnkLecture"
            CssClass="dropdown-item"
            Text="Lecture"
            OnCommand="CategoryFilter_Command"
            CommandArgument="Lecture"
            runat="server" />
        <asp:LinkButton
            ID="lnkArticle"
            CssClass="dropdown-item"
            Text="Article"
            OnCommand="CategoryFilter_Command"
            CommandArgument="Article"
            runat="server" />
        <asp:LinkButton
            ID="lnkBook"
            CssClass="dropdown-item"
            Text="Book"
            OnCommand="CategoryFilter_Command"
            CommandArgument="Book"
            runat="server" />
        <asp:LinkButton
            ID="lnkCategoryNone"
            CssClass="dropdown-item"
            Text="None"
            OnCommand="CategoryFilter_Command"
            CommandArgument="None"
            runat="server" /> 
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
            ID="TitleSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="Title (A-Z)" 
            OnCommand="Sort_Command"
            CommandArgument="Title (A-Z)"/>
        <asp:LinkButton 
            ID="AuthorSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="Author (A-Z)" 
            OnCommand="Sort_Command"
            CommandArgument="Author (A-Z)"/>
        <asp:LinkButton 
            ID="MostRecentSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="Most Recent" 
            OnCommand="Sort_Command"
            CommandArgument="Most Recent"/>
        <asp:LinkButton 
            ID="NoneSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="None" 
            OnCommand="Sort_Command"
            CommandArgument="None"/>
    </asp:Panel>
</asp:Content>

<asp:Content ID="ShareResource" ContentPlaceHolderID="ActionGroupPlaceholder" runat="server">
    <asp:LinkButton ID="lnkShareResource" runat="server" CssClass="rightmost-action-group">
        <img src="~/Images/add.png" runat="server" class="action-group-component"/>
        <asp:Label ID=lblShareResource runat="server" Text="Share Resource"/>
    </asp:LinkButton>
</asp:Content>

<asp:Content ID="Resources" ContentPlaceHolderID="BoxesPlaceholder" runat="server">
    <asp:Repeater ID="BoxRepeater" runat="server">
        <ItemTemplate>
            <asp:LinkButton ID="lnkBox" runat="server" CssClass="box" CommandArgument='<%# Eval("ResourceID") %>' OnCommand="Box_Command">
                <label class="box-secondary-text">
                    <%# Eval("Category") %>
                </label>
                <div class="box-group">
                    <label class="box-primary-text">
                        <%# Eval("Title") %>
                    </label>
                    <span class="tag rightmost-tag"><%# Eval("Year") %></span>
                </div>
                <label class="box-secondary-text">
                    <%# Eval("Author") %>
                </label>
            </asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
