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