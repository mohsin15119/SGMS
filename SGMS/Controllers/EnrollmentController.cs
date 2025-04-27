using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGMS.Models;

namespace SGMS.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ManagmentSystemContext _context;

        public EnrollmentController(ManagmentSystemContext context)
        {
            _context = context;
        }

        // GET: Enrollment
        public async Task<IActionResult> Index()
        {
            var managmentSystemContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Instructor).Include(e => e.Student);
            return View(await managmentSystemContext.ToListAsync());
        }

        // GET: Enrollment/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Instructor)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Enrollmentid == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollment/Create
        public IActionResult Create()
        {
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid");
            ViewData["Instructorid"] = new SelectList(_context.Instructors, "Instructorid", "Instructorid");
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid");
            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Enrollmentid,Studentid,Courseid,Instructorid,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", enrollment.Courseid);
            ViewData["Instructorid"] = new SelectList(_context.Instructors, "Instructorid", "Instructorid", enrollment.Instructorid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", enrollment.Studentid);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", enrollment.Courseid);
            ViewData["Instructorid"] = new SelectList(_context.Instructors, "Instructorid", "Instructorid", enrollment.Instructorid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", enrollment.Studentid);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Enrollmentid,Studentid,Courseid,Instructorid,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.Enrollmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Enrollmentid))
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
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", enrollment.Courseid);
            ViewData["Instructorid"] = new SelectList(_context.Instructors, "Instructorid", "Instructorid", enrollment.Instructorid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", enrollment.Studentid);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Instructor)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Enrollmentid == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(string id)
        {
            return _context.Enrollments.Any(e => e.Enrollmentid == id);
        }
    }
}
