

namespace Foxy.Core.Dtos.RequestDtos;

public class GameCategoryReqDto
{
    public Guid? Id { get; set; }
    public string Title { get; set; }

    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}

