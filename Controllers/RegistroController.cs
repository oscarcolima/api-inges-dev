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

    [HttpGet(Name = "tecnologias")]
    public IEnumerable<Technologies> Tecnologias()
    {
        return contex.technologies.ToList();
    }
    [HttpGet(Name = "clientes")]
    public IEnumerable<Technologies> Clinetes()
    {
        return contex.technologies.ToList();
    }
}
