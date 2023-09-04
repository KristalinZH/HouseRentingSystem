namespace HouseRentingSystem.Services.Data
{
    using System.Threading.Tasks;
	using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
	using Services.Mapping;
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Data.Models;
    using Interfaces;
	using Web.ViewModels.User;

	public class UserService : IUserService
    {
        private readonly HouseRentingDbContext dbContext;
        public UserService(HouseRentingDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
            List<UserViewModel> allUsers = new List<UserViewModel>();
            IEnumerable<UserViewModel> agents = await dbContext
                .Agents
                .Include(a => a.User)
                .To<UserViewModel>()
                .ToListAsync();
            allUsers.AddRange(agents);
            IEnumerable<UserViewModel> users = await dbContext
                .Users
                .Where(u => !dbContext.Agents.Any(a => a.UserId == u.Id))
                .Select(u => new UserViewModel()
                {
                    Id=u.Id.ToString(),
                    FullName = u.FirstName + " " + u.LastName,
                    Email = u.Email,
                    PhoneNumber = string.Empty
                })
                .ToListAsync();
            allUsers.AddRange(users);
            return allUsers;
        }

		public async Task<string> GetFullNameByEmailAsync(string email)
        {
            ApplicationUser? user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return string.Empty;
            return $"{user.FirstName} {user.LastName}";
        }

		public async Task<string> GetFullNameByIdAsync(string userId)
		{
            ApplicationUser? user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
                return string.Empty;
            return $"{user.FirstName} {user.LastName}";
		}
	}
}
