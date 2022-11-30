using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.DTOs
{
    public class CredencialesUsuarios
    {
        [Required]
        [EmailAddress]

        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
