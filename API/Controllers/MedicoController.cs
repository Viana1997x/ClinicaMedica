using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Core.Services;
using Core.Interfaces;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        /// <summary>
        /// Registra um novo médico no sistema.
        /// </summary>
        /// <param name="medicoDTO">Objeto com os dados do médico a ser registrado.</param>
        /// <returns>Retorna uma mensagem de sucesso ou erro.</returns>
        /// <response code="200">Médico registrado com sucesso</response>
        /// <response code="400">Erro ao registrar médico</response>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarMedico(MedicoDTO medicoDTO)
        {
            var result = await _medicoService.RegistrarMedico(medicoDTO);
            if (result > 0)
                return Ok("Médico registrado com sucesso");
            return BadRequest("Erro ao registrar médico");
        }

        /// <summary>
        /// Realiza o login do médico.
        /// </summary>
        /// <param name="request">Objeto contendo CPF e Senha do médico.</param>
        /// <returns>Retorna os dados do médico ou uma mensagem de erro.</returns>
        /// <response code="200">Retorna os dados do médico</response>
        /// <response code="401">CPF ou Senha incorretos</response>
        [HttpPost("login")]
        public async Task<IActionResult> LoginMedico([FromBody] LoginRequest request)
        {
            var medico = await _medicoService.LoginMedico(request.CPF, request.Senha);
            if (medico != null)
                return Ok(medico);
            return Unauthorized("CPF ou Senha incorretos");
        }

        /// <summary>
        /// Obtém todas as consultas de um médico específico.
        /// </summary>
        /// <param name="medicoId">ID do médico.</param>
        /// <returns>Retorna a lista de consultas do médico.</returns>
        /// <response code="200">Retorna a lista de consultas</response>
        [HttpGet("{medicoId}/consultas")]
        public async Task<IActionResult> ObterConsultas(int medicoId)
        {
            var consultas = await _medicoService.ObterConsultasPorMedico(medicoId);
            return Ok(consultas);
        }


        /// <summary>
        /// Obtém todos os medicos do sistema.
        /// </summary>
        /// <returns>Lista de todos os medicos.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllMedicos()
        {
            var medicos = await _medicoService.GetAllMedicos();
            return Ok(medicos);
        }
    }
}
