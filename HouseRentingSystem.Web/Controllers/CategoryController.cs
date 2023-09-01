namespace HouseRentingSystem.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Infrastructure.Extensions;
	using Services.Data.Interfaces;
	using ViewModels.Category;
	public class CategoryController : BaseController
	{
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			try
			{
				IEnumerable<AllCategoriesViewModel> viewModel =
				await categoryService.AllCategoriesForListAsync();

				return View(viewModel);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			try
			{
				bool categoryExists = await categoryService.ExistsByIdAsync(id);
				if (!categoryExists)
				{
					return NotFound();
				}

				CategoryDetailsViewModel viewModel =
					await categoryService.GetDetailsByIdAsync(id);
				if (viewModel.GetUrlInformation() != information)
				{
					return NotFound();
				}

				return View(viewModel);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}
	}
}
