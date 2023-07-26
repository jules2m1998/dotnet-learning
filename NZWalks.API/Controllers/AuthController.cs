using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs.AuthDTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if(!identityResult.Succeeded || registerRequestDto.Roles == null || !registerRequestDto.Roles.Any()) return BadRequest(identityResult);

            identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
            if(!identityResult.Succeeded) return BadRequest(identityResult);
            return Ok(identityResult);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user == null) return BadRequest("Username or password incorrect");

            var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!checkPasswordResult) return Unauthorized("Username or password incorrect");
            var roles = await userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any()) return Unauthorized();
            var response = new LoginResponseDto
            {
                Jwt = tokenRepository.CreateJWTToken(user, roles.ToList())
            };
            return Ok(response);
        }
    }
}
