using AutoMapper;
using Business.Models;
using Business.NovelManagement.Dto;

namespace Business.NovelManagement
{
    public class NovelAutoMapperProfile: Profile
    {
        public NovelAutoMapperProfile()
        {
            CreateMap<Novel, NovelDto>();
            CreateMap<NovelCreateDto, Novel>();
            CreateMap<NovelUpdateDto, Novel>();
        }
    }
}