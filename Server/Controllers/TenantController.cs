using daryon_house_ui.Client.Model;
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
    public class TenantController : ControllerBase
    {
        private readonly ILogger<TenantController> _logger;
        private readonly ApiService _apiService;
        public TenantController(
            ILogger<TenantController> logger,
            ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<BaseListResponseModel<Kontrak>> Get([FromQuery] ApplicationUserGetRequest request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            List<Kontrak> listKontrak = new List<Kontrak>();

            var getDataUsers = await _apiService.GetAsync<BaseListResponseModel<ApplicationUser>>($"https://localhost:7034/api/ApplicationUser/UserClaims?page={request.Page}&pageSize={request.PageSize}", token.AccessToken);

            if (getDataUsers.Data is null)
            {
                return new BaseListResponseModel<Kontrak>();
            }

            foreach (var dataUser in getDataUsers.Data)
            {
                var getDataKontrak = await _apiService.GetAsync<BaseResponseModel<Tenant>>($"https://localhost:7051/api/Tenant/tenant-by-username?username={dataUser.UserName}", token.AccessToken);

                if(getDataKontrak is null)
                {
                    continue;
                }

                var getDataHome = await _apiService.GetAsync<BaseResponseModel<Property>>($"https://localhost:7051/api/Property/property?id={getDataKontrak.Data.PropertyId}", token.AccessToken);

                if (getDataHome is null)
                {
                    continue;
                }

                listKontrak.Add(new Kontrak
                {
                    //User Info
                    Name = dataUser.Name,
                    GivenName = dataUser.GivenName,
                    FamilyName = dataUser.FamilyName,
                    Address = dataUser.Address,
                    Locality = dataUser.Locality,
                    PostalCode = dataUser.PostalCode,
                    Country = dataUser.Country,
                    UserName = dataUser.UserName,

                    //Kontrak Info
                    Status = getDataKontrak.Data.Status,
                    StatusDisplay = getDataKontrak.Data.StatusDisplay,
                    PaymentStatus = getDataKontrak.Data.PaymentStatus,
                    PaymentStatusDisplay = getDataKontrak.Data.PaymentStatus ? "LUNAS" : "BELUM LUNAS",
                    StarDate = getDataKontrak.Data.StarDate,
                    EndDate = getDataKontrak.Data.EndDate,

                    //Home Info
                    HomeName = getDataHome.Data.Name,
                    HomeAddress = getDataHome.Data.Address,
                    Price = getDataHome.Data.Price,
                    Wide = getDataHome.Data.Wide,
                    TotalRoom = getDataHome.Data.TotalRoom,
                    Facility = getDataHome.Data.Facility
                });
            }

            return new BaseListResponseModel<Kontrak>
            {
                Code = getDataUsers.Code,
                Data = listKontrak,
                Message = getDataUsers.Message,
                Status = getDataUsers.Status,
                TotalItems = listKontrak.Count()
            };
        }

        [HttpPost]
        [Route("Create")]
        public async Task<BaseResponseModel<bool>> Create([FromBody] KontrakRequest request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.PostAsync<BaseResponseModel<bool>, KontrakRequest>("https://localhost:7051/api/Tenant/create", request, token.AccessToken);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<BaseResponseModel<bool>> Delete([FromBody] UserNameModel request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            return await _apiService.PostAsync<BaseResponseModel<bool>, UserNameModel>("https://localhost:7051/api/Tenant/delete", request, token.AccessToken);
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<BaseResponseModel<Kontrak>> GetByUsername([FromQuery] UserNameModel request)
        {
            var token = await HttpContext.GetUserAccessTokenAsync();

            var getDataKontrak = await _apiService.GetAsync<BaseResponseModel<Tenant>>($"https://localhost:7051/api/Tenant/tenant-by-username?username={request.UserName}", token.AccessToken);

            if (getDataKontrak is null)
            {
                return new BaseResponseModel<Kontrak>();
            }

            var getDataHome = await _apiService.GetAsync<BaseResponseModel<Property>>($"https://localhost:7051/api/Property/property?id={getDataKontrak.Data.PropertyId}", token.AccessToken);

            if (getDataHome is null)
            {
                return new BaseResponseModel<Kontrak>();
            }

            var getAllDataKontrak = await _apiService.GetAsync<BaseListResponseModel<Tenant>>($"https://localhost:7051/api/Tenant/tenants?page=1&pageSize=99999", token.AccessToken);

            var dataKontrak = new Kontrak
            {
                //Kontrak Info

                PropertyId = getDataHome.Data.Id,
                Status = getDataKontrak.Data.Status,
                StatusDisplay = getDataKontrak.Data.StatusDisplay,
                PaymentStatusDisplay = getDataKontrak.Data.PaymentStatus ? "LUNAS" : "BELUM LUNAS",
                StarDate = getDataKontrak.Data.StarDate,
                EndDate = getDataKontrak.Data.EndDate,
                RangeDate = string.Concat(getDataKontrak.Data.StarDate, " - ", getDataKontrak.Data.EndDate)
            };

            return new BaseResponseModel<Kontrak>
            {
                Code = getDataKontrak.Code,
                Data = dataKontrak,
                Message = getDataKontrak.Message,
                Status = getDataKontrak.Status,
                TotalItems = getAllDataKontrak.TotalItems
            };
        }
    }
}
