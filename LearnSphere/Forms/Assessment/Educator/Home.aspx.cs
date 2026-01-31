using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LearnSphere.Master;

namespace LearnSphere.Forms.Assessment.Educator
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void MineFilter_Command(object sender, CommandEventArgs e)
        {
            string value = e.CommandArgument.ToString();
            ((AssessmentHomeMasterPage)this.Master).FilterText = value;
            ((AssessmentHomeMasterPage)this.Master).CloseMineFilterColumn();
        }
    }
}