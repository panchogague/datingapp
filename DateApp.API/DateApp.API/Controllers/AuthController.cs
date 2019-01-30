using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DateApp.API.Data;
using DateApp.API.Dto;
using DateApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DateApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this._repo = repo;
            this._config = config;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (UserDTO userDTO)
        {
            userDTO.UserName = userDTO.UserName.ToLower();

            if (await _repo.ExistUser(userDTO.UserName))
                return BadRequest("Username already exists");

            var newUser = new UserModel
            {
                UserName = userDTO.UserName
            };

            var createdUser = await _repo.Register(newUser, userDTO.Password);

            string token = await (GenerateToken(createdUser.ID.ToString(), createdUser.UserName));
            return Ok(new
            {
                user= createdUser,
                token
            });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (UserForLoginDTO userForLogin)
        {
            var userFromRepo = await _repo.Login(userForLogin.UserName.ToLower(), userForLogin.Password);

            if (userFromRepo == null)
                return BadRequest();

            string token = await (GenerateToken(userFromRepo.ID.ToString(), userFromRepo.UserName));

            return Ok(new
            {
                token
            });

        }

        public async Task<string> GenerateToken(string userID,string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userID),
                new Claim (ClaimTypes.Name, userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}