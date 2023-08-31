
namespace HouseRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Category;
    public class Category
    {
        public Category()
        {
            House = new HashSet<House>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = null!;
        public virtual ICollection<House> House { get; set; }
    }
}