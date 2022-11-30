using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApiRifaCasinoPIA.Entidades;

namespace ApiRifaCasinoPIA
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Rifa> rifas { get; set; }
        public DbSet<ParticipanteDeRifa> participantes { get; set; }
        public DbSet<PremioDeRifa> premios { get; set; }
        public DbSet<RifaParticipante> rifaParticipantes { get; set; }
        public DbSet<Tarjeta> tarjetas { get; set; }
    }
}

