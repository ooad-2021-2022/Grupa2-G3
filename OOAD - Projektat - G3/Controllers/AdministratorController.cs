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
    public class AdministratorController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministratorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult PrijavaAdministrator()
        {
            return View("Login");
        }

        public IActionResult LoginAdministrator(string naziv = "", string password = "")
        {
            string privremeniNaziv = naziv;
            string privremeniPassword = password;
            List<Administrator> lista = _context.Administrator.ToList();

            if (lista == null)
            {
                return PrijavaAdministrator();
            }

            Administrator pomocna = lista.Find(k => k.naziv != null && k.password != null && k.naziv.Equals(privremeniNaziv) && k.password.Equals(privremeniPassword));

            if (pomocna == null)
                return PrijavaAdministrator();
            else
            {
                return RedirectToAction(nameof(MainAdministrator));
      
            }

        }

        public IActionResult MainAdministrator()
        {
            return View();
        }

        public IActionResult RegistrovaniPregledRacuna()
        {
            return View(_context.RegistrovaniKorisnik.ToList());
        }
        public IActionResult KompanijaPregledRacuna()
        {
            return View(_context.KorisnikKompanija.ToList());
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = _context.RegistrovaniKorisnik.ToList();
            var artikli = _context.Artikal.ToList();

            var korisnik = lista.Find(a => a.id.Equals(id));
            

            if(korisnik == null)
            {
                
                var lista1 = _context.KorisnikKompanija.ToList();
                var korisnik1 = lista1.Find(a => a.id.Equals(id));
                

                if (korisnik1 == null)
                {
                    return NotFound();
                }


                artikli.RemoveAll(a => a.vlasnikKorisnik != korisnik1.id);
                _context.Artikal.RemoveRange(artikli);
                _context.SaveChanges();
                _context.KorisnikKompanija.Remove(korisnik1);
                _context.SaveChanges();
                return RedirectToAction(nameof(KompanijaPregledRacuna), _context.KorisnikKompanija.ToList());    
            }

            artikli.RemoveAll(a => a.vlasnikKorisnik != korisnik.id);
            _context.Artikal.RemoveRange(artikli);
            _context.SaveChanges();
            _context.RegistrovaniKorisnik.Remove(korisnik);
            _context.SaveChanges();
            return RedirectToAction(nameof(RegistrovaniPregledRacuna), _context.RegistrovaniKorisnik.ToList());

            
        }

        // POST: Racun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}
