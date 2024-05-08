using api_inges_dev.Context;
using api_inges_dev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace api_inges_dev.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistroController : ControllerBase
{

    private readonly ConexionSQLServer contex;

    public RegistroController(ConexionSQLServer contex)
    {
        this.contex = contex;
    }

    [HttpGet("tecnologias")]
    public IEnumerable<Technologies> Tecnologias()
    {
        return contex.technologies.ToList();
    }
    [HttpPost("registrar")]
    public Registro? registrar(Registro registro)
    {
        contex.Registers.Add(registro);
        contex.SaveChanges();


        return contex.Registers.OrderBy(r => r.id).Last();
    }
}
