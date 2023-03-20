using CarsInventory.Common;
using System.ComponentModel.DataAnnotations;

namespace CarsInventory.DataAccessLayer.Model
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(AppConstants.StringMax, ErrorMessage = AppConstants.StringMinMaxLength, MinimumLength = AppConstants.StringMin)]
        public string Name { get; set; }

        [Required]
        [StringLength(AppConstants.StringMax, ErrorMessage = AppConstants.password, MinimumLength = AppConstants.StringMin)]

        public string Password { get; set; }

        [Required]
        public int Age { get; set; }
        public string Gender { get; set; }
        public virtual ICollection<CarsModel> CarsModels { get; } = new List<CarsModel>();
    }
}
