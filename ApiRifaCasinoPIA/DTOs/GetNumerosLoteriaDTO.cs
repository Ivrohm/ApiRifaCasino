using ApiRifaCasinoPIA.Entidades;
using ApiRifaCasinoPIA.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.DTOs
{
    public class GetNumerosLoteriaDTO
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [PrimerLetraM]
        public string name { get; set; }
        public List<Tarjeta> Tarjetas { get; set; }
    }
}
