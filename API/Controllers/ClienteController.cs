using Microsoft.AspNetCore.Mvc;
using Core.DTOs;

using Core.Services;
using Core.Interfaces;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas ao cliente.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        /// <summary>
        /// Construtor do ClienteController.
        /// </summary>
        /// <param name="clienteService">Serviço para manipulação dos dados de cliente.</param>
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Registra um novo cliente.
        /// </summary>
        /// <param name="clienteDTO">Dados do cliente a ser registrado.</param>
        /// <returns>Confirmação de sucesso ou erro ao registrar o cliente.</returns>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarCliente(ClienteDTO clienteDTO)
        {
            try
            {
                var result = await _clienteService.RegistrarCliente(clienteDTO);
                if (result > 0)
                    return Ok("Cliente registrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest("Erro ao registrar cliente");
        }

        /// <summary>
        /// Realiza o login de um cliente.
        /// </summary>
        /// <param name="request">Requisição contendo CPF e senha do cliente.</param>
        /// <returns>Dados do cliente caso o login seja bem-sucedido, ou erro de autenticação.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginCliente([FromBody] LoginRequest request)
        {
            var cliente = await _clienteService.LoginCliente(request.CPF, request.Senha);
            if (cliente != null)
                return Ok(cliente);
            return Unauthorized("CPF ou Senha incorretos");
        }

        /// <summary>
        /// Obtém as consultas de um cliente específico.
        /// </summary>
        /// <param name="clienteId">ID do cliente.</param>
        /// <returns>Lista de consultas do cliente.</returns>
        [HttpGet("{clienteId}/consultas")]
        public async Task<IActionResult> ObterConsultas(int clienteId)
        {
            var consultas = await _clienteService.ObterConsultasPorCliente(clienteId);
            return Ok(consultas);
        }

        /// <summary>
        /// Obtém todos os clientes do sistema.
        /// </summary>
        /// <returns>Lista de todos os clientes.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _clienteService.GetAllClientes();
            return Ok(clientes);
        }


        /// <summary>
        /// Deleta um cliente específico pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser deletado.</param>
        /// <returns>Confirmação da exclusão ou mensagem de erro.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _clienteService.DeleteClienteAsync(id);
            if (result > 0)
                return Ok("Cliente deletado com sucesso");
            return NotFound("Cliente não encontrado");
        }


    }



    /// <summary>
    /// Classe que representa a requisição de login de um cliente.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// CPF do cliente.
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Senha do cliente.
        /// </summary>
        public string Senha { get; set; }
    }
}
