using Volo.Abp.Application.Dtos;

namespace Business.NovelManagement.Dto
{
    public class GetNovelInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterName { get; set; }
        public string FilterDescription { get; set; }
    }
}