using api_inges_dev.Context;
using api_inges_dev.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    public async Task<Registro?> registrar(Registro registro)
    {
        contex.Registers.Add(registro);
        contex.SaveChanges();

        var user = contex.Registers.OrderBy(r => r.id).Last();

       
        var url = "https://hooks.zapier.com/hooks/catch/8944102/2ucbrwg/";
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine(json);


        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(url, content);
            string responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response: {responseString}");
        }

        return user;
    }

    [HttpGet("Getlevel")]
    public Registro getLevel(int id)
    {

        var res = contex.Registers.FirstOrDefault(r => r.id == id);

        return res;
    }
}
