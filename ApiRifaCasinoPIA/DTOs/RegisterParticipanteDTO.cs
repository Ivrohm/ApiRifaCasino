using ApiRifaCasinoPIA.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.DTOs
{
    public class RegisterParticipanteDTO //registro de participantes
    {
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [PrimerLetraM]
        public string name { get; set; }

        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [Range(18, 99)]
        public int edad { get; set; }
    }
}
