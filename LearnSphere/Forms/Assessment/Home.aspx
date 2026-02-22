<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LearnSphere.Forms.Assessment.Home" %>

<asp:Content ID="DomainFiltersColumn" ContentPlaceHolderID="DomainFiltersColumnPlaceholder" runat="server">
    <asp:Panel ID="colDomainFilters" runat="server" CssClass="dropdown-column">
        <asp:Repeater ID="DomainRepeater" runat="server">
            <ItemTemplate>
                <asp:LinkButton 
                    ID="lnkDomain" 
                    runat="server" 
                    CssClass="dropdown-item" 
                    Text='<%# Eval("DomainFilter") %>' 
                    OnCommand="DomainFilter_Command"
                    CommandArgument='<%# Eval("DomainFilter") %>'/>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>

<asp:Content ID="MineFiltersColumn" ContentPlaceHolderID="MineFiltersColumnPlaceholder" runat="server">
    <asp:Panel ID="colMineFilters" runat="server" CssClass="dropdown-column">
        <asp:LinkButton 
            CssClass="dropdown-item" 
            Text="Mine"
            CommandArgument="Mine"  
            OnCommand="MineFilter_Command" 
            runat="server"/>
        <asp:LinkButton
            CssClass="dropdown-item"
            Text="None"
            CommandArgument="None"
            OnCommand="MineFilter_Command"
            runat="server"/>
    </asp:Panel>
</asp:Content>


<asp:Content ID="SortsColumn" ContentPlaceHolderID="SortsColumnPlaceholder" runat="server">
    <asp:Panel ID="colSorts" runat="server" CssClass="dropdown-column">
        <asp:LinkButton 
            ID="TitleSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="Title (A-Z)" 
            OnCommand="Sort_Command"
            CommandArgument="Title (A-Z)"/>
        <asp:LinkButton 
            ID="EducatorSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="Educator (A-Z)" 
            OnCommand="Sort_Command"
            CommandArgument="Educator (A-Z)"/>
        <asp:LinkButton 
            ID="NoneSort" 
            runat="server" 
            CssClass="dropdown-item" 
            Text="None" 
            OnCommand="Sort_Command"
            CommandArgument="None"/>
    </asp:Panel>
</asp:Content>

<asp:Content ID="CreatePaper" ContentPlaceHolderID="ActionGroupPlaceholder" runat="server">
    <asp:LinkButton ID="lnkCreatePaper" runat="server" CssClass="rightmost-action-group">
        <img src="~/Images/add.png" runat="server" class="action-group-component"/>
        <asp:Label ID=lblCreatePaper runat="server" Text="Create Paper"/>
    </asp:LinkButton>
</asp:Content>

<asp:Content ID="Papers" ContentPlaceHolderID="BoxesPlaceholder" runat="server">
    <asp:Repeater ID="BoxRepeater" runat="server" OnItemDataBound="BoxRepeater_ItemDataBound">
        <ItemTemplate>
            <asp:LinkButton ID="lnkBox" runat="server" CssClass="box" CommandArgument='<%# Eval("PaperID") %>' OnCommand="Box_Command">
                <label class="box-primary-text">
                    <%# Eval("Title") %>
                </label>

                <div class="tag-group">
                    <asp:Repeater ID="TagRepeater" runat="server">
                        <ItemTemplate>
                            <span class="tag"><%# Container.DataItem %></span>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="user-tag">
                    <img src="~/Images/profile.png" class="user-tag-profile-icon" runat="server"/>
                    <div class="user-tag-text-group">
                        <asp:Label CssClass="user-tag-username" runat="server" Text='<%# Eval("EducatorName") %>' />
                        <asp:Label CssClass="user-tag-domain" runat="server" Text='<%# Eval("EducatorDomain") %>' />
                    </div>
                </div>
            </asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
