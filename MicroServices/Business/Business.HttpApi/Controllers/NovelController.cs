using Business.NovelManagement;
using Business.NovelManagement.Dto;
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
    [Route("api/business/novel")]
    public class NovelController : AbpController
    {
        private readonly INovelAppService _NovelAppService;

        public NovelController(INovelAppService NovelAppService)
        {
            _NovelAppService = NovelAppService;
        }

        [HttpPost]
        public Task<NovelDto> Create(NovelCreateDto input)
        {
            return _NovelAppService.Create(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<NovelDto> Update(Guid id, NovelUpdateDto input)
        {
            return _NovelAppService.Update(id, input);
        }
        
        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _NovelAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<NovelDto> Get(Guid id)
        {
            return _NovelAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<NovelDto>> GetAll(GetNovelInputDto input)
        {
            return _NovelAppService.GetAll(input);
        }
    }
}
