using APISecurityBasic.Models;
using APISecurityBasic.Models.DTO;
using APISecurityBasic.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APISecurityBasic.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDBContext _DB;
        private string secretKey;
        public UserRepository(ApplicationDBContext dB, IConfiguration configuration)
        {
            _DB = dB;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool isUniqueUser(string username)
        {
            var user = _DB.LocalUsers.FirstOrDefault(x=>x.UserName == username);
            if(user == null)
            {
                return true;
            }
            return false;   
        }
        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            LocalUser user = new()
            {
                UserName = registerationRequestDTO.Username,
                Password = registerationRequestDTO.Password,
                Name = registerationRequestDTO.Name,
                Role = registerationRequestDTO.Role,
            };
            _DB.LocalUsers.Add(user);
            await _DB.SaveChangesAsync();
            user.Password = "";
            return user;    
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _DB.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() &&
            u.Password == loginRequestDTO.Password
            );
            if(user == null)
            {
               return new LoginResponseDTO()
               {
                   Token="",
                   User= null,
               };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }
                ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }
    }
}
