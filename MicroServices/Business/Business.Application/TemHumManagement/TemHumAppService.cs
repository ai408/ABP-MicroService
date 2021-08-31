using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Business.TemHumManagement.Dto;
using Business.Models;

namespace Business.TemHumManagement
{
    //[Authorize(BusinessPermissions.TemHum.Default)]
    public class TemHumAppService : ApplicationService,ITemHumAppService
    {
        private IRepository<TemHum, Guid> _repository;

        public TemHumAppService(IRepository<TemHum, Guid> repository)
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<TemHumDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<TemHum, TemHumDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<TemHumDto>> GetAll(GetTemHumInputDto input)
        {
            var query = _repository.WhereIf(!string.IsNullOrWhiteSpace(input.FilterHum.ToString(CultureInfo.CurrentCulture)), a => Convert.ToDecimal(a.Tem) > input.FilterHum);
            
            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id").ToListAsync();
            
            var dto = ObjectMapper.Map<List<TemHum>, List<TemHumDto>>(items);
            return new PagedResultDto<TemHumDto>(totalCount, dto);
        }
        
        public async Task<TemHumDto> CreateOrUpdate(CreateOrUpdateTemHumDto input)
        {
            TemHum result = null;
            if (!input.Id.HasValue)
            {
                var TemHum = new TemHum(
                    GuidGenerator.Create(),
                    CurrentTenant.Id,
                    input.Tem,
                    input.Hum,
                    input.Remark);
                result = await _repository.InsertAsync(TemHum);
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<TemHum, TemHumDto>(result);
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