using LearnSphere.DAL;
using System;
using System.Data;

namespace LearnSphere.Forms.Resource
{
    public partial class ResourceDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("~/Forms/Resource/Home.aspx");
                    return;
                }

                int id;
                if (!int.TryParse(Request.QueryString["id"], out id))
                {
                    Response.Redirect("~/Forms/Resource/Home.aspx");
                    return;
                }

                DataTable dt = ResourceDAL.GetResourceById(id);
                if (dt.Rows.Count == 0)
                {
                    Response.Redirect("~/Forms/Resource/Home.aspx");
                    return;
                }

                DataRow row = dt.Rows[0];
                lblTitle.Text = row["Title"].ToString();
                lblAuthor.Text = row["Author"].ToString();
                lblYear.Text = row["PublicationYear"].ToString();
                lblCategory.Text = row["Category"].ToString();
                lblDomain.Text = row["DomainName"].ToString();
                lnkLocator.Text = row["Locator"].ToString();
                lnkLocator.NavigateUrl = row["Locator"].ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Resource/Home.aspx");
        }
    }
}