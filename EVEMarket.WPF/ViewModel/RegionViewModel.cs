using System;
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
                    var staticData = new Data.EveDbContext();
                    var constellations = staticData.Constellations.Where(x => x.RegionId == _model.Id).ToList();

                    _constellations = new ObservableCollection<ConstellationViewModel>(constellations
                        .Select(x => new ConstellationViewModel(x))
                        .ToList());

                    _selectedConstellation = _constellations.First();
                }

                return _constellations;
            }
        }

        public string Name => _model.Name;

        public int Id => _model.Id;

        public ConstellationViewModel SelectedConstellation
        {
            get => _selectedConstellation;
            set => Set(ref _selectedConstellation, value);
        }

        public RegionViewModel(Region model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}