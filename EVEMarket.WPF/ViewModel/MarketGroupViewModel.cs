using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EVEMarket.Model;
using GalaSoft.MvvmLight;
using EVEMarket.DataProviders;
using CommonServiceLocator;
using System;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketGroupViewModel : ViewModelBase
    {
        private readonly MarketGroup _model;

        private ObservableCollection<object> _childGroups;

        public ObservableCollection<object> ChildItems
        {
            get
            {
                if (_childGroups == null)
                {
                    var staticData = ServiceLocator.Current.GetInstance<IStaticData>();

                    var groups = staticData.MarketGroups.Where(x => x.ParentMarketGroupId == _model.Id).ToList();
                    var types = staticData.Types.Where(x => x.MarketGroupId == _model.Id).ToList();

                    _childGroups = new ObservableCollection<object>(groups
                        .Select(x => (object)new MarketGroupViewModel(x))
                        .Union(types.Select(x => (object)new TypeViewModel(x))));
                }

                return _childGroups;
            }
        }

        public string Name => _model.Name;

        public MarketGroupViewModel(MarketGroup model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}