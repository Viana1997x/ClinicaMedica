using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Core.Services;
using Core.Interfaces;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Controlador para operações relacionadas a consultas e pagamentos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;
        private readonly IPagamentoService _pagamentoService;

        /// <summary>
        /// Construtor do ConsultaController.
        /// </summary>
        /// <param name="consultaService">Serviço para manipulação de dados de consulta.</param>
        /// <param name="pagamentoService">Serviço para processamento de pagamentos.</param>
        public ConsultaController(IConsultaService consultaService, IPagamentoService pagamentoService)
        {
            _consultaService = consultaService;
            _pagamentoService = pagamentoService;
        }

        /// <summary>
        /// Marca uma nova consulta.
        /// </summary>
        /// <param name="consultaDTO">Dados da consulta a ser marcada.</param>
        /// <returns>Confirmação de sucesso ou erro ao marcar a consulta.</returns>
        [HttpPost("marcar")]
        public async Task<IActionResult> MarcarConsulta(ConsultaDTO consultaDTO)
        {
            var result = await _consultaService.MarcarConsulta(consultaDTO);
            if (result > 0)
            {
                return Ok("Consulta marcada com sucesso");
            }

            return BadRequest("Erro ao marcar consulta");
        }

        /// <summary>
        /// Realiza pagamento de consulta via Pix.
        /// </summary>
        /// <param name="request">Requisição contendo os dados para o pagamento via Pix.</param>
        /// <returns>Confirmação do pagamento.</returns>
        [HttpPost("pagar-pix")]
        public IActionResult PagarViaPix([FromBody] PagamentoRequestPix request)
        {
            var resultadoPagamento = _pagamentoService.RealizarPagamentoPorPix(request.Nome, request.CPF);
            return Ok(resultadoPagamento);
        }

        /// <summary>
        /// Realiza pagamento de consulta via cartão de crédito.
        /// </summary>
        /// <param name="request">Requisição contendo os dados para o pagamento via cartão de crédito.</param>
        /// <returns>Confirmação do pagamento.</returns>
        [HttpPost("pagar-cartao")]
        public IActionResult PagarViaCartaoCredito([FromBody] PagamentoRequestCartao request)
        {
            var resultadoPagamento = _pagamentoService.RealizarPagamentoPorCartaoCredito(
                request.Nome, request.CPF, request.NumeroCartao, request.DataValidade, request.CVV);
            return Ok(resultadoPagamento);
        }
    }

    /// <summary>
    /// Classe que representa a requisição para pagamento via Pix.
    /// </summary>
    public class PagamentoRequestPix
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// CPF do cliente.
        /// </summary>
        public string CPF { get; set; }
    }

    /// <summary>
    /// Classe que representa a requisição para pagamento via cartão de crédito.
    /// </summary>
    public class PagamentoRequestCartao
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// CPF do cliente.
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Número do cartão de crédito.
        /// </summary>
        public string NumeroCartao { get; set; }

        /// <summary>
        /// Data de validade do cartão de crédito.
        /// </summary>
        public string DataValidade { get; set; }

        /// <summary>
        /// Código de verificação (CVV) do cartão de crédito.
        /// </summary>
        public string CVV { get; set; }
    }
}
