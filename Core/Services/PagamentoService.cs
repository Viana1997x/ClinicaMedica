using Core.Interfaces;
using Core.Pagamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PagamentoService : IPagamentoService
    {
        public string RealizarPagamentoPorPix(string nome, string cpf)
        {
            var pagamentoPix = new PagamentoPix(nome, cpf);
            return pagamentoPix.ProcessarPagamento();
        }

        public string RealizarPagamentoPorCartaoCredito(string nome, string cpf, string numeroCartao, string dataValidade, string cvv)
        {
            var pagamentoCartao = new PagamentoCartaoCredito(nome, cpf, numeroCartao, dataValidade, cvv);
            return pagamentoCartao.ProcessarPagamento();
        }
    }

}
