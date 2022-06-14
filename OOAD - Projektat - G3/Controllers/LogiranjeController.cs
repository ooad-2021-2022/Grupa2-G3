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
    public class LogiranjeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;

        public LogiranjeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Prijava()
        {
            return View();
        }
        public IActionResult PrijavaKorisnika()
        {
            return View();
        }
        public IActionResult PrijavaKompanije()
        {
            return View();
        }

        public IActionResult LoginCompanyUser(string email = "", string password = "")
        {
            List<KorisnikKompanija> lista = _context.KorisnikKompanija.ToList();

            if(lista == null)
            {
                return null;
            }

            KorisnikKompanija pomocna = lista.Find( k =>k.email!=null && k.password!=null && k.email.Equals(email) && k.password.Equals(password));

            if (pomocna == null)
                return null;
            else
            {    
                return View("~/Views/KorisnikKompanija/MainCompany.cshtml", pomocna); ;
            }

        }

        public IActionResult LoginRegisterUser(string korisnickoIme = "", string password = "")
        {
            List<RegistrovaniKorisnik> lista = _context.RegistrovaniKorisnik.ToList();

            if (lista == null)
            {
                return null;
            }

            RegistrovaniKorisnik pomocna = lista.Find(k => k.korisnickoIme != null && k.password != null && k.korisnickoIme.Equals(korisnickoIme) && k.password.Equals(password));

            if (pomocna == null)
                return null;
            else
            {
                return View("~/Views/RegistrovaniKorisnik/MainUser.cshtml", pomocna); ;
            }
        }
    } 
}
