using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pagamentos
{
    public abstract class Pagamento
    {
        public string Nome { get; set; }
        public string CPF { get; set; }

        public Pagamento(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
        }

        public abstract string ProcessarPagamento();
    }

}
