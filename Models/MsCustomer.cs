using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    [Table("MsCustomer")]
    public class MsCustomer
    {
        [Key]
        [Column("Customer_id")]
        [StringLength(36)]
        public string CustomerId { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Nama wajib diisi")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password wajib diisi")]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nomor telepon wajib diisi")]
        [Column("phone_number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Alamat wajib diisi")]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Column("driver_license_number")]
        [StringLength(100)]
        public string? DriverLicenseNumber { get; set; }

        // Navigation Property
        public virtual ICollection<TrRental> Rentals { get; set; } = new List<TrRental>();
    }
}