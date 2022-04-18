using ManejoPresupuesto.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta//:IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Campo Nombre Es Obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La Longitud del Campo Nombre debe estar entre {2} y {1}")]
        [Display(Name ="Nombre Tipo Cuenta")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteTipoCuenta",controller:"TiposCuentas")]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Nombre != null && Nombre.Length > 0)
        //    {
        //        var primeraLetra = Nombre[0].ToString();
        //        if (primeraLetra != primeraLetra.ToUpper())
        //        {
        //            yield return 
        //            new ValidationResult("La Primera Letra Debe Ser En Mayuscula", new[] { nameof(Nombre) });
        //        }
        //    }
        //}

        ////Pruebas de Otras Validaciones por Defecto
        //[Required(ErrorMessage ="El Campo {0} es Requerido")]
        //[EmailAddress(ErrorMessage ="El Email Debe ser Valido")]
        //public string Email { get; set; }

        //[Required(ErrorMessage ="Debe Ingresar Una Edad")]
        //[Range(minimum:18,maximum:100,ErrorMessage ="La Edad debe estar entre los {1} y {2} años")]
        //public int Edad { get; set; }
        //[Url(ErrorMessage = "Debe Ingresar una URL Valida")]
        //public string URL { get; set; }
        //[Required(ErrorMessage ="La Tarjeta de Credito es Obligatoria")]
        //[CreditCard(ErrorMessage ="La Tarjeta de Credito no es Valida")]
        //[Display(Name ="Tarjeta De Credito")]
        //public string TarjetaCredito { get; set; }



    }
}
