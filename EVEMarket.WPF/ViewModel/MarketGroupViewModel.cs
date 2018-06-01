using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommonServiceLocator;
using EVEMarket.DataProviders;
using EVEMarket.Model;
using GalaSoft.MvvmLight;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketGroupViewModel : ViewModelBase
    {
        private readonly MarketGroup _model;

        private ReadOnlyCollection<TypeViewModel> _childTypes;
        private ReadOnlyCollection<MarketGroupViewModel> _childGroups;
        private ReadOnlyCollection<object> _childItems;


        public ReadOnlyCollection<TypeViewModel> ChildTypes
        {
            get
            {
                if (_childTypes == null)
                {
                    var staticData = ServiceLocator.Current.GetInstance<IStaticData>();
                    var types = staticData.Types.Where(x => x.MarketGroupId == _model.Id).ToList();
                    _childTypes = new ReadOnlyCollection<TypeViewModel>(types.Select(x => new TypeViewModel(x)).ToList());
                }

                return _childTypes;
            }
        }

        public ReadOnlyCollection<MarketGroupViewModel> ChildGroups
        {
            get
            {
                if (_childGroups == null)
                {
                    var staticData = ServiceLocator.Current.GetInstance<IStaticData>();
                    var types = staticData.MarketGroups.Where(x => x.ParentMarketGroupId == _model.Id).ToList();
                    _childGroups = new ReadOnlyCollection<MarketGroupViewModel>(types.Select(x => new MarketGroupViewModel(x)).ToList());
                }

                return _childGroups;
            }
        }

        public ReadOnlyCollection<object> ChildItems
        {
            get
            {
                if (_childItems == null)
                {
                    _childItems = new ReadOnlyCollection<object>(ChildGroups.Union<object>(ChildTypes).ToList());
                }

                return _childItems;
            }
        }

        public string Name => _model.Name;

        public MarketGroupViewModel(MarketGroup model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}