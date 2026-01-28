using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Master
{
    public partial class HomeMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CloseSortDropdown()
        {
            pnlSortDropdown.Visible = false;
        }

        public void SelectSortDropdown(string chosenOptionText)
        {
            pnlSortDropdown.Visible = false;
            Label lbl = (Label) lnkSort.FindControl("lblSortOption");
            lbl.Text = chosenOptionText;
        }

        protected void SortDropdown_Click(object sender, EventArgs e)
        {
            pnlSortDropdown.Visible = !pnlSortDropdown.Visible;
        }

        public void SelectFilterDropdown(string chosenOptionText)
        {
            pnlFilterDropdown.Visible = false;
            Label lbl = (Label) lnkFilter.FindControl("lblFilterOption");
            lbl.Text = chosenOptionText;
        }

        protected void FilterDropdown_Click(object sender, EventArgs e)
        {
            pnlFilterDropdown.Visible = !pnlFilterDropdown.Visible;
        }
    }
}