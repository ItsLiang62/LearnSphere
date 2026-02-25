using LearnSphere.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Forms.Profile
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblSaveStatus.Visible = false;

                int accountId = Convert.ToInt32(Session["AccountID"]);

                if (!Session["UserType"].Equals("Educator"))
                {
                    lblFieldDomain.Visible = false;
                    lblValueDomain.Visible = false;
                }

                lblUserType.Text = Session["UserType"] as string;
                lblFieldUserTypeID.Text = lblUserType.Text + " ID";

                string profileSql =
                    @"SELECT a.ID, a.Username, a.Email, e.DomainName,
                  COALESCE(l.ID, e.ID, ad.ID) 
                  AS UserTypeID
                  FROM Account a
                  LEFT JOIN Learner l ON a.ID = l.AccountID
                  LEFT JOIN Educator e ON a.ID = e.AccountID
                  LEFT JOIN Administrator ad ON a.ID = ad.AccountID
                  WHERE a.ID = @accountId";

                DataTable profile = DbHelper.ExecuteQuery(
                    profileSql, new Dictionary<string, object>
                    { { "@accountId", accountId } });

                if (profile.Rows.Count > 0)
                {
                    DataRow row = profile.Rows[0];

                    txtUsername.Text = row["Username"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                    lblValueDomain.Text = row["DomainName"]?.ToString();
                    lblAccountID.Text = row["ID"].ToString();
                    lblValueUserTypeID.Text = row["UserTypeID"].ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblSaveStatus.Visible = true;

            string updateSql = @"UPDATE Account SET
                                 Username = @username, Email = @email
                                 WHERE ID = @id";

            string dupAccountCountSql = @"SELECT COUNT(*)
                                       FROM Account
                                       WHERE (Username = @username
                                       OR Email = @email) AND
                                       ID != @id";
            try
            {
                int accountCount = DbHelper.ExecuteScalar(
                    dupAccountCountSql,
                    new Dictionary<string, object>
                    {
                        { "@username", txtUsername.Text },
                        { "@email", txtEmail.Text },
                        { "@id", Session["AccountID"] }
                    });

                if (accountCount > 0)
                {
                    
                    lblSaveStatus.CssClass = "error-text";
                    lblSaveStatus.Text = "Username or email already taken";
                } else
                {
                    DbHelper.ExecuteNonQuery(updateSql,
                    new Dictionary<string, object>
                    {
                        { "@id", Session["AccountID"] },
                        { "@username", txtUsername.Text },
                        { "@email", txtEmail.Text }
                    });
                    lblSaveStatus.CssClass = "success-text";
                    lblSaveStatus.Text = "Profile updated successfully";
                }
            } catch (Exception ex)
            {
                lblSaveStatus.CssClass = "error-text";
                lblSaveStatus.Text = ex.Message;
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Registration/Login.aspx"); 
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Resource/Home.aspx");
        }
    }
}