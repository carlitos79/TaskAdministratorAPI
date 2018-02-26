using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAdministratorAPI.Models;

namespace TaskAdministratorAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Assignments")]
    public class AssignmentsController : Controller
    {
        private readonly TaskAdministratorAPIContext _context;

        public AssignmentsController(TaskAdministratorAPIContext context)
        {
            _context = context;
        }

        // GET: api/Assignments
        // Read
        [HttpGet]
        public IEnumerable<Assignments> GetAssignments()
        {
            return _context.Assignments;
        }

        // GET: api/Assignments/5
        // Read
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assignments = await _context.Assignments.SingleOrDefaultAsync(m => m.TaskID == id);

            if (assignments == null)
            {
                return NotFound();
            }

            return Ok(assignments);
        }

        // PUT: api/Assignments/5
        // Update/Replace
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignments([FromRoute] int id, [FromBody] Assignments assignments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assignments.TaskID)
            {
                return BadRequest();
            }

            _context.Entry(assignments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentsExists(id))
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

        // POST: api/Assignments
        // Create
        [HttpPost]
        public async Task<IActionResult> PostAssignments([FromBody] Assignments assignments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Assignments.Add(assignments);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssignmentsExists(assignments.TaskID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAssignments", new { id = assignments.TaskID }, assignments);
        }

        // DELETE: api/Assignments/5/5
        [HttpDelete("{taskId}/{userId}")]
        public async Task<IActionResult> DeleteAssignments(int taskId, int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assignments = await _context.Assignments.SingleOrDefaultAsync(m => m.TaskID == taskId && m.UserID == userId);

            if (assignments == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignments);
            await _context.SaveChangesAsync();

            return Ok(assignments);
        }

        private bool AssignmentsExists(int id)
        {
            return _context.Assignments.Any(e => e.TaskID == id);
        }
    }
}