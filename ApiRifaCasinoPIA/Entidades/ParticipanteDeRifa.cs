using ApiRifaCasinoPIA.Validaciones;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.Entidades
{
    public class ParticipanteDeRifa
    {
        public int Id { get; set; }

        [StringLength(maximumLength:60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]       
        [PrimerLetraM]
        public string name { get; set; }

        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [Range(18, 99)]
        
        public int edad { get; set; }
        public List<RifaParticipante> RifasParticipantes { get; set; }
        

    }
}
