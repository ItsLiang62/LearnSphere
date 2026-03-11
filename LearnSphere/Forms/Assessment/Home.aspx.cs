using LearnSphere.DAL;
using LearnSphere.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Forms.Assessment
{
    public partial class Home : System.Web.UI.Page
    {
        protected List<string> SelectedFilters
        {
            get
            {
                if (ViewState["Filters"] == null)
                    if (string.Equals(Session["UserType"], "Educator"))
                    {
                        ViewState["Filters"] = new List<string> { "None", "None" };
                    } else
                    {
                        ViewState["Filters"] = new List<string> { "None" };
                    }
                    

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

        protected void Page_Init(object sender, EventArgs e)
        {
            bool isEducator = string.Equals(Session["UserType"] as string, "Educator");

            lnkCreatePaper.Visible = isEducator;

            var plcMineFiltersColumn =
                this.Master.FindControl("MineFiltersColumnPlaceholder");

            if (plcMineFiltersColumn != null)
                plcMineFiltersColumn.Visible = isEducator;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((HomeMasterPage)this.Master).SearchButtonClicked += new EventHandler(btnSearch_Click);

            if (!IsPostBack)
            {
                // Set placeholder text of search bar
                TextBox txt = (TextBox)this.Master.FindControl("txtSearch");
                if (txt != null)
                    txt.Attributes["placeholder"] = "Search by Title / Tag / Educator";


                // Retrieve papers from database
                string papersSql = @"SELECT p.ID, p.Title, a.Username, e.DomainName, t.TagName
                                    FROM Paper p
                                    LEFT JOIN Account a ON p.AccountID = a.ID
                                    LEFT JOIN Educator e ON a.ID = e.AccountID
                                    LEFT JOIN PaperTag t ON p.ID = t.PaperID";
                DataTable papers = DbHelper.ExecuteQuery(papersSql, null);

                // Load papers to boxes
                LoadPapers(papers);

                // Load domain filters from database
                DataTable domains = DbHelper.ExecuteQuery(
                    @"SELECT DomainName AS DomainFilter FROM Domain", null
                );

                DomainRepeater.DataSource = domains;
                DomainRepeater.DataBind();
            }
        }
        protected void LoadPapers(DataTable papers)
        {
            var paperList = papers.AsEnumerable()
                    .GroupBy(row => new
                    {
                        PaperID = row.Field<int>("ID"),
                        PaperTitle = row.Field<string>("Title"),
                        EducatorName = row.Field<string>("Username"),
                        EducatorDomain = row.Field<string>("DomainName")
                    })
                    .Select(g => new
                    {
                        g.Key.PaperID,
                        g.Key.PaperTitle,
                        g.Key.EducatorName,
                        g.Key.EducatorDomain,
                        PaperTag = g
                            .Select(r => r.Field<string>("TagName"))
                            .ToList()
                    })
                    .ToList();

            BoxRepeater.DataSource = paperList;
            BoxRepeater.DataBind();
            ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);
        }

        protected void BoxRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var dataItem = e.Item.DataItem;

                var tags = (List<string>)DataBinder.Eval(dataItem, "PaperTag");

                Repeater tagRepeater = (Repeater)e.Item.FindControl("TagRepeater");
                tagRepeater.DataSource = tags;
                tagRepeater.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string papersSql = @"SELECT p.ID, p.Title, a.Username, e.DomainName, t.TagName
                                FROM Paper p
                                LEFT JOIN Account a ON p.AccountID = a.ID
                                LEFT JOIN Educator e ON a.ID = e.AccountID
                                LEFT JOIN PaperTag t ON p.ID = t.PaperID
                                WHERE 
                                (@domainName = 'None' OR e.DomainName = @domainName)
                                AND (@eduAccId = -1 OR a.ID = @eduAccId)
                                AND
                                (@search IS NULL 
                                OR p.Title LIKE '%' + @search + '%' 
                                OR t.TagName LIKE '%' + @search + '%'
                                OR a.Username LIKE '%' + @search + '%')
                                ORDER BY 
                                CASE WHEN @sortBy = 'Title (A-Z)'
                                THEN p.Title END ASC,
                                CASE WHEN @sortBy = 'Educator (A-Z)'
                                THEN a.Username END ASC,
                                CASE WHEN @sortBy = 'None' 
                                THEN p.ID END ASC;";

            int eduAccID = Convert.ToInt32(Session["AccountID"]);

            TextBox txtSearch = (TextBox)this.Master.FindControl("txtSearch");
            string search =
                string.IsNullOrEmpty(txtSearch.Text.Trim())
                ? null : "%" + txtSearch.Text.Trim() + "%";

            if (SelectedFilters.Count == 1 || SelectedFilters[1] == "None") eduAccID = -1;
            DataTable papers = DbHelper.ExecuteQuery(papersSql,
                new Dictionary<string, object>
                {
                    { "@domainName", SelectedFilters[0] },
                    { "@eduAccId", eduAccID },
                    { "@sortBy", SelectedSort },
                    { "@search", search }
                });

            LoadPapers(papers);
        }

        protected void Box_Command(object sender, CommandEventArgs e)
        {
            int paperID = Convert.ToInt32(e.CommandArgument);
            Session["PaperID"] = paperID;
            Response.Redirect("~/Forms/Assessment/ViewPaper.aspx");
        }

        protected void DomainFilter_Command(object sender, CommandEventArgs e)
        {
            var selectedFilters = SelectedFilters;
            selectedFilters[0] = e.CommandArgument.ToString();
            SelectedFilters = selectedFilters;
            ((HomeMasterPage)this.Master).SetLblFilter(SelectedFilters);
            ((HomeMasterPage)this.Master).CloseFilterColumn(colDomainFilters);
        }

        protected void MineFilter_Command(object sender, CommandEventArgs e)
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

        protected void btnCreatePaper_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Assessment/CreatePaper.aspx");
        }
    }
}