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
    }
}