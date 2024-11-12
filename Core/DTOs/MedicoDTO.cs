using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MedicoDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string AreaEspecializacao { get; set; }
        public string Senha { get; set; }
    }


}
