using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;

namespace ProjectsManagingSystem.Services.Task
{
   

    public class TaskService : ITaskService
    {
        private readonly ProjectSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public TaskService(ProjectSystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public ProjectTaskResponseDto GetById(int id)
        {
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
