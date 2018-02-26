using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAdministratorAPI.Models;

namespace TaskAdministratorAPI.Controllers
{
    public class TaskViewController : Controller
    {
        private readonly TaskAdministratorAPIContext _context;

        public TaskViewController(TaskAdministratorAPIContext context)
        {
            _context = context;
        }

        // GET: TaskView
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }        

        public IActionResult SendToAssignmentsIndex()
        {
            return RedirectToAction("Index", "AssignmentsView");
        }
        public IActionResult SendToUsersIndex()
        {
            return RedirectToAction("Index", "UsersView");
        }

        // GET: TaskView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.SingleOrDefaultAsync(m => m.TaskID == id);

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

            return View(task);
        }

        // GET: TaskView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,Title,BeginDateTime,DeadlineDateTime,Requirements")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tasks);
        }

        // GET: TaskView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.TaskID == id);
            if (tasks == null)
            {
                return NotFound();
            }
            return View(tasks);
        }

        // POST: TaskView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,Title,BeginDateTime,DeadlineDateTime,Requirements")] Tasks tasks)
        {
            if (id != tasks.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.TaskID))
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
            return View(tasks);
        }

        // GET: TaskView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .SingleOrDefaultAsync(m => m.TaskID == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: TaskView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.TaskID == id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskID == id);
        }
    }
}
