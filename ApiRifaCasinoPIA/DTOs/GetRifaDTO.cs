using ApiRifaCasinoPIA.Entidades;
using ApiRifaCasinoPIA.Controllers;
using System.ComponentModel.DataAnnotations;
using ApiRifaCasinoPIA.Validaciones;

namespace ApiRifaCasinoPIA.DTOs
{
    public class GetRifaDTO
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]

        [PrimerLetraM]
        public string name { get; set; }
        public List<GetPremiosDTO> premioDeRifa { get; set; }
    }
}
