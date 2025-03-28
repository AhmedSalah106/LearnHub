using System.ComponentModel.DataAnnotations;

namespace LearnHub.DTOs
{
    public class RoleDTO
    {
        [MinLength(1)]
        public string RoleName { get; set; }
    }
}
