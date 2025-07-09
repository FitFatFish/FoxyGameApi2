namespace Foxy.Core.Dtos.RequestDtos;

public class TicketReqDto
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid UserProfileId { get; set; }
}
