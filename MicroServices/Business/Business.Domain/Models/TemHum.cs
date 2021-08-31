using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Business.Models
{
    public class TemHum: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public bool IsDeleted { get; set; }
        
        /// <summary>
        /// 温度
        /// </summary>
        public decimal Tem { get; set; }

        
        /// <summary>
        /// 湿度
        /// </summary>
        public decimal Hum { get; set; }

        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
        public TemHum(Guid id, Guid? tenantId, decimal tem, decimal hum, string remark)
        {
            Id = id;
            TenantId = tenantId;
            Tem = tem;
            Hum = hum;
            Remark = remark;
        }
    }
}