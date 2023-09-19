using APISecurityBasic.Models;
using APISecurityBasic.Models.DTO;

namespace APISecurityBasic.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO);  
    }
}
