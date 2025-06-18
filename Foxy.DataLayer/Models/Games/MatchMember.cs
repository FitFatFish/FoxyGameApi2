using Foxy.Core.Infrastructures.Enums;
using Foxy.DataLayer.Models.Users;

namespace Foxy.DataLayer.Models.Games;
public class MatchMember
{
    public Guid Id { get; set; }

    public Guid MatchId { get; set; }
    public Guid UserProfileId { get; set; }

    public string TeamName { get; set; }
    public int Score { get; set; }
    public ResultTypeEnum ResultType { get; set; }

    public Match Match { get; set; }
    public UserProfile UserProfile { get; set; }
}
