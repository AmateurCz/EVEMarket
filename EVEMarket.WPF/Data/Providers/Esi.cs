using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EVEMarket.Data.Providers;
using EVEMarket.Model;
using Newtonsoft.Json;

namespace EVEMarket.WPF.Data.Providers
{
    public class Esi : IEsiData
    {
        public string EsiUrl => Properties.Settings.Default.EsiUrl;

        private readonly HttpClient _client;

        public Esi()
        {
            _client = new HttpClient() { BaseAddress = new Uri(EsiUrl) };
        }

        #region Market

        public Task<List<MarketOrder>> GetSellOrdersAsync(int regionId, int typeId, CancellationToken cancellationToken)
        {
            return GetData<List<MarketOrder>>(
                $"markets/{regionId}/orders/?datasource=tranquility&order_type=sell&type_id={typeId}",
                cancellationToken);
        }

        public Task<List<MarketOrder>> GetBuyOrdersAsync(int regionId, int typeId, CancellationToken cancellationToken)
        {
            return GetData<List<MarketOrder>>(
                $"markets/{regionId}/orders/?datasource=tranquility&order_type=buy&type_id={typeId}",
                cancellationToken);
        }

        public Task<List<Asset>> GetCharacterAssetsAsync(int characterId, CancellationToken cancellationToken)
        {
            return GetData<List<Asset>>($"characters/{characterId}/assets/", cancellationToken); 
        }

        #endregion Market

        protected async Task<T> GetData<T>(string requestUri, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                throw new TaskCanceledException();

            var payload = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(payload);
        }
    }
}