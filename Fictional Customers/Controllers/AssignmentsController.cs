using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fictional_Customers.Data;
using Fictional_Customers.Models;

namespace Fictional_Customers.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public long[] Employee { get; set; }
        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {

            return View(await _context.Assignments.Include(x => x.Employee).ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments.Include(x => x.Employee)
                .FirstOrDefaultAsync(m => m.AssignmentsId == id);
            if (assignments == null)
            {
                return NotFound();
            }

            return View(assignments);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            ViewData["Employee"] = new MultiSelectList(_context.Staff.OrderBy(x => x.Name), nameof(Staff.StaffId), nameof(Staff.Name));
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentsId,Company,Stack,Task,ProgLang,StartDate")] Assignments assignments)
        {
            if (ModelState.IsValid)
            {
                foreach(var id in Employee)
                {
                    assignments.Employee.Add(_context.Staff.Find(id));
                }
                _context.Add(assignments);
                await _context.SaveChangesAsync();
                TempData["success"] = "Assignment Created Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(assignments);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments.FindAsync(id);
            if (assignments == null)
            {
                return NotFound();
            }
            return View(assignments);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentsId,Company,Stack,Task,ProgLang,StartDate")] Assignments assignments)
        {
            if (id != assignments.AssignmentsId)
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
                    if (!AssignmentsExists(assignments.AssignmentsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "Assignment Updated Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(assignments);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments
                .FirstOrDefaultAsync(m => m.AssignmentsId == id);
            if (assignments == null)
            {
                return NotFound();
            }

            return View(assignments);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignments = await _context.Assignments.FindAsync(id);
            _context.Assignments.Remove(assignments);
            await _context.SaveChangesAsync();
            TempData["success"] = "Assignment Removed Successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentsExists(int id)
        {
            return _context.Assignments.Any(e => e.AssignmentsId == id);
        }
    }
}
