using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    [Table("TrMaintenance")]
    public class TrMaintenance
    {
        [Key]
        [Column("Maintenance_id")]
        [StringLength(36)]
        public string MaintenanceId { get; set; } = Guid.NewGuid().ToString();

        [Column("maintenance_date")]
        public DateTime MaintenanceDate { get; set; }

        [Column("description")]
        [StringLength(4000)]
        public string? Description { get; set; }

        [Column("cost")]
        public decimal Cost { get; set; }

        [Column("car_id")]
        [StringLength(36)]
        public string CarId { get; set; } = string.Empty;

        [Column("employee_id")]
        [StringLength(36)]
        public string EmployeeId { get; set; } = string.Empty;

        // Navigation Properties
        [ForeignKey("CarId")]
        public virtual MsCar? Car { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual MsEmployee? Employee { get; set; }
    }
}