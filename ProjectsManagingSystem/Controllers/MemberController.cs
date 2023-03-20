using Microsoft.AspNetCore.Mvc;
using ProjectsManagingSystem.Models;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Services.Member;

namespace ProjectsManagingSystem.Controllers;

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

    [HttpGet("{id:int}")]
    public IActionResult GetMember([FromRoute] int id)
    {
        return Ok(_memberService.GetById(id));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteMember([FromRoute] int id)
    {
        var isDeleted = _memberService.Delete(id);
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateMember([FromBody] MemberDto request, [FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest();

        var member = _memberService.Update(request, id);
        return member ? Ok() : NotFound();
    }

    [HttpPost("register")]
    public ActionResult RegisterMember([FromBody] MemberDto dto)
    {
        _memberService.RegisterMember(dto);
        return Ok();
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto dto)
    {
        string token = _memberService.GenerateJwt(dto);
        return Ok(token);

    }

    [HttpGet("test")]
    public IActionResult GetRoleId()
    {
        var roleId = _memberService.GetJwt();
        return Ok(roleId);
    }

}