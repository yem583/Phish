using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Windows.Controls;

namespace Phish.Desktop.Wpf.Docking
{
    public class DockingRegionAdapter : RegionAdapterBase<RadDocking>
    {

        public DockingRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, RadDocking regionTarget)
        {
            regionTarget.PanesSource = region.Views;
            regionTarget.ActivePaneChanged += RegionTarget_ActivePaneChanged;
        }

        private void RegionTarget_ActivePaneChanged(object sender, Telerik.Windows.Controls.Docking.ActivePangeChangedEventArgs e)
        {
            if (e.NewPane?.PaneGroup != null)
            {
                e.NewPane.PaneGroup.DropDownDisplayMode = TabControlDropDownDisplayMode.Visible;
            }
        }

        protected override IRegion CreateRegion()
        {
            var region = new Region();
            return region;
        }
    }
}
