using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FelipeEventos.Application.Dtos;
using FelipeEventos.Application.Services.Interfaces;
using FelipeEventos.Domain.Models;
using FelipeEventos.Persistence.Interfaces;

namespace FelipeEventos.Application.Services
{
    public class RedeSocialService : IRedeSocialService
    {
        private readonly IRedesSocial _redeSocial;
        private readonly IMapper _mapper;
        public RedeSocialService(IRedesSocial redesocial, IMapper mapper)
        {
            _mapper = mapper;
            _redeSocial = redesocial;

        }
        public async Task AddRedeSocial(int Id, RedeSocialDto model, bool isEvento)
        {
            try
            {
                var redeSocial = _mapper.Map<RedeSocial>(model);
               
                if (isEvento)
                {
                    redeSocial.EventoId = Id;
                    redeSocial.PalestranteId = null;
                }
                else
                {
                    redeSocial.EventoId = null;
                    redeSocial.PalestranteId = Id;
                }

                _redeSocial.Add<RedeSocial>(redeSocial);

                await _redeSocial.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto[]> SaveByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                var redeSocialEv = await _redeSocial.GetAllByEventoIdAsync(eventoId);
                if ( redeSocialEv == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddRedeSocial(eventoId, model, true);
                    }
                    else
                    {
                        var RedeSocial = redeSocialEv.FirstOrDefault(redeSocial => redeSocial.Id == model.Id);
                        model.EventoId = eventoId;

                        _mapper.Map(model, RedeSocial);

                        _redeSocial.Update<RedeSocial>(RedeSocial);

                        await _redeSocial.SaveChangesAsync();
                    }
                }

                var RedeSocialRetorno = await _redeSocial.GetAllByEventoIdAsync(eventoId);

                return _mapper.Map<RedeSocialDto[]>(RedeSocialRetorno);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto[]> SaveByPalestrante(int palestranteId, RedeSocialDto[] models)
        {
            try
            {
                var redeSocialPa = await _redeSocial.GetAllByPalestranteIdAsync(palestranteId);
                if ( redeSocialPa == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddRedeSocial(palestranteId, model, false);
                    }
                    else
                    {
                        var RedeSocial = redeSocialPa.FirstOrDefault(redeSocial => redeSocial.Id == model.Id);
                        model.PalestranteId = palestranteId;

                        _mapper.Map(model, RedeSocial);

                        _redeSocial.Update<RedeSocial>(RedeSocial);

                        await _redeSocial.SaveChangesAsync();
                    }
                }

                var RedeSocialRetorno = await _redeSocial.GetAllByPalestranteIdAsync(palestranteId);

                return _mapper.Map<RedeSocialDto[]>(RedeSocialRetorno);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                var redeSocialEv = await _redeSocial.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);
                if (redeSocialEv == null) throw new Exception("Rede Social para deletar não encontrado.");

                _redeSocial.Delete<RedeSocial>(redeSocialEv);
                return await _redeSocial.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

         public async Task<bool> DeleteByPalestrante(int palestranteId, int redeSocialId)
        {
            try
            {
                var redeSocialPa = await _redeSocial.GetRedeSocialPalestranteByIdsAsync(palestranteId, redeSocialId);
                if (redeSocialPa == null) throw new Exception("Rede Social para deletar não encontrado.");

                _redeSocial.Delete<RedeSocial>(redeSocialPa);
                return await _redeSocial.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto[]> GetAllByEventoIdAsync(int eventoId)
        {
            try
            {
                var redeSocialEv = await _redeSocial.GetAllByEventoIdAsync(eventoId);
                if (redeSocialEv == null) return null;

                var resultado = _mapper.Map<RedeSocialDto[]>(redeSocialEv);

                return resultado;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto[]> GetAllByPalestranteIdAsync(int palestranteId)
        {
            try
            {
                var redeSocialPa = await _redeSocial.GetAllByPalestranteIdAsync(palestranteId);
                if (redeSocialPa == null) return null;

                var resultado = _mapper.Map<RedeSocialDto[]>(redeSocialPa);

                return resultado;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync(int eventoId, int redeSocialId)
        {
            try
            {
                var redeSocialEv = await _redeSocial.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);
                if (redeSocialEv == null) return null;

                var resultado = _mapper.Map<RedeSocialDto>(redeSocialEv);

                return resultado;
                
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int redeSocialId)
        {
            try
            {
                var redeSocialPa = await _redeSocial.GetRedeSocialPalestranteByIdsAsync(palestranteId, redeSocialId);
                if (redeSocialPa == null) return null;

                var resultado = _mapper.Map<RedeSocialDto>(redeSocialPa);

                return resultado;
                
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}
