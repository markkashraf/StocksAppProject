using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StocksDbContext : DbContext
    {
        public DbSet<BuyOrder> buyOrders { get; set; }
        public DbSet<SellOrder> sellOrders { get; set; }
        public StocksDbContext(DbContextOptions<StocksDbContext> options) : base(options)
        {

        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BuyOrder>().ToTable(nameof(BuyOrder));
            modelBuilder.Entity<SellOrder>().ToTable(nameof(SellOrder));

        }
    }
}
