using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD___Projektat___G3.Models
{
    public class ArtikalKategorija
    {
        [Key]
        public int id { get; set; }
        public Kategorija kategorija{ get; set; }

        [ForeignKey("Artikal")]
        public int ArtikalID { get; set; }

        public Artikal Artikal { get; set; }

        public ArtikalKategorija() { }
    }
}
