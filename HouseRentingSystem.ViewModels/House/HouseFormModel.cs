namespace HouseRentingSystem.Web.ViewModels.House
{
    using System.ComponentModel.DataAnnotations;
    using Category;
    using static Common.EntityValidationConstants.House;

    public class HouseFormModel
    {
        public HouseFormModel()
        {
            Categories = new HashSet<HouseSelectCategoryFromModel>();
        }
        [Required]
        [StringLength(TitleMaxLenght,MinimumLength =TitleMinLenght)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(AddressMaxLenght,MinimumLength =AddressMinLenght)]
        public string Address { get; set; } = null!;
        [Required]
        [StringLength(DescriptionMaxLenght,MinimumLength =DescriptionMinLenght)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(ImageUrlMaxLenght)]
        [Display(Name ="Image link")]
        public string ImageUrl { get; set; } = null!;
        [Range(typeof(decimal),PricePerMonthMinValue,PricePerMonthMaxValue)]
        [Display(Name ="Monthly Price")]
        public decimal PricePerMonth { get; set; }
        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseSelectCategoryFromModel> Categories { get; set; }
    }
}
