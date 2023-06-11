using AspCoreWebAPIDemos.Models;
using AspCoreWebAPIDemos.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/authen")]
    [ApiController]
    public class AuthenController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public class AuthenRequestBody
        {
            public string? UserName { get; set; }

            public string? Password { get; set; }    
        }

        private class UserCrendetials {

            public int UserId { get; set; }

            public string Username { get; set; }

            public string Email { get; set; }

            public string FullName { get; set; }   

            public string City { get; set; }    

            public UserCrendetials(int userId, string username, string email, string fullName, string city)
            {
                UserId = userId;
                Username = username;
                Email = email;
                FullName = fullName;
                City = city;
            }
        }

        public AuthenController(
            IConfiguration configuration, 
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate(AuthenRequestBody authenRequestBody)
        {
            var user = await ValidateUserCredentials(authenRequestBody.UserName, authenRequestBody.Password);

            if (user is null)
            {
                return Unauthorized("You are not authorized to access this resource");
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretKey"]!));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimForToken = new List<Claim>
            {
                new Claim("id", user.UserId.ToString()),
                new Claim("username", user.Username),
                new Claim("email", user.Email),
                new Claim("full_name", user.FullName),
                new Claim("city", user.City)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                signingCredentials); 

            var returnToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(returnToken);
        }

        private async Task<UserCrendetials?> ValidateUserCredentials(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                return null;
            }

            var authorizedUser = await _cityInfoRepository.GetUserCredentials(username, password);
            if (authorizedUser is null) {
                return null;
            }
            var userCredential = _mapper.Map<User>(authorizedUser);

            return new UserCrendetials(userCredential.Id, userCredential.Name!, $"{userCredential.Name}@gmail.com", "Authorized User", authorizedUser.City);
        }
    }
}

