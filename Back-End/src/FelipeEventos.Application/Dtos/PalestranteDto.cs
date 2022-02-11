using System.Collections.Generic;

namespace FelipeEventos.Application.Dtos
{
    public class PalestranteDto
    {
        public int Id { get; set; }
        public string MiniCurriculo { get; set; }
        public int UserId { get; set; }
        public UserUpdateDto User { get; set; }
        public IEnumerable<RedeSocialDto> RedeSociais { get; set; }
        public IEnumerable<EventoDto> Eventos { get; set; }

    }
}