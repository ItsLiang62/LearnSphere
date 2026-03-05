using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Master
{
    public partial class HomeMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlFilter.Visible = false;
                pnlSort.Visible = false;
                if (!Session["UserType"].Equals("Admin"))
                {
                    lblApplication.Visible = false;
                }
            }
        }

        public string SelectedFilters
        {
            get => lblFilter.Text;
            set => lblFilter.Text = value;
        }

        public string SortText
        {
            get => lblSort.Text;
            set => lblSort.Text = value;
        }

        public event EventHandler SearchButtonClicked;

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public void CloseSortPanel()
        {
            pnlSort.Visible = false;
        }

        protected void SortPanel_Toggle(object sender, CommandEventArgs e)
        {
            pnlSort.Visible = !pnlSort.Visible;
        }

        protected void FilterPanel_Toggle(object sender, CommandEventArgs e)
        {
            if (!pnlFilter.Visible)
            {
                // Temporarily make all filter columns visible
                // To accurately calculate whether filter panel should be visible
                FilterColumns_SetVisible(); 
                pnlFilter.Visible =
                    pnlFilter.Controls
                        .OfType<ContentPlaceHolder>()
                        .SelectMany(plc => plc.Controls.OfType<Panel>())
                        .Any(p => p.Visible);
            }
            else
            {
                pnlFilter.Visible = false;
            }
        }

        protected void FilterColumns_SetVisible()
        {
            // Temporarily make filter panel visible
            // To enable visibility of filter columns
            pnlFilter.Visible = true; 

            foreach (Control filterPanelChild in pnlFilter.Controls)
            {
                if (filterPanelChild is ContentPlaceHolder plc) 
                {
                    foreach (Control plcChild in plc.Controls)
                    {
                        
                        if (plcChild is Panel column)
                        {
                            column.Visible = FindControlsRecursive<LinkButton>(column).Any();
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> FindControlsRecursive<T>(Control parent) where T : Control
        {
            foreach (Control child in parent.Controls)
            {
                if (child is T typedChild)
                    yield return typedChild;

                foreach (var descendent in FindControlsRecursive<T>(child))
                    yield return descendent;
            }
        }

        public void ConditionallyCloseFilterPanel()
        {
            if (pnlFilter.Controls.OfType<ContentPlaceHolder>().All(plc => plc.Controls.OfType<Panel>().All(column => !column.Visible)))
            {
                pnlFilter.Visible = false;
            }
        }

        public void CloseFilterColumn(Panel filterColumn)
        {
            filterColumn.Visible = false;
            ConditionallyCloseFilterPanel();
        }

        public void SetLblFilter(List<string> selectedFilters)
        {
            lblFilter.Text = string.Join(", ", selectedFilters);
        }

        public void lblResources_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Resource/Home.aspx");
        }

        public void lblForums_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Forum/Home.aspx");
        }

        public void lblAssessments_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Assessment/Home.aspx");
        }

        public void lblApplications_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Application/Home.aspx");
        }

        public void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Profile/Profile.aspx");
        }

    }
}