using AutoMapper;
using ApiRifaCasinoPIA.DTOs;
using ApiRifaCasinoPIA.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRifaCasinoPIA.Controllers
{
    [ApiController]
    [Route("api/participantes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ParticipantesController> logger;
        private readonly IMapper mapper;

        public ParticipantesController(ApplicationDbContext context, ILogger<ParticipantesController> logger,
            IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<GetParticipantesDTO>>> GetAll()
        {
            var participantes = await dbContext.participantes.ToListAsync();

            if (participantes.Count == 0)
            {
                return NotFound("No hay ningún participante en el sistema");
            }

            return mapper.Map<List<GetParticipantesDTO>>(participantes);
        }

        [HttpPost("RegistrarParticipante")]
        public async Task<ActionResult> Post(RegisterParticipanteDTO registerParticipanteDTO)
        {
            var existe = await dbContext.participantes.AnyAsync(x => x.name == registerParticipanteDTO.name);

            if (existe)
            {
                return BadRequest($"Ya esta registrado" + $"{registerParticipanteDTO.name}");
            }

            var participante = mapper.Map<ParticipanteDeRifa>(registerParticipanteDTO);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Nuevo participante registrado");
            return Ok();
        }


        [HttpPost("AddParticipantes")]
        public async Task<ActionResult> Post(AddParticipanteRifaDTO addParticipanteRifaDTO)
        {
            if (addParticipanteRifaDTO == null) { return NotFound("<--No hay nada que agregar-->"); }

            if (addParticipanteRifaDTO.NumerodeLaLoteria < 0 || addParticipanteRifaDTO.NumerodeLaLoteria > 54)
            {
                return BadRequest("<--Numero de tarjeta inválido-->");
            }

            var tarjetaExistente = await dbContext.rifaParticipantes.AnyAsync(x => x.NumerodeLaLoteria == addParticipanteRifaDTO.NumerodeLaLoteria && x.RifaId == addParticipanteRifaDTO.RifaId);

            if (tarjetaExistente)
            {
                return BadRequest($"<--Un participante ya esta utilizando el numero de tarjeta: " + $"{addParticipanteRifaDTO.NumerodeLaLoteria}->");
            }

            var participante = mapper.Map<RifaParticipante>(addParticipanteRifaDTO);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("<--Participante Nuevo-->");
            return Ok();
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var existe = await dbContext.participantes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            dbContext.Remove(new ParticipanteDeRifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            logger.LogInformation("<--Se elimino participante-->");
            return Ok();
        }



    }
}
