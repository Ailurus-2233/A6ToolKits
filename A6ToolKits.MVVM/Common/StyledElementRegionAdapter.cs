using A6ToolKits.Common.Extensions;
using Avalonia;
using Avalonia.Controls;
using static System.Collections.Specialized.NotifyCollectionChangedAction;

namespace A6ToolKits.MVVM.Common;

public class StyledElementRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
    : RegionAdapterBase<StyledElement>(regionBehaviorFactory)
{
    protected override void Adapt(IRegion region, StyledElement regionTarget)
    {
        // Implement your adaptation logic here
        if (regionTarget is ContentControl contentControl)
        {
            region.ActiveViews.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case Add:
                        e.NewItems!.ForEach<StyledElement>(view => contentControl.Content ??= view);
                        break;
                    case Remove:
                        e.OldItems!.ForEach<StyledElement>(view =>
                        {
                            if (Equals(contentControl.Content, view))
                                contentControl.Content = null;
                        });
                        break;
                }
            };
        }
    }

    protected override IRegion CreateRegion()
    {
        return new AllActiveRegion();
    }
}