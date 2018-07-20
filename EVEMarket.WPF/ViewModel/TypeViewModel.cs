using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommonServiceLocator;
using EVE.Esi;
using EVE.Esi.Model;
using EVEMarket.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace EVEMarket.WPF.ViewModel
{
    public class TypeViewModel : ViewModelBase
    {
        public int Id => _model.Id;

        public string Name => _model.Name?.En ?? string.Empty;

        private ObservableCollection<MarketOrderViewModel> _sellOrders;

        private ObservableCollection<MarketOrderViewModel> _buyOrders;

        public ICommand RefreshData { get; }

        public ObservableCollection<MarketOrderViewModel> SellOrders
        {
            get => _sellOrders;
            private set { Set(ref _sellOrders, value); }
        }

        public ObservableCollection<MarketOrderViewModel> BuyOrders
        {
            get => _buyOrders;
            private set { Set(ref _buyOrders, value); }
        }

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
                MessageBox.Show(t.Exception.Message, "Async task failed");
            }
        }

        private async Task RefreshOrdersAsync()
        {
            var mainVm = ServiceLocator.Current.GetInstance<MainViewModel>();
            var regionId = mainVm.SelectedRegion?.Id;

            if (regionId.HasValue)
            {
                var staticData = new Data.EveDbContext();

                var client = new Client(Properties.Settings.Default.EsiUrl);
                var cancelationToken = new CancellationToken();

                var sellOrders = await client.GetSellOrdersAsync(regionId.Value, this.Id, cancelationToken);
                var buyOrders = await client.GetSellOrdersAsync(regionId.Value, this.Id, cancelationToken);

                var locationIds = sellOrders.Concat(buyOrders).Select(x => x.LocationId).Distinct().ToList();
                var names = await staticData.UniqueNames.Where(x => locationIds.Contains(x.Id)).ToDictionaryAsync(x => x.Id);

                SellOrders = CreateOrderVms(sellOrders, names);
                BuyOrders = CreateOrderVms(buyOrders, names);
            }
        }

        private ObservableCollection<MarketOrderViewModel> CreateOrderVms(List<MarketOrder> orders, Dictionary<long, UniqueName> names)
        {
            List<MarketOrderViewModel> vms = new List<MarketOrderViewModel>(orders.Count);

            foreach (var order in orders)
            {
                string name = string.Empty;
                if (names.TryGetValue(order.LocationId, out var uniqueName))
                {
                    name = uniqueName.Name;
                }

                vms.Add(new MarketOrderViewModel(order, name));
            }

            return new ObservableCollection<MarketOrderViewModel>(vms);
        }
    }
}