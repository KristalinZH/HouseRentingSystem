namespace HouseRentingSystem.Services.Data
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Data.Models;
    using Interfaces;

    public class UserService : IUserService
    {
        private readonly HouseRentingDbContext dbContext;
        public UserService(HouseRentingDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

		public async Task<string> GetFullNameByEmailAsync(string email)
        {
            ApplicationUser? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return string.Empty;
            return $"{user.FirstName} {user.LastName}";
        }
    }
}
