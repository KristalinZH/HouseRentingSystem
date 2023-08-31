namespace HouseRentingSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using HouseRentingSystem.Data;
    using Web.ViewModels.Category;
    using Interfaces;

    public class CategoryService : ICategoryService
    {
        private readonly HouseRentingDbContext dbContext;
        public CategoryService(HouseRentingDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<HouseSelectCategoryFromModel>> AllCategoriesAsync()
            => await dbContext
            .Categories
            .AsNoTracking()
            .Select(c => new HouseSelectCategoryFromModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToArrayAsync();

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
            => await dbContext.Categories.Select(c => c.Name).ToArrayAsync();

		public async Task<bool> ExistsByIdAsync(int id)
        => await dbContext.Categories.AnyAsync(c => c.Id == id);
	}
}
