<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPaper.aspx.cs" Inherits="LearnSphere.Forms.Assessment.ViewPaper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Paper</title>
    <link rel="stylesheet" runat="server" href="~/CSS/Assessment/ViewPaper.css"/>
</head>
<script>
    function toggleAttempts(btn) {
        const panel = document.getElementById('attemptsListPanel');
        const arrow = btn.querySelector('.toggle-arrow');
        const isOpen = panel.style.display !== 'none';
        panel.style.display = isOpen ? 'none' : 'block';
        arrow.style.transform = isOpen ? 'rotate(0deg)' : 'rotate(180deg)';
    }
</script>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Forms/Assessment/Home.aspx">
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
                    <iframe id="pdfFrame" runat="server" class="pdf-frame"
                        src='<%# "~/Papers/" + Session["PaperID"] + ".pdf" %>'>
                    </iframe>
                </div>

            </div>

            <!-- right panel -->
            <div class="side-panel">

                <div class="panel-card">

                    <asp:Literal ID="litPanelTitle" runat="server" Text='<div class="panel-title">Attempt</div>'/>

                    <!-- learner state -->
                    <asp:Panel ID="pnlLearner" runat="server">
                        <asp:Panel ID="pnlNoAttempt" runat="server">
                            <div class="upload-box">
                                <label class="upload-label">Upload Attempt (PDF)</label>
                                <asp:FileUpload ID="fuAttempt" runat="server" CssClass="file-input" Accept=".pdf"/>
                                <asp:Label ID="lblUploadError" runat="server" CssClass="error-text" Visible="false"/>
                            </div>
                            <asp:Button
                                ID="btnSubmitAttempt"
                                runat="server"
                                Text="Submit Attempt"
                                CssClass="panel-btn submit-btn"
                                OnClick="btnSubmitAttempt_Click"/>
                        </asp:Panel>
                        <asp:Panel ID="pnlAttemptMade" runat="server" Visible="false">
                            <div class="status-text">Attempt Submitted</div>
                            <div class="marks-box">
                                Marks: <asp:Label ID="lblMarks" runat="server"/>
                            </div>
                        </asp:Panel>
                    </asp:Panel>

                    <!-- educator state -->
                    <asp:Panel ID="pnlEducator" runat="server">
                        <button type="button" class="panel-btn view-btn" onclick="toggleAttempts(this)">
                            View Attempts <span class="toggle-arrow">▼</span>
                        </button>
                        <div class="attempts-list" id="attemptsListPanel" style="display:none;">
                            <asp:Repeater ID="rptAttempts" runat="server">
                                <ItemTemplate>
                                    <div class="attempt-row">
                                        <span class="attempt-name"><%# Eval("LearnerName") %></span>
                                        <asp:Button
                                            runat="server"
                                            Text="View"
                                            CssClass="view-attempt-btn"
                                            CommandArgument='<%# Eval("AttemptID") %>'
                                            OnClick="btnViewAttempt_Click"/>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Label ID="lblNoAttempts" runat="server" CssClass="no-attempts-text" Visible="false" Text="No attempts yet."/>
                        </div>
                        <asp:Button
                            ID="btnDeletePaper"
                            runat="server"
                            Text="Delete Paper"
                            CssClass="panel-btn delete-btn"
                            OnClick="btnDeletePaper_Click"/>
                    </asp:Panel>

                    <!-- admin state -->
                    <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                        <asp:Button
                            ID="btnAdminDeletePaper"
                            runat="server"
                            Text="Delete Paper"
                            CssClass="panel-btn delete-btn"
                            OnClick="btnDeletePaper_Click"/>
                    </asp:Panel>

                </div>

            </div>

        </div>
    </form>
</body>
</html>
