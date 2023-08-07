using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ACTIVIDAD_FORMULARIO.Models
{
    public class Alumno
    {
        public Alumno()
        {
            FechaNacimiento = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo es requerido")]
        [RegularExpression("^1490-\\d\\d-\\d{2,6}$", ErrorMessage ="El carnet no cumple con el formato requerido")]
        public string Carnet { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^\\w{2,}( \\w{2,})?$", ErrorMessage = "Solo puede poner 2 nombres como maximo y cada nombre debe de tener mas de 1 letra")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$"
            , ErrorMessage = "El Correo No cumple con el formato requerido")]
        public string Correo { get; set; }
    }
}
