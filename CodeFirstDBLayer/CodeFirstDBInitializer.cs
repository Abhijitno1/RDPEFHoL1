using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CodeFirstDBLayer
{
    public class CodeFirstDBInitializer: DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            //First add few Products
            context.Products.AddRange(new[] {
                        new Product { Name = "ABC" },
                        new Product { Name = "PQR" },
                        new Product { Name = "XYZ" }
                    });
            var dest = new HolidayDestination
            {
                Name = "Indo",
                Description = "Ecotourism Place",
                Country = "Indonesia"
            };
            context.Destinations.Add(dest);

            HolidayLodging lodging = new HolidayLodging
            {
                Name = "Azcaban Elephant Park",
                Destination = dest,
                isResort = true,
                LodgingDetails = new LodgingDetails
                {
                    CreatedOn = DateTime.Today,
                    NumOfRooms = 35,
                    RoomRent = 3000,
                    RoomType = RoomType.RegularWithBed
                },
                Owner = "Mr. Mo Shee"
            };
            context.Lodgings.Add(lodging);

            base.Seed(context);
        }
    }
}
