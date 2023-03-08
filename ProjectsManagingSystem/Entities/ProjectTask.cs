using System.Data;
using Microsoft.VisualBasic;

namespace ProjectsManagingSystem.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
        public string Description { get; set; }
        public DateTime DeadOfCreation { get; set; }
        public DateTime DateOfCreation { get; set; }
        public Member AssignTo{ get; set; }
        public int AssignToId{ get; set; }
        //public Member Creator { get; set; }
        public State State { get; set; }
    }
}
