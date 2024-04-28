using Assel.Contacts.Repository.Entities;
using Assel.Contacts.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Assel.Contacts.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersController(JwtHandler jwtHandler, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        {
            if (userRequest == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<User>(userRequest);
            var result = await _userManager.CreateAsync(user, userRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserAuthenticationRequest userAuthenticationRequest)
        {
            var user = await _userManager.FindByNameAsync(userAuthenticationRequest.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userAuthenticationRequest.Password))
                return Unauthorized(new UserAuthenticationResponse { ErrorMessage = "Invalid Authentication" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new UserAuthenticationResponse { IsAuthSuccessful = true, Token = token });
        }
    }
}
