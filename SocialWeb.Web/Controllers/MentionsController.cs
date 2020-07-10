using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Web.Controllers
{
    public class MentionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MentionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mentions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mentions.Include(m => m.AppUser).Include(m => m.Tweet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mentions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mention = await _context.Mentions
                .Include(m => m.AppUser)
                .Include(m => m.Tweet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mention == null)
            {
                return NotFound();
            }

            return View(mention);
        }

        // GET: Mentions/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name");
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text");
            return View();
        }

        // POST: Mentions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,AppUserId,TweetId,CreateDate,ModifiedDate,DeletedDate")] Mention mention)
        {
            if (ModelState.IsValid)
            {
                mention.Status = Domain.Enums.Status.Active;
                _context.Add(mention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", mention.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", mention.TweetId);
            return View(mention);
        }

        // GET: Mentions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mention = await _context.Mentions.FindAsync(id);
            if (mention == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", mention.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", mention.TweetId);
            return View(mention);
        }

        // POST: Mentions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,AppUserId,TweetId,CreateDate,ModifiedDate,DeletedDate,Status")] Mention mention)
        {
            if (id != mention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MentionExists(mention.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", mention.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", mention.TweetId);
            return View(mention);
        }

        // GET: Mentions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mention = await _context.Mentions
                .Include(m => m.AppUser)
                .Include(m => m.Tweet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mention == null)
            {
                return NotFound();
            }

            return View(mention);
        }

        // POST: Mentions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mention = await _context.Mentions.FindAsync(id);
            _context.Mentions.Remove(mention);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MentionExists(int id)
        {
            return _context.Mentions.Any(e => e.Id == id);
        }
    }
}
