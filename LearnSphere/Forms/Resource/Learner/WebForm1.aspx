<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ResourcesHome.master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LearnSphere.Forms.Resource.Learner.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShareResourcePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedMineFiltersPlaceholder" runat="server">
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
