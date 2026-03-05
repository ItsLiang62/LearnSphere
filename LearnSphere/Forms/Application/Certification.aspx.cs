using LearnSphere.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Forms.Application
{
    public partial class Certification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int appId = Convert.ToInt32(Request.QueryString["appId"]);
                string appInfoSql = @"SELECT ID, Username,
                                    Email, DomainName,
                                    CASE
                                    WHEN Completed = 0 THEN 'Pending'
                                    WHEN Completed = 1 THEN 'Completed'
                                    END AS Status
                                    FROM EducatorApplication
                                    WHERE ID = @id";

                DataTable appInfo = DbHelper.ExecuteQuery(
                    appInfoSql,
                    new Dictionary<string, object> { { "@id", appId } }
                );

                fvApplicationInfo.DataSource = appInfo;
                fvApplicationInfo.DataBind();

                string certPath = "/Certifications/" + appId + ".pdf";

                if (!System.IO.File.Exists(Server.MapPath(certPath)))
                {
                    ltCert.Text = $"<label class=\"error-text\">Error: Certification Not Found</label>";
                }
                else
                {
                    ltCert.Text = $"<iframe src='{certPath}'></iframe>";
                }

                if (appInfo
                    .AsEnumerable()
                    .FirstOrDefault()
                    .Field<string>("Status") == "Completed")
                {
                    actionContainer.Visible = false;
                }
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int appId = Convert.ToInt32(Request.QueryString["appId"]);
            string completeSql = @"UPDATE EducatorApplication
                                   SET Completed = 1
                                   WHERE ID = @id";
            string appInfoSql = @"SELECT Username,
                                  Email, DomainName, PasswordHash
                                  FROM EducatorApplication
                                  WHERE ID = @id";
            string newAccSql = @"INSERT INTO Account 
                              (Username, Email, PasswordHash)
                              VALUES
                              (@username, @email, @passwordHash);
                              SELECT CAST (SCOPE_IDENTITY() AS INT)";
            string newEducatorSql = @"INSERT INTO Educator 
                                      (AccountID, DomainName)
                                      VALUES
                                      (@accountId, @domainName)";

            try
            {
                DbHelper.ExecuteNonQuery(completeSql,
                    new Dictionary<string, object> { { "@id", appId } }
                );

                // Retrieve application information for account creation
                string username = DbHelper.ExecuteQuery(appInfoSql,
                    new Dictionary<string, object> { { "@id", appId } }
                ).AsEnumerable().FirstOrDefault().Field<string>("Username");

                string email = DbHelper.ExecuteQuery(appInfoSql,
                    new Dictionary<string, object> { { "@id", appId } }
                ).AsEnumerable().FirstOrDefault().Field<string>("Email");

                string passwordHash = DbHelper.ExecuteQuery(appInfoSql,
                    new Dictionary<string, object> { { "@id", appId } }
                ).AsEnumerable().FirstOrDefault().Field<string>("PasswordHash");

                string domain = DbHelper.ExecuteQuery(appInfoSql,
                    new Dictionary<string, object> { { "@id", appId } }
                ).AsEnumerable().FirstOrDefault().Field<string>("DomainName");

                // Create account
                int accId = DbHelper.ExecuteScalar(newAccSql,
                    new Dictionary<string, object> {
                    { "@username", username},
                    { "@email", email },
                    { "@passwordHash", passwordHash }
                    });

                // Link account to educator
                DbHelper.ExecuteNonQuery(newEducatorSql,
                    new Dictionary<string, object> {
                    { "@accountId", accId },
                    { "@domainName", domain }
                    });

                actionContainer.Visible = false;
            } 
            catch (SqlException ex)
            {
                ltCert.Text = $"<label class=\"error-text\">{ex.Message}</label>";
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            int appId = Convert.ToInt32(Request.QueryString["appId"]);

            string delAppSql = @"DELETE FROM EducatorApplication
                                 WHERE ID = @id";

            try
            {
                DbHelper.ExecuteNonQuery(delAppSql,
                    new Dictionary<string, object> { { "@id", appId } }
                    );

                string certPath = Server.MapPath(string.Format("~/Certifications/{0}", appId + ".pdf"));
                if (File.Exists(certPath))
                {
                    File.Delete(certPath);
                }

                ltCert.Text = $"<label class=\"error-text\">Application successfully rejected. Please return to home page.</label>";
                actionContainer.Visible = false;
            }
            catch (SqlException ex)
            {
                ltCert.Text = $"<label class=\"error-text\">SqlException: {ex.Message}</label>";
            }
            catch (IOException ex)
            {
                ltCert.Text = $"<label class=\"error-text\">IOException: {ex.Message}</label>";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Application/Home.aspx");
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Profile/Profile.aspx");
        }
    }
}