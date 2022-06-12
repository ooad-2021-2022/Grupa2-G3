using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD___Projektat___G3.Models
{
    public class Artikal
    {
        [Key]
        public int id { get; set; }
        public string naziv { get; set; }
        public double cijena { get; set; }
        public double kolicina { get; set; }
        public string opis { get; set; }
        public string slika { get; set; }
        public int brojac { get; set; }
        [ForeignKey("User")]
        public int vlasnikKorisnik { get; set; }

        public User User { get; set; }

        public Artikal() { }
    }
}
