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
    public class SharesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SharesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shares
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Shares.Include(s => s.AppUser).Include(s => s.Tweet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Shares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var share = await _context.Shares
                .Include(s => s.AppUser)
                .Include(s => s.Tweet)
                .FirstOrDefaultAsync(m => m.TweetId == id);
            if (share == null)
            {
                return NotFound();
            }

            return View(share);
        }

        // GET: Shares/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name");
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text");
            return View();
        }

        // POST: Shares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TweetId,AppUserId,CreateDate,ModifiedDate,DeletedDate")] Share share)
        {
            if (ModelState.IsValid)
            {
                share.Status = Domain.Enums.Status.Active;
                _context.Add(share);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", share.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", share.TweetId);
            return View(share);
        }

        // GET: Shares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var share = await _context.Shares.FindAsync(id);
            if (share == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", share.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", share.TweetId);
            return View(share);
        }

        // POST: Shares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TweetId,AppUserId,CreateDate,ModifiedDate,DeletedDate,Status")] Share share)
        {
            if (id != share.TweetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(share);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShareExists(share.TweetId))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Name", share.AppUserId);
            ViewData["TweetId"] = new SelectList(_context.Tweets, "Id", "Text", share.TweetId);
            return View(share);
        }

        // GET: Shares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var share = await _context.Shares
                .Include(s => s.AppUser)
                .Include(s => s.Tweet)
                .FirstOrDefaultAsync(m => m.TweetId == id);
            if (share == null)
            {
                return NotFound();
            }

            return View(share);
        }

        // POST: Shares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var share = await _context.Shares.FindAsync(id);
            _context.Shares.Remove(share);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShareExists(int id)
        {
            return _context.Shares.Any(e => e.TweetId == id);
        }
    }
}
