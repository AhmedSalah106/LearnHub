using LearnHub.DTOs;
using LearnHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpPost("AssignRule")]
        public async Task<IActionResult> AssignRule(AssignRoleDTO role)
        {
            try
            {
                if (!ModelState.IsValid || string.IsNullOrWhiteSpace(role.RoleName))
                {
                    return BadRequest(ModelState);
                }

                ApplicationUser user = await userManager.FindByEmailAsync(role.UserEmail);
                if (user == null)
                {
                    return NotFound($"No User With this Email {role.UserEmail}");
                }

                IdentityResult result = await userManager.AddToRoleAsync(user,role.RoleName);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.Select(e => e.Description));
                }

                return Ok(new { Message = $"User {user.UserName} has been assigned to the {role.RoleName} role successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }

        }


    }
}
