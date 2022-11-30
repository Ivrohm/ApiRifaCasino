namespace ApiRifaCasinoPIA.DTOs
{
    public class RespuestaAutenticacionDTO
    {
        public string Token { get; set; } //se devuelve al cliente 
        public DateTime Expiracion { get; set; }//expira esta morido
    }
}
