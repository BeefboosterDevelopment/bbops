using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Beefbooster.Bull.Entities.Models;
using Beefbooster.Data.Mapping;
using Repository.Pattern.Ef6;

namespace Beefbooster.Data
{
    //public class BullContext : DbContext, IDataContext
    public class BullContext : DataContext
    {
        static BullContext()
        {
            Database.SetInitializer<BullContext>(null);
        }

        public BullContext()
            : base("Name=BullConnectionString")
        {
        }

        public DbSet<SpringSaleDate> SpringSaleDates { get; set; }
        public DbSet<SpringSale> SpringSales { get; set; }
        //public DbSet<PO> POs { get; set; }
        //public DbSet<VWPOD> PODetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SpringSaleDateMap());
            modelBuilder.Configurations.Add(new SpringSaleMap());
            //modelBuilder.Configurations.Add(new POMap());
            //modelBuilder.Configurations.Add(new vwPODMap());

            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}