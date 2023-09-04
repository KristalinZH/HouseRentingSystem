namespace HouseRentingSystem.Web.ViewModels.User
{
	using Services.Mapping;
	using Data.Models;
	using AutoMapper;

	public class UserViewModel : IMapFrom<Agent>, IHaveCustomMappings
	{
		public string Id { get; set; } = null!;
		public string Email { get; set; } = null!;

		public string FullName { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<Agent, UserViewModel>()
				.ForMember(d=>d.Id,opt=>opt.MapFrom(s=>s.UserId.ToString()))
				.ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
				.ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName));
		}
	}
}
