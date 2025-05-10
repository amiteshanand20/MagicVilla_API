using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClient;
        private string villaUrl;
        public VillaNumberService(IHttpClientFactory httpClient,IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dTO)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.POST,
                Url = villaUrl + "/api/VillaNumberAPI",
                Data = dTO
            });
        }
        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaNumberAPI/" + id
            });
        }
        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumberAPI"
            });
        }
        public async Task<T> GetAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumberAPI/" + id
            });
        }
        public async Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dTO)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.PUT,
                Url = villaUrl + "/api/VillaNumberAPI/" + dTO.VillaNo,
                Data = dTO
            });
        }
    }
    
}
