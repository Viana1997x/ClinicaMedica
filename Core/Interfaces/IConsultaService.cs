using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IConsultaService
    {
        Task<int> MarcarConsulta(ConsultaDTO consultaDTO);
        Task<int> DesmarcarConsulta(int consultaId);

        Task<IEnumerable<ConsultaDTO>> GetAllConsultas();



    }

}
