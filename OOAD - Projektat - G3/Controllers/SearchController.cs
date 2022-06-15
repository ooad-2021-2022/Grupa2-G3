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
    public class SearchController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        public List<Artikal> listaPrikazanihArtikala {get; set;}


        public IActionResult Kupi(int? id = null, int? idKors = null)
        {

            var artikal = _context.Artikal.Find(id);
            ViewBag.idKorisnika = idKors;
            return View(artikal);
        }
        
        
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public  IActionResult Prikaz(int? id = null, int? idKors = null)
        {
            if (id == null)
            {
                return Search(idKors);
            }

            var artikal = _context.Artikal.Find(id);
            artikal.brojac++;

            _context.Artikal.Update(artikal);
            _context.SaveChanges();
            if (artikal == null)
            {
                return Search(idKors);
            }

            ViewBag.idKorisnika = idKors;
            return View(artikal);
        }
        // GET: Search
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Artikal.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Search/Details/5
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

        // GET: Search/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Search(int? idKorisnika = null)
        {


            ViewBag.korisnikID = idKorisnika;
            

            List<Artikal> lista = _context.Artikal.ToList();
            Predicate<Artikal> match = a => a.vlasnikKorisnik.Equals(idKorisnika);
            lista.RemoveAll(match);
            ViewBag.lista = lista; 
            return View(lista);
        }


        public IActionResult Pretraga(string? rijec = "", double donjaGR = 0, double gornjaGR = 0, int? kateg = null, int? id = null)
        {
            Kategorija? kategorija = null;
            if (kateg != null)
                kategorija = (Kategorija)kateg;

            List<Artikal> mojiArtikli = _context.Artikal.ToList();
            if (id != -1)
            {
                mojiArtikli.RemoveAll(a => !a.vlasnikKorisnik.Equals(id));
            }

            

            List<Artikal> listaArtikala;


            if (rijec != null && !rijec.Contains("Unesite tekst...") && rijec != "")
            {
                listaArtikala = _context.Artikal.ToList().FindAll((Artikal artikal) => artikal.naziv.ToLower().Contains(rijec.ToLower()));
            } 
            else
            {
                listaArtikala = _context.Artikal.ToList();
            }

            if (gornjaGR != 0)
            {
                listaArtikala.RemoveAll((Artikal artikal) => artikal.cijena <= donjaGR 
                                                          || artikal.cijena >= gornjaGR);
            }else if( gornjaGR == 0 && donjaGR != 0)
            {
                listaArtikala.RemoveAll((Artikal artikal) => artikal.cijena <= donjaGR);
            }

            if(id != -1)
            {
                listaArtikala.RemoveAll(artikalPomocna => mojiArtikli.Contains(artikalPomocna));
            }

            if (kategorija != null)
            {
                List<ArtikalKategorija> kategorijaIzBaze = _context.ArtikalKategorija.ToList();
                kategorijaIzBaze.RemoveAll((ArtikalKategorija k) => k.kategorija != kategorija);

                List<Artikal> pomocna= new List<Artikal>();


                foreach(Artikal art in listaArtikala)
                {
                    List<ArtikalKategorija> kategorijaPomocna = kategorijaIzBaze.FindAll(kat => kat.ArtikalID.Equals(art.id));

                    if (kategorijaPomocna.Count() > 0)
                    {
                        pomocna.Add(art);
                    }

                }

                ViewBag.korisnikID = id;
                return View(pomocna);
            }

            ViewBag.korisnikID = id;
            return View(listaArtikala);
        }

        // POST: Search/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,naziv,cijena,kolicina,opis,slika,brojac,vlasnikKorisnik")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artikal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(artikal);
        }

        // GET: Search/Edit/5
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

        // POST: Search/Edit/5
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
           
            return View(artikal);
        }

        // GET: Search/Delete/5
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

        // POST: Search/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artikal = await _context.Artikal.FindAsync(id);
            int korId = artikal.vlasnikKorisnik;
       
            _context.Artikal.Remove(artikal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Search", korId);
        }


        public IActionResult KupiIObrisi(int? id = null, int? idKorisnika = null)
        {
            var artikal =  _context.Artikal.Find(id);

            _context.Artikal.Remove(artikal);
            _context.SaveChanges();
            return RedirectToAction("Search","Search", new { idKorisnika = idKorisnika } );
        }

        private bool ArtikalExists(int id)
        {
            return _context.Artikal.Any(e => e.id == id);
        }
    }
}
