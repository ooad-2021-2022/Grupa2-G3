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
    public class KorisnikKompanijaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int IDkorisnik { get; set; }
        public KorisnikKompanijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ZatvoriRacun(int? kor)
        {
            if (kor == null)
            {
                return NotFound();
            }

            var artikli = _context.Artikal.ToList();
            var korisnik = _context.KorisnikKompanija.Find(kor);
            artikli.RemoveAll(a => a.vlasnikKorisnik != kor);
            _context.Artikal.RemoveRange(artikli);
            _context.SaveChanges();
            _context.KorisnikKompanija.Remove(korisnik);
            _context.SaveChanges();
            return RedirectToAction("Start", "Start");
        }

        public IActionResult MainCompany(int? kor = null)
        {
            if(kor == null)
            {
                int? data = TempData["indeksKompanije"] as int?;

                KorisnikKompanija korisnik = _context.KorisnikKompanija.Find(data);
                return View(korisnik);
            }

            KorisnikKompanija koris = _context.KorisnikKompanija.Find(kor);
            return View(koris);
        }

        // GET: RegistriranjeKompanija/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Sucessfull(KorisnikKompanija korisnik)
        {
            return View(korisnik);
        }
        // POST: RegistriranjeKompanija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nazivKompanije,email,password,adresa,brojTelefona,id")] KorisnikKompanija korisnikKompanija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korisnikKompanija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Sucessfull), korisnikKompanija);
            }
            return View(korisnikKompanija);
        }

        // GET: RegistriranjeKompanija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnikKompanija = await _context.KorisnikKompanija.FindAsync(id);
            if (korisnikKompanija == null)
            {
                return NotFound();
            }
            return View(korisnikKompanija);
        }

        // POST: RegistriranjeKompanija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nazivKompanije,email,password,adresa,brojTelefona,id")] KorisnikKompanija korisnikKompanija)
        {
            if (id != korisnikKompanija.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnikKompanija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikKompanijaExists(korisnikKompanija.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("~/Views/KorisnikKompanija/MainCompany.cshtml", korisnikKompanija);
            }
            return View(korisnikKompanija);
        }

        // GET: RegistriranjeKompanija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnikKompanija = await _context.KorisnikKompanija
                .FirstOrDefaultAsync(m => m.id == id);
            if (korisnikKompanija == null)
            {
                return NotFound();
            }

            return View(korisnikKompanija);
        }

        // POST: RegistriranjeKompanija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korisnikKompanija = await _context.KorisnikKompanija.FindAsync(id);
            _context.KorisnikKompanija.Remove(korisnikKompanija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikKompanijaExists(int id)
        {
            return _context.KorisnikKompanija.Any(e => e.id == id);
        }
    }
}
