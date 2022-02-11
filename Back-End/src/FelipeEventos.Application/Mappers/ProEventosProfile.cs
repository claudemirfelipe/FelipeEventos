using AutoMapper;
using FelipeEventos.Application.Dtos;
using FelipeEventos.Domain.Identity;
using FelipeEventos.Domain.Models;

namespace FelipeEventos.Application.Mappers
{
    public class FelipeEventosProfile : Profile
    {
        public FelipeEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteAddDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteUpdateDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
        
    }
}