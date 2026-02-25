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
                runat="server"
                ClientIDMode="Static" />

            <img src='<%= ResolveUrl("~/Images/eye.png") %>'
                class="eye-icon"
                onclick="togglePasswordLogin()"
                alt="Toggle Password" />
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
        <asp:Button 
            ID="btnCreate" 
            OnClick="btnLogin_Click"
            runat="server" 
            Text="Login" 
            CssClass="btn-primary" />
    </div>

    <asp:Label 
        ID="lblLoginStatus" 
        runat="server" 
        Visible="false"
        CssClass="error-text"
        EnableViewState="false" />

    <div class="form-component">
        <label>Not yet have an account? 
            <asp:LinkButton ID="lnkSignUp" runat="server" OnClick="lnkSignUp_Click">Sign Up</asp:LinkButton>
        </label>
    </div>

        <script>
            function togglePasswordLogin() {
                var txt = document.getElementById("txtPassword");
                if (txt) {
                    txt['type'] = (txt['type'] === 'password') ? 'text' : 'password';
                }
            }
        </script>
</asp:Content>
