using AutoMapper;
using Business.TemHumManagement.Dto;
using Business.Models;

namespace Business.TemHumManagement
{
    public class TemHumAutoMapperProfile : Profile
    {
        public TemHumAutoMapperProfile()
        {
            CreateMap<TemHum, TemHumDto>();
            CreateMap<CreateOrUpdateTemHumDto, TemHum>();
        }
    }
}
