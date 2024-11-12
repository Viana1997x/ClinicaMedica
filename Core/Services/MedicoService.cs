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
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public MedicoService(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task<int> RegistrarMedico(MedicoDTO medicoDTO)
        {
            var medico = _mapper.Map<Medico>(medicoDTO);
            return await _medicoRepository.RegistrarMedico(medico);
        }

        public async Task<MedicoDTO> LoginMedico(string cpf, string senha)
        {
            var medico = await _medicoRepository.LoginMedico(cpf, senha);
            return _mapper.Map<MedicoDTO>(medico);
        }

        public async Task<IEnumerable<ConsultaDTO>> ObterConsultasPorMedico(int medicoId)
        {
            var consultas = await _medicoRepository.ObterConsultasPorMedico(medicoId);
            return _mapper.Map<IEnumerable<ConsultaDTO>>(consultas);
        }
        public async Task<IEnumerable<MedicoDTO>> GetAllMedicos()
        {
            var medicos = await _medicoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MedicoDTO>>(medicos);
        }
        public async Task<int> DeleteMedicoAsync(int id)  // Implementação do método DeleteMedicoAsync
        {
            return await _medicoRepository.DeleteAsync(id);  // Chama o repositório para deletar o médico
        }
    }

}
