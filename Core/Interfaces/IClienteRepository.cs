using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IClienteRepository
    {
        Task<int> AddAsync(Cliente cliente);
        Task<Cliente> GetByCpfAndSenhaAsync(string cpf, string senha);
        Task<IEnumerable<Consulta>> GetConsultasByClienteAsync(int clienteId);

        // Adiciona o método para buscar todos os clientes
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<int> DeleteAsync(int id);


    }
}
