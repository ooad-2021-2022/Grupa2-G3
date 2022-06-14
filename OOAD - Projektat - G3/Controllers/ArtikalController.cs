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
    public class ArtikalController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        public int korisnikVlasnik = -1;
        public ArtikalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artikal
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Artikal.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ArtikliKorisnika(User user)
        {
            var applicationDbContext = _context.Artikal.Include(a => a.User.Equals(user));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Artikal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikal = await _context.Artikal
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (artikal == null)
            {
                return NotFound();
            }

            return View(artikal);
        }

        // GET: Artikal/Create
        public IActionResult Create(int korisnikID)
        {
            korisnikVlasnik = korisnikID;
            ViewBag.korisnikID = korisnikID;
            ViewData["vlasnikKorisnik"] = new SelectList(_context.User, "id", "id");
            return View();
        }

        // POST: Artikal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int vlasnikKorisnik, [Bind("id,naziv,cijena,kolicina,opis,slika,brojac,vlasnikKorisnik")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                artikal.brojac = 0;
                artikal.vlasnikKorisnik = vlasnikKorisnik;
                _context.Add(artikal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["vlasnikKorisnik"] = new SelectList(_context.User, "id", "id", artikal.vlasnikKorisnik);
            return View(artikal);
        }

        // GET: Artikal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikal = await _context.Artikal.FindAsync(id);
            if (artikal == null)
            {
                return NotFound();
            }
            ViewData["vlasnikKorisnik"] = new SelectList(_context.User, "id", "id", artikal.vlasnikKorisnik);
            return View(artikal);
        }

        // POST: Artikal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,naziv,cijena,kolicina,opis,slika,brojac,vlasnikKorisnik")] Artikal artikal)
        {
            if (id != artikal.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artikal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtikalExists(artikal.id))
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
            ViewData["vlasnikKorisnik"] = new SelectList(_context.User, "id", "id", artikal.vlasnikKorisnik);
            return View(artikal);
        }

        // GET: Artikal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikal = await _context.Artikal
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (artikal == null)
            {
                return NotFound();
            }

            return View(artikal);
        }

        // POST: Artikal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artikal = await _context.Artikal.FindAsync(id);
            _context.Artikal.Remove(artikal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtikalExists(int id)
        {
            return _context.Artikal.Any(e => e.id == id);
        }
    }
}
