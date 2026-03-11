using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using LearnSphere.DAL;

namespace LearnSphere.Forms.Assessment
{
    public partial class ViewAttempt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int attemptID = Convert.ToInt32(Session["AttemptID"]);

                // Point the iframe at the uploaded attempt PDF
                pdfFrame.Attributes["src"] = ResolveUrl("~/Attempts/" + attemptID + ".pdf");

                int marks = GetMarks(attemptID);

                if (marks == -1)
                {
                    // Not yet marked
                    pnlEnterMarks.Visible = true;
                    pnlMarked.Visible = false;
                }
                else
                {
                    // Already marked
                    pnlEnterMarks.Visible = false;
                    pnlMarked.Visible = true;
                    lblMarks.Text = marks.ToString();
                }
            }
        }

        private int GetMarks(int attemptID)
        {
            string sql = "SELECT Marks FROM Attempt WHERE AttemptID = @AttemptID";
            var p = new Dictionary<string, object>
            {
                { "@AttemptID", attemptID }
            };
            DataTable dt = DbHelper.ExecuteQuery(sql, p);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["Marks"]) : -1;
        }

        protected void btnSubmitMarks_Click(object sender, EventArgs e)
        {
            // Validate input
            if (!int.TryParse(txtMarks.Text.Trim(), out int marks) || marks < 0 || marks > 100)
            {
                lblMarksError.Text = "Please enter a valid mark between 0 and 100.";
                lblMarksError.Visible = true;
                return;
            }

            int attemptID = Convert.ToInt32(Session["AttemptID"]);

            string sql = "UPDATE Attempt SET Marks = @Marks WHERE AttemptID = @AttemptID";
            var p = new Dictionary<string, object>
            {
                { "@Marks", marks },
                { "@AttemptID", attemptID }
            };
            DbHelper.ExecuteNonQuery(sql, p);

            // Switch panels
            pnlEnterMarks.Visible = false;
            pnlMarked.Visible = true;
            lblMarks.Text = marks.ToString();
        }
    }
}
