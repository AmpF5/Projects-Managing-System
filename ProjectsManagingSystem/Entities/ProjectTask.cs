using System.Data;
using Microsoft.VisualBasic;

namespace ProjectsManagingSystem.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int MemberId{ get; set; }
        public Member Member { get; set; }
        //public Member Creator { get; set; }
        public State State { get; set; }
    }
}