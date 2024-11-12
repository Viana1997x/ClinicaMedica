using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ConsultaDTO
    {
        public int ClienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataConsulta { get; set; }
        public string MetodoPagamento { get; set; }
    }

}
