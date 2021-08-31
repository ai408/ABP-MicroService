using System;
using Volo.Abp.Application.Dtos;

namespace Business.TemHumManagement.Dto
{
    public class CreateOrUpdateTemHumDto: EntityDto<Guid?>
    {
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
    }
}