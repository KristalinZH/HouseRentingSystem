
namespace HouseRentingSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.ApplicationUser;
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            RentedHouses = new HashSet<House>();
        }
        [Required]
        [MaxLength(FirstNameMaxLenght)]
        [PersonalData]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(LastNameMaxLenght)]
        [PersonalData]
        public string LastName { get; set; } = null!;
        public virtual ICollection<House> RentedHouses { get; set; }
    }
}
