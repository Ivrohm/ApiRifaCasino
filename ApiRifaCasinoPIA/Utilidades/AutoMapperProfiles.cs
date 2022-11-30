using AutoMapper;
using ApiRifaCasinoPIA.DTOs;
using ApiRifaCasinoPIA.Entidades;

namespace ApiRifaCasinoPIA.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RifaCreacionDTO, Rifa>();

            CreateMap<Rifa, GetRifaDTO>();

            CreateMap<RifaModDTO, Rifa>();

            CreateMap<AddParticipanteRifaDTO, RifaParticipante>();

            CreateMap<RegisterParticipanteDTO, ParticipanteDeRifa>();
              
            CreateMap<PremioDeRifa, GetPremiosDTO>();

            CreateMap<AddPremioDTO, PremioDeRifa>();

            CreateMap<ModificarPremioPatchDTO, PremioDeRifa>();

            CreateMap<ParticipanteDeRifa, GetParticipantesDTO>();

            CreateMap<RifaParticipanteDTO, RifaParticipante>();
        }
    }
}
