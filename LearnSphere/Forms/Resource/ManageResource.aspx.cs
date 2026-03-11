using LearnSphere.DAL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace LearnSphere.Forms.Resource
{
    public partial class ManageResource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "Admin")
            {
                Response.Redirect("~/Forms/Resource/Home.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string sql = @"SELECT ID, Title, Author, PublicationYear, Category, Locator, DomainName FROM LearningResource";
            DataTable dt = DbHelper.ExecuteQuery(sql, null);
            gvResources.DataSource = dt;
            gvResources.DataBind();
        }

        protected void gvResources_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvResources.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvResources_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvResources.EditIndex = -1;
            BindGrid();
        }

        protected void gvResources_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvResources.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvResources.Rows[e.RowIndex];

            string title = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
            string author = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string yearText = ((TextBox)row.Cells[3].Controls[0]).Text.Trim();
            string category = ((TextBox)row.Cells[4].Controls[0]).Text.Trim();
            string locator = ((TextBox)row.Cells[5].Controls[0]).Text.Trim();
            string domainName = ((TextBox)row.Cells[6].Controls[0]).Text.Trim();

            int year;
            if (!int.TryParse(yearText, out year))
            {
                lblMessage.Text = "Invalid year.";
                return;
            }

            try
            {
                ResourceDAL.UpdateResource(id, title, author, year, category, locator, domainName);
                gvResources.EditIndex = -1;
                BindGrid();
                lblMessage.Text = "Resource updated successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Update failed: " + ex.Message;
            }
        }

        protected void gvResources_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvResources.DataKeys[e.RowIndex].Value);

            try
            {
                ResourceDAL.DeleteResource(id);
                BindGrid();
                lblMessage.Text = "Resource deleted successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Delete failed: " + ex.Message;
            }
        }
    }
}