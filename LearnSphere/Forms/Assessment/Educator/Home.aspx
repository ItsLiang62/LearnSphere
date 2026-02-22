<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LearnSphere.Forms.Assessment.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="CreateAssessment" ContentPlaceHolderID="ActionGroupPlaceholder" runat="server">
    <div class="rightmost-action-group">
        <img src="~/Images/add.png" runat="server" class="action-group-component"/>
        <label>Create Paper</label>
    </div>
</asp:Content>
<asp:Content ID="Papers" ContentPlaceHolderID="BoxesPlaceholder" runat="server">
    <asp:Repeater ID="BoxRepeater" runat="server">
        <ItemTemplate>
            <div class="box">
                <label class="box-primary-text"><%# Eval("Title") %></label>
                <div class="box-group">
                    <asp:Repeater ID="TagRepeater" runat="server">
                        <ItemTemplate>
                            <span class="tag"><%# Eval("PaperTag") %></span>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

<asp:Content ID="PaperSortOptions" ContentPlaceHolderID="SortDropdownItemsPlaceholder" runat="server">
    <asp:LinkButton ID="TitleSort" runat="server" CssClass="dropdown-item" Text="Title (A-Z)" OnClick="TitleSort_Click" />
    <asp:LinkButton ID="EducatorSort" runat="server" CssClass="dropdown-item" Text="Educator (A-Z)" OnClick="EducatorSort_Click" />
    <asp:LinkButton ID="NoneSort" runat="server" CssClass="dropdown-item" Text="None" OnClick="NoneSort_Click" />
</asp:Content>

<asp:Content ID="FilterSortOptions" ContentPlaceHolderID="FilterDropdownItemsPlaceholder" runat="server">
    <asp:LinkButton ID="MineFilter" runat="server" CssClass="dropdown-item" Text="My Papers" OnClick="MineFilter_Click" />
    <asp:LinkButton ID="NoneFilter" runat="server" CssClass="dropdown-item" Text="None" OnClick="NoneFilter_Click" />
</asp:Content>
