using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommonServiceLocator;
using EVEMarket.Data.Providers;
using EVEMarket.Model;
using GalaSoft.MvvmLight;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketGroupViewModel : ViewModelBase
    {
        private readonly MarketGroup _model;

        public ReadOnlyCollection<object> ChildItems { get; }

        public string Name => _model.Name;

        public MarketGroupViewModel(MarketGroup model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            var types = model.ChildTypes.Select(t => new TypeViewModel(t));
            var subGroups = model.ChildMarketGroups.Select(mGroup => new MarketGroupViewModel(mGroup));

            ChildItems = new ReadOnlyCollection<object>(types.Union<object>(subGroups).ToList());
        }
    }
}