using TaskMangmentSystem.API.Dtos.IssueDto;

namespace TaskMangmentSystem.API.Hubs
{
    public interface INotificationsHub
    {
        Task IssueAdded(IssueDto issueDto);
        Task IssueUpdated(IssueDto issueDto);
    }
}
