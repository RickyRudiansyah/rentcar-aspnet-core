using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    [Table("TrRental")]
    public class TrRental
    {
        [Key]
        [Column("Rental_id")]
        [StringLength(36)]
        public string RentalId { get; set; } = Guid.NewGuid().ToString();

        [Column("rental_date")]
        public DateTime RentalDate { get; set; }

        [Column("return_date")]
        public DateTime ReturnDate { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("payment_status")]
        public bool PaymentStatus { get; set; } = false;

        [Column("customer_id")]
        [StringLength(36)]
        public string CustomerId { get; set; } = string.Empty;

        [Column("car_id")]
        [StringLength(36)]
        public string CarId { get; set; } = string.Empty;

        // Navigation Properties
        [ForeignKey("CustomerId")]
        public virtual MsCustomer? Customer { get; set; }

        [ForeignKey("CarId")]
        public virtual MsCar? Car { get; set; }

        public virtual LtPayment? Payment { get; set; }
    }
}