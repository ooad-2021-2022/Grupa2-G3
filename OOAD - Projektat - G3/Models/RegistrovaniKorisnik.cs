using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD___Projektat___G3.Models
{
    public class RegistrovaniKorisnik : User
    {
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage =
        "Ime mora imati između 3 i 40 karaktera!")]
        public string ime { get; set; }
        
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage =
        "Prezime mora imati između 3 i 40 karaktera!")]
        public string prezime { get; set; }

        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage =
        "Korisničko ime mora imati između 3 i 40 karaktera!")]
     
        public string korisnickoIme { get; set; }
        

        public string email { get; set; }
        public string password { get; set; }
    }
}
