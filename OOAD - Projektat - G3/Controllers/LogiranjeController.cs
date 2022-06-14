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

        public IActionResult LoginCompanyUser(string email = "", string lozinka = "")
        {
            List<KorisnikKompanija> lista = _context.KorisnikKompanija.ToList();

            if(lista == null)
            {
                return null;
            }

            KorisnikKompanija pomocna = lista.Find( k => k.email.Equals(email) && k.password.Equals(lozinka));

            if (pomocna == null)
                return null;
            else
            {    
                return View("~/Views/KorisnikKompanija/MainCompany.cshtml", pomocna); ;
            }
        }
    } 
}
