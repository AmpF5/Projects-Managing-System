namespace ProjectsManagingSystem.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<MemberProject> MemberProjects { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
        //public Role Role { get; set; }


    }
}
