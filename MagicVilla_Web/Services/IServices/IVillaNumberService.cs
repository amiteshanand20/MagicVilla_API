using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaNumberCreateDTO dTO);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dTO);
        Task<T> DeleteAsync<T>(int id);

    }
}
