using ApiRifaCasinoPIA.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.Entidades
{
    public class Tarjeta
    {

        public int Id { get; set; }
       
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [PrimerLetraM]
        public string name { get; set; }

    }
}
