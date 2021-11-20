using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace APBD4.Model
{
    public class Animal
    {
       
        public int IdAnimal { get; set; }
        [Required(ErrorMessage = "Imie jest wymagane")]
        [MaxLength(200,ErrorMessage = "Długość może być max 200")]
        public string Name { get; set; }
        [MaxLength(200,ErrorMessage = "Długość może być max 200")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Kategoria jest wymagana")]
        [MaxLength(200,ErrorMessage = "Długość może być max 200")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Area jest wymagana")]
        [MaxLength(200,ErrorMessage = "Długość może być max 200")]
        public string Area { get; set; }
    }
}