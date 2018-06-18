using EVEMarket.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EVEMarket.Data.Providers
{
    public interface IEsiData
    {
        Task<List<MarketOrder>> GetSellOrdersAsync(int regionId, int typeId, CancellationToken cancellationToken);

        Task<List<MarketOrder>> GetBuyOrdersAsync(int regionId, int typeId, CancellationToken cancellationToken);
    }
}