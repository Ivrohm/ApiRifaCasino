using ApiRifaCasinoPIA.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRifaCasinoPIA.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiRifaCasinoPIA.Controllers
{
    [ApiController]
    [Route("api/premios")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "esAdmin")]
    public class PremiosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public PremiosController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }


        [HttpGet("ObtenerTodosLosPremios")]
        public async Task<ActionResult<List<GetPremiosDTO>>> GetAll()
        {
            var premios = await dbContext.premios.ToListAsync();

            if (premios.Count == 0)
            {
                return NotFound("No hay ningún premio en el sistema");
            }

            return mapper.Map<List<GetPremiosDTO>>(premios);
        }

        [HttpPost("AgregarNuevoPremio")]
        public async Task<ActionResult> Post(AddPremioDTO agregarPremioDTO)
        {
            var idRifa = agregarPremioDTO.IdRifa;

            if (agregarPremioDTO == null) { return BadRequest("No hay ningún premio que agregar"); }

            var rifaBd = await dbContext.rifas.Include(rifa => rifa.premioDeRifa)
                .FirstOrDefaultAsync(x => x.Id == idRifa);

            if (rifaBd == null) { return NotFound("No existe la rifa"); }

            var premio = mapper.Map<PremioDeRifa>(agregarPremioDTO);

            premio.Rifa = rifaBd;

            dbContext.Add(premio);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ModificarPremioPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }

            var premioEnBD = await dbContext.premios.FirstOrDefaultAsync(x => x.Id == id);

            if (premioEnBD == null) { return NotFound("No se encontró el id"); }

            var premioDTO = mapper.Map<ModificarPremioPatchDTO>(premioEnBD);

            patchDocument.ApplyTo(premioDTO, ModelState);

            var esValido = TryValidateModel(premioDTO);

            if (!esValido) { return BadRequest(ModelState); }

            mapper.Map(premioDTO, premioEnBD);

            await dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpGet("{id:int}/Ganador")]
        public async Task<ActionResult<Object>> GetWinner(int id)
        {
            var rifaDB = await dbContext.rifas.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (rifaDB == null) 
                return BadRequest();
            var participantesDeLaRifa = await dbContext.rifaParticipantes.Where(x => x.RifaId == id).ToListAsync();
            if (participantesDeLaRifa.Count == 0)
                return BadRequest();
            var premiosDB = await dbContext.premios.Where(x => x.RifaId == id).ToListAsync();
            if (premiosDB.Count == 0)
                return BadRequest();

            Random random = new Random();
            var ganadorRandom = participantesDeLaRifa.OrderBy(x => random.Next()).Take(1).FirstOrDefault();

            var premioGanador = premiosDB.Last();

            dbContext.premios.Remove(premioGanador);

            await dbContext.SaveChangesAsync();

            var participante = await dbContext.participantes.Where(x => x.Id == ganadorRandom.ParticipanteId).FirstOrDefaultAsync();
            var tarjetaLoteriaGanadora = await dbContext.tarjetas.Where(x => x.Id == ganadorRandom.NumerodeLaLoteria).FirstOrDefaultAsync();

            var Result = new
            {
                NombreParticipante = participante.name,
                NumLoteria = tarjetaLoteriaGanadora.Id,
                NombreTarjeta = tarjetaLoteriaGanadora.name,
                premioGanado = premioGanador.name
            };
        
            return Result;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var existe = await dbContext.premios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("No se encontró el premio que buscaba");
            }

            dbContext.Remove(new PremioDeRifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
