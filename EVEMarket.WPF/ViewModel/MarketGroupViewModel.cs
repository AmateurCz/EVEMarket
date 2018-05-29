using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EVEMarket.Model;
using GalaSoft.MvvmLight;
using EVEMarket.WPF.Interfaces;
using EVEMarket.DataProviders;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketGroupViewModel : ViewModelBase, IMarketTreeItem
    {
        private readonly MarketGroup _model;
        private readonly IStaticData _staticData;

        private ObservableCollection<IMarketTreeItem> _childGroups;

        public ObservableCollection<IMarketTreeItem> ChildItems
        {
            get
            {
                if (_childGroups == null)
                {
                    var groups = _model.Children
                        .Where(x => x.ParentMarketGroupId == _model.Id)
                        .Select(x => (IMarketTreeItem)new MarketGroupViewModel(x, _staticData));

                    var types = _staticData.Types.Values.Where(x => x.MarketGroupId == _model.Id)
                            .Select(x => (IMarketTreeItem)new TypeViewModel(x));

                    _childGroups = new ObservableCollection<IMarketTreeItem>(groups.Union(types));
                }

                return _childGroups;
            }
        }

        public string Name => _model.Name;

        public MarketGroupViewModel(MarketGroup model, IStaticData sData)
        {
            _model = model;
            _staticData = sData;
        }
    }
}