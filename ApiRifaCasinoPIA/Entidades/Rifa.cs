using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using ApiRifaCasinoPIA.Validaciones;

namespace ApiRifaCasinoPIA.Entidades
{
    public class Rifa
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} maximo 60 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio: {0}")]
        [PrimerLetraM]
        public string name { get; set; }
        public List<RifaParticipante> RifasParticipantes { get; set; }
        public List<PremioDeRifa> premioDeRifa { get; set; }   
        public List<Tarjeta> tarjeta { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser usuario { get; set; }
    }
}
