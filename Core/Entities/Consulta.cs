using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Consulta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataConsulta { get; set; }
        public string MetodoPagamento { get; set; }
    }

}
