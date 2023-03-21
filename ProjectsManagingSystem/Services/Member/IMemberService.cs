using ProjectsManagingSystem.Models.Member;

namespace ProjectsManagingSystem.Services.Member;

public interface IMemberService
{
    MemberResponseDto Create(MemberDto dto);
    MemberResponseDto GetById(int id);
    bool Delete(int id);
    bool Update(MemberDto dto, int id);
    void RegisterMember(MemberDto dto);
    string GenerateJwt(LoginDto dto);
    bool AuthorizeModerator(int projectId);
    bool AuthorizeMemberInProject(int projectId);
}