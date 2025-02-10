using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// A dummy controller for cheking error handlings
public class BuggyController(DataContext context) : BaseApiController
{

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuthError()
    {
        return "secret text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFoundError()
    {
        var thing = context.Users.Find(-1);
        if (thing == null) return NotFound();
        return thing;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequestError()
    {
        return BadRequest("This was not a good request");
    }

    // using Exception Maddleware to handle server errors
    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        var thing = context.Users.Find(-1) ?? throw new Exception("A bad thing has happened");
        return thing;
    }
}
