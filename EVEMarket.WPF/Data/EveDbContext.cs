using EVEMarket.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEMarket.WPF.Data
{
    public class EveDbContext : DbContext
    {
        public EveDbContext() : base("EveDb")
        {
        }

        public DbSet<Region> Regions { get; set; }
    }
}
