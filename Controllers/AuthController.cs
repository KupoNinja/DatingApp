using System;
using System.Threading.Tasks;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using DatingApp.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _ase;

        public AuthController(IAuthService ase)
        {
            _ase = ase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            try
            {
                var userToCreate = new User
                {
                    UserName = username
                };

                return Ok(await _ase.Register(userToCreate, password));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}