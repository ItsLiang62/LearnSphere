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
            if (!Page.IsPostBack)
            {
                pnlFilter.Visible = false;
                pnlSort.Visible = false;
                pnlDomainFilters.Visible = false;
                pnlCategoryFilters.Visible = false;
                pnlMineFilters.Visible = false;
            }
            
        }

        public string FilterText
        {
            get => lblFilter.Text;
            set => lblFilter.Text = value;
        }

        public string SortText
        {
            get => lblSort.Text;
            set => lblSort.Text = value;
        }

        public void CloseSortOptionsColumn()
        {
            pnlSortOptions.Visible = false;
            pnlSort.Visible = false;
        }

        private void CloseFilterPanelIfComplete()
        {
            if (pnlCategoryFilters.Visible == false &&
                pnlMineFilters.Visible == false &&
                pnlDomainFilters.Visible==false)
            {
                pnlFilter.Visible = false;
            }
        }

        public void CloseDomainFilterColumn()
        {
            pnlDomainFilters.Visible = false;
            CloseFilterPanelIfComplete();
        }

        public void CloseCategoryFilterColumn()
        {
            pnlCategoryFilters.Visible = false;
            CloseFilterPanelIfComplete();
        }

        public void CloseMineFilterColumn()
        {
            pnlMineFilters.Visible = false;
            CloseFilterPanelIfComplete();
        }

        protected void SortPanel_Click(object sender, EventArgs e)
        {
            pnlSort.Visible = !pnlSort.Visible;
            pnlSortOptions.Visible = true;
        }

        protected void FilterPanel_Click(object sender, EventArgs e)
        {
            if (pnlFilter.Visible)
            {
                pnlFilter.Visible = false; 
            } else
            {
                Repeater rptDomain = (Repeater)DomainFiltersPlaceholder.FindControl("DomainRepeater");
                ContentPlaceHolder cphMine = (ContentPlaceHolder)MineFiltersPlaceholder.FindControl("NestedMineFiltersPlaceholder");

                if (rptDomain != null && cphMine != null)
                {
                    pnlFilter.Visible = rptDomain.Items.Count > 0 ||
                                        CategoryFiltersPlaceholder.Controls.OfType<WebControl>().Any() ||
                                        cphMine.Controls.OfType<WebControl>().Any();

                    pnlDomainFilters.Visible = rptDomain.Items.Count > 0;
                    pnlCategoryFilters.Visible = CategoryFiltersPlaceholder.Controls.OfType<WebControl>().Any();
                    pnlMineFilters.Visible = cphMine.Controls.OfType<WebControl>().Any();
                }
            }
                
        }
    }
}