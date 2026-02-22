using LearnSphere.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Forms.Registration
{
    public partial class EduApplc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable domains = DbHelper.ExecuteQuery(
                @"SELECT ID, DomainName FROM Domain", null
                );

                ddlDomain.DataSource = domains;
                ddlDomain.DataTextField = "DomainName";
                ddlDomain.DataBind();
            } 
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            string domain = ddlDomain.SelectedValue;
            string username = Request.QueryString["username"];
            string email = Request.QueryString["email"];
            string password = Session["Password"].ToString();

            string insertNewAppSql = @"INSERT INTO EducatorApplication
                                 (Username, Email, PasswordHash, DomainName)
                                 VALUES
                                 (@username, @email, @password, @domain);
                                 SELECT CAST(SCOPE_IDENTITY() AS INT);";

            lblApplyStatus.Visible = true;
            try
            {
                int newAppId = DbHelper.ExecuteScalar(
                    insertNewAppSql,
                    new Dictionary<string, object>
                    {
                        { "@username", username},
                        { "@email", email },
                        { "@password", password},
                        { "@domain", domain }
                    }
                );

                if (fileUpload.HasFile)
                {
                    string folderPath = Server
                        .MapPath("~/Pending-Application-Certifications/");

                    if (!System.IO.Directory.Exists(folderPath)) System.IO.Directory.CreateDirectory(folderPath);

                    string extension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                    if (extension != ".pdf") throw new Exception("Only .pdf files allowed");

                    string fileName = newAppId + extension;
                    string savePath = System.IO.Path.Combine(folderPath, fileName);
                    fileUpload.SaveAs(savePath);
                }
                lblApplyStatus.CssClass = "success-text";
                lblApplyStatus.Text = "Application submitted successfully";
            } catch (Exception ex)
            {
                lblApplyStatus.CssClass = "error-text";
                lblApplyStatus.Text = ex.Message;
            }
            
        }
    }
}