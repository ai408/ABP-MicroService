using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Business.NovelManagement.Dto;

namespace Business.NovelManagement
{
    public interface INovelAppService : IApplicationService
    {
        Task<NovelDto> Get(Guid id);
        Task<PagedResultDto<NovelDto>> GetAll(GetNovelInputDto input);
        Task<NovelDto> Create(NovelCreateDto input);
        Task<NovelDto> Update(Guid id, NovelUpdateDto input);
        Task Delete(List<Guid> ids);
    }
}