﻿namespace HouseRentingSystem.Services.Data.Interfaces
{
    using Web.ViewModels.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<HouseSelectCategoryFormModel>> AllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllCategoryNamesAsync();

		Task<IEnumerable<AllCategoriesViewModel>> AllCategoriesForListAsync();

		Task<CategoryDetailsViewModel> GetDetailsByIdAsync(int id);
	}
}
