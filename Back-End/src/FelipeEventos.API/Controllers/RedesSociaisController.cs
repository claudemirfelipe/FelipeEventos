using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FelipeEventos.API.Extensions;
using FelipeEventos.Application.Dtos;
using FelipeEventos.Application.Services.Interfaces;


namespace FelipeEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RedesSociaisController : ControllerBase
    {
        private readonly IRedeSocialService _redeSocialService;
        private readonly IEventoService _eventoService;
        private readonly IPalestranteService _palestranteService;
        public RedesSociaisController(IRedeSocialService redeSocialService, IEventoService eventoService, IPalestranteService palestranteService)
        {
            _redeSocialService = redeSocialService;
            _eventoService = eventoService;
            _palestranteService = palestranteService;
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<IActionResult> GetByEvento(int eventoId)
        {
            try
            {
                if (! (await AutorEvento(eventoId)))
                    return Unauthorized();

                var redeSocialEv = await _redeSocialService.GetAllByEventoIdAsync(eventoId);
                if (redeSocialEv == null) return NoContent();

                return Ok(redeSocialEv);
            }
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Redes Sociais do Evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("palestrante")]
        public async Task<IActionResult> GetByPalestrante()
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();

                var redeSocialPa = await _redeSocialService.GetAllByPalestranteIdAsync(palestrante.Id);
                if (redeSocialPa == null) return NoContent();

                return Ok(redeSocialPa);
            }
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Redes Sociais do Evento. Erro: {ex.Message}");
            }
        }


        [HttpPut("evento/{eventoId}")]
        public async Task<IActionResult> SaveRedeSocialByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                if(! (await AutorEvento(eventoId)))
                    return Unauthorized();

                var redeSocialEv = await _redeSocialService.SaveByEvento(eventoId, models);
                if (redeSocialEv == null) return NoContent();

                return Ok(redeSocialEv);
            }
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar Rede Social do Evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("palestrante")]
        public async Task<IActionResult> SaveRedeSocialByPalestrante(RedeSocialDto[] models)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();

                var redeSocialEv = await _redeSocialService.SaveByPalestrante(palestrante.Id, models);
                if (redeSocialEv == null) return NoContent();

                return Ok(redeSocialEv);
            }
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar Rede Social do palestrante. Erro: {ex.Message}");
            }
        }

        [HttpDelete("evento/{eventoId}/{redeSocialId}")]
        public async Task<IActionResult> DeleteRedeSocialByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                if(! (await AutorEvento(eventoId)))
                    return Unauthorized();

                var redeSocialEv = await _redeSocialService.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);
                if (redeSocialEv == null) return NoContent();
                
                return await _redeSocialService.DeleteByEvento(eventoId, redeSocialId)
                    ? Ok( new { message = "Rede Social do Evento Deletado" })
                    : throw new Exception( " Ocorreu um problema não esperado");
            }
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar a Rede Social do Evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("palestrante/{redeSocialId}")]
        public async Task<IActionResult> DeleteRedeSocialByPalestrante(int redeSocialId)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();

                var redeSocialPa = await _redeSocialService.GetRedeSocialPalestranteByIdsAsync(palestrante.Id, redeSocialId);
                if (redeSocialPa == null) return NoContent();
                
                return await _redeSocialService.DeleteByPalestrante(palestrante.Id, redeSocialId)
                    ? Ok( new { message = "Rede Social do Palestrante Deletado" })
                    : throw new Exception( " Ocorreu um problema não esperado");
            }
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar a Rede Social do Palestrante. Erro: {ex.Message}");
            }
        }

        [NonAction]
        private async Task<bool> AutorEvento(int eventoId)
        {
            var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), eventoId, false);
            if (evento == null) return false;

            return true;
        }
    }
}
