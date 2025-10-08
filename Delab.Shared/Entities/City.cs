using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delab.Shared.Entities
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public int StateId { get; set; }
       
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor de {1} caracteres")]
        [Display(Name = "Depart/Estado")]
        public string Name { get; set; } = null!;

        //Relaciones
        public State? State { get; set; }

    }
}
