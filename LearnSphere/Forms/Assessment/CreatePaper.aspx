<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePaper.aspx.cs" Inherits="LearnSphere.Forms.Assessment.CreatePaper" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Create Paper</title>
    <link href="~/CSS/Assessment/CreatePaper.css" rel="stylesheet" />
</head>

<body>

<form id="form1" runat="server">

<!-- HEADER -->
<div class="header">

    <a href="~/Forms/Assessment/Home.aspx">
        <img runat="server" src="~/Images/back.png" class="nav-icon"/>
    </a>

    <div class="logo">
        <img runat="server" src="~/Images/logo-dark-mode.png"/>
    </div>

    <a href="~/Forms/Profile/Profile.aspx">
        <img runat="server" src="~/Images/profile.png" class="nav-icon"/>
    </a>

</div>

<div class="center-container">

<div class="paper-card">

<h2>Create Assessment Paper</h2>

<div class="form-group">
<label>Paper Title</label>

<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>

<asp:RegularExpressionValidator
ID="valTitleRegex"
runat="server"
ControlToValidate="txtTitle"
ValidationExpression="^[A-Za-z0-9 ]+$"
ErrorMessage="Title must contain alphabets, numbers and spaces only."
CssClass="error"
Display="Dynamic" />

<asp:RequiredFieldValidator
ID="valTitleRequired"
runat="server"
ControlToValidate="txtTitle"
ErrorMessage="Title cannot be empty."
CssClass="error"
Display="Dynamic" />

</div>


<div class="form-group">

<label>Upload Paper (PDF)</label>

<asp:FileUpload ID="filePaper" runat="server" />

<asp:Label ID="lblFileError" runat="server" CssClass="error"></asp:Label>

</div>


<div class="form-group">

<label>Tags (comma separated)</label>

<asp:TextBox ID="txtTags" runat="server"></asp:TextBox>

</div>


<asp:Button
ID="btnSubmit"
runat="server"
Text="Create Paper"
CssClass="submit-btn"
OnClick="btnSubmit_Click"
/>

</div>

</div>

</form>

</body>
</html>