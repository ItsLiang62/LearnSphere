using BCrypt.Net;
using LearnSphere.DAL;
using LearnSphere.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Forms.Resource
{
    public partial class Home : System.Web.UI.Page
    {
        protected List<string> SelectedFilters
        {
            get
            {
                if (ViewState["Filters"] == null)
                    ViewState["Filters"] = new List<string> { "None", "None", "None" };

                return (List<string>)ViewState["Filters"];
            }
            set { ViewState["Filters"] = value; }
        }

        protected string SelectedSort
        {
            get
            {
                if (ViewState["Sort"] == null)
                    ViewState["Sort"] = "None";

                return (string)ViewState["Sort"];
            }
            set { ViewState["Sort"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((HomeMasterPage)this.Master).SearchButtonClicked += new EventHandler(btnSearch_Click);

            if (!IsPostBack)
            {
                TextBox txt = (TextBox)this.Master.FindControl("txtSearch");
                if (txt != null)
                    txt.Attributes["placeholder"] = "Search by Title / Author";

                string resourcesSql = @"SELECT ID AS ResourceID, Title, Author, Category, PublicationYear AS Year
                                     FROM LearningResource";

                DataTable resources = DbHelper.ExecuteQuery(resourcesSql, null);

                BoxRepeater.DataSource = resources;
                BoxRepeater.DataBind();

                ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);

                if (Session["UserType"].Equals("Admin"))
                {
                    lnkShareResource.Visible = false;
                }

                // Retrieve domains from database
                DataTable domains = DbHelper.ExecuteQuery(
                    @"SELECT ID, DomainName FROM Domain", null
                    );

                var domainList = domains.AsEnumerable()
                    .Select(row => new
                    {
                        ID = row.Field<int>("ID"),
                        DomainFilter = row.Field<string>("DomainName")
                    })
                    .ToList();

                rptDomain.DataSource = domainList;
                rptDomain.DataBind();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string resourcesSql = @"SELECT ID AS ResourceID, Title, Author, Category, PublicationYear AS Year
                                     FROM LearningResource
                                     WHERE 
                                     (@domainName = 'None' OR DomainName = @domainName)
                                     AND (@category = 'None' OR Category = @category)
                                     AND (@sharerId = 0 OR SharerID = @sharerId)
                                     AND
                                     (@search IS NULL 
                                     OR Title LIKE '%' + @search + '%' 
                                     OR Author LIKE '%' + @search + '%')
                                     ORDER BY 
                                     CASE WHEN @sortBy = 'Topic (A-Z)'
                                     THEN Title END ASC,
                                     CASE WHEN @sortBy = 'Author (A-Z)'
                                     THEN Author END ASC,
                                     CASE WHEN @sortBy = 'Most Recent'
                                     THEN Year END DESC,
                                     CASE WHEN @sortBy = 'None' 
                                     THEN ID END ASC;";

            int sharerId = Convert.ToInt32(Session["AccountID"]);

            TextBox txtSearch = (TextBox)this.Master.FindControl("txtSearch");
            string search =
                string.IsNullOrEmpty(txtSearch.Text.Trim())
                ? null : "%" + txtSearch.Text.Trim() + "%";

            if (SelectedFilters[2] == "None") sharerId = 0;
            DataTable resources = DbHelper.ExecuteQuery(resourcesSql,
                new Dictionary<string, object>
                {
                    { "@domainName", SelectedFilters[0] },
                    { "@category", SelectedFilters[1] },
                    { "@sharer", sharerId },
                    { "@sortBy", SelectedSort },
                    { "@search", search }
                });

            BoxRepeater.DataSource = resources;
            BoxRepeater.DataBind();

            ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);
        }

        protected void DomainFilter_Command(object sender, CommandEventArgs e)
        {
            var selectedFilters = SelectedFilters;
            selectedFilters[0] = e.CommandArgument.ToString();
            SelectedFilters = selectedFilters;
            ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);
            ((HomeMasterPage)this.Master).CloseFilterColumn(colDomainFilters);
        }

        protected void CategoryFilter_Command(object sender, CommandEventArgs e)
        {
            var selectedFilters = SelectedFilters;
            selectedFilters[1] = e.CommandArgument.ToString();
            SelectedFilters = selectedFilters;
            ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);
            ((HomeMasterPage)this.Master).CloseFilterColumn(colCategoryFilters);
        }

        protected void MineFilter_Command(Object sender, CommandEventArgs e)
        {
            var selectedFilters = SelectedFilters;
            selectedFilters[2] = e.CommandArgument.ToString();
            SelectedFilters = selectedFilters;
            ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);
            ((HomeMasterPage)this.Master).CloseFilterColumn(colMineFilters);
        }

        protected void Sort_Command(object sender, CommandEventArgs e)
        {
            string selectedSort = e.CommandArgument.ToString();
            SelectedSort = selectedSort;
            string value = e.CommandArgument.ToString();
            ((HomeMasterPage)this.Master).SortText = value;
            ((HomeMasterPage)this.Master).CloseSortPanel();
        }

        protected void Box_Command(object sender, CommandEventArgs e)
        {
            int resourceId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/Forms/Resource/ResourceDetails.aspx?id=" + resourceId);
        }

        protected void lnkShareResource_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Resource/CreateResource.aspx");
        }
    }
}