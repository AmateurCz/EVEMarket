using System;
using EVEMarket.Model;

namespace EVEMarket.WPF.ViewModel
{
    public class SolarSystemViewModel
    {
        private readonly SolarSystem _model;

        public string Name => _model.Name;

        public SolarSystemViewModel(SolarSystem model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}