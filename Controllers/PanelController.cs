using api_inges_dev.Context;
using api_inges_dev.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace api_inges_dev.Controllers;

[Route("api/[controller]")]
// [Produces("application/json")]
[ApiController]
public class PanelController : ControllerBase
{

    private readonly ConexionSQLServer contex;

    public PanelController(ConexionSQLServer contex)
    {
        this.contex = contex;
    }

    [HttpGet("getPanel")]
    public ObjectResult getpanel()
    {
        var res = contex.Registers.ToList();
        return Ok(res);
    }



    [HttpPost("login")]
    public ObjectResult Login([FromBody]Login login)
    {
        var res = contex.Registers.ToList();
        return Ok(res);
    }
}

