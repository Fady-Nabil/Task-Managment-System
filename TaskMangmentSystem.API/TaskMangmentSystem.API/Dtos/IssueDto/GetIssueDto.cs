namespace TaskMangmentSystem.API.Dtos.IssueDto
{
    public class GetIssueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public GetIssueDto()
        {

        }
        public GetIssueDto(int id, string name, string description, int status,
        string statusName, DateTime addedDate, DateTime? completedDate,
        int? employeeId, string? employeeName)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            StatusName = statusName;
            AddedDate = addedDate;
            CompletedDate = completedDate;
            EmployeeName = employeeName;
            EmployeeId = employeeId;
        }
    }
}
