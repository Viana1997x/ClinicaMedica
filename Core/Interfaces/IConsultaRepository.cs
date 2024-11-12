using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IConsultaRepository
    {
        Task<int> MarcarConsulta(Consulta consulta);
        Task<int> DesmarcarConsulta(int consultaId);
        Task<IEnumerable<Consulta>> ObterConsultasPorCliente(int clienteId);
        Task<IEnumerable<Consulta>> ObterConsultasPorMedico(int medicoId);

        Task<int> AddAsync(Consulta consulta);
        Task<IEnumerable<Consulta>> GetAllAsync();
    }

}
