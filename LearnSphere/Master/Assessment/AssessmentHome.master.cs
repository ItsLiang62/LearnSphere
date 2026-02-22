using LearnSphere.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LearnSphere.Master
{
    public partial class AssessmentHomeMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox txt = (TextBox)this.Master.FindControl("txtSearch");
                if (txt != null)
                    txt.Attributes["placeholder"] = "Search by Title / Tag / Educator";
            }

            if (!IsPostBack)
            {
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
            }

            string role = Session["UserType"] as string;
        }

        public string FilterText
        {
            get => ((HomeMasterPage)this.Master).FilterText;
            set => ((HomeMasterPage)this.Master).FilterText = value;
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

        protected void Box_Click(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            int paperID = Convert.ToInt32(lnk.CommandArgument);
        }
    }
}