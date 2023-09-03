namespace HouseRentingSystem.Web.ViewModels.House
{
    using System.ComponentModel.DataAnnotations;
    using Category;
	using Services.Mapping;
    using Data.Models;
	using AutoMapper;
	using static Common.EntityValidationConstants.House;

	public class HouseFormModel:IMapTo<House>,IHaveCustomMappings
    {
        public HouseFormModel()
        {
            Categories = new HashSet<HouseSelectCategoryFormModel>();
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

        public IEnumerable<HouseSelectCategoryFormModel> Categories { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
            configuration.CreateMap<HouseFormModel, House>()
                .ForMember(d => d.AgentId,opt => opt.Ignore());
		}
	}
}
