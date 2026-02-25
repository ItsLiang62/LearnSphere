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

namespace LearnSphere.Forms.Forum
{
    public partial class Home : System.Web.UI.Page
    {
        protected List<string> SelectedFilters
        {
            get
            {
                if (ViewState["Filters"] == null)
                    ViewState["Filters"] = new List<string> { "None", "None" };

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

                return (string) ViewState["Sort"];
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
                    txt.Attributes["placeholder"] = "Search by Topic / Tag";

                string forumsSql = @"SELECT f.ID, f.Topic, f.DomainName, t.TagName
                                     FROM Forum f
                                     LEFT JOIN ForumTag t on f.ID = t.ForumID";

                DataTable forums = DbHelper.ExecuteQuery(forumsSql, null);
                LoadForums(forums);

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

                if (Session["UserType"].Equals("Admin"))
                {
                    lnkCreateForum.Visible = false;
                }
            }
        }

        protected void LoadForums(DataTable forums)
        {
            var forumList = forums.AsEnumerable()
                    .GroupBy(r => new
                    {
                        Topic = r.Field<string>("Topic"),
                        Domain = r.Field<string>("DomainName"),
                        ID = r.Field<int>("ID")
                    })
                    .Select(g => new
                    {
                        g.Key.ID,
                        g.Key.Domain,
                        g.Key.Topic,
                        Tags = g.Select(r => r.Field<string>("TagName")).Where(t => t != null).ToList()
                    }).ToList();

            BoxRepeater.DataSource = forumList;
            BoxRepeater.DataBind();

            ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string forumsSql = @"SELECT f.ID, f.Topic, f.DomainName, t.TagName
                                     FROM Forum f
                                     LEFT JOIN ForumTag t on f.ID = t.ForumID
                                     WHERE 
                                     (@domainName = 'None' OR f.DomainName = @domainName)
                                     AND (@creatorId = 0 OR f.CreatorID = @creatorID)
                                     AND
                                     (@search IS NULL 
                                     OR f.Topic LIKE '%' + @search + '%' 
                                     OR t.TagName LIKE '%' + @search + '%')
                                     ORDER BY 
                                     CASE WHEN @sortBy = 'Topic (A-Z)'
                                     THEN f.Topic END ASC,
                                     CASE WHEN @sortBy = 'None' 
                                     THEN f.ID END ASC;";

            int creatorID = Convert.ToInt32(Session["AccountID"]);

            TextBox txtSearch = (TextBox)this.Master.FindControl("txtSearch");
            string search = 
                string.IsNullOrEmpty(txtSearch.Text.Trim()) 
                ? null : "%" + txtSearch.Text.Trim() + "%";

            if (SelectedFilters[1] == "None") creatorID = 0;
            DataTable forums = DbHelper.ExecuteQuery(forumsSql,
                new Dictionary<string, object>
                {
                    { "@domainName", SelectedFilters[0] },
                    { "@creatorId", creatorID },
                    { "@sortBy", SelectedSort },
                    { "@search", search }
                });

            LoadForums(forums);
        }


        protected void DomainFilter_Command(object sender, CommandEventArgs e)
        {
            var selectedFilters = SelectedFilters;
            selectedFilters[0] = e.CommandArgument.ToString();
            SelectedFilters = selectedFilters;
            ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);
            ((HomeMasterPage)this.Master).CloseFilterColumn(colDomainFilters);
        }

        protected void MineFilter_Command(Object sender, CommandEventArgs e)
        {
            var selectedFilters = SelectedFilters;
            selectedFilters[1] = e.CommandArgument.ToString();
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

        }
    }
}