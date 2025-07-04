namespace Foxy.Core.Dtos.ResultDtos;

public class MatchResDto
    {
    public Guid Id { get; set; }
    public Guid GameId { get; set; }

    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}

