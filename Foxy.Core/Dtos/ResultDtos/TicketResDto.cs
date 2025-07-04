namespace Foxy.Core.Dtos.ResultDtos;

public class TicketResDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid UserProfileId { get; set; }
}
