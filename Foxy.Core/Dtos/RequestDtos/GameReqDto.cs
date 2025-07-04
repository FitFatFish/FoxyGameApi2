

namespace Foxy.Core.Dtos.RequestDtos;

    public class GameResDto
{
    public Guid Id { get; set; } 
    public Guid GameCategoryId { get; set; }

    public string Title { get; set; }
    public string ImageGuid { get; set; }
    public string Documentation { get; set; }
}

