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
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole>roleManager) 
        {
            this.roleManager = roleManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole(RoleDTO roleDTO)
        {
            try
            {

                if (!ModelState.IsValid||string.IsNullOrWhiteSpace(roleDTO.RoleName))
                {
                    return BadRequest(ModelState);
                }

                IdentityRole role = new IdentityRole()
                {
                    Name = roleDTO.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return Ok(new { Message = $"Role {roleDTO.RoleName} Created Successfully" });
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

    }
}
