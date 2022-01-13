using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fictional_Customers.Data;
using Fictional_Customers.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fictional_Customers.Controllers
{
    public class StaffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Staff.Include(s => s.Assignments);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Assignments)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AssignmentsId"] = new SelectList(_context.Assignments, "AssignmentsId", "Company");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,Name,Email,PhoneNmr,AssignmentsId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                TempData["success"] = "Employee Created Successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignmentsId"] = new SelectList(_context.Assignments, "AssignmentsId", "Company", staff.AssignmentsId);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["AssignmentsId"] = new SelectList(_context.Assignments, "AssignmentsId", "Company", staff.AssignmentsId);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("StaffId,Name,Email,PhoneNmr,AssignmentsId")] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "Employee Updated Successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignmentsId"] = new SelectList(_context.Assignments, "AssignmentsId", "Company", staff.AssignmentsId);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Assignments)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var staff = await _context.Staff.FindAsync(id);
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            TempData["success"] = "Employee Deleted Successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(long id)
        {
            return _context.Staff.Any(e => e.StaffId == id);
        }
    }
}
