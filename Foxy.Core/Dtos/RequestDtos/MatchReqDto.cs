namespace Foxy.Core.Dtos.RequestDtos;

public class MatchReqDto
{
    public Guid? Id { get; set; }
    public Guid GameId { get; set; }

    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}