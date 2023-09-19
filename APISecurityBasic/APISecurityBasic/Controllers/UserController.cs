using APISecurityBasic.Models;
using APISecurityBasic.Models.DTO;
using APISecurityBasic.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APISecurityBasic.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            this._response = new();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            bool ifUserNameUnique =  _userRepo.isUniqueUser(model.Username);
            if(!ifUserNameUnique)
            {
                _response.StatusCode= HttpStatusCode.BadRequest;
                _response.IsSuccess= false;
                _response.ErrorMessages.Add("Username already esxits");
                return BadRequest(_response);
            }
            var user = await _userRepo.Register(model);
            if(user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while register");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }
    }
}
