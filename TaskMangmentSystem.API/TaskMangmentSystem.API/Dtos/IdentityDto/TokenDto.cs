using System.ComponentModel.DataAnnotations;

namespace TaskMangmentSystem.API.Dtos.IdentityDto
{
    public class TokenDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
