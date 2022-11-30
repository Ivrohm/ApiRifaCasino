using ApiRifaCasinoPIA.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.DTOs
{
    public class RifaParticipanteDTO
    {
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        public int RifaId { get; set; }
        public int NumerodeLaLoteria { get; set; }
    }
}
