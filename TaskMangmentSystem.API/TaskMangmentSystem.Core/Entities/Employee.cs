namespace TaskMangmentSystem.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Issue> AssignedIssues { get; private set; } = new HashSet<Issue>();

    }
}
