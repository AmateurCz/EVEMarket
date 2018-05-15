using System;
using EVEMarket.Model;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketOrderViewModel
    {
        private MarketOrder _model;

        public string Location => _model.LocationId.ToString();
               
        public double Price => _model.Price;

        public string Volume => $"{_model.VolumeRemain}/{_model.VolumeTotal}";

        public int Duration => _model.Duration;

        public DateTime Issued => _model.Issued;


        public MarketOrderViewModel(MarketOrder model)
        {
            _model = model;
        }
    }
}