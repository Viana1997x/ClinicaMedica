using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPagamentoService
    {
        string RealizarPagamentoPorPix(string nome, string cpf);
        string RealizarPagamentoPorCartaoCredito(string nome, string cpf, string numeroCartao, string dataValidade, string cvv);
    }
}
