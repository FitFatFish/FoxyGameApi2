using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foxy.DataLayer.Models.FoxyGame;
using Foxy.DataLayer.Models.Support;

namespace Foxy.DataLayer.Models.FoxyUser
{
    public class FoxyUserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 

        
        public string AccountId { get; set; }
        public string Bio { get; set; }
        public String ProfileImage { get; set; }
        public String HeaderImage { get; set; }

        public List<MatchMember>  matchMembers { get; set; }
        public List<UserGameScore> gameScores { get; set; }
        public List<UserItem> userItems { get; set; }
        public List<SuggestionVote> suggestionVotes { get; set; }

        public List<Ticket> tickets { get; set; }
    }
}
