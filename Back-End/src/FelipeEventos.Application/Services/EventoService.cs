using System;
using System.Threading.Tasks;
using AutoMapper;
using FelipeEventos.Application.Dtos;
using FelipeEventos.Application.Services.Interfaces;
using FelipeEventos.Domain.Models;
using FelipeEventos.Persistence.Interfaces;
using FelipeEventos.Persistence.Paginacao;

namespace FelipeEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IGeral _geral;
        private readonly IEventos _evento;
        private readonly IMapper _mapper;
        public EventoService(IGeral geral, IEventos evento, IMapper mapper)
        {
            _mapper = mapper;
            _evento = evento;
            _geral = geral;

        }
        public async Task<EventoDto> AddEvento(int userId, EventoDto model)
        {
            
            try
            {
                var evento = _mapper.Map<Evento>(model);
                evento.UserId = userId;

                _geral.Add<Evento>(evento);

                if (await _geral.SaveChangesAsync())
                {
                    return _mapper.Map<EventoDto>(await _evento.GetEventoByIdAsync(userId, evento.Id, false));
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model)
        {
            try
            {

                var evento = await _evento.GetEventoByIdAsync(userId, eventoId, false);

                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId; 

                _mapper.Map(model, evento);

                _geral.Update<Evento>(evento);

                if (await _geral.SaveChangesAsync())
                {
                    return _mapper.Map<EventoDto>(await _evento.GetEventoByIdAsync(userId, evento.Id, false));
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            try
            {
                var evento = await _evento.GetEventoByIdAsync(userId, eventoId, false);

                if (evento == null) throw new Exception("Evento não foi encontrado para exclusão.");

                _geral.Delete<Evento>(evento);

                return await _geral.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _evento.GetAllEventosAsync(userId, pageParams, includePalestrantes);

                if (eventos == null) return null;

                var resultado = _mapper.Map<PageList<EventoDto>>(eventos);

                resultado.CurrentPage = eventos.CurrentPage;
                resultado.TotalPages = eventos.TotalPages;
                resultado.PageSize = eventos.PageSize;
                resultado.TotalCount = eventos.TotalCount;


                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _evento.GetEventoByIdAsync(userId, eventoId, includePalestrantes);

                if (evento == null) return null;

                var resultado = _mapper.Map<EventoDto>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
