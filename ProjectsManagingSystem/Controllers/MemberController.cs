using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Services.Member;

namespace ProjectsManagingSystem.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MemberController : Controller
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    public IActionResult CreateMember([FromBody] MemberDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var member = _memberService.Create(request);
        return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
    }

    [HttpGet("{memberId:int}")]
    public IActionResult GetMember([FromRoute] int memberId)
    {
        return Ok(_memberService.GetById(memberId));
    }

    [HttpDelete("{memberId:int}")]
    public IActionResult DeleteMember([FromRoute] int memberId)
    {
        var isDeleted = _memberService.Delete(memberId);
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{memberId:int}")]
    public IActionResult UpdateMember([FromBody] MemberDto request, [FromRoute] int memberId)
    {
        if (!ModelState.IsValid) return BadRequest();
        var member = _memberService.Update(request, memberId);
        return member ? Ok() : NotFound();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public ActionResult RegisterMember([FromBody] MemberDto dto)
    {
        _memberService.RegisterMember(dto);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto dto)
    {
        var  token = _memberService.GenerateJwt(dto);
        return Ok(token);
    }
}