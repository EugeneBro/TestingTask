using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestingTask.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestingTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        TaskksContext DB;

        public StatusController(TaskksContext context)
        {
            DB = context;

            if (DB.Statuses.Count() == 0)
            {
                DB.Statuses.Add(new Status { status = "Created" });
                DB.Statuses.Add(new Status { status = "In progress" });
                DB.Statuses.Add(new Status { status = "Finished" });
                DB.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> Get()
        {
            return await DB.Statuses.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> Get(int Id)
        {
            Status status = await DB.Statuses.FirstOrDefaultAsync(x => x.status_id == Id);
            if (status == null)
                return NotFound();
            return new ObjectResult(status);
        }
    }
}
