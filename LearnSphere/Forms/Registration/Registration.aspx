<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Master/Registration.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="LearnSphere.Forms.Registration.WebForm1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="registration-form">
        <h2>Sign Up for LearnSphere</h2>

        <div class="input-group">
            <label>I am a...</label>
            
            <asp:DropDownList 
                ID="ddlUserType" 
                runat="server" 
                CssClass="input-field dropdown-style">
                <asp:ListItem Text="Learner" Value="Learner"/>
                <asp:ListItem Text="Educator" Value="Educator"/>
            </asp:DropDownList>
        </div>
      
        <div class="input-group">
            <label>Username</label>

            <asp:TextBox 
                ID="txtUsername" 
                runat="server" 
                CssClass="input-field"/>

            <asp:RequiredFieldValidator 
                ID="rfvName" 
                runat="server" 
                ControlToValidate="txtUsername"
                ErrorMessage="Username is required" 
                Display="Dynamic" 
                CssClass="error-text"/>
        </div>

        <div class="input-group">
            <label>Email</label>
            
            <asp:TextBox 
                ID="txtEmail" 
                runat="server" 
                CssClass="input-field" 
                TextMode="Email"/>
            
            <asp:RequiredFieldValidator 
                ID="rfvEmail" 
                runat="server" 
                ControlToValidate="txtEmail"
                ErrorMessage="Email is required"
                Display="Dynamic"
                CssClass="error-text" />
        </div>
        
        <div class="input-group">
            <label>Password</label>
    
            <div class="password-wrapper">
                <asp:TextBox 
                    ID="txtPassword" 
                    runat="server" 
                    CssClass="input-field" 
                    TextMode="Password"/>

                <img src="~/Images/eye.png" 
                     class="eye-icon" 
                     onclick="togglePassword()" 
                     alt="Toggle Password" 
                     runat="server"/>
            </div>

            <asp:RequiredFieldValidator 
                ID="rfvPassword" 
                runat="server" 
                ControlToValidate="txtPassword"
                ErrorMessage="Password is required"
                Display="Dynamic"
                CssClass="error-text" />
        </div>


        <asp:Button ID="btnCreate" runat="server" Text="Create Account" CssClass="btn-primary" />

        <div class="footer-links">
            Already have an account? <a href="#">Sign In</a>
        </div>
    </div>

    <script>
        function togglePassword() {
            var txt = document.getElementById('<%= txtPassword.ClientID %>');
            txt.type = txt.type == 'password' ? 'text' : 'password';
        }
    </script>
</asp:Content>

