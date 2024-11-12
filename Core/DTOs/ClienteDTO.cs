using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ClienteDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve ter exatamente 11 dígitos numéricos.")]
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
    }


}
