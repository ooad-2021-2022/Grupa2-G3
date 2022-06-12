using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD___Projektat___G3.Models
{
    public class RegistrovaniKorisnik : User
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public string korisnickoIme { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
