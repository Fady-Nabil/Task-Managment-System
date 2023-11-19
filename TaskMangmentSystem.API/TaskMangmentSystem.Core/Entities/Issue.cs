using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMangmentSystem.Core.Enums;

namespace TaskMangmentSystem.Core.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IssueStatus Status { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? CompletedDate { get; set; }
        public int? AssigneeId { get; set; }
        public Employee Assignee { get; set; }
    }
}
