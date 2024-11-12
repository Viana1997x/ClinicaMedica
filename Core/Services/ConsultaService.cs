using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;

        public ConsultaService(IConsultaRepository consultaRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mapper = mapper;
        }

        public async Task<int> MarcarConsulta(ConsultaDTO consultaDTO)
        {
            var consulta = _mapper.Map<Consulta>(consultaDTO);
            return await _consultaRepository.MarcarConsulta(consulta);
        }

        public async Task<int> DesmarcarConsulta(int consultaId)
        {
            return await _consultaRepository.DesmarcarConsulta(consultaId);
        }
        public async Task<IEnumerable<ConsultaDTO>> GetAllConsultas()
        {
            var consultas = await _consultaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ConsultaDTO>>(consultas);
        }
    }

}
