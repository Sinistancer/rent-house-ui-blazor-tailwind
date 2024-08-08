using daryon_house_ui.Server.Applications;
using daryon_house_ui.Server.Models;
using daryon_house_ui.Server.Models.PENYEWA;
using daryon_house_ui.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace daryon_house_ui.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ILogger<ApplicationUserController> _logger;
        private readonly ApiService _apiService;
        public ApplicationUserController(
            ILogger<ApplicationUserController> logger,
            ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<BaseListResponseModel<ApplicationUser>> Get([FromQuery] ApplicationUserGetRequest request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.GetAsync<BaseListResponseModel<ApplicationUser>>($"https://localhost:7034/api/ApplicationUser/UserClaims?page={request.Page}&pageSize={request.PageSize}", token.AccessToken);
        }

        [HttpGet]
        [Route("Dropdown")]
        public async Task<BaseListResponseModel<ApplicationUserDropdown>> Get()
        {
            var token = await HttpContext.GetUserAccessTokenAsync();
            var getDataUser = await _apiService.GetAsync<BaseListResponseModel<ApplicationUserDropdown>>($"https://localhost:7034/api/ApplicationUser/dropdown", token.AccessToken);
            
            List<ApplicationUserDropdown> listDataApplicationUser = new List<ApplicationUserDropdown>();

            //Get Data Tenant
            foreach (var dataUser in getDataUser.Data)
            {
                var getDataKontrak = await _apiService.GetAsync<BaseResponseModel<Tenant>>($"https://localhost:7051/api/Tenant/tenant-by-username?username={dataUser.UserName}", token.AccessToken);

                if(getDataKontrak is null)
                {
                    listDataApplicationUser.Add(new ApplicationUserDropdown{
                        Name = dataUser.Name,
                        UserName = dataUser.UserName
                    });
                }
            }

            return new BaseListResponseModel<ApplicationUserDropdown>{
                Code = getDataUser.Code,
                Data = listDataApplicationUser,
                Message = getDataUser.Message,
                Status = getDataUser.Status,
                TotalItems = getDataUser.TotalItems
            };
        }

        [HttpPost]
        [Route("Create")]
        public async Task<BaseResponseModel<bool>> Create([FromBody] ApplicationUserRequest request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.PostAsync<BaseResponseModel<bool>, ApplicationUserRequest>("https://localhost:7034/api/ApplicationUser/create", request, token.AccessToken);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<BaseResponseModel<bool>> Update([FromBody] ApplicationUserRequest request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.PostAsync<BaseResponseModel<bool>, ApplicationUserRequest>("https://localhost:7034/api/ApplicationUser/update", request, token.AccessToken);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<BaseResponseModel<bool>> Delete([FromBody] UserNameModel request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.PostAsync<BaseResponseModel<bool>, UserNameModel>("https://localhost:7034/api/ApplicationUser/delete", request, token.AccessToken);
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<BaseResponseModel<ApplicationUser>> GetById([FromQuery] UserNameModel request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.GetAsync<BaseResponseModel<ApplicationUser>>($"https://localhost:7034/api/ApplicationUser/Detail?username={request.UserName}", token.AccessToken);
        }
    }
}
