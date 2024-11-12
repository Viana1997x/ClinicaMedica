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
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly IDbConnection _dbConnection;

        public ConsultaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> MarcarConsulta(Consulta consulta)
        {
            string sql = "INSERT INTO Consultas (ClienteId, MedicoId, DataConsulta, MetodoPagamento) VALUES (@ClienteId, @MedicoId, @DataConsulta, @MetodoPagamento)";
            return await _dbConnection.ExecuteAsync(sql, consulta);
        }

        public async Task<int> DesmarcarConsulta(int consultaId)
        {
            string sql = "DELETE FROM Consultas WHERE Id = @ConsultaId";
            return await _dbConnection.ExecuteAsync(sql, new { ConsultaId = consultaId });
        }

        public async Task<IEnumerable<Consulta>> ObterConsultasPorCliente(int clienteId)
        {
            string sql = "SELECT * FROM Consultas WHERE ClienteId = @ClienteId";
            return await _dbConnection.QueryAsync<Consulta>(sql, new { ClienteId = clienteId });
        }

        public async Task<IEnumerable<Consulta>> ObterConsultasPorMedico(int medicoId)
        {
            string sql = "SELECT * FROM Consultas WHERE MedicoId = @MedicoId";
            return await _dbConnection.QueryAsync<Consulta>(sql, new { MedicoId = medicoId });
        }
        public async Task<int> AddAsync(Consulta consulta)
        {
            var sql = @"INSERT INTO Consultas (MedicoId, ClienteId, DataConsulta, Observacoes)
                        VALUES (@MedicoId, @ClienteId, @DataConsulta, @Observacoes)";
            return await _dbConnection.ExecuteAsync(sql, consulta);
        }

        // Implementar GetAllAsync
        public async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Consultas";
            return await _dbConnection.QueryAsync<Consulta>(sql);
        }
    }

}
