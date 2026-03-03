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
    public partial class ForumDetails : System.Web.UI.Page
    {
        private int forumId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/Forms/Forum/Home.aspx");
                return;
            }

            forumId = Convert.ToInt32(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                LoadForumHeader();
                LoadTags();
                LoadPosts();
                ApplyRoleUI();
            }
        }

        private bool IsAdmin()
        {
            return (Session["UserType"]?.ToString() ?? "") == "Admin";
        }

        private bool IsLoggedIn()
        {
            return Session["AccountID"] != null;
        }

        private int CurrentAccountId()
        {
            return Convert.ToInt32(Session["AccountID"]);
        }

        private void ApplyRoleUI()
        {
            bool admin = IsAdmin();

            pnlComment.Visible = !admin;
            pnlAdminRemoveForum.Visible = admin;

            foreach (RepeaterItem item in rptPosts.Items)
            {
                LinkButton btn = item.FindControl("btnRemove") as LinkButton;
                if (btn != null) btn.Visible = admin;
            }
        }

        private void LoadForumHeader()
        {
            string sql = @"
                SELECT f.Topic, f.DomainName, a.Username
                FROM Forum f
                JOIN Account a ON f.CreatorID = a.ID
                WHERE f.ID = @ForumID";

            var p = new Dictionary<string, object> { { "@ForumID", forumId } };
            DataTable dt = DbHelper.ExecuteQuery(sql, p);

            if (dt.Rows.Count == 0)
            {
                Response.Redirect("~/Forms/Forum/Home.aspx");
                return;
            }

            lblTopic.Text = dt.Rows[0]["Topic"].ToString();
            lblDomain.Text = dt.Rows[0]["DomainName"].ToString();
            lblCreator.Text = dt.Rows[0]["Username"].ToString();
        }

        private void LoadTags()
        {
            string sql = "SELECT TagName FROM ForumTag WHERE ForumID=@ForumID";
            var p = new Dictionary<string, object> { { "@ForumID", forumId } };

            rptTags.DataSource = DbHelper.ExecuteQuery(sql, p);
            rptTags.DataBind();
        }

        private void LoadPosts()
        {
            string sql = @"
                SELECT 
                    p.ID AS PostID,
                    p.Content,
                    p.CreatedAt,
                    a.Username
                FROM Post p
                JOIN Account a ON p.AccountID = a.ID
                WHERE p.ForumID = @ForumID
                ORDER BY p.CreatedAt ASC";

            var pms = new Dictionary<string, object> { { "@ForumID", forumId } };

            rptPosts.DataSource = DbHelper.ExecuteQuery(sql, pms);
            rptPosts.DataBind();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!IsLoggedIn()) return;
            if (IsAdmin()) return;

            string content = (txtComment.Text ?? "").Trim();
            if (content.Length == 0) return;

            string sql = "INSERT INTO Post (Content, ForumID, AccountID) VALUES (@Content, @ForumID, @AccountID)";
            var p = new Dictionary<string, object>
            {
                { "@Content", content },
                { "@ForumID", forumId },
                { "@AccountID", CurrentAccountId() }
            };

            DbHelper.ExecuteNonQuery(sql, p);

            txtComment.Text = "";
            LoadPosts();
            ApplyRoleUI();
        }

        protected void rptPosts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                if (!IsAdmin()) return;

                int postId = Convert.ToInt32(e.CommandArgument);

                string sql = "DELETE FROM Post WHERE ID=@PostID";
                var p = new Dictionary<string, object> { { "@PostID", postId } };

                DbHelper.ExecuteNonQuery(sql, p);

                LoadPosts();
                ApplyRoleUI();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Forum/Home.aspx");
        }

        protected void btnRemoveForum_Click(object sender, EventArgs e)
        {
            if (!IsAdmin()) return;

            string sql = "DELETE FROM Forum WHERE ID=@ForumID";
            var p = new Dictionary<string, object> { { "@ForumID", forumId } };

            DbHelper.ExecuteNonQuery(sql, p);

            Response.Redirect("~/Forms/Forum/Home.aspx");
        }
    }
}