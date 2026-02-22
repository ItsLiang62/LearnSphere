<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Registration.Master" AutoEventWireup="true" CodeBehind="EduApplc.aspx.cs" Inherits="LearnSphere.Forms.Registration.EduApplc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2 class="form-component">Apply as an Educator</h2>

        <div class="form-component">
            <label>Domain</label>

            <asp:DropDownList
                ID="ddlDomain"
                CssClass="input-field dropdown-style"
                runat="server">
            </asp:DropDownList>
        </div>

        <div class="form-component">
            <label>Certifications (PDF)</label>

            <label for="fileUpload" class="upload-box">
                <img src="~/Images/upload.png" runat="server"/>
                <span id="lblFiles"></span>
            </label>
            

            <asp:FileUpload 
                ID="fileUpload"
                accept=".pdf"
                CssClass="hidden-upload"
                onchange="displayFileName(this)"
                runat="server"
                ClientIDMode="Static"/>

            <asp:RequiredFieldValidator 
                ID="rfvUpload" 
                runat="server" 
                ControlToValidate="fileUpload"
                ErrorMessage="Please upload a file" 
                Display="Dynamic" 
                CssClass="error-text"/>
        </div>

         <div class="form-component">
             <asp:Button 
                 ID="btnApply" 
                 runat="server" 
                 Text="Apply" 
                 CssClass="btn-primary"
                 OnClick="btnApply_Click"/>
         </div>

        <asp:Label 
            ID="lblApplyStatus" 
            runat="server" 
            Visible="false"
            EnableViewState="false" />
 
         <div class="form-component">
             <label>We will notify you of your application status via email within 60 minutes as part of the verification process.</label>
         </div>
    </div>

    <script type="text/javascript">
        function displayFileName(input) {
            if (input.files && input.files.length > 0) {
                var file = input.files[0];
                if (file.type == "application/pdf") {
                    document.getElementById('lblFiles').innerText = file.name;
                } else {
                    input.value = "";
                }
            }
        }
    </script>
</asp:Content>
