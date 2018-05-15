using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using CommonServiceLocator;
using EVEMarket.Model;
using EVEMarket.WPF.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;

namespace EVEMarket.WPF.ViewModel
{
    public class TypeViewModel : ViewModelBase, IMarketTreeItem
    {
        public int Id => _model.Id;

        public string Name => _model.Name?.En ?? string.Empty;

        public ICommand RefreshData { get; }

        public ObservableCollection<IMarketTreeItem> ChildItems { get; }

        public ObservableCollection<MarketOrderViewModel> SellOrders { get; }

        public ObservableCollection<MarketOrderViewModel> BuyOrders { get; }

        private readonly Model.Type _model;

        public TypeViewModel(Model.Type model)
        {
            _model = model ?? throw new System.ArgumentNullException(nameof(model));
            RefreshData = new RelayCommand(RefreshOrders);

            ChildItems = new ObservableCollection<IMarketTreeItem>();
            SellOrders = new ObservableCollection<MarketOrderViewModel>();
            BuyOrders = new ObservableCollection<MarketOrderViewModel>();
        }

        private async void RefreshOrders()
        {
            var mainVm = ServiceLocator.Current.GetInstance<MainViewModel>();
            var regionId = mainVm.SelectedRegion?.Id;

            if (regionId.HasValue)
            {
                var client = new HttpClient();
                var result = await client.GetAsync($"https://esi.evetech.net/latest/markets/{regionId}/orders/?datasource=tranquility&order_type=sell&type_id={this.Id}", HttpCompletionOption.ResponseContentRead);

                var content = await result.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<MarketOrder>>(content);

                SellOrders.Clear();
                foreach (var order in orders)
                {
                    SellOrders.Add(new MarketOrderViewModel(order));
                }

                result = await client.GetAsync($"https://esi.evetech.net/latest/markets/{regionId}/orders/?datasource=tranquility&order_type=buy&type_id={this.Id}", HttpCompletionOption.ResponseContentRead);

                content = await result.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<List<MarketOrder>>(content);

                BuyOrders.Clear();
                foreach (var order in orders)
                {
                    BuyOrders.Add(new MarketOrderViewModel(order));
                }
            }
        }
    }
}
