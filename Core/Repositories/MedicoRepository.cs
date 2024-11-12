using Core.Entities;
using Core.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly IDbConnection _dbConnection;

        public MedicoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> RegistrarMedico(Medico medico)
        {
            string sql = "INSERT INTO Medicos (Nome, CPF, Endereco, DataNascimento, Telefone, AreaEspecializacao, Senha) VALUES (@Nome, @CPF, @Endereco, @DataNascimento, @Telefone, @AreaEspecializacao, @Senha)";
            return await _dbConnection.ExecuteAsync(sql, medico);
        }

        public async Task<Medico> LoginMedico(string cpf, string senha)
        {
            string sql = "SELECT * FROM Medicos WHERE CPF = @CPF AND Senha = @Senha";
            return await _dbConnection.QueryFirstOrDefaultAsync<Medico>(sql, new { CPF = cpf, Senha = senha });
        }

        public async Task<IEnumerable<Consulta>> ObterConsultasPorMedico(int medicoId)
        {
            string sql = "SELECT * FROM Consultas WHERE MedicoId = @MedicoId";
            return await _dbConnection.QueryAsync<Consulta>(sql, new { MedicoId = medicoId });
        }
        public async Task<IEnumerable<Medico>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Medicos";
            return await _dbConnection.QueryAsync<Medico>(sql);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"DELETE FROM Medicos WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }

}
