using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public List<TodoModel> Get([FromServices] TodoContext context)
        {
            return context.Todos.ToList();
        }

        [HttpGet("/{id:int}")]
        public TodoModel GetById(
        [FromRoute] int id,
        [FromServices] TodoContext context)
        {
            return context.Todos
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id);
        }

        [HttpPost("/")]
        public TodoModel Post(
            [FromBody] TodoModel todo,
            [FromServices] TodoContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }


        [HttpPut("/{id:int}")]
        public TodoModel Put(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] TodoContext context)
        {
            var model = context.Todos
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            if (model == null)
                return todo;

            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();

            return model;
        }

        [HttpDelete("/{id:int}")]
        public string Delete(
            [FromRoute] int id,
            [FromServices] TodoContext context
        )
        {
            var model = context.Todos
                            .AsNoTracking()
                            .FirstOrDefault(x => x.Id == id);

            context.Todos.Remove(model);
            context.SaveChanges();

            return "Task delete sucessfull";
        }

    }
}