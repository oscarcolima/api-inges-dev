using api_inges_dev.Context;
using api_inges_dev.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

        var random = new Random(contex.Questions.ToList().Count);
        var questions = contex.Questions.ToList();
        var randomItems = questions
            .OrderBy(x => random.Next())
            .Take(25)
            .Select(x => x);

        foreach (Questions item in randomItems)
        {
            var respuest = contex.Answers
                .Where(x => x.fk_question == item.id)
                .ToList();
            random = new Random(respuest.Count);

            var randomRespuesta = respuest
                .OrderBy(x => random.Next())
                .Select(x => x)
                .ToList();

            respuestas.Add(new QuestionsAnswers
            {
                id = item.id,
                question = item.question,
                type = item.type,
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
    public ObjectResult calificar(int usuario, List<QuestionsAnswers> respuestas)
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

        return Created();

    }
}