using Volo.Abp.Application.Dtos;

namespace Business.TemHumManagement.Dto
{
    public class GetTemHumInputDto : PagedAndSortedResultRequestDto
    {
        public decimal FilterTem { get; set; }
        public decimal FilterHum { get; set; }
    }
}