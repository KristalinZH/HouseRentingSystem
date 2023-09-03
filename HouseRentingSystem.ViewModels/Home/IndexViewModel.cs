namespace HouseRentingSystem.Web.ViewModels.Home
{
	using Services.Mapping;
    using Data.Models;
	public class IndexViewModel:IMapFrom<House>
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
