using AutoMapper;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models;
using ProjectsManagingSystem.Models.Member;

namespace ProjectsManagingSystem.Services.Member;

public class MemberService : IMemberService
{
    private readonly ProjectSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public MemberService(ProjectSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public MemberResponseDto Create(MemberDto dto)
    {
        var member = _mapper.Map<Entities.Member>(dto);
        _dbContext.Members.Add(member);
        _dbContext.SaveChanges();
        
        return _mapper.Map<MemberResponseDto>(member);
    }

    public MemberResponseDto GetById(int id)
    {
        var member = _dbContext.Members.FirstOrDefault(i => i.Id == id);
        return _mapper.Map<MemberResponseDto>(member);
        
    }

    public bool Delete(int id)
    {
        var member = _dbContext.Members.FirstOrDefault(i => i.Id == id);
        if (member is null) return false;
        _dbContext.Members.Remove(member);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(MemberDto dto, int id)
    {
        var member = _dbContext.Members.FirstOrDefault(i => i.Id == id);
        if (member is null) return false;
        member.Name = dto.Name;
        member.Surname = dto.Surname;
        member.Email = dto.Email;
        member.Password = dto.Password;

        _dbContext.SaveChanges();
        return true;
    }
}