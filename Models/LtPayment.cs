using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    [Table("LtPayment")]
    public class LtPayment
    {
        [Key]
        [Column("Payment_id")]
        [StringLength(36)]
        public string PaymentId { get; set; } = Guid.NewGuid().ToString();

        [Column("payment_date")]
        public DateTime PaymentDate { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("payment_method")]
        [StringLength(100)]
        public string? PaymentMethod { get; set; }

        [Column("rental_id")]
        [StringLength(36)]
        public string RentalId { get; set; } = string.Empty;

        // Navigation Property
        [ForeignKey("RentalId")]
        public virtual TrRental? Rental { get; set; }
    }
}