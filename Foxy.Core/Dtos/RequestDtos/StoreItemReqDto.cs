namespace Foxy.Core.Dtos.RequestDtos;

public class StoreItemResDto
{
    public Guid? Id { get; set; }
    public Guid? GameId { get; set; }
    public string Title { get; set; }
    public int Level { get; set; }
    public string ImageGuid { get; set; }
    public int Type { get; set; } // Use enum or int as needed
}
