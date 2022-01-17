#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CodingsController : Controller
    {
        private readonly RyanDbContext _context;

        public CodingsController(RyanDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Codings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Codings.ToListAsync());
        }

        // GET: Admin/Codings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coding = await _context.Codings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coding == null)
            {
                return NotFound();
            }

            return View(coding);
        }

        // GET: Admin/Codings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Codings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Percent,Id,CreatedDate")] Coding coding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coding);
        }

        // GET: Admin/Codings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coding = await _context.Codings.FindAsync(id);
            if (coding == null)
            {
                return NotFound();
            }
            return View(coding);
        }

        // POST: Admin/Codings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Percent,Id,CreatedDate")] Coding coding)
        {
            if (id != coding.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodingExists(coding.Id))
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
            return View(coding);
        }

        // GET: Admin/Codings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coding = await _context.Codings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coding == null)
            {
                return NotFound();
            }

            return View(coding);
        }

        // POST: Admin/Codings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coding = await _context.Codings.FindAsync(id);
            _context.Codings.Remove(coding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CodingExists(int id)
        {
            return _context.Codings.Any(e => e.Id == id);
        }
    }
}
