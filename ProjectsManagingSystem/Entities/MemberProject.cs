namespace ProjectsManagingSystem.Entities;

public class MemberProject
{
    public int MemberId { get; set; }
    public Member Member { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public Role Role { get; set; }
}