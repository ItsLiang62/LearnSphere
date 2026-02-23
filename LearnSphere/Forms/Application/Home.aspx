<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LearnSphere.Forms.Application.Home" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        .action-container {
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="ApplicationStatusFiltersColumn" ContentPlaceHolderID="ApplicationStatusFiltersColumnPlaceholder" runat="server">
    <asp:Panel ID="colApplicationStatusFilters" runat="server" CssClass="dropdown-column">
        <asp:LinkButton 
            CssClass="dropdown-item"
            Text="Pending"
            CommandArgument="Pending"
            OnCommand="ApplicationStatusFilter_Command"
            runat="server"/>
        <asp:LinkButton 
            CssClass="dropdown-item"
            Text="Completed"
            CommandArgument="Completed"
            OnCommand="ApplicationStatusFilter_Command"
            runat="server"/>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Applications" ContentPlaceHolderID="BoxesPlaceholder" runat="server">
    <asp:Repeater ID="BoxRepeater" runat="server">
        <ItemTemplate>
            <asp:LinkButton ID="lnkBox" runat="server" CssClass="box" CommandArgument='<%# Eval("EducatorApplicationID") %>' OnCommand="Box_Command">
                
                <label class="box-secondary-text">
                    <%# Eval("DomainName") %>
                </label>
                <label class="box-primary-text">
                    <%# Eval("Username") %>
                </label>
                <label class="box-secondary-text">
                    <%# Eval("Email") %>
                </label>

                <div class="tag-group">
                    <span class="tag"><%# Eval("Status") %></span>
                </div>

            </asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
