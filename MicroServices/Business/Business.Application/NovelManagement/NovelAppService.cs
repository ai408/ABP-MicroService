using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Business.Models;
using Business.NovelManagement.Dto;

namespace Business.NovelManagement
{
    //[Authorize(BusinessPermissions.Novel.Default)]
    public class NovelAppService : ApplicationService,INovelAppService
    {
        private IRepository<Novel, Guid> _repository;

        public NovelAppService(
            IRepository<Novel, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<NovelDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<Novel, NovelDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<NovelDto>> GetAll(GetNovelInputDto input)
        {
            var query = _repository.WhereIf(!string.IsNullOrWhiteSpace(input.FilterName), a => a.Name.Contains(input.FilterName))
                .WhereIf(!string.IsNullOrWhiteSpace(input.FilterDescription), a => a.Name.Contains(input.FilterDescription));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                        .ToListAsync();

            var dto = ObjectMapper.Map<List<Novel>, List<NovelDto>>(items);
            return new PagedResultDto<NovelDto>(totalCount, dto);
        }

        public async Task<NovelDto> Create(NovelCreateDto input)
        {
            Novel result = null;
            if (!input.Id.HasValue)
            {
                var novel = new Novel(
                    GuidGenerator.Create(),
                    CurrentTenant.Id,
                    input.Name,
                    input.Description,
                    input.Price);
                result = await _repository.InsertAsync(novel);
            }
            return ObjectMapper.Map<Novel, NovelDto>(result);
        }
        
        public async Task<NovelDto> Update(Guid id, NovelUpdateDto input)
        {
            var data = await _repository.GetAsync(id);
            data.Name = input.Name;
            data.Description = input.Description;
            data.Price = input.Price;
            var result = await _repository.UpdateAsync(data);
            return ObjectMapper.Map<Novel, NovelDto>(result);
        }
        
        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _repository.DeleteAsync(item);
            }
        }
        
        #endregion
    }
}