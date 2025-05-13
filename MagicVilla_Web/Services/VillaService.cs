using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json.Linq;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _httpClient;
        private string villaUrl;
        public VillaService(IHttpClientFactory httpClient,IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaCreateDTO dTO, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.POST,
                Url = villaUrl + "/api/v1/VillaAPI",
                Data = dTO,
                Token = token
            });
        }
        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/v1/VillaAPI/" + id,
                Token = token
            });
        }
        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/VillaAPI",
                Token = token
            });
        }
        public async Task<T> GetAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/VillaAPI/" + id,
                Token = token
            });
        }
        public async Task<T> UpdateAsync<T>(VillaUpdateDTO dTO, string token)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.PUT,
                Url = villaUrl + "/api/v1/VillaAPI/" + dTO.Id,
                Data = dTO,
                Token = token
            });
        }
    }
    
}
