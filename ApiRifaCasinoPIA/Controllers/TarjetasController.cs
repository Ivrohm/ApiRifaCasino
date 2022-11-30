using ApiRifaCasinoPIA.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRifaCasinoPIA.Controllers
{
    [ApiController]
    [Route("api/tarjetas")]
    public class TarjetasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TarjetasController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet("getTarjetasId")]
        public async Task<ActionResult<List<Tarjeta>>> GetAll()
        {
            return await dbContext.tarjetas.ToListAsync();
        }

    }
}
