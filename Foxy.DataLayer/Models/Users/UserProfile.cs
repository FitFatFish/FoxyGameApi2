using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Foxy.DataLayer.Models.FoxyGame;
using Foxy.DataLayer.Models.Support;

namespace Foxy.DataLayer.Models.Users
{
    public class UserProfile
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 
        
        public string AccountId { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }
        public string HeaderImage { get; set; }

        public ICollection<MatchMember>  MatchMembers { get; set; }
        public ICollection<UserGameScore> GameScores { get; set; }
        public ICollection<UserItem> UserItems { get; set; }
        public ICollection<SuggestionVote> SuggestionVotes { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
