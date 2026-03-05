using LearnSphere.DAL;
using LearnSphere.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Forms.Application
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBoxes();
            }

            Control searchContainer = Master.FindControl("searchContainer");
            Control sortDropdown = Master.FindControl("sortDropdown");
            if (searchContainer != null) searchContainer.Visible = false;
            if (sortDropdown != null) sortDropdown.Visible = false;
        }

        protected void ApplicationStatusFilter_Command(object sender, CommandEventArgs e)
        {
            string value = e.CommandArgument.ToString();
            ((HomeMasterPage)this.Master).SelectedFilters = value;
            colApplicationStatusFilters.Visible = false;
            ((HomeMasterPage)this.Master).ConditionallyCloseFilterPanel();
        }

        protected void Box_Command(object sender, CommandEventArgs e)
        {
            int appId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect($"Certification.aspx?appId={appId}");
        }

        protected void BindBoxes()
        {
            string appsSql = @"SELECT ID AS EducatorApplicationID,
                               DomainName,
                               Username,
                               Email,
                               CASE
                               WHEN Completed = 0 THEN 'Pending'
                               WHEN Completed = 1 THEN 'Completed'
                               END AS Status
                               FROM EducatorApplication";

            DataTable apps = DbHelper.ExecuteQuery(appsSql, null);

            BoxRepeater.DataSource = apps;
            BoxRepeater.DataBind();
        }
    }
}