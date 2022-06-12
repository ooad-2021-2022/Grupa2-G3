using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD___Projektat___G3.Models
{
    public class KorisnikKompanija: User
    {
       
       
        public string nazivKompanije { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string adresa { get; set; }
        public string brojTelefona { get; set; }
 
    }
}
