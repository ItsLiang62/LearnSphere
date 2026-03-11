using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Web.UI;
using LearnSphere.DAL;
using System.Web.UI.WebControls;

namespace LearnSphere.Forms.Assessment
{
    public partial class ViewPaper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int paperID = Convert.ToInt32(Session["PaperID"]);
            pdfFrame.Attributes["src"] = ResolveUrl("~/Papers/" + paperID + ".pdf");

            string userType = Session["UserType"]?.ToString();

            if (userType == "Educator")
            {
                pnlLearner.Visible = false;
                pnlEducator.Visible = true;
                pnlAdmin.Visible = false;

                if (!IsPostBack)
                    LoadAttempts(paperID);
            }
            else if (userType == "Admin")
            {
                pnlLearner.Visible = false;
                pnlEducator.Visible = false;
                pnlAdmin.Visible = true;
                litPanelTitle.Text = string.Empty;
            }
            else
            {
                pnlLearner.Visible = true;
                pnlEducator.Visible = false;
                pnlAdmin.Visible = false;

                if (!IsPostBack)
                {
                    int accountID = Convert.ToInt32(Session["AccountID"]);
                    int learnerID = GetLearnerID(accountID);
                    bool attemptMade = CheckAttemptExists(paperID, learnerID);

                    pnlNoAttempt.Visible = !attemptMade;
                    pnlAttemptMade.Visible = attemptMade;

                    if (attemptMade)
                    {
                        int marks = GetMarks(paperID, learnerID);
                        lblMarks.Text = marks == -1 ? "Pending" : marks.ToString();
                    }
                }
            }
        }

        protected void btnDeletePaper_Click(object sender, EventArgs e)
        {
            int paperID = Convert.ToInt32(Session["PaperID"]);

            // Delete all attempt PDF files from disk
            DataTable attempts = DbHelper.ExecuteQuery(
                "SELECT AttemptID FROM Attempt WHERE PaperID = @PaperID",
                new Dictionary<string, object> { { "@PaperID", paperID } });

            foreach (DataRow row in attempts.Rows)
            {
                string attemptPath = Server.MapPath("~/Attempts/" + row["AttemptID"] + ".pdf");
                if (File.Exists(attemptPath))
                    File.Delete(attemptPath);
            }

            // Delete the paper PDF from disk
            string paperPath = Server.MapPath("~/Papers/" + paperID + ".pdf");
            if (File.Exists(paperPath))
                File.Delete(paperPath);

            // Delete paper from DB (cascades to Attempt, PaperTag)
            DbHelper.ExecuteNonQuery(
                "DELETE FROM Paper WHERE ID = @PaperID",
                new Dictionary<string, object> { { "@PaperID", paperID } });

            Session.Remove("PaperID");
            Response.Redirect("~/Forms/Assessment/Home.aspx");
        }

        private bool CheckAttemptExists(int paperID, int learnerID)
        {
            string sql = "SELECT COUNT(*) FROM Attempt WHERE PaperID = @PaperID AND LearnerID = @LearnerID";
            var p = new Dictionary<string, object>
            {
                { "@PaperID", paperID },
                { "@LearnerID", learnerID }
            };
            return DbHelper.ExecuteScalar(sql, p) > 0;
        }

        private int GetLearnerID(int accountID)
        {
            string sql = "SELECT ID FROM Learner WHERE AccountID = @AccountID";
            var p = new Dictionary<string, object>
            {
                { "@AccountID", accountID }
            };
            return DbHelper.ExecuteScalar(sql, p);
        }

        private int GetMarks(int paperID, int learnerID)
        {
            string sql = "SELECT Marks FROM Attempt WHERE PaperID = @PaperID AND LearnerID = @LearnerID";
            var p = new Dictionary<string, object>
            {
                { "@PaperID", paperID },
                { "@LearnerID", learnerID }
            };
            DataTable dt = DbHelper.ExecuteQuery(sql, p);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["Marks"]) : -1;
        }

        protected void btnSubmitAttempt_Click(object sender, EventArgs e)
        {
            if (!fuAttempt.HasFile)
            {
                lblUploadError.Text = "Please select a PDF file.";
                lblUploadError.Visible = true;
                return;
            }

            if (Path.GetExtension(fuAttempt.FileName).ToLower() != ".pdf")
            {
                lblUploadError.Text = "Only PDF files are allowed.";
                lblUploadError.Visible = true;
                return;
            }

            int paperID = Convert.ToInt32(Session["PaperID"]);
            int accountID = Convert.ToInt32(Session["AccountID"]);
            int learnerID = GetLearnerID(accountID);

            string sql = "INSERT INTO Attempt (PaperID, LearnerID) OUTPUT INSERTED.AttemptID VALUES (@PaperID, @LearnerID)";
            var p = new Dictionary<string, object>
            {
                { "@PaperID", paperID },
                { "@LearnerID", learnerID }
            };
            int attemptID = DbHelper.ExecuteScalar(sql, p);

            string fileName = attemptID + ".pdf";

            string attemptsDir = Server.MapPath("~/Attempts/");
            if (!Directory.Exists(attemptsDir))
                Directory.CreateDirectory(attemptsDir);

            string savePath = Path.Combine(attemptsDir, fileName);
            fuAttempt.SaveAs(savePath);

            pnlNoAttempt.Visible = false;
            pnlAttemptMade.Visible = true;
            lblMarks.Text = "Pending";
        }

        private void LoadAttempts(int paperID)
        {
            string sql = @"
                SELECT a.AttemptID, ac.Username AS LearnerName
                FROM Attempt a
                JOIN Learner l ON a.LearnerID = l.ID
                JOIN Account ac ON l.AccountID = ac.ID
                WHERE a.PaperID = @PaperID";

            var p = new Dictionary<string, object> { { "@PaperID", paperID } };
            DataTable dt = DbHelper.ExecuteQuery(sql, p);

            if (dt.Rows.Count == 0)
            {
                lblNoAttempts.Visible = true;
            }
            else
            {
                rptAttempts.DataSource = dt;
                rptAttempts.DataBind();
            }
        }

        protected void btnViewAttempt_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Session["AttemptID"] = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("~/Forms/Assessment/ViewAttempt.aspx");
        }
    }
}