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
    public class PricingDetailsController : Controller
    {
        private readonly RyanDbContext _context;

        public PricingDetailsController(RyanDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PricingDetails
        public async Task<IActionResult> Index()
        {
            var ryanDbContext = _context.PricingDetails.Include(p => p.Pricing);
            return View(await ryanDbContext.ToListAsync());
        }

        // GET: Admin/PricingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricingDetail = await _context.PricingDetails
                .Include(p => p.Pricing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pricingDetail == null)
            {
                return NotFound();
            }

            return View(pricingDetail);
        }

        // GET: Admin/PricingDetails/Create
        public IActionResult Create()
        {
            ViewData["PricingId"] = new SelectList(_context.Pricings, "Id", "Icon");
            return View();
        }

        // POST: Admin/PricingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Exists,New,PricingId,Id,CreatedDate")] PricingDetail pricingDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pricingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PricingId"] = new SelectList(_context.Pricings, "Id", "Icon", pricingDetail.PricingId);
            return View(pricingDetail);
        }

        // GET: Admin/PricingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricingDetail = await _context.PricingDetails.FindAsync(id);
            if (pricingDetail == null)
            {
                return NotFound();
            }
            ViewData["PricingId"] = new SelectList(_context.Pricings, "Id", "Icon", pricingDetail.PricingId);
            return View(pricingDetail);
        }

        // POST: Admin/PricingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Exists,New,PricingId,Id,CreatedDate")] PricingDetail pricingDetail)
        {
            if (id != pricingDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pricingDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PricingDetailExists(pricingDetail.Id))
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
            ViewData["PricingId"] = new SelectList(_context.Pricings, "Id", "Icon", pricingDetail.PricingId);
            return View(pricingDetail);
        }

        // GET: Admin/PricingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricingDetail = await _context.PricingDetails
                .Include(p => p.Pricing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pricingDetail == null)
            {
                return NotFound();
            }

            return View(pricingDetail);
        }

        // POST: Admin/PricingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pricingDetail = await _context.PricingDetails.FindAsync(id);
            _context.PricingDetails.Remove(pricingDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PricingDetailExists(int id)
        {
            return _context.PricingDetails.Any(e => e.Id == id);
        }
    }
}
