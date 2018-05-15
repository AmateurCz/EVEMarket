using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EVEMarket.Model;
using GalaSoft.MvvmLight;
using EVEMarket.WPF.Interfaces;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketGroupViewModel : ViewModelBase, IMarketTreeItem
    {
        private readonly MarketGroup _model;
        private readonly IEnumerable<Type> _types;
        private readonly IEnumerable<MarketGroup> _otherGroups;

        private ObservableCollection<IMarketTreeItem> _childGroups;

        public ObservableCollection<IMarketTreeItem> ChildItems
        {
            get
            {
                if (_childGroups == null)
                {
                    var groups = _otherGroups
                        .Where(x => x.ParentMarketGroupId == _model.Id)
                        .Select(x => (IMarketTreeItem)new MarketGroupViewModel(x, _otherGroups, _types));

                    var types = _types.Where(x => x.MarketGroupId == _model.Id)
                            .Select(x => (IMarketTreeItem)new TypeViewModel(x));

                    _childGroups = new ObservableCollection<IMarketTreeItem>(
                            groups.Union(types));
                }

                return _childGroups;
            }
        }

        public string Name => _model.Name;

        public MarketGroupViewModel(MarketGroup model, IEnumerable<MarketGroup> otherGroups, IEnumerable<Type> types)
        {
            _model = model;
            _types = types;
            _otherGroups = otherGroups;
        }
    }
}