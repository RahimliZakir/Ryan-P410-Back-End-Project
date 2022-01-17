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
    public class KnowledgesController : Controller
    {
        private readonly RyanDbContext _context;

        public KnowledgesController(RyanDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Knowledges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Knowledges.ToListAsync());
        }

        // GET: Admin/Knowledges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledge = await _context.Knowledges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledge == null)
            {
                return NotFound();
            }

            return View(knowledge);
        }

        // GET: Admin/Knowledges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Knowledges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedDate")] Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowledge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knowledge);
        }

        // GET: Admin/Knowledges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledge = await _context.Knowledges.FindAsync(id);
            if (knowledge == null)
            {
                return NotFound();
            }
            return View(knowledge);
        }

        // POST: Admin/Knowledges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,CreatedDate")] Knowledge knowledge)
        {
            if (id != knowledge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowledge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowledgeExists(knowledge.Id))
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
            return View(knowledge);
        }

        // GET: Admin/Knowledges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledge = await _context.Knowledges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledge == null)
            {
                return NotFound();
            }

            return View(knowledge);
        }

        // POST: Admin/Knowledges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knowledge = await _context.Knowledges.FindAsync(id);
            _context.Knowledges.Remove(knowledge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnowledgeExists(int id)
        {
            return _context.Knowledges.Any(e => e.Id == id);
        }
    }
}
