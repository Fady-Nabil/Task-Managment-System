using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaskMangmentSystem.API.Dtos.IssueDto;
using TaskMangmentSystem.API.Errors;
using TaskMangmentSystem.API.Hubs;
using TaskMangmentSystem.Core.Entities;
using TaskMangmentSystem.Core.Interfaces;

namespace TaskMangmentSystem.API.Controllers
{
    public class IssuesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationsHub, INotificationsHub> _hubContext;
        public IssuesController(IUnitOfWork unitOfWork, IMapper mapper,
            IHubContext<NotificationsHub, INotificationsHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetIssueDto>> GetIssueById(int id)
        {
            var issue = await _unitOfWork.Repository<Issue>().GetByIdAsync(id);
            if (issue is null) return NotFound(new ApiResponse(404, $"Not found With Id {id}"));
            return _mapper.Map<Issue, GetIssueDto>(issue);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetIssueDto>>> GetIssues()
        {
            var issues = await _unitOfWork.Repository<Issue>().ListAllAsync();
            return _mapper.Map<List<Issue>, List<GetIssueDto>>(issues.ToList());
        }

        [HttpPost("add")]
        public async Task<ActionResult<GetIssueDto>> AddAsync([FromBody] IssueDto issueDto)
        {
            var issueToAdd = new Issue
            {
                Name = issueDto.Name,
                Description = issueDto.Description,
                Status = issueDto.Status,
                CompletedDate = issueDto.CompletedDate,
                AssigneeId = issueDto?.AssigneeId,
            };

            var addedIssue = await _unitOfWork.Repository<Issue>().AddAsync(issueToAdd);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return BadRequest(new ApiResponse(400, $"Something Wrong happened While Adding"));
           
            await _hubContext.Clients.All.IssueAdded(issueDto!);
            return _mapper.Map<Issue, GetIssueDto>(addedIssue);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<GetIssueDto>> UpdateAsync(int id, [FromBody] IssueDto issueDto)
        {
            var getIssue = await _unitOfWork.Repository<Issue>().GetByIdAsync(id);
            if (getIssue is null) return NotFound(new ApiResponse(404, $"Not found With Id {id}"));

            getIssue.Name = issueDto.Name;
            getIssue.Description = issueDto.Description;
            getIssue.Status = issueDto.Status;

            if(issueDto.CompletedDate is not null)
                getIssue.CompletedDate = issueDto.CompletedDate;
            if(issueDto.AssigneeId is not null)
                getIssue.AssigneeId = issueDto?.AssigneeId;

            var updatedIssue = _unitOfWork.Repository<Issue>().Update(getIssue);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return BadRequest(new ApiResponse(400, $"Something Wrong happened While Updating"));
            
            await _hubContext.Clients.All.IssueUpdated(issueDto!);
            return _mapper.Map<Issue, GetIssueDto>(updatedIssue);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<GetIssueDto>> DeleteAsync(int id)
        {
            var getIssue = await _unitOfWork.Repository<Issue>().GetByIdAsync(id);
            if (getIssue is null) return NotFound(new ApiResponse(404, $"Not found With Id {id}"));

            var deletedIssue = _unitOfWork.Repository<Issue>().Delete(getIssue);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return BadRequest(new ApiResponse(400, $"Something Wrong happened While Deleting"));
            return _mapper.Map<Issue, GetIssueDto>(deletedIssue);
        }
    }
}
