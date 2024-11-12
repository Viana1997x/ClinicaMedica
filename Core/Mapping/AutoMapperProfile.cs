using AutoMapper;
using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Medico, MedicoDTO>().ReverseMap();
            CreateMap<Consulta, ConsultaDTO>().ReverseMap();
        }
    }

}
