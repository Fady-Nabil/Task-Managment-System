using System.ComponentModel.DataAnnotations;

namespace TaskMangmentSystem.API.Dtos.EmployeeDto
{
    public class EmployeeDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public EmployeeDto(int id, string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
