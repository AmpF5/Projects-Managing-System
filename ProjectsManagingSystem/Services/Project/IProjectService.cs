﻿using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Models;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Project;
using ProjectsManagingSystem.Models.ProjectTask;

namespace ProjectsManagingSystem.Services.Project;

public interface IProjectService
{
    ProjectResponseDto Create(ProjectDto dto);
    ProjectResponseDto GetById(int id);
    IEnumerable<MemberResponseDto> GetMembers(int id);
    IEnumerable<ProjectTaskResponseDto> GetTasks(int id);
    bool AssignMemberToTask(int projectId, int taskId, int memberId);
    bool Delete(int id);
    bool Update(ProjectDto dto, int id);
}