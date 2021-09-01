using Business.TemHumManagement;
using Business.TemHumManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Business.Controllers
{
    [RemoteService]
    [Area("Business")]
    [Route("api/business/TemHum")]
    public class TemHumController : AbpController
    {
        private readonly ITemHumAppService _TemHumAppService;

        public TemHumController(ITemHumAppService TemHumAppService)
        {
            _TemHumAppService = TemHumAppService;
        }

        [HttpPost]
        [HttpPut]
        public Task<TemHumDto> CreateOrUpdate(CreateOrUpdateTemHumDto input)
        {
            return _TemHumAppService.CreateOrUpdate(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _TemHumAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<TemHumDto> Get(Guid id)
        {
            return _TemHumAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<TemHumDto>> GetAll(GetTemHumInputDto input)
        {
            return _TemHumAppService.GetAll(input);
        }
    }
}