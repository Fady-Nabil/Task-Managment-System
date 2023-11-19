using System.ComponentModel.DataAnnotations;
using TaskMangmentSystem.Core.Enums;

namespace TaskMangmentSystem.API.Dtos.IssueDto
{
    public class IssueDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IssueStatus Status { get; set; }
        [Required]
        public DateTime AddedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? AssigneeId { get; set; }
        public IssueDto(string name, string description, IssueStatus status,
            DateTime? completedDate, int? assigneeId)
        {
            Name = name;
            Description = description;
            Status = status;
            AddedDate = DateTime.Now;
            CompletedDate = completedDate;
            AssigneeId = assigneeId;
        }
    }
}
