using ProjectsManagingSystem.Models;

namespace ProjectsManagingSystem.Services.Member;

public interface IMemberService
{
    MemberResponseDto Create(MemberDto dto);
    MemberResponseDto GetById(int id);
    bool Delete(int id);
    bool Update(MemberDto dto, int id);
}