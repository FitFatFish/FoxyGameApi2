namespace Foxy.DataLayer.Models.Games;
public class Match
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }

    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Game Game { get; set; }
    public ICollection<MatchMember> MatchMembers { get; set; }
}
