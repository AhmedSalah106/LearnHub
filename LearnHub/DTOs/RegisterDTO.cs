using System.ComponentModel.DataAnnotations;

namespace LearnHub.DTOs
{
    public class RegisterDTO
    {
        [MinLength(2) , MaxLength(50)]
        public string UserName {  get; set; }
        [MinLength(4) ]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
