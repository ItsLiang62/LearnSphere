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
    public partial class WebForm1 : System.Web.UI.Page
    {
        HomeMasterPage master;

        protected void Page_Init(object sender, EventArgs e)
        {
            master = (HomeMasterPage)this.Master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set placeholder text of search bar
                TextBox txt = (TextBox)this.Master.FindControl("txtSearch");
                if (txt != null)
                    txt.Attributes["placeholder"] = "Search by Title / Tag / Educator";

                // Retrieve papers from database
                DataTable papers = DbHelper.ExecuteQuery(
                    @"SELECT p.ID AS PaperID, p.Title, a.Username, e.DomainName, t.TagName
                    FROM Paper p
                    LEFT JOIN Educator e ON p.EducatorID = e.ID
                    LEFT JOIN PaperTag t ON p.ID = t.PaperID
                    LEFT JOIN Account a ON e.AccountID = a.ID", null
                    );

                var paperList = papers.AsEnumerable()
                    .GroupBy(row => new
                    {
                        PaperID = row.Field<int>("PaperID"),
                        Title = row.Field<string>("Title"),
                        EducatorName = row.Field<string>("Username"),
                        EducatorDomain = row.Field<string>("DomainName")
                    })
                    .Select(g => new
                    {
                        g.Key.PaperID,
                        g.Key.Title,
                        g.Key.EducatorName,
                        g.Key.EducatorDomain,
                        PaperTags = g
                            .Select(r => r.Field<string>("TagName"))
                            .ToList()
                    })
                    .ToList();

                BoxRepeater.DataSource = paperList;
                BoxRepeater.DataBind();

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

                DomainRepeater.DataSource = domainList;
                DomainRepeater.DataBind();
            }

            string role = Session["UserType"] as string;
            role = "Educator";
            lnkCreatePaper.Visible = role == "Educator";
            
            if (role != "Educator")
            {
                colMineFilters.Parent.Controls.Remove(colMineFilters);
            }
        }

        protected void BoxRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                dynamic paper = e.Item.DataItem;
                var tagRepeater = (Repeater)e.Item.FindControl("TagRepeater");
                tagRepeater.DataSource = paper.PaperTags;
                tagRepeater.DataBind();
            }
        }

        protected void Box_Command(object sender, CommandEventArgs e)
        {
            int paperID = Convert.ToInt32(e.CommandArgument);
        }

        protected void MineFilter_Command(object sender, CommandEventArgs e)
        {
            string value = e.CommandArgument.ToString();
            ((HomeMasterPage)this.Master).FilterText = value;
            CloseFilterColumn(colMineFilters);
        }

        protected void DomainFilter_Command(object sender, CommandEventArgs e)
        {
            string value = e.CommandArgument.ToString();
            ((HomeMasterPage)this.Master).FilterText = value;
            CloseFilterColumn(colDomainFilters);
        }

        protected void Sort_Command(object sender, CommandEventArgs e)
        {
            string value = e.CommandArgument.ToString();
            ((HomeMasterPage)this.Master).SortText = value;
            ((HomeMasterPage)this.Master).CloseSortPanel();
        }

        protected void CloseFilterColumn(Panel column)
        {
            column.Visible = false;
            ((HomeMasterPage)this.Master).ConditionallyCloseFilterPanel();
        }
    }
}