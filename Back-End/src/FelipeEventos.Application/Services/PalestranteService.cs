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
    public class PalestranteService : IPalestranteService
    {
        private readonly IMapper _mapper;
        private readonly IPalestrantes _palestrante;
        public PalestranteService(IPalestrantes palestrante, IMapper mapper)
        {
            _palestrante = palestrante;
            _mapper = mapper;

        }
        public async Task<PalestranteDto> AddPalestrantes(int userId, PalestranteAddDto model)
        {
            
            try
            {
                var palestrante = _mapper.Map<Palestrante>(model);
                palestrante.UserId = userId;

                _palestrante.Add<Palestrante>(palestrante);

                if (await _palestrante.SaveChangesAsync())
                {
                    return _mapper.Map<PalestranteDto>(await _palestrante.GetPalestrantesByUserIdAsync(userId, false));
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model)
        {
            try
            {

                var palestrante = await _palestrante.GetPalestrantesByUserIdAsync(userId, false);

                if (palestrante == null) return null;

                model.Id = palestrante.Id;
                model.UserId = userId; 

                _mapper.Map(model, palestrante);

                _palestrante.Update<Palestrante>(palestrante);

                if (await _palestrante.SaveChangesAsync())
                {
                    return _mapper.Map<PalestranteDto>(await _palestrante.GetPalestrantesByUserIdAsync(userId, false));
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestrante.GetAllPalestrantesAsync(pageParams, includeEventos);

                if (palestrantes == null) return null;

                var resultado = _mapper.Map<PageList<PalestranteDto>>(palestrantes);

                resultado.CurrentPage = palestrantes.CurrentPage;
                resultado.TotalPages = palestrantes.TotalPages;
                resultado.PageSize = palestrantes.PageSize;
                resultado.TotalCount = palestrantes.TotalCount;


                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false)
        {
            try
            {
                var palestrante = await _palestrante.GetPalestrantesByUserIdAsync(userId, includeEventos);

                if (palestrante == null) return null;

                var resultado = _mapper.Map<PalestranteDto>(palestrante);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
