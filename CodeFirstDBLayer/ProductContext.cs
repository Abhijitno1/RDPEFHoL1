using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CodeFirstDBLayer
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("ProductDatabase") 
        {
            //Now setting in configuration file
            //Database.SetInitializer(new CodeFirstDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleSP>().MapToStoredProcedures(cfg =>
                {
                    cfg.Insert(icfg => icfg.HasName("InsertVehicle"));
                    cfg.Update(icfg => icfg.HasName("UpdateVehicle"));
                    cfg.Delete(icfg => icfg.HasName("DeleteVehicle")
                        .Parameter(veh => veh.Id, "VehicleId"));
                }
            );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }

        public DbSet<HolidayDestination> Destinations { get; set; }
        public DbSet<HolidayLodging> Lodgings { get; set; }
        public DbSet<Passport> Passports { get; set; }

        public DbSet<VehicleSP> Vehicles { get; set; }
    }
    
}
