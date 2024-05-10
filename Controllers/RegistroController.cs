using api_inges_dev.Context;
using api_inges_dev.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace api_inges_dev.Controllers;

[Route("api/[controller]")]
// [Produces("application/json")]
[ApiController]
public class RegistroController : ControllerBase
{

    private readonly ConexionSQLServer contex;

    public RegistroController(ConexionSQLServer contex)
    {
        this.contex = contex;
    }

    [HttpGet("tecnologias")]
    public ObjectResult Tecnologias()
    {
        var res = contex.technologies.ToList();
        return Ok(res);
    }
    [HttpPost("registrar")]
    public Registro? registrar(Registro registro)
    {
        contex.Registers.Add(registro);
        contex.SaveChanges();


        return contex.Registers.OrderBy(r => r.id).Last();
    }

    [HttpGet("Getlevel")]
    public Registro getLevel(int id)
    {

        var res = contex.Registers.FirstOrDefault(r => r.id == id);

        return res;
    }
}
