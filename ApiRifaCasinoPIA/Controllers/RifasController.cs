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
    [Route("api/rifas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "esAdmin")]

    public class RifasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public RifasController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }



        [HttpGet("ObtenerRifas")]
        public async Task<ActionResult<List<GetRifaDTO>>> Get()
        {

            var rifas = await dbContext.rifas.Include(rifasDb => rifasDb.premioDeRifa).ToListAsync();
            if (rifas == null)
            {
                return NotFound("No hay ninguna rifa en el sistema");
            }

            return mapper.Map<List<GetRifaDTO>>(rifas);
        }

        [HttpGet("ObtenerTarjetasDisponiblesPorIdDeRifa")]
        public async Task<ActionResult<List<int>>> GetById(int id)
        {

            var registros = await dbContext.rifaParticipantes.Where(
                registro => registro.RifaId == id).ToListAsync();
            if (registros.Count == 0) { return NotFound("La rifa no tiene ningún participante registrado"); }

            List<int> NumNoDisp = new List<int>();
            foreach (var registro in registros)
            {
                NumNoDisp.Add(registro.NumerodeLaLoteria);
            }

            var numTarjetasBd = await dbContext.tarjetas.ToListAsync();
            List<int> Disponibles = new List<int>();
            foreach (var numLoteria in numTarjetasBd)
            {
                if(!NumNoDisp.Contains(numLoteria.Id)) { Disponibles.Add(numLoteria.Id); }
            }

            return Disponibles;
        }


        [HttpPost("NuevaRifa")]
        public async Task<ActionResult> Post(RifaCreacionDTO creacionRifaDTO)
        {
            var existeRifaConMismoNombre = await dbContext.rifas.AnyAsync(x => x.name == creacionRifaDTO.name);

            if (existeRifaConMismoNombre)
            {
                return BadRequest($"Ya existe una rifa con el nombre {creacionRifaDTO.name}");
            }

            var rifa = mapper.Map<Rifa>(creacionRifaDTO);

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("ModificarRifaPorID")]
        public async Task<ActionResult> Put(RifaModDTO modificacionRifaDTO, int id)
        {
            if (modificacionRifaDTO.Id != id)
            {
                return BadRequest("El id de la rifa no coincide con el establecido en la url.");
            }

            var rifa = mapper.Map<Rifa>(modificacionRifaDTO);

            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("EliminarRifaPorID")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var existe = await dbContext.rifas.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound("No se encontró la rifa que buscaba");
            }

            dbContext.Remove(new Rifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
