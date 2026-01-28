using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LearnSphere.Master;

namespace LearnSphere.Forms.Assessment
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void TitleSort_Click(object sender, EventArgs e)
        {
            ((HomeMasterPage)this.Master).SelectSortDropdown(TitleSort.Text);

        }

        protected void EducatorSort_Click(object sender, EventArgs e)
        {
            ((HomeMasterPage)this.Master).SelectSortDropdown(EducatorSort.Text);
        }

        protected void NoneSort_Click(object sender, EventArgs e)
        {
            ((HomeMasterPage)this.Master).SelectSortDropdown(NoneSort.Text);
        }

        protected void MineFilter_Click(object sender, EventArgs e)
        {
            ((HomeMasterPage)this.Master).SelectFilterDropdown(MineFilter.Text);
        }

        protected void NoneFilter_Click(object sender, EventArgs e)
        {
            ((HomeMasterPage)this.Master).SelectFilterDropdown(NoneFilter.Text);
        }
    }
}