using Foxy.Core.Infrastructures.Enums;

namespace Foxy.Core.Dtos.ResultDtos;

public class StoreItemResDto
{
    public Guid Id { get; set; }
    public Guid? GameId { get; set; }
    public string Title { get; set; }
    public int Level { get; set; }
    public string ImageGuid { get; set; }
    public StoreItemTypeEnum Type { get; set; } // Use enum or int as needed
}