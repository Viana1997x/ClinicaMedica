using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pagamentos
{
    public class PagamentoCartaoCredito : Pagamento
    {
        public string NumeroCartao { get; set; }
        public string DataValidade { get; set; }
        public string CVV { get; set; }

        public PagamentoCartaoCredito(string nome, string cpf, string numeroCartao, string dataValidade, string cvv)
            : base(nome, cpf)
        {
            NumeroCartao = numeroCartao;
            DataValidade = dataValidade;
            CVV = cvv;
        }

        public override string ProcessarPagamento()
        {
            // Simula a aprovação de pagamento com cartão de crédito
            return $"Pagamento via Cartão de Crédito realizado com sucesso. Número do cartão: {NumeroCartao.Substring(0, 4)} **** **** ****";
        }
    }

}
