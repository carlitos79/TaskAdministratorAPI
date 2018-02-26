using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAdministratorAPI.Models;

namespace TaskAdministratorAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        private readonly TaskAdministratorAPIContext _context;

        public TasksController(TaskAdministratorAPIContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<Tasks> GetTask()
        {
            return _context.Tasks;
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await _context.Tasks.Where(m => m.TaskID == id).FirstOrDefaultAsync();

            List<Users> listOfUsers = new List<Users>();
            List<Users> tempList = new List<Users>();

            var users = from u in _context.Users select u;
            var assignments = from a in _context.Assignments select a;

            foreach (var user in users)
            {
                foreach (var assignment in assignments)
                {
                    if (user.UserID == assignment.UserID && task.TaskID == assignment.TaskID)
                    {
                        listOfUsers.Add(user);
                    }
                }                
            }

            foreach (var user in listOfUsers)
            {
                if (!tempList.Contains(user))
                {
                    tempList.Add(user);
                }
            }

            task.Responsables = tempList;

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks([FromRoute] int id, [FromBody] Tasks tasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tasks.TaskID)
            {
                return BadRequest();
            }

            _context.Entry(tasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> PostTasks([FromBody] Tasks tasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = tasks.TaskID }, tasks);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.TaskID == id);
            if (tasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return Ok(tasks);
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskID == id);
        }
    }
}