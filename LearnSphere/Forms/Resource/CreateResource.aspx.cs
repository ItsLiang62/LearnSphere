using LearnSphere.DAL;
using System;
using System.Data;

namespace LearnSphere.Forms.Resource
{
    public partial class CreateResource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable domains = ResourceDAL.GetAllDomains();
                ddlDomain.DataSource = domains;
                ddlDomain.DataTextField = "DomainName";
                ddlDomain.DataValueField = "DomainName";
                ddlDomain.DataBind();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Session["AccountID"] == null)
            {
                lblMessage.Text = "Please login first.";
                return;
            }

            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string locator = txtLocator.Text.Trim();
            string category = ddlCategory.SelectedValue;
            string domainName = ddlDomain.SelectedValue;

            int publicationYear;
            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(locator))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            if (!int.TryParse(txtYear.Text.Trim(), out publicationYear))
            {
                lblMessage.Text = "Publication year is invalid.";
                return;
            }

            if (publicationYear < 1900 || publicationYear > 2050)
            {
                lblMessage.Text = "Publication year must be between 1900 and 2050.";
                return;
            }

            int sharerId = Convert.ToInt32(Session["AccountID"]);

            try
            {
                ResourceDAL.InsertResource(title, author, publicationYear, category, locator, domainName, sharerId);
                Response.Redirect("~/Forms/Resource/Home.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Failed to create resource: " + ex.Message;
            }
        }
    }
}