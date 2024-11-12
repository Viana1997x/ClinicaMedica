using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMedicoService
    {
        Task<int> RegistrarMedico(MedicoDTO medicoDTO);
        Task<MedicoDTO> LoginMedico(string cpf, string senha);
        Task<IEnumerable<ConsultaDTO>> ObterConsultasPorMedico(int medicoId);

        Task<IEnumerable<MedicoDTO>> GetAllMedicos();

    }

}
