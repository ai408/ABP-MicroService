using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Business.TemHumManagement.Dto;

namespace Business.TemHumManagement
{
    public interface ITemHumAppService : IApplicationService
    {
        Task<TemHumDto> Get(Guid id);
        Task<PagedResultDto<TemHumDto>> GetAll(GetTemHumInputDto input);
        Task<TemHumDto> CreateOrUpdate(CreateOrUpdateTemHumDto input);
        Task Delete(List<Guid> ids);
    }
}