using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pagamentos
{
    public class PagamentoPix : Pagamento
    {
        public PagamentoPix(string nome, string cpf) : base(nome, cpf)
        {
        }

        public override string ProcessarPagamento()
        {
            // Gera um código Pix de 9 dígitos para simular o pagamento
            Random random = new Random();
            string codigoPix = random.Next(100000000, 999999999).ToString();

            return $"Pagamento via Pix realizado com sucesso. Código de pagamento: {codigoPix}";
        }
    }

}
