using ApiRifaCasinoPIA.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.DTOs
{
    public class ModificarPremioPatchDTO
    {  
       
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        public int RifaId { get; set; }
        
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [PrimerLetraM]
        public string name { get; set; }
       
 
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [PrimerLetraM]
        public string Descripcion { get; set; }
    }
}
