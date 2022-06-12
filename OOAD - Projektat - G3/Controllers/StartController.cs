using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOAD___Projektat___G3.Data;
using OOAD___Projektat___G3.Models;

namespace OOAD___Projektat___G3.Controllers
{
    public class StartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Start
        public async Task<IActionResult> Index()
        {
            return View(await _context.NeregistrovaniKorisnik.ToListAsync());
        }

        // GET: Start/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neregistrovaniKorisnik = await _context.NeregistrovaniKorisnik
                .FirstOrDefaultAsync(m => m.id == id);
            if (neregistrovaniKorisnik == null)
            {
                return NotFound();
            }

            return View(neregistrovaniKorisnik);
        }

        // GET: Start/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Start/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id")] NeregistrovaniKorisnik neregistrovaniKorisnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(neregistrovaniKorisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(neregistrovaniKorisnik);
        }

        // GET: Start/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neregistrovaniKorisnik = await _context.NeregistrovaniKorisnik.FindAsync(id);
            if (neregistrovaniKorisnik == null)
            {
                return NotFound();
            }
            return View(neregistrovaniKorisnik);
        }

        // POST: Start/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id")] NeregistrovaniKorisnik neregistrovaniKorisnik)
        {
            if (id != neregistrovaniKorisnik.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(neregistrovaniKorisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NeregistrovaniKorisnikExists(neregistrovaniKorisnik.id))
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
            return View(neregistrovaniKorisnik);
        }

        // GET: Start/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neregistrovaniKorisnik = await _context.NeregistrovaniKorisnik
                .FirstOrDefaultAsync(m => m.id == id);
            if (neregistrovaniKorisnik == null)
            {
                return NotFound();
            }

            return View(neregistrovaniKorisnik);
        }

        // POST: Start/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var neregistrovaniKorisnik = await _context.NeregistrovaniKorisnik.FindAsync(id);
            _context.NeregistrovaniKorisnik.Remove(neregistrovaniKorisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NeregistrovaniKorisnikExists(int id)
        {
            return _context.NeregistrovaniKorisnik.Any(e => e.id == id);
        }
    }
}
