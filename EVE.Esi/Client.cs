using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EVE.Esi.Model;
using Newtonsoft.Json;

namespace EVE.Esi
{
    public class Client
    {
        private readonly HttpClient _client;

        public string EsiUrl { get; }

        public Client(string esiUrl)
        {
            EsiUrl = esiUrl;
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