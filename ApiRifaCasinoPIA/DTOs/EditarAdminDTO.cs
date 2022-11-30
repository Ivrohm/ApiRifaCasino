using System.ComponentModel.DataAnnotations;

namespace ApiRifaCasinoPIA.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]

        public string email { get; set; }
    }
}
