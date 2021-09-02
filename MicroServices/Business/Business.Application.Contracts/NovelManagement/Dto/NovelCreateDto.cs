using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Business.NovelManagement.Dto
{
    public class NovelCreateDto: EntityDto<Guid?>
    {
        /// <summary>
        /// 书名
        /// </summary>
        [Required]
        public string Name { get; set; }

        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
    }
}