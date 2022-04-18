using ManejoPresupuesto.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Models
{
    public class Cuenta
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [StringLength(maximumLength:50,MinimumLength =3,ErrorMessage ="El campo debe ser minimo de 3 y maximo 50 caracteres")]
        [PrimeraLetraMayuscula]
        public string  Nombre { get; set; }

        [Display(Name ="Tipo Cuenta")]
        public int TipoCuentaId { get; set; }
        public decimal Balance { get; set; }

        [StringLength(maximumLength:1000)]
        public string Descripcion { get; set; }

        public string TipoCuenta { get; set; }
    }
}
