﻿using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Project;
using ProjectsManagingSystem.Models.ProjectTask;

namespace ProjectsManagingSystem.Services.Project;

public interface IProjectService
{
    ProjectResponseDto Create(ProjectDto dto);
    ProjectResponseDto GetById(int id);
    ProjectTaskResponseDto AddTaskToProject(int id, ProjectTaskDto dto);
    IEnumerable<MemberResponseDto> GetMembers(int id);
    IEnumerable<ProjectTaskResponseDto> GetTasks(int id);
    IEnumerable<ProjectTaskResponseDto> GetMemberProjectTask(int id, int memberId);
    bool AddMemberToProject(int id, int memberId);
    bool AssignMemberToTask(int projectId, int taskId, int memberId);
    bool Delete(int id);
    bool Update(ProjectDto dto, int id);
}