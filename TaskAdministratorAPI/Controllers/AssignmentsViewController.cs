using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TaskAdministratorAPI.Models;

namespace TaskAdministratorAPI.Controllers
{
    public class AssignmentsViewController : Controller
    {
        private readonly TaskAdministratorAPIContext _context;

        public AssignmentsViewController(TaskAdministratorAPIContext context)
        {
            _context = context;
        }
        public IActionResult SendToTasksIndex()
        {
            return RedirectToAction("Index", "TaskView");
        }

        // GET: AssignmentsView
        public async Task<IActionResult> Index()
        {
            return View(await _context.Assignments.ToListAsync());
        }

        // GET: AssignmentsView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments
                .SingleOrDefaultAsync(m => m.TaskID == id);
            if (assignments == null)
            {
                return NotFound();
            }

            return View(assignments);
        }

        // GET: AssignmentsView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssignmentsView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,UserID")] Assignments assignments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assignments);
        }

        // GET: AssignmentsView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments.SingleOrDefaultAsync(m => m.TaskID == id);
            if (assignments == null)
            {
                return NotFound();
            }
            return View(assignments);
        }

        // POST: AssignmentsView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,UserID")] Assignments assignments)
        {
            if (id != assignments.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentsExists(assignments.TaskID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(assignments);
        }

        // GET: AssignmentsView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments
                .SingleOrDefaultAsync(m => m.TaskID == id);
            if (assignments == null)
            {
                return NotFound();
            }

            return View(assignments);
        }

        // POST: AssignmentsView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignments = await _context.Assignments.SingleOrDefaultAsync(m => m.TaskID == id);
            _context.Assignments.Remove(assignments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentsExists(int id)
        {
            return _context.Assignments.Any(e => e.TaskID == id);
        }
    }
}
