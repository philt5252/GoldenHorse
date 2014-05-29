using Microsoft.Practices.Prism.Regions;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Helpers
{
    public static class RegionHelpers
    {
        public static void AddAndActivate(this IRegion region, object view, string viewName="")
        {
            if(viewName != "")
                region.Add(view, viewName);
            else
            {
                region.Add(view);
            }

            region.Activate(view);
        }

        public static void ClearAddAndActivate(this IRegion region, object view)
        {
            foreach (object regionView in region.Views)
            {
                region.Remove(regionView);
            }

            region.AddAndActivate(view);
        }

    }
}