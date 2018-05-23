using EVEMarket.Model;
using Microsoft.EntityFrameworkCore;

namespace EVEMarket.WPF.Data
{
    public class EveDbContext : DbContext
    {
        public EveDbContext() : base()
        {
        }

        public DbSet<Region> Regions { get; set; }
    }
}
