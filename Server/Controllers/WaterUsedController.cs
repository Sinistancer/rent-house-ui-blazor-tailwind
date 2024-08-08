using daryon_house_ui.Server.Applications;
using daryon_house_ui.Server.Models;
using daryon_house_ui.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace daryon_house_ui.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class WaterUsedController : ControllerBase
    {
        private readonly ILogger<WaterUsedController> _logger;
        private readonly WaterUsedService _waterUsedService;

        public WaterUsedController(
            ILogger<WaterUsedController> logger,
            WaterUsedService waterUsedService)
        {
            _logger = logger;
            _waterUsedService = waterUsedService;
        }

        [HttpPost]
        [Route("Get")]
        public async Task<IEnumerable<WaterUsed>?> Get([FromBody] WaterUsedRequest request)
        {
            return _waterUsedService.GetWaterUsed(request.Month, request.Year);
        }

        [HttpGet]
        [Route("MonthYear")]
        public async Task<IEnumerable<string>?> MonthYear()
        {
            return _waterUsedService.GetYearMonth();
        }
    }
}
