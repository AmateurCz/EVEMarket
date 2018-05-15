using System.Collections.ObjectModel;

namespace EVEMarket.WPF.Interfaces
{
    public interface IMarketTreeItem
    {
        string Name { get; }

        ObservableCollection<IMarketTreeItem> ChildItems { get; }
    }
}