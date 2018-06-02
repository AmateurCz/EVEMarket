using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommonServiceLocator;
using EVEMarket.Data.Providers;
using EVEMarket.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EVEMarket.WPF.ViewModel
{
    public class TypeViewModel : ViewModelBase
    {
        public int Id => _model.Id;

        public string Name => _model.Name?.En ?? string.Empty;

        public ICommand RefreshData { get; }

        public ObservableCollection<MarketOrderViewModel> SellOrders { get; }

        public ObservableCollection<MarketOrderViewModel> BuyOrders { get; }

        private readonly Model.Type _model;

        public TypeViewModel(Model.Type model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            RefreshData = new RelayCommand(() => RefreshOrdersAsync().ContinueWith(HandleTask));

            SellOrders = new ObservableCollection<MarketOrderViewModel>();
            BuyOrders = new ObservableCollection<MarketOrderViewModel>();
        }

        private void HandleTask(Task t)
        {
            if (t.IsFaulted)
            {
                MessageBox.Show(t.Exception.Message ,"Async task failed");
            }
        }

        private async Task RefreshOrdersAsync()
        {
            var mainVm = ServiceLocator.Current.GetInstance<MainViewModel>();
            var regionId = mainVm.SelectedRegion?.Id;

            if (regionId.HasValue)
            {
                var staticData = ServiceLocator.Current.GetInstance<IStaticData>();

                var client = new HttpClient();
                var result = await client.GetAsync($"https://esi.evetech.net/latest/markets/{regionId}/orders/?datasource=tranquility&order_type=sell&type_id={this.Id}", HttpCompletionOption.ResponseContentRead);

                var content = await result.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<MarketOrder>>(content);

                var locationIds = orders.Select(x => x.LocationId).Distinct().ToList();

                var names = await staticData.UniqueNames.Where(x => locationIds.Contains(x.Id)).ToDictionaryAsync(x => x.Id);

                SellOrders.Clear();
                foreach (var order in orders)
                {
                    string name = string.Empty;
                    if (names.TryGetValue(order.LocationId, out var uniqueName))
                    {
                        name = uniqueName.ItemName;
                    }

                    SellOrders.Add(new MarketOrderViewModel(order, name));
                }

                result = await client.GetAsync($"https://esi.evetech.net/latest/markets/{regionId}/orders/?datasource=tranquility&order_type=buy&type_id={this.Id}", HttpCompletionOption.ResponseContentRead);

                content = await result.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<List<MarketOrder>>(content);

                locationIds = orders.Select(x => x.LocationId).Distinct().ToList();
                names = await staticData.UniqueNames.Where(x => locationIds.Contains(x.Id)).ToDictionaryAsync(x => x.Id);

                BuyOrders.Clear();
                foreach (var order in orders)
                {
                    string name = string.Empty;
                    if (names.TryGetValue(order.LocationId, out var uniqueName))
                    {
                        name = uniqueName.ItemName;
                    }

                    BuyOrders.Add(new MarketOrderViewModel(order, name));
                }
            }
        }
    }
}