<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAttempt.aspx.cs" Inherits="LearnSphere.Forms.Assessment.ViewAttempt" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Attempt</title>
    <link rel="stylesheet" runat="server" href="~/CSS/Assessment/ViewAttempt.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Forms/Assessment/ViewPaper.aspx">
                <img runat="server" src="~/Images/back.png" class="nav-icon"/>
            </asp:HyperLink>
            <div class="logo">
                <img runat="server" src="~/Images/logo-dark-mode.png"/>
            </div>
            <asp:HyperLink ID="lnkProfile" runat="server" NavigateUrl="~/Forms/Profile/Profile.aspx">
                <img runat="server" src="~/Images/profile.png" class="nav-icon"/>
            </asp:HyperLink>
        </div>
        <div class="page-container">
            <!-- PDF viewer -->
            <div class="viewer-container">
                <div class="pdf-viewer">
                    <iframe id="pdfFrame" runat="server" class="pdf-frame"></iframe>
                </div>
            </div>
            <!-- right panel -->
            <div class="side-panel">
                <div class="panel-card">
                    <div class="panel-title">Marks</div>

                    <!-- Not yet marked: show input -->
                    <asp:Panel ID="pnlEnterMarks" runat="server">
                        <div class="marks-input-box">
                            <label class="marks-label">Enter Marks (0 – 100)</label>
                            <asp:TextBox
                                ID="txtMarks"
                                runat="server"
                                CssClass="marks-input"
                                placeholder="e.g. 75"/>
                            <asp:Label ID="lblMarksError" runat="server" CssClass="error-text" Visible="false"/>
                        </div>
                        <asp:Button
                            ID="btnSubmitMarks"
                            runat="server"
                            Text="Submit Marks"
                            CssClass="panel-btn submit-btn"
                            OnClick="btnSubmitMarks_Click"/>
                    </asp:Panel>

                    <!-- Already marked: show result -->
                    <asp:Panel ID="pnlMarked" runat="server" Visible="false">
                        <div class="marked-badge">Marked</div>
                        <div class="marks-box">
                            Marks: <asp:Label ID="lblMarks" runat="server"/>
                            <span class="marks-suffix">/ 100</span>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
