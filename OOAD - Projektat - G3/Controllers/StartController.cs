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

        public IActionResult Start()
        {
            return View();
        }

        public IActionResult IzborRacunaRegistracija()
        {
            return View();
        }
        /*protected void PrijavaKorisnika(object sender, EventArgs e)
        {
            Response.Redirect("");
        }*/
    }    
}
