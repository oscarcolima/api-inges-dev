using api_inges_dev.Context;
using api_inges_dev.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace api_inges_dev.Controllers;

[Route("api/[controller]")]
// [Produces("application/json")]
[ApiController]
public class PreguntaController : ControllerBase
{

    private readonly ConexionSQLServer contex;

    public PreguntaController(ConexionSQLServer contex)
    {
        this.contex = contex;
    }

    [HttpGet()]
    public ObjectResult getPreguntas()
    {
        List<QuestionsAnswers> respuestas = [];
        var questions = contex.Questions.Where(x => !x.eliminado).ToList();



        foreach (Questions item in questions)
        {
            var respuest = contex.Answers
                .Where(x => x.fk_question == item.id)
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
                answers = respuest,

            });

        }

        return Ok(respuestas);
    }


    [HttpDelete("elimar")]

    public ObjectResult eleiminar(int id)
    {
        var res = contex.Questions.FirstOrDefault(r => r.id == id);

        /*   if (res == null)
          {
              return NoContent();
          } */
        res!.eliminado = true;

        contex.Questions.Update(res);
        contex.SaveChanges();

        return Ok(res);

    }


    [HttpPut("editar")]
    public ObjectResult editar(int pregunta, QuestionsAnswers question)
    {

        var fecha = DateTime.Now;
        var editar = contex.Questions.First(x => x.id == pregunta);

        editar.question = question.question;
        editar.rightScore = question.rightScore;
        editar.wrongScore = question.wrongScore;
        editar.update_at = fecha;

        contex.Questions.Update(editar);


        foreach (var respuesta in question.answers!)
        {
            var editarRespuesta = contex.Answers.First(x => x.id == respuesta.id);
            editarRespuesta.iscorrect = respuesta.iscorrect;
            editarRespuesta.answer = respuesta.answer;
            editarRespuesta.update_at = fecha;
            contex.Answers.Update(editarRespuesta);
        }

        contex.SaveChanges();

        return Ok(question);

    }

    [HttpPost("crear")]
    public ObjectResult crear(QuestionsAnswers question)
    {

        contex.Questions.Add(new Questions
        {
            question = question.question,
            type = question.type,
            eliminado = question.eliminado,
            rightScore = question.rightScore,
            wrongScore = question.wrongScore,
            create_at = question.create_at,
            update_at = question.update_at,
        });

        contex.SaveChanges();
        var nuevo = contex.Questions.OrderBy(r => r.id).Last();


        foreach (var respuesta in question.answers!)
        {
            respuesta.fk_question = nuevo.id;
            var editarRespuesta = contex.Answers.Add(respuesta);
        }

        contex.SaveChanges();

        return Ok(true);

    }

}

