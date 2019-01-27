using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateApp.API.Data;
using DateApp.API.Dto;
using DateApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DateApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            this._repo = repo;
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

            return StatusCode(201);

        }
    }
}