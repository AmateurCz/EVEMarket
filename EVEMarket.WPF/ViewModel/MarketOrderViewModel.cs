using System;
using System.Globalization;
using EVEMarket.Model;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketOrderViewModel
    {
        private readonly MarketOrder _model;

        public string Location { get; }

        public double Price => _model.Price;

        public string Volume => $"{_model.VolumeRemain}/{_model.VolumeTotal}";

        public int Duration => _model.Duration;

        public DateTime Issued => _model.Issued.ToLocalTime();

        public DateTime EndsAt => _model.Issued.ToLocalTime() + TimeSpan.FromDays(Duration);

        public int EndsIn =>  (int)Math.Floor((TimeSpan.FromDays(Duration) - (DateTime.Now - _model.Issued.ToLocalTime())).TotalDays);

        public MarketOrderViewModel(MarketOrder model, string locationName)
        {
            _model = model;
            Location = locationName;
        }
    }
}