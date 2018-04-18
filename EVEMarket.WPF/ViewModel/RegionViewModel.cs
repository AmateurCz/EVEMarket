using System.Collections.ObjectModel;
using System.Linq;
using EVEMarket.Model;
using GalaSoft.MvvmLight;

namespace EVEMarket.WPF.ViewModel
{
    public class RegionViewModel : ViewModelBase
    {
        private readonly Region _model;
        private ObservableCollection<ConstellationViewModel> _constellations;
        private ConstellationViewModel _selectedConstellation;

        public ObservableCollection<ConstellationViewModel> Constellations
        {
            get
            {
                if (_constellations == null)
                {
                    _constellations = new ObservableCollection<ConstellationViewModel>(
                        _model.Constellations.OrderBy(x=>x.Id).Select(x => new ConstellationViewModel(x)).ToList());
                    _selectedConstellation = _constellations.First();
                }

                return _constellations;
            }
        }

        public ConstellationViewModel SelectedConstellation
        {
            get => _selectedConstellation;
            set => Set(ref _selectedConstellation, value);
        }

        public RegionViewModel(Region model)
        {
            _model = model;
        }

        public string Name => _model.Name;
    }
}
