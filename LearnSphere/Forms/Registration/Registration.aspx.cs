using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LearnSphere.DAL;
using BCrypt.Net;


namespace LearnSphere.Forms.Registration
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (!Page.IsValid)
                return;

            // Retrieve registration information
            string userType = ddlUserType.SelectedValue;
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Validation
            lblUsernameError.Visible = false;
            lblEmailError.Visible = false;
            lblPasswordError.Visible = false;

            string usernameCountSql = @"SELECT COUNT(*) 
                                        FROM Account 
                                        WHERE Username = @username";
            int usernameCount = DbHelper.ExecuteScalar(
                usernameCountSql,
                new Dictionary<string, object>{{ "@username", username }}
            );
            bool usernameExist = usernameCount > 0;

            string emailCountSql = @"SELECT COUNT(*)
                                     FROM Account
                                     WHERE Email = @email";
            int emailCount = DbHelper.ExecuteScalar(
                emailCountSql,
                new Dictionary<string, object> { { "@email", email } }
            );
            bool emailExist = emailCount > 0;

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
            {
                lblUsernameError.Visible = true;
                lblUsernameError.Text = "Must contain only alphabets and numbers";
            } else if (usernameExist)
            {
                lblUsernameError.Visible = true;
                lblUsernameError.Text = "Username is already taken.";
            }

            if (emailExist)
            {
                lblEmailError.Visible = true;
                lblEmailError.Text = "Email is already taken.";
            }

            if (password.Length < 4)
            {
                lblPasswordError.Visible = true;
                lblPasswordError.Text = "Must contain at least 4 characters";
            }

            if (!lblPasswordError.Visible &&
                !lblEmailError.Visible &&
                !lblUsernameError.Visible)
            {
                lblCreateStatus.Visible = true;

                if (ddlUserType.SelectedValue == "Educator")
                {
                    // Prevent duplicate pending educator application
                    string dupPendingCountSql = @"SELECT COUNT(*)
                                           FROM EducatorApplication
                                           WHERE Completed = 0
                                           AND (Username = @username
                                           OR Email = @email)";
                    int dupPendingCount = DbHelper.ExecuteScalar(
                        dupPendingCountSql,
                        new Dictionary<string, object>
                        {
                            { "@username", username },
                            { "@email", email }
                        }
                    );

                    bool dupPending = dupPendingCount > 0;

                    if (dupPending)
                    {
                        lblCreateStatus.CssClass = "error-text";
                        lblCreateStatus.Text =
                        "Username or email already has pending educator application";
                        return;
                    }

                    // Redirect to educator application page
                    Session["Password"] = BCrypt.Net.BCrypt.HashPassword(password);
                    Response.Redirect($@"EduApplc.aspx?username={username}&email={email}");
                    return;
                }
                lblCreateStatus.CssClass = "success-text";
                lblCreateStatus.Text = "Account successfully created";
            }
        }
    }
}