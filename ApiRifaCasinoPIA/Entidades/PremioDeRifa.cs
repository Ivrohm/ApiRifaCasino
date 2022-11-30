using ApiRifaCasinoPIA.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.Entidades
{
    public class PremioDeRifa
    {
        public int Id { get; set; }

       
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")] 
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [PrimerLetraM]
        public string name { get; set; }

        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [PrimerLetraM]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        public int RifaId { get; set; }

        public Rifa Rifa { get; set; }


    }
}
