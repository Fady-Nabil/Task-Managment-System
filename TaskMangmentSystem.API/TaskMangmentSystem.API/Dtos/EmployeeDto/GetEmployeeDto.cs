using TaskMangmentSystem.Core.Entities;

namespace TaskMangmentSystem.API.Dtos.EmployeeDto
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public GetEmployeeDto()
        {
        }
        public GetEmployeeDto(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
