using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMedicoRepository
    {
        Task<int> RegistrarMedico(Medico medico);
        Task<Medico> LoginMedico(string cpf, string senha);
        Task<IEnumerable<Consulta>> ObterConsultasPorMedico(int medicoId);
        Task<IEnumerable<Medico>> GetAllAsync();
        Task<int> DeleteAsync(int id);
    }

}
