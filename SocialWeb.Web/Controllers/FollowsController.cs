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
    public class FollowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FollowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Follows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Follows.Include(f => f.Follower).Include(f => f.Following);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Follows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
                .Include(f => f.Follower)
                .Include(f => f.Following)
                .FirstOrDefaultAsync(m => m.FollowerId == id);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // GET: Follows/Create
        public IActionResult Create()
        {
            ViewData["FollowerId"] = new SelectList(_context.AppUsers, "Id", "Name");
            ViewData["FollowingId"] = new SelectList(_context.AppUsers, "Id", "Name");
            return View();
        }

        // POST: Follows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FollowerId,FollowingId,CreateDate,ModifiedDate,DeletedDate")] Follow follow)
        {
            if (ModelState.IsValid)
            {
                follow.Status = Domain.Enums.Status.Active;
                _context.Add(follow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FollowerId"] = new SelectList(_context.AppUsers, "Id", "Name", follow.FollowerId);
            ViewData["FollowingId"] = new SelectList(_context.AppUsers, "Id", "Name", follow.FollowingId);
            return View(follow);
        }

        // GET: Follows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }
            ViewData["FollowerId"] = new SelectList(_context.AppUsers, "Id", "Name", follow.FollowerId);
            ViewData["FollowingId"] = new SelectList(_context.AppUsers, "Id", "Name", follow.FollowingId);
            return View(follow);
        }

        // POST: Follows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FollowerId,FollowingId,CreateDate,ModifiedDate,DeletedDate,Status")] Follow follow)
        {
            if (id != follow.FollowerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(follow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowExists(follow.FollowerId))
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
            ViewData["FollowerId"] = new SelectList(_context.AppUsers, "Id", "Name", follow.FollowerId);
            ViewData["FollowingId"] = new SelectList(_context.AppUsers, "Id", "Name", follow.FollowingId);
            return View(follow);
        }

        // GET: Follows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
                .Include(f => f.Follower)
                .Include(f => f.Following)
                .FirstOrDefaultAsync(m => m.FollowerId == id);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // POST: Follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var follow = await _context.Follows.FindAsync(id);
            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowExists(int id)
        {
            return _context.Follows.Any(e => e.FollowerId == id);
        }
    }
}
