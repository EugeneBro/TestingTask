using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestingTask.Models;

namespace TestingTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskksController : ControllerBase
    {
        TaskksContext DB;

        public TaskksController(TaskksContext context)
        {
            DB = context;

            //initialize default values for testing app
            if (DB.Taskks.Count<Taskk>() == 0)
            {
                DB.Taskks.Add(new Taskk { Name = "task1", Description = "do job", status_id = 1 });
                DB.Taskks.Add(new Taskk { Name = "task2", Description = "do work", status_id = 2 });
                DB.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Taskk>>> Get()
        {
            return await DB.Taskks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Taskk>> Get(int Id)
        {
            Taskk task = await DB.Taskks.FirstOrDefaultAsync(x => x.Id == Id);
            if (task == null)
                return NotFound();
            return new ObjectResult(task);
        }

        [HttpPost]
        public async Task<ActionResult<Taskk>> Post(Taskk task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            DB.Taskks.Add(task);
            await DB.SaveChangesAsync();
            return Ok(task);
        }
 
        [HttpPut]
        public async Task<ActionResult<Taskk>> Put(Taskk task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            if (!DB.Taskks.Any(x => x.Id == task.Id))
            {
                return NotFound();
            }
 
            DB.Update(task);
            await DB.SaveChangesAsync();
            return Ok(task);
        }
 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Taskk>> Delete(int Id)
        {
            Taskk task = DB.Taskks.FirstOrDefault(x => x.Id == Id);
            if (task == null)
            {
                return NotFound();
            }
            DB.Taskks.Remove(task);
            await DB.SaveChangesAsync();
            return Ok(task);
        }
    }
}
