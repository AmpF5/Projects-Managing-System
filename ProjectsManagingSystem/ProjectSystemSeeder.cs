using ProjectsManagingSystem.Entities;
using System.Net;
using Microsoft.VisualBasic;

namespace ProjectsManagingSystem
{
    public class ProjectSystemSeeder
    {

        private readonly ProjectSystemDbContext _dBContext;

        public ProjectSystemSeeder(ProjectSystemDbContext dBContext)
        {
            _dBContext = dBContext;
        }

        public void Seed()
        {
            if (_dBContext.Database.CanConnect())
            {
                if (!_dBContext.Projects.Any())
                {
                    var projects = GetProjects();
                    _dBContext.Projects.AddRange(projects);
                    _dBContext.SaveChanges();
                }
            }
            
        }
        private IEnumerable<Project> GetProjects()
        {
            var projects = new List<Project>()
            {
                new Project()
                {
                    Name = "Project MVC",
                    Description = "Project model-view-controler",
                    Deadline = new DateTime(2023, 06, 20),
                    DateOfCreation = new DateTime(2023, 03, 20),
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask()
                        {
                            Name = "Create Views",
                            Description = "Create Views for frontend",
                            DateOfCreation = new DateTime(2023, 03, 20),
                            Deadline = new DateTime(2023, 04,30),
                            State = State.ToDo,
                            MemberId = 1

                        },
                        new ProjectTask()
                        {
                            Name = "Configure DbContexClass",
                            Description = "Configure DbContexClass for entityframework",
                            DateOfCreation = new DateTime(2023, 03, 20),
                            Deadline = new DateTime(2023, 04,30),
                            State = State.ToDo,
                            MemberId = 2
                        },
                    },
                },
                new Project()
                {
                    Name = "Project API",
                    Description = "Project ASP.NET CORE API",
                    Deadline = new DateTime(2023, 08, 30),
                    DateOfCreation = new DateTime(2023, 05, 30),
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask()
                        {
                            Name = "Create Models",
                            Description = "Create Models for user, restaurant and dish",
                            DateOfCreation = new DateTime(2023, 03, 20),
                            Deadline = new DateTime(2023, 04,30),
                            State = State.ToDo,
                            MemberId = 1

                        },

                        new ProjectTask()
                        {
                            Name = "Configure DbContexClass",
                            Description = "Configure DbContexClass for entityframework",
                            DateOfCreation = new DateTime(2023, 03, 20),
                            Deadline = new DateTime(2023, 04,30),
                            State = State.ToDo,
                            MemberId = 1

                        }

                    },
                },
            };
            return projects;
        }
    }
}
