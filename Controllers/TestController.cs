using api_inges_dev.Context;
using api_inges_dev.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;


namespace api_inges_dev.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{

    private readonly ConexionSQLServer contex;

    public TestController(ConexionSQLServer contex)
    {
        this.contex = contex;
    }

    [HttpGet("getquestions")]
    public IEnumerable<QuestionsAnswers> getquestions()
    {
        List<QuestionsAnswers> respuestas = [];
        var questions = contex.Questions.ToList();
        var random = new Random();
        var randomItems = questions.Where(x => !x.eliminado)
            .OrderBy(x => random.Next(0, questions.Count))
            .Take(25)
            .Select(x => x);


        foreach (Questions item in randomItems)
        {
            var respuest = contex.Answers
                .Where(x => x.fk_question == item.id)
                .ToList();

            var randomRespuesta = respuest
                .OrderBy(x => random.Next(0, respuest.Count))
                .Select(x => x)
                .ToList();

            respuestas.Add(new QuestionsAnswers
            {
                id = item.id,
                question = item.question,
                type = item.type,
                eliminado = item.eliminado,
                rightScore = item.rightScore,
                wrongScore = item.wrongScore,
                create_at = item.create_at,
                update_at = item.update_at,
                answers = randomRespuesta,

            });

        }

        return respuestas;

    }


    [HttpPost("calificar")]
    public async Task<ObjectResult> calificar(int usuario, List<QuestionsAnswers> respuestas)
    {
        int calificar = 0;
        byte coret = 0;

        foreach (var pre in respuestas)
        {
            bool sino = pre.answers!.First().iscorrect;
            if (sino) { calificar += pre.rightScore; coret += 1; }
            else calificar -= pre.rightScore;
        }

        calificar = calificar / respuestas.Count();

        if (calificar < 0) calificar = 0;



        string level = "";

        if (calificar >= 25) level = "C1";
        else if (calificar >= 20) level = "B2";
        else if (calificar >= 15) level = "B1";
        else if (calificar >= 10) level = "A2";
        else if (calificar >= 5) level = "A1";
        else level = "Beginner";

        // // Para el caso especial
        // if (correctAnswersCount >= 14 && totalScore >= 10 && totalScore <= 15)
        // {
        //     level = "A2 o B1";
        // }

        var usuairio = contex.Registers.FirstOrDefault(x => x.id == usuario);
        usuairio!.score = calificar;
        usuairio!.correct_answers = coret;
        usuairio!.level = level;
        contex.SaveChanges();

        var url = "https://hooks.zapier.com/hooks/catch/8944102/2ucbrwg/";
        var json = JsonConvert.SerializeObject(usuairio);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine(json);


        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(url, content);
            string responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response: {responseString}");
        }



        return Created();

    }
}