using LearnSphere.DAL;
using System;
using System.Collections.Generic;
using System.IO;

namespace LearnSphere.Forms.Assessment
{
    public partial class CreatePaper : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblFileError.Text = "";

            if (!filePaper.HasFile)
            {
                lblFileError.Text = "Please upload a PDF file.";
                return;
            }

            string extension = Path.GetExtension(filePaper.FileName).ToLower();

            if (extension != ".pdf")
            {
                lblFileError.Text = "Only PDF files are allowed.";
                return;
            }

            string title = txtTitle.Text.Trim();
            int accountID = Convert.ToInt32(Session["AccountID"]);

            // Insert Paper FIRST
            string sql = @"INSERT INTO Paper (Title, AccountID)
                           VALUES (@Title, @AccountID);
                           SELECT SCOPE_IDENTITY();";

            var parameters = new Dictionary<string, object>()
            {
                {"@Title", title},
                {"@AccountID", accountID}
            };

            int paperID = DbHelper.ExecuteScalar(sql, parameters);

            // Save PDF using paperID
            string folder = Server.MapPath("~/Papers/");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, paperID + ".pdf");

            filePaper.SaveAs(filePath);

            // Insert Tags
            string[] tags = txtTags.Text.Split(',');

            foreach (string tag in tags)
            {
                string cleanTag = tag.Trim();

                if (cleanTag.Length == 0)
                    continue;

                string tagSql = @"INSERT INTO PaperTag (TagName, PaperID)
                                  VALUES (@TagName, @PaperID)";

                var tagParams = new Dictionary<string, object>()
                {
                    {"@TagName", cleanTag},
                    {"@PaperID", paperID}
                };

                DbHelper.ExecuteNonQuery(tagSql, tagParams);
            }

            Response.Redirect("~/Forms/Assessment/Home.aspx");
        }
    }
}