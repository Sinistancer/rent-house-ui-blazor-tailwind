using daryon_house_ui.Server.Applications;
using daryon_house_ui.Server.Models;
using daryon_house_ui.Server.Models.HOME;
using daryon_house_ui.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace daryon_house_ui.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly ApiService _apiService;
        public PropertyController(
            ILogger<PropertyController> logger,
            ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;   
        }

        [HttpGet]
        public async Task<BaseListResponseModel<Property>> Get([FromQuery] PropertyGetRequest propertyGetRequest)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.GetAsync<BaseListResponseModel<Property>>($"https://localhost:7051/api/Property/properties?page={propertyGetRequest.Page}&pageSize={propertyGetRequest.PageSize}", token.AccessToken);
        }

        [HttpGet]
        [Route("Dropdown")]
        public async Task<BaseListResponseModel<PropertyDropdown>> Get()
        {
            var token = await HttpContext.GetUserAccessTokenAsync();
            
            return await _apiService.GetAsync<BaseListResponseModel<PropertyDropdown>>($"https://localhost:7051/api/Property/dropdown", token.AccessToken);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<BaseResponseModel<bool>> Create([FromBody] PropertyRequest propertyCreateRequest)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.PostAsync<BaseResponseModel<bool>, PropertyRequest>("https://localhost:7051/api/Property/create", propertyCreateRequest, token.AccessToken);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<BaseResponseModel<bool>> Delete([FromBody] IdModel property)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.PostAsync<BaseResponseModel<bool>, IdModel>("https://localhost:7051/api/Property/delete", property, token.AccessToken);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<BaseResponseModel<bool>> Update([FromBody] PropertyRequest propertyUpdateRequest)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.PostAsync<BaseResponseModel<bool>, PropertyRequest>("https://localhost:7051/api/Property/update", propertyUpdateRequest, token.AccessToken);
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<BaseResponseModel<Property>> GetById([FromQuery] IdModel property)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.GetAsync<BaseResponseModel<Property>>($"https://localhost:7051/api/Property/property?id={property.Id}", token.AccessToken);
        }
    }
}
