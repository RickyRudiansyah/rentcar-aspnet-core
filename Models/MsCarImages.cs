using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    [Table("MsCarImages")]
    public class MsCarImages
    {
        [Key]
        [Column("Image_car_id")]
        [StringLength(36)]
        public string ImageCarId { get; set; } = Guid.NewGuid().ToString();

        [Column("Car_id")]
        [StringLength(36)]
        public string CarId { get; set; } = string.Empty;

        [Column("image_link")]
        [StringLength(2000)]
        public string? ImageLink { get; set; }

        // Navigation Property
        [ForeignKey("CarId")]
        public virtual MsCar? Car { get; set; }
    }
}