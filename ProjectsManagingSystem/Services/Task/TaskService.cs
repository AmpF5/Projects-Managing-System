using AutoMapper;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models.ProjectTask;
using ProjectsManagingSystem.Services.Member;

namespace ProjectsManagingSystem.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly ProjectSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;

        public TaskService(ProjectSystemDbContext dbContext, IMapper mapper, IMemberService memberService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _memberService = memberService;
        }

        public ProjectTaskResponseDto GetById(int id)
        {
            if (!_memberService.AuthorizeMemberInProject(id)) return null;
            var task = _dbContext.ProjectTasks
                .FirstOrDefault(x => x.Id == id);

            var result = _mapper.Map<ProjectTaskResponseDto>(task);

            return result;
        }

        public  IEnumerable<ProjectTaskResponseDto> GetAll()
        {
            var tasks = _dbContext
                .ProjectTasks
                .ToList();

            var result = _mapper.Map<List<ProjectTaskResponseDto>>(tasks);

            return result;
        }

        public bool Delete(int id)
        {
            if (!_memberService.AuthorizeModerator(id)) return false;

            var task = _dbContext
                .ProjectTasks
                .FirstOrDefault(e => e.Id == id);

            if (task is null) return false;

            _dbContext.ProjectTasks.Remove(task);
            _dbContext.SaveChanges();

            return true;
        }

        public int Create(ProjectTaskDto dto)
        {

            dto.MemberId = 1;

            var task = _mapper.Map<ProjectTask>(dto);
            _dbContext.ProjectTasks.Add(task);
            _dbContext.SaveChanges();

            return task.Id;
        }

        public bool Update(ProjectTaskDto dto, int id)
        {
            var task = _dbContext
                .ProjectTasks
                .FirstOrDefault(e => e.Id == id);

            if (task == null) return false;

            task.Name = dto.Name;
            task.Description = dto.Description;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
