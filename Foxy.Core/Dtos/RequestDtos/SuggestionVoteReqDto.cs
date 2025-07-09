namespace Foxy.Core.Dtos.RequestDtos;

public class SuggestionVoteReqDto
{
    public Guid? Id { get; set; }
    public Guid SuggestionId { get; set; }
    public Guid UserProfileId { get; set; }
    public bool Like { get; set; }
}

