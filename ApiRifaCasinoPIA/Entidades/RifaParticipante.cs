using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.Entidades
{
    public class RifaParticipante : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        public int RifaId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        public int ParticipanteId { get; set; }
        public ParticipanteDeRifa Participante { get; set; }
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        public int NumerodeLaLoteria { get; set; }
        public Rifa Rifa { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NumerodeLaLoteria <= 1 || NumerodeLaLoteria >= 54)
            {
                yield return new ValidationResult("No esta en el rango", new string[]
                {
                    nameof(NumerodeLaLoteria)
                });
            }
            throw new NotImplementedException();
        }
    }
}
