<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Master/Registration.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="LearnSphere.Forms.Registration.Registration" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">


        <h2 class="form-component">Sign Up for LearnSphere</h2>

        <div class="form-component">
            <label>I am a...</label>
            
            <asp:DropDownList 
                ID="ddlUserType" 
                runat="server" 
                CssClass="input-field dropdown-style">
                <asp:ListItem Text="Learner" Value="Learner"/>
                <asp:ListItem Text="Educator" Value="Educator"/>
            </asp:DropDownList>
        </div>
      
        <div class="form-component">
            <label>Username</label>

            <asp:TextBox 
                ID="txtUsername" 
                runat="server" 
                CssClass="input-field"/>

            <asp:Label 
                ID="lblUsernameError" 
                runat="server" 
                CssClass="error-text" 
                Visible="false"
                EnableViewState="false" />

            <asp:RequiredFieldValidator 
                ID="rfvName" 
                runat="server" 
                ControlToValidate="txtUsername"
                ErrorMessage="Username is required" 
                Display="Dynamic" 
                CssClass="error-text"/>
        </div>

        <div class="form-component">
            <label>Email</label>
            
            <asp:TextBox 
                ID="txtEmail" 
                runat="server" 
                CssClass="input-field" 
                TextMode="Email"/>

            <asp:Label 
                ID="lblEmailError" 
                runat="server" 
                CssClass="error-text" 
                Visible="false"
                EnableViewState="false" />
            
            <asp:RequiredFieldValidator 
                ID="rfvEmail" 
                runat="server" 
                ControlToValidate="txtEmail"
                ErrorMessage="Email is required"
                Display="Dynamic"
                CssClass="error-text" />
        </div>
        
        <div class="form-component">
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

            <asp:Label 
                ID="lblPasswordError" 
                runat="server" 
                CssClass="error-text" 
                Visible="false"
                EnableViewState="false" />

            <asp:RequiredFieldValidator 
                ID="rfvPassword" 
                runat="server" 
                ControlToValidate="txtPassword"
                ErrorMessage="Password is required"
                Display="Dynamic"
                CssClass="error-text" />
        </div>

        <div class="form-component">
            <asp:Button 
                ID="btnCreate" 
                runat="server" 
                Text="Create Account" 
                CssClass="btn-primary"
                OnClick="btnCreate_Click"/>
        </div>

        <asp:Label 
            ID="lblCreateStatus" 
            runat="server" 
            Visible="false"
            EnableViewState="false" />

        <div class="form-component">
            <label>Already have an account? <a href="#">Sign In</a></label>
        </div>
    </div>

    <script>
        function togglePassword() {
            var txt = document.getElementById('<%= txtPassword.ClientID %>');
            txt.type = txt.type == 'password' ? 'text' : 'password';
        }
    </script>
</asp:Content>

