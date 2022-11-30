using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.DTOs
{
    public class AddParticipanteRifaDTO
    {
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        public int RifaId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        public int ParticipanteId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [Range(1, 54)]// el rango 
        public int NumerodeLaLoteria { get; set; }
    }
}
