using Foxy.DataLayer.Models.Games;
using Foxy.DataLayer.Models.Support;

namespace Foxy.DataLayer.Models.Users
{
    public class UserProfile
    {
        public Guid Id { get; set; } 
        
        public string AccountId { get; set; }
        public string Bio { get; set; }
        public string ProfileImageGuid { get; set; }
        public string HeaderImageGuid { get; set; }

        public ICollection<MatchMember>  MatchMembers { get; set; }
        //public ICollection<UserGameScore> GameScores { get; set; }
        public ICollection<UserItem> UserItems { get; set; }
        public ICollection<SuggestionVote> SuggestionVotes { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
