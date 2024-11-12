using Core.DTOs;
using Core.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<int> RegistrarCliente(ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            try
            {
                return await _clienteRepository.AddAsync(cliente);
            }
            catch (Exception ex)
            {
                // Retorna mensagem de erro se CPF já existir
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDTO> LoginCliente(string cpf, string senha)
        {
            var cliente = await _clienteRepository.GetByCpfAndSenhaAsync(cpf, senha);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<IEnumerable<ConsultaDTO>> ObterConsultasPorCliente(int clienteId)
        {
            var consultas = await _clienteRepository.GetConsultasByClienteAsync(clienteId);
            return _mapper.Map<IEnumerable<ConsultaDTO>>(consultas);
        }

        // Implementa o método GetAllClientes
        public async Task<IEnumerable<ClienteDTO>> GetAllClientes()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }
        public async Task<int> DeleteClienteAsync(int id)  // Implementação do método Delete
        {
            return await _clienteRepository.DeleteAsync(id);  // Chama o repositório para deletar o cliente
        }

    }
}
