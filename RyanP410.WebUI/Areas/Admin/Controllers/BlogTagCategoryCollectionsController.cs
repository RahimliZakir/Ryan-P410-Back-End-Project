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
    public class BlogTagCategoryCollectionsController : Controller
    {
        private readonly RyanDbContext _context;

        public BlogTagCategoryCollectionsController(RyanDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BlogTagCategoryCollections
        public async Task<IActionResult> Index()
        {
            var ryanDbContext = _context.BlogTagCategoryCollections.Include(b => b.Blog).Include(b => b.BlogCategory).Include(b => b.CreatedByUser).Include(b => b.Tag);
            return View(await ryanDbContext.ToListAsync());
        }

        // GET: Admin/BlogTagCategoryCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTagCategoryCollection = await _context.BlogTagCategoryCollections
                .Include(b => b.Blog)
                .Include(b => b.BlogCategory)
                .Include(b => b.CreatedByUser)
                .Include(b => b.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogTagCategoryCollection == null)
            {
                return NotFound();
            }

            return View(blogTagCategoryCollection);
        }

        // GET: Admin/BlogTagCategoryCollections/Create
        public IActionResult Create()
        {
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Description");
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name");
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // POST: Admin/BlogTagCategoryCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,TagId,BlogCategoryId,CreatedByUserId,Id,CreatedDate")] BlogTagCategoryCollection blogTagCategoryCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogTagCategoryCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Description", blogTagCategoryCollection.BlogId);
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blogTagCategoryCollection.BlogCategoryId);
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", blogTagCategoryCollection.CreatedByUserId);
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", blogTagCategoryCollection.TagId);
            return View(blogTagCategoryCollection);
        }

        // GET: Admin/BlogTagCategoryCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTagCategoryCollection = await _context.BlogTagCategoryCollections.FindAsync(id);
            if (blogTagCategoryCollection == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Description", blogTagCategoryCollection.BlogId);
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blogTagCategoryCollection.BlogCategoryId);
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", blogTagCategoryCollection.CreatedByUserId);
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", blogTagCategoryCollection.TagId);
            return View(blogTagCategoryCollection);
        }

        // POST: Admin/BlogTagCategoryCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,TagId,BlogCategoryId,CreatedByUserId,Id,CreatedDate")] BlogTagCategoryCollection blogTagCategoryCollection)
        {
            if (id != blogTagCategoryCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogTagCategoryCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogTagCategoryCollectionExists(blogTagCategoryCollection.Id))
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
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Description", blogTagCategoryCollection.BlogId);
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blogTagCategoryCollection.BlogCategoryId);
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", blogTagCategoryCollection.CreatedByUserId);
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", blogTagCategoryCollection.TagId);
            return View(blogTagCategoryCollection);
        }

        // GET: Admin/BlogTagCategoryCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTagCategoryCollection = await _context.BlogTagCategoryCollections
                .Include(b => b.Blog)
                .Include(b => b.BlogCategory)
                .Include(b => b.CreatedByUser)
                .Include(b => b.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogTagCategoryCollection == null)
            {
                return NotFound();
            }

            return View(blogTagCategoryCollection);
        }

        // POST: Admin/BlogTagCategoryCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogTagCategoryCollection = await _context.BlogTagCategoryCollections.FindAsync(id);
            _context.BlogTagCategoryCollections.Remove(blogTagCategoryCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogTagCategoryCollectionExists(int id)
        {
            return _context.BlogTagCategoryCollections.Any(e => e.Id == id);
        }
    }
}
