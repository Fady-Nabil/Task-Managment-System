using System.ComponentModel.DataAnnotations;

namespace TaskMangmentSystem.API.Dtos.IdentityDto
{
    public class AddRoleDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
