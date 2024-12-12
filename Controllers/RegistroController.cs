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
    public Registro registrar(Registro registro)
    {
        
        try
        {
            contex.Registers.Add(registro);
            contex.SaveChanges();
            var user = contex.Registers.OrderBy(r => r.id).Last();
           
            return user;

        }
        catch (Exception e)
        {
            
            return new Registro() 
            { 
                correct_answers=0,
                eliminado = false,
                email = "",
                experiencia = "",
                fk_technology = 0,
                id = 0,
                level = "",
                nombre_completo = e.Message,
                profesion = "",
                score = 0,
                telefono = ""
            };

       
        }

       
        //var url = "https://hooks.zapier.com/hooks/catch/8944102/2ucbrwg/";
        //var json = JsonConvert.SerializeObject(user);
        //var content = new StringContent(json, Encoding.UTF8, "application/json");
        //Console.WriteLine(json);


        //using (var client = new HttpClient())
        //{
        //    var response = await client.PostAsync(url, content);
        //    string responseString = await response.Content.ReadAsStringAsync();

        //    Console.WriteLine($"Status Code: {response.StatusCode}");
        //    Console.WriteLine($"Response: {responseString}");
        //}

    }

    [HttpGet("Getlevel")]
    public Registro getLevel(int id)
    {

        var res = contex.Registers.FirstOrDefault(r => r.id == id);

        return res;
    }
}
