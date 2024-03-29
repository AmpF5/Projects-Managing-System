﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models.Member;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProjectsManagingSystem.Exceptions;
using ProjectsManagingSystem.ExtensionMethods;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;

namespace ProjectsManagingSystem.Services.Member;

public class MemberService : IMemberService
{
    private readonly ProjectSystemDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<Entities.Member> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MemberService(ProjectSystemDbContext dbContext, IMapper mapper, IPasswordHasher<Entities.Member> passwordHasher, 
        AuthenticationSettings authenticationSettings, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
        _httpContextAccessor = httpContextAccessor;
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
        var loggedMember = GetMemberIdFromJwt();


        var hasCommonProject = _dbContext.MemberProjects
            .Any(mp => mp.MemberId == loggedMember && _dbContext.MemberProjects
                .Any(mp2 => mp2.MemberId == id && mp2.ProjectId == mp.ProjectId));

        if (!hasCommonProject) return null;

        var member = _dbContext.Members.FirstOrDefault(i => i.Id == id);
        return _mapper.Map<MemberResponseDto>(member);
    }

    public bool Delete(int id)
    {
        var loggedMember = GetMemberIdFromJwt();
        if (id != loggedMember) return false;

        var member = _dbContext.Members.FirstOrDefault(i => i.Id == id);
        if (member is null) return false;
        
        _dbContext.Members.Remove(member);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(MemberDto dto, int id)
    {
        var loggedMember = GetMemberIdFromJwt();
        if (id != loggedMember) return false;

        var member = _dbContext.Members.FirstOrDefault(i => i.Id == id);
        if (member is null) return false;
        member.Name = dto.Name;
        member.Surname = dto.Surname;
        member.Email = dto.Email;
        member.Password = dto.Password;

        _dbContext.SaveChanges();
        return true;
    }

    public void RegisterMember(MemberDto dto)
    {
        var newMember = new Entities.Member()
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email

        };
        var hashedPassword = _passwordHasher.HashPassword(newMember, dto.Password);
        newMember.Password = hashedPassword;
        
        _dbContext.Members.Add(newMember);
        _dbContext.SaveChanges();
    }

    public string GenerateJwt(LoginDto dto)
    {
        var member = _dbContext.Members
            .FirstOrDefault(u => u.Email == dto.Email);

        if (member is null)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var result = _passwordHasher.VerifyHashedPassword(member, member.Password, dto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{member.Name} {member.Surname}"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public bool AuthorizeModerator(int projectId)
    {
        if (_httpContextAccessor.HttpContext == null) return false;
        
        var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var id = int.Parse(result);

        var project = _dbContext
            .Projects
            .IncludeMembers()
            .FirstOrDefault(x => x.Id == projectId);
        if (project is null) return false;

        var member = project.MemberProjects.FirstOrDefault(x => x.MemberId == id);
        if (member is null) return false;
        return member is { Role: Role.Moderator or Role.Administrator };
    }

    public bool AuthorizeMemberInProject(int projectId)
    {
        if (_httpContextAccessor.HttpContext == null) return false;

        var jwtId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var memberId = int.Parse(jwtId);

        var project = _dbContext
            .Projects
            .IncludeMembers()
            .FirstOrDefault(x => x.Id == projectId);
        if (project is null) return false;

        var member = project.MemberProjects.FirstOrDefault(x => x.MemberId == memberId);
        return member is not null;
    }

    public int GetMemberIdFromJwt()
    {
        if (_httpContextAccessor.HttpContext == null) return 0;
        var jwtId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var memberId = int.Parse(jwtId);
        return memberId;

    }
}
