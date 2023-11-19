using System.ComponentModel.DataAnnotations;

namespace TaskMangmentSystem.API.Dtos.IdentityDto
{
    public class RegisterDto
    {
        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }
    }
}
