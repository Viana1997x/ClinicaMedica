using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IClienteService
    {
        Task<int> RegistrarCliente(ClienteDTO clienteDTO);
        Task<ClienteDTO> LoginCliente(string cpf, string senha);
        Task<IEnumerable<ConsultaDTO>> ObterConsultasPorCliente(int clienteId);

        Task<IEnumerable<ClienteDTO>> GetAllClientes();
        Task<int> DeleteClienteAsync(int id);


    }

}
