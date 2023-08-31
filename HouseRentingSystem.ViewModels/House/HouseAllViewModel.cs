namespace HouseRentingSystem.Web.ViewModels.House
{
	using System.ComponentModel.DataAnnotations;

	public class HouseAllViewModel
	{
		public string Id { get; set; } = null!;

		public string Title { get; set; } = null!;

		public string Address { get; set; } = null!;
		[Display(Name ="Image link")]
		public string ImageUrl { get; set; } = null!;
		[Display(Name ="Montly Price")]
		public decimal PricePerMonth { get; set; }
		[Display(Name ="Is rented")]
		public bool IsRented { get; set; }
	}
}
