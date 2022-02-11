using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FelipeEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="O Campo {0} é obrigatório.")]
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage ="O Campo {0} é obrigatório.")]
        [StringLength(20, ErrorMessage = "{0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string Tema { get; set; }

        [Display(Name = "Quantidade de Pessoas")]
        [Required(ErrorMessage ="O Campo {0} é obrigatório.")]
        [Range(1, 500, ErrorMessage ="O Campo {0} não pode ser menor que {1} e maior que {2}.")]
        public int QtdPessoas { get; set; }

        [Display(Name = "Imagem")]
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Imagem em formato invalido. (gif, jpg, jpeg, bmp ou png)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage ="O Campo {0} é obrigatório.")]
        [Phone(ErrorMessage ="O Campo {0} está em formato invalido.")]
        public string Telefone { get; set; }

        [Display(Name = "e-mail")]
        [Required(ErrorMessage ="O Campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage ="O Campo {0} está em formato invalido.")]
        public string Email { get; set; } 

        public int UserId { get; set; }
        public UserDto UserDto { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedeSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}