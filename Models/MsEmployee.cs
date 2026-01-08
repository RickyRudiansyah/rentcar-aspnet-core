using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    [Table("MsEmployee")]
    public class MsEmployee
    {
        [Key]
        [Column("Employee_id")]
        [StringLength(36)]
        public string EmployeeId { get; set; } = Guid.NewGuid().ToString();

        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Position { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [Column("phone_number")]
        [StringLength(50)]
        public string? PhoneNumber { get; set; }

        // Navigation Property
        public virtual ICollection<TrMaintenance> Maintenances { get; set; } = new List<TrMaintenance>();
    }
}