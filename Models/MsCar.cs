using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    [Table("MsCar")]
    public class MsCar
    {
        [Key]
        [Column("Car_id")]
        [StringLength(36)]
        public string CarId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Column("license_plate")]
        [StringLength(50)]
        public string? LicensePlate { get; set; }

        [Column("number_of_car_seats")]
        public int NumberOfCarSeats { get; set; }

        [StringLength(100)]
        public string? Transmission { get; set; }

        [Column("price_per_day")]
        public decimal PricePerDay { get; set; }

        public bool Status { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<MsCarImages> CarImages { get; set; } = new List<MsCarImages>();
        public virtual ICollection<TrRental> Rentals { get; set; } = new List<TrRental>();
        public virtual ICollection<TrMaintenance> Maintenances { get; set; } = new List<TrMaintenance>();
    }
}