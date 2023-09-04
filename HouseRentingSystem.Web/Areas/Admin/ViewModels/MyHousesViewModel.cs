namespace HouseRentingSystem.Web.Areas.Admin.ViewModels
{
	using HouseRentingSystem.Web.ViewModels.House;

	public class MyHousesViewModel
	{
        public MyHousesViewModel()
        {
			AddedHouses = new HashSet<HouseAllViewModel>();
			RentedHouses = new HashSet<HouseAllViewModel>();
		}
        public IEnumerable<HouseAllViewModel> AddedHouses { get; set; }
		public IEnumerable<HouseAllViewModel> RentedHouses { get; set; }
	}
}
