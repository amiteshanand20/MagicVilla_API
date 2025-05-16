using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string Username);
        Task<LoginResponseDTO> Login (LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register (RegistrationRequestDTO registrationRequestDTO);

    }
}
