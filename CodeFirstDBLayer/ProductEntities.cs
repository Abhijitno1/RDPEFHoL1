using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDBLayer
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductDetails> ProductDetails { get; set; }
    }

    public class ProductDetails
    {
        [Key]
        public int ProductDetId { get; set; }
        public string ProductTitle { get; set; }
        public string CategoryId { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }

    public class VehicleSP
    {
        public int Id { get; set; }
        [Required]
        public string VehicleType { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [Required]
        public string RegNumber { get; set; }
    }

    public class HolidayDestination
    {
        [Key]
        public int DestinationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }
        public List<HolidayLodging> Lodgings { get; set; }
        [NotMapped]
        public string DestinationCode
        {
            get
            {
                return Country.Substring(0, 2) + ":" + Name.Substring(0, 2);
            } 
        }
    }

    public class HolidayLodging
    {
        public HolidayLodging()
        {
            LodgingDetails = new LodgingDetails();
        }
        [Key]
        public int LodgingId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }
        public bool isResort { get; set; }
        public HolidayDestination Destination { get; set; }
        public LodgingDetails LodgingDetails { get; set; }
    }

    [ComplexType]
    public class LodgingDetails
    {
        public RoomType RoomType { get; set; }
        public int NumOfRooms { get; set; }
        public decimal RoomRent { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public enum RoomType
    {
        RegularWithBed = 1,
        DayTimeOnly,
        DeluxAC
    }
    public class Passport
    {
        [Column(Order=1)]
        [MaxLength(30)]
        [Key]
        public string PassportNumber { get; set; }
        [Column(Order=2)]
        [MaxLength(30)]
        [Key]
        public string IssuingCountry { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
