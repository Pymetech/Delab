using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Delab.Shared.Entities
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required (ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength (100,ErrorMessage ="El campo {0} no puede ser mayor de {1} caracteres")]
        [Display (Name="Pais") ]
        public string Name { get; set; } = null!;
        //public string? Name { get; set; }           // El ? Significa que acepta nulos 
        //public string Name { get; set; }= null!;    // El ? Significa que acepta nulos o = null!;

        [MaxLength(10, ErrorMessage = "El campo {0} no puede ser mayor de {1} caracteres")]
        [Display(Name = "Cod Phone")]
        public string? CodPhone { get; set; }

        // Este codigo es para pedirme los datos de State al crear un Country
        // public ICollection<State> States { get; set; }  // sin el ? , me pedira obligatoriedad
        public ICollection<State>? States { get; set; }
    }
}
