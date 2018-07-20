using System.Collections.ObjectModel;
using System.Linq;
using EVEMarket.Model;
using GalaSoft.MvvmLight;

namespace EVEMarket.WPF.ViewModel
{
    public class ConstellationViewModel : ViewModelBase
    {
        private readonly Constellation _model;
        private ObservableCollection<SolarSystemViewModel> _solarSystems;
        private SolarSystemViewModel _selectedSolarSystem;

        public string Name => _model.Name;

        public ObservableCollection<SolarSystemViewModel> SolarSystems
        {
            get
            {
                if (_solarSystems == null)
                {
                    var staticData = new Data.EveDbContext();
                    var solarSystems = staticData.SolarSystems.Where(x => x.ConstellationId == _model.Id).ToList();

                    _solarSystems = new ObservableCollection<SolarSystemViewModel>(solarSystems.Select(x => new SolarSystemViewModel(x)));
                    _selectedSolarSystem = _solarSystems.First();
                }

                return _solarSystems;
            }
        }

        public SolarSystemViewModel SelectedSolarSystem
        {
            get => _selectedSolarSystem;
            set => Set(ref _selectedSolarSystem, value);
        }

        public ConstellationViewModel(Constellation model)
        {
            this._model = model;
        }
    }
}