namespace HouseRentingSystem.WebApi.Controllers
{
    using HouseRentingSystem.Services.Data.Interfaces;
    using HouseRentingSystem.Services.Data.Models.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    [Route("api/statistics")]
    [ApiController]
    public class StatisticApiController : ControllerBase
    {
        private readonly IHouseService houseService;
        public StatisticApiController(IHouseService _houseService)
        {
            houseService = _houseService;
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200,Type = typeof(StatisticServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticServiceModel serviceModel = await houseService.GetStatisticAsync();
                return Ok(serviceModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
