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
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Likes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Likes.Include(l => l.AppUser).Include(l => l.Tweet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Likes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.AppUser)
                .Include(l => l.Tweet)
                .FirstOrDefaultAsync(m => m.TweetId == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // GET: Likes/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name");
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text");
            return View();
        }

        // POST: Likes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TweetId,AppUserId,CreateDate,ModifiedDate,DeletedDate")] Like like)
        {
            if (ModelState.IsValid)
            {
                like.Status = Domain.Enums.Status.Active;
                _context.Add(like);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", like.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", like.TweetId);
            return View(like);
        }

        // GET: Likes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", like.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", like.TweetId);
            return View(like);
        }

        // POST: Likes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TweetId,AppUserId,CreateDate,ModifiedDate,DeletedDate,Status")] Like like)
        {
            if (id != like.TweetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(like);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikeExists(like.TweetId))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", like.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", like.TweetId);
            return View(like);
        }

        // GET: Likes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.AppUser)
                .Include(l => l.Tweet)
                .FirstOrDefaultAsync(m => m.TweetId == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.TweetId == id);
        }
    }
}
