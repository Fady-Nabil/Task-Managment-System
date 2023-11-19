using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
using TaskMangmentSystem.Core.Entities;


namespace TaskMangmentSystem.Infrastructure.Data
{
    public class IssueContext : DbContext
    {
        public IssueContext(DbContextOptions<IssueContext> options) 
            : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Issue> Issues { get; set; }
    }
}
