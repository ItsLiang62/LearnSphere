<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Registration.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LearnSphere.Forms.Registration.Login" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="form-component">Login to LearnSphere</h2>

    <div class="form-component">
        <label>Username or email</label>
        <asp:TextBox
            ID="txtUsernameEmail"
            CssClass="input-field"
            runat="server"/>

        <asp:RequiredFieldValidator 
            ID="rfvUsernameEmail" 
            runat="server" 
            ControlToValidate="txtUsernameEmail"
            ErrorMessage="Username or email is required"
            Display="Dynamic"
            CssClass="error-text" />
    </div>

    <div class="form-component">
        <label>Password</label>

        <div class="password-wrapper">
            <asp:TextBox
                ID="txtPassword"
                TextMode="Password"
                CssClass="input-field"
                runat="server"/>

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

    <div class="form-component">
        <asp:Button ID="btnCreate" runat="server" Text="Login" CssClass="btn-primary" />
    </div>

    <div class="form-component">
        <label>Not yet have an account? <a href="#">Sign Up</a></label>
        <label>Forgot your password? <a href="#">Reset Password</a></label>
    </div>

    <script>
        function togglePassword() {
            var txt = document.getElementById('<%= txtPassword.ClientID %>');
            txt.type = txt.type == 'password' ? 'text' : 'password';
        }
    </script>
</asp:Content>
