namespace Foxy.Core.Dtos.RequestDtos;

public class SuggestionVoteResDto
{
    public Guid? Id { get; set; }
    public Guid SuggestionId { get; set; }
    public Guid UserProfileId { get; set; }
    public bool Like { get; set; }
}

