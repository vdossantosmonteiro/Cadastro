using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Services.Models
{
    public class ClienteCadastroModel
    {
        [Required(ErrorMessage ="Nome obrigatório")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage ="Email inválido")]
        [Required(ErrorMessage ="Email obrigatório")]
        public string Email { get; set; }
    }
}
