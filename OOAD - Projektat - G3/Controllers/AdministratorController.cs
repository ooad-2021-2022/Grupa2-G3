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
            List<Administrator> lista = _context.Administrator.ToList();

            if (lista == null)
            {
                return PrijavaAdministrator();
            }

            Administrator pomocna = lista.Find(k => k.naziv != null && k.password != null && k.naziv.Equals(naziv) && k.password.Equals(password));

            if (pomocna == null)
                return PrijavaAdministrator();
            else
            {
                return RedirectToAction(nameof(MainAdministrator));
      
            }

        }

        public IActionResult MainAdministrator()
        {
            return null;
        }
    }
}
