namespace HouseRentingSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using HouseRentingSystem.Data;
	using HouseRentingSystem.Data.Models;
    using Web.ViewModels.Category;
    using Interfaces;

	public class CategoryService : ICategoryService
    {
        private readonly HouseRentingDbContext dbContext;
        public CategoryService(HouseRentingDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<HouseSelectCategoryFormModel>> AllCategoriesAsync()
            => await dbContext
            .Categories
            .AsNoTracking()
            .Select(c => new HouseSelectCategoryFormModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToArrayAsync();

		public async Task<IEnumerable<AllCategoriesViewModel>> AllCategoriesForListAsync()
		{
			IEnumerable<AllCategoriesViewModel> allCategories = await dbContext
			   .Categories
			   .AsNoTracking()
			   .Select(c => new AllCategoriesViewModel()
			   {
				   Id = c.Id,
				   Name = c.Name,
			   })
			   .ToArrayAsync();

			return allCategories;
		}

		public async Task<IEnumerable<string>> AllCategoryNamesAsync()
            => await dbContext.Categories.Select(c => c.Name).ToArrayAsync();

		public async Task<bool> ExistsByIdAsync(int id)
        => await dbContext.Categories.AnyAsync(c => c.Id == id);

		public async Task<CategoryDetailsViewModel> GetDetailsByIdAsync(int id)
		{
			Category category = await dbContext
				.Categories
				.FirstAsync(c => c.Id == id);

			CategoryDetailsViewModel viewModel = new CategoryDetailsViewModel()
			{
				Id = category.Id,
				Name = category.Name
			};
			return viewModel;
		}
	}
}
