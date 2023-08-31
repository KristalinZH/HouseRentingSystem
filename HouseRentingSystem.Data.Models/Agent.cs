namespace HouseRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Agent;
    public class Agent
    {
        public Agent()
        {
            Id = Guid.NewGuid();
            OwnedHouses = new HashSet<House>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(PhoneNumberMaxLenght)]
        public string PhoneNumber { get; set; } = null!;
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<House> OwnedHouses { get; set; }
    }
}
