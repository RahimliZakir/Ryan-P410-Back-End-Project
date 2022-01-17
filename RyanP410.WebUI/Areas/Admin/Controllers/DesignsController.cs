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
    public class DesignsController : Controller
    {
        private readonly RyanDbContext _context;

        public DesignsController(RyanDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Designs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Designs.ToListAsync());
        }

        // GET: Admin/Designs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Designs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (design == null)
            {
                return NotFound();
            }

            return View(design);
        }

        // GET: Admin/Designs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Designs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Percent,Id,CreatedDate")] Design design)
        {
            if (ModelState.IsValid)
            {
                _context.Add(design);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(design);
        }

        // GET: Admin/Designs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Designs.FindAsync(id);
            if (design == null)
            {
                return NotFound();
            }
            return View(design);
        }

        // POST: Admin/Designs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Percent,Id,CreatedDate")] Design design)
        {
            if (id != design.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(design);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignExists(design.Id))
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
            return View(design);
        }

        // GET: Admin/Designs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Designs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (design == null)
            {
                return NotFound();
            }

            return View(design);
        }

        // POST: Admin/Designs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var design = await _context.Designs.FindAsync(id);
            _context.Designs.Remove(design);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignExists(int id)
        {
            return _context.Designs.Any(e => e.Id == id);
        }
    }
}
