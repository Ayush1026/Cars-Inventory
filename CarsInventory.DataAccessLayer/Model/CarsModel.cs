using CarsInventory.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsInventory.DataAccessLayer.Model
{
    public class CarsModel
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(AppConstants.StringMax, ErrorMessage = AppConstants.StringMinMaxLength)]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        [Precision(10, 2)]
        public Decimal Price { get; set; }

        public bool New { get; set; }
        public int UserId { get; set; }
        public virtual UserModel? UserModels { get; set; } = null;
    }
}
