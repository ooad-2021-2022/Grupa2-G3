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
    public class RegistrovaniKorisnikController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int IDkorisnik { get; set; }
        
        public RegistrovaniKorisnikController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MainUser(int? kor = null)
        {
            if (kor == null)
            {
                int? data = TempData["indeksKorisnika"] as int?;

                RegistrovaniKorisnik korisnik = _context.RegistrovaniKorisnik.Find(data);
                return View(korisnik);
            }

            if(kor == -1)
            {
                return RedirectToAction("Start","Start");
            }

            RegistrovaniKorisnik koris = _context.RegistrovaniKorisnik.Find(kor);
            return View(koris);
        }

        public IActionResult ZatvoriRacun(int? kor)
        {
            if(kor == null)
            {
                return NotFound();
            }

            var artikli = _context.Artikal.ToList();
            var korisnik = _context.RegistrovaniKorisnik.Find(kor);
            artikli.RemoveAll(a => a.vlasnikKorisnik != kor);
            _context.Artikal.RemoveRange(artikli);
            _context.SaveChanges();
            _context.RegistrovaniKorisnik.Remove(korisnik);
            _context.SaveChanges();
            return RedirectToAction("Start","Start");
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegistriranjeRegistrovaniKorisnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ime,prezime,korisnickoIme,email,password,id")] RegistrovaniKorisnik registrovaniKorisnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrovaniKorisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Sucessfull), registrovaniKorisnik);
            }
            return View(registrovaniKorisnik);
        }

        public IActionResult Sucessfull(RegistrovaniKorisnik korisnik)
        {
            return View(korisnik);
        }

        // GET: RegistriranjeRegistrovaniKorisnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrovaniKorisnik = await _context.RegistrovaniKorisnik.FindAsync(id);
            if (registrovaniKorisnik == null)
            {
                return NotFound();
            }
            return View(registrovaniKorisnik);
        }

        // POST: RegistriranjeRegistrovaniKorisnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ime,prezime,korisnickoIme,email,password,id")] RegistrovaniKorisnik registrovaniKorisnik)
        {
            if (id != registrovaniKorisnik.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrovaniKorisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrovaniKorisnikExists(registrovaniKorisnik.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("~/Views/RegistrovaniKorisnik/MainUser.cshtml", registrovaniKorisnik);
            }
            return View(registrovaniKorisnik);
        }

        // GET: RegistriranjeRegistrovaniKorisnik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrovaniKorisnik = await _context.RegistrovaniKorisnik
                .FirstOrDefaultAsync(m => m.id == id);
            if (registrovaniKorisnik == null)
            {
                return NotFound();
            }

            return View(registrovaniKorisnik);
        }

        // POST: RegistriranjeRegistrovaniKorisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registrovaniKorisnik = await _context.RegistrovaniKorisnik.FindAsync(id);
            _context.RegistrovaniKorisnik.Remove(registrovaniKorisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrovaniKorisnikExists(int id)
        {
            return _context.RegistrovaniKorisnik.Any(e => e.id == id);
        }
    }
}
