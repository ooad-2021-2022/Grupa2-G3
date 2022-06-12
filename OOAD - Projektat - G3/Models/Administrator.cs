using System.ComponentModel.DataAnnotations;

namespace OOAD___Projektat___G3.Models
{
    public class Administrator
    {
        [Key]
        public int id { get; set; }
        public string naziv { get; set; }
        public string password { get; set; }

        public Administrator() { }
    }
}
