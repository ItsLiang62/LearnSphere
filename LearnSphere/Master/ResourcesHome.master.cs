using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearnSphere.Master
{
    public partial class ResourcesHomeMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string FilterText
        {
            get => ((HomeMasterPage)this.Master).FilterText;
            set => ((HomeMasterPage)this.Master).FilterText = value;
        }
    }
}