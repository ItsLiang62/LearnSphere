using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LearnSphere.DAL;

namespace LearnSphere.Forms.Forum
{
    public partial class CreateForum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = "SELECT DomainName FROM Domain ORDER BY DomainName";
                DataTable dt = DbHelper.ExecuteQuery(sql, null);

                ddlDomain.DataSource = dt;
                ddlDomain.DataTextField = "DomainName";
                ddlDomain.DataValueField = "DomainName";
                ddlDomain.DataBind();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Session["AccountID"] == null)
            {
                Response.Redirect("~/Forms/Registration/Login.aspx");
                return;
            }

            int creatorId = Convert.ToInt32(Session["AccountID"]);

            string sqlForum = @"
                INSERT INTO Forum (Topic, DomainName, CreatorID)
                VALUES (@Topic, @DomainName, @CreatorID);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            var pForum = new Dictionary<string, object>
            {
                { "@Topic", txtTopic.Text.Trim() },
                { "@DomainName", ddlDomain.SelectedValue },
                { "@CreatorID", creatorId }
            };

            int forumId = DbHelper.ExecuteScalar(sqlForum, pForum);

            string[] tags = txtTags.Text.Split(',');
            foreach (string t in tags)
            {
                string tag = t.Trim();
                if (tag.Length == 0) continue;

                string sqlTag = "INSERT INTO ForumTag (TagName, ForumID) VALUES (@TagName, @ForumID)";
                var pTag = new Dictionary<string, object>
                {
                    { "@TagName", tag },
                    { "@ForumID", forumId }
                };
                DbHelper.ExecuteNonQuery(sqlTag, pTag);
            }

            Response.Redirect("Home.aspx");
        }
    }
}