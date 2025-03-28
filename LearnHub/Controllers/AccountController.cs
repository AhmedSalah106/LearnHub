using LearnHub.DTOs;
using LearnHub.Models;
using LearnHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<ApplicationUser> userManager
            ,IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ApplicationUser user = new ApplicationUser()
                {
                    Email = userDTO.Email,
                    UserName = userDTO.UserName
                };

                IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded)
                {
                    return Ok(new { Message = $"User {userDTO.UserName} Was Registered Successfully" });
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ApplicationUser user = await userManager.FindByNameAsync(userDTO.UserName);

                if (user == null)
                {
                    return BadRequest("Invalid Data");
                }

                bool UserFounded = await userManager.CheckPasswordAsync(user, userDTO.Password);

                if (!UserFounded)
                {
                    return BadRequest("Invalid Data");
                }

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userDTO.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                List<string> roles = (List<string>)await userManager.GetRolesAsync(user);

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"]));
                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                    (
                        issuer: configuration["JWT:ValidISS"],
                        audience: configuration["JWT:ValidAud"],
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials: signingCredentials
                    );

                return Ok(new
                {
                    jwtSecurityToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    expire = jwtSecurityToken.ValidTo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
