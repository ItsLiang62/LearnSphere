<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AssessmentHome.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LearnSphere.Forms.Assessment.Educator.Home" %>

<asp:Content ID="CreateAssessment" ContentPlaceHolderID="CreateAssessmentPlaceholder" runat="server">
    <asp:LinkButton runat="server" CssClass="action-group">
        <img src="~/Images/add.png" runat="server" class="action-group-component"/>
        <asp:Label ID=lblCreatePaper runat="server" Text="Create Paper"/>
    </asp:LinkButton>
</asp:Content>

<asp:Content ID="NestedMineFilters" ContentPlaceHolderID="NestedMineFiltersPlaceholder" runat="server">
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
</asp:Content>