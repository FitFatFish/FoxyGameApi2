using Foxy.DataLayer.Models.Users;

namespace Foxy.DataLayer.Models.Support;

public class SuggestionVote
{
    public Guid Id { get; set; }
    public Guid SuggestionId { get; set; }
    public Guid UserProfileId { get; set; }
    public bool Like { get; set; }

    public Suggestion Suggestion { get; set; }
    public UserProfile UserProfile { get; set; }
}

