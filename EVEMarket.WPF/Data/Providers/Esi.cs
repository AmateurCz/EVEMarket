using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EVEMarket.Model;
using Newtonsoft.Json;

namespace EVEMarket.WPF.Data.Providers
{
    public class Esi
    {
        public string EsiUrl => Properties.Settings.Default.EsiUrl;

        private readonly HttpClient _client;

        public Esi()
        {
            _client = new HttpClient() { BaseAddress = new Uri(EsiUrl) };
        }

        #region Market

        public Task<List<MarketOrder>> GetSellOrders(int regionId, int typeId, CancellationToken cancellationToken)
        {
            return GetData<List<MarketOrder>>(
                $"markets/{regionId}/orders/?datasource=tranquility&order_type=sell&type_id={typeId}",
                cancellationToken);
        }

        public Task<List<MarketOrder>> GetBuyOrders(int regionId, int typeId, CancellationToken cancellationToken)
        {
            return GetData<List<MarketOrder>>(
                $"markets/{regionId}/orders/?datasource=tranquility&order_type=buy&type_id={typeId}",
                cancellationToken);
        }

        #endregion Market

        protected async Task<T> GetData<T>(string requestUri, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, cancellationToken);
            var payload = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(payload);
        }
    }
}