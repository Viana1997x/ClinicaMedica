using Core.Entities;
using Core.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbConnection _dbConnection;

        public ClienteRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Método para adicionar um novo cliente com verificação de unicidade do CPF
        public async Task<int> AddAsync(Cliente cliente)
        {
            // Verifica se o CPF já está registrado no banco
            var existingClient = await GetByCpfAsync(cliente.CPF);
            if (existingClient != null)
            {
                throw new System.Exception("CPF já registrado no sistema.");
            }

            var sql = @"INSERT INTO Clientes (Nome, CPF, Endereco, DataNascimento, Telefone, Senha)
                        VALUES (@Nome, @CPF, @Endereco, @DataNascimento, @Telefone, @Senha)";
            return await _dbConnection.ExecuteAsync(sql, cliente);
        }

        // Implementa o método para buscar cliente por CPF e senha
        public async Task<Cliente> GetByCpfAndSenhaAsync(string cpf, string senha)
        {
            var sql = @"SELECT * FROM Clientes WHERE CPF = @CPF AND Senha = @Senha";
            return await _dbConnection.QueryFirstOrDefaultAsync<Cliente>(sql, new { CPF = cpf, Senha = senha });
        }

        // Implementa o método para buscar todas as consultas de um cliente
        public async Task<IEnumerable<Consulta>> GetConsultasByClienteAsync(int clienteId)
        {
            var sql = @"SELECT * FROM Consultas WHERE ClienteId = @ClienteId";
            return await _dbConnection.QueryAsync<Consulta>(sql, new { ClienteId = clienteId });
        }

        // Implementa o método para buscar todos os clientes
        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Clientes";
            return await _dbConnection.QueryAsync<Cliente>(sql);
        }

        // Implementa o método para buscar cliente pelo CPF
        public async Task<Cliente> GetByCpfAsync(string cpf)
        {
            var sql = @"SELECT * FROM Clientes WHERE CPF = @CPF";
            return await _dbConnection.QueryFirstOrDefaultAsync<Cliente>(sql, new { CPF = cpf });
        }

        // Implementa o método para buscar cliente pelo ID
        public async Task<Cliente> GetByIdAsync(int id)
        {
            var sql = @"SELECT * FROM Clientes WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"DELETE FROM Clientes WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
