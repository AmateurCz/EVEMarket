using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EVEMarket.Model;
using GalaSoft.MvvmLight;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketGroupViewModel : ViewModelBase
    {
        private readonly MarketGroup _model;
        private readonly IEnumerable<MarketGroup> _otherGroups;

        private ObservableCollection<MarketGroupViewModel> _childGroups;

        public ObservableCollection<MarketGroupViewModel> ChildGroups
        {
            get
            {
                if (_childGroups == null)
                {
                    _childGroups = new ObservableCollection<MarketGroupViewModel>(
                        _otherGroups
                            .Where(x=> x.ParentMarketGroupId == _model.Id)
                            .Select(x=> new MarketGroupViewModel(x, _otherGroups)));
                }

                return _childGroups;
            }
        }

        public string Name => _model.Name;

        public MarketGroupViewModel(MarketGroup model, IEnumerable<MarketGroup> otherGroups)
        {
            _model = model;
            _otherGroups = otherGroups;
        }
    }
}