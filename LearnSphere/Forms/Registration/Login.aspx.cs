using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt.Net;
using LearnSphere.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace LearnSphere.Forms.Registration
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserType"] = "Visitor";
            Session["AccountID"] = null;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            lblLoginStatus.Visible = false;

            string usernameOrEmail = txtUsernameEmail.Text;

            string accountSql = @"SELECT ID, PasswordHash
                                        FROM Account
                                        WHERE Username = @usernameOrEmail
                                        OR Email = @usernameOrEmail";

            DataRow account = DbHelper.ExecuteQuery(
                accountSql,
                new Dictionary<string, object>
                {
                    { "@usernameOrEmail", usernameOrEmail }
                }
            ).AsEnumerable().FirstOrDefault();

            if (account != null &&
                BCrypt.Net.BCrypt.Verify(
                    txtPassword.Text,
                    account.Field<string>("PasswordHash")))
            {
                Session["AccountID"] = account.Field<int>("ID");

                string learnerAccountSql = @"SELECT ID, AccountID
                                             FROM Learner
                                             WHERE AccountID = @accountID";
                string educatorAccountSql = @"SELECT ID, AccountID, DomainName
                                              FROM Educator
                                              WHERE AccountID = @accountID";
                string adminAccountSql = @"SELECT ID, AccountID
                                              FROM Administrator
                                              WHERE AccountID = @accountID";

                try
                {
                    DataRow learner = DbHelper.ExecuteQuery(
                        learnerAccountSql,
                        new Dictionary<string, object> { { "@accountID", Session["AccountID"] } }
                    ).AsEnumerable().FirstOrDefault();

                    DataRow educator = DbHelper.ExecuteQuery(
                        educatorAccountSql,
                        new Dictionary<string, object> { { "@accountID", Session["AccountID"] } }
                    ).AsEnumerable().FirstOrDefault();

                    DataRow admin = DbHelper.ExecuteQuery(
                        adminAccountSql,
                        new Dictionary<string, object> { { "@accountID", Session["AccountID"] } }
                    ).AsEnumerable().FirstOrDefault();

                    if (learner != null)
                    {
                        Session["UserType"] = "Learner";
                    }
                    else if (educator != null)
                    {
                        Session["UserType"] = "Educator";
                    }
                    else if (admin != null)
                    {
                        Session["UserType"] = "Admin";
                        
                    }
                    Response.Redirect($"~/Forms/Resource/Home.aspx");
                } catch (SqlException ex)
                {
                    lblLoginStatus.Visible = true;
                    lblLoginStatus.Text = ex.Message;
                }
            } else
            {
                lblLoginStatus.Visible = true;
                lblLoginStatus.Text = "Incorrect credentials";
            }
        }

        protected void lnkSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Registration/Registration.aspx");
        }
    }
}