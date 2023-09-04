namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Caching.Memory;

	using Services.Data.Interfaces;
	using Web.ViewModels.Rent;

	using static Common.GeneralApplicationConstants;

	public class RentController : BaseController
	{
		private readonly IRentService rentService;
		private readonly IMemoryCache memoryCache;

		public RentController(IRentService rentService, IMemoryCache memoryCache)
		{
			this.rentService = rentService;
			this.memoryCache = memoryCache;
		}

		[Route("Rent/All")]
		[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client, NoStore = false)]
		public async Task<IActionResult> All()
		{
			try
			{
				IEnumerable<RentViewModel> allRents =
					this.memoryCache.Get<IEnumerable<RentViewModel>>(RentsCacheKey);
				if (allRents == null)
				{
					allRents = await this.rentService.AllAsync();

					MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
						.SetAbsoluteExpiration(TimeSpan.FromMinutes(RentsCacheDurationMinutes));

					this.memoryCache.Set(RentsCacheKey, allRents, cacheOptions);
				}

				return this.View(allRents);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}
	}
}
