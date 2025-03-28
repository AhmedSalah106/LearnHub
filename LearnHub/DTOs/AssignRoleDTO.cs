using System.ComponentModel.DataAnnotations;

namespace LearnHub.DTOs
{
    public class AssignRoleDTO
    {
        [MinLength(1)]
        public string RoleName { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
