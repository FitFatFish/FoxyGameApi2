using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.DataLayer.Models.FoxyGame
{

    // Game:match -> 1:N
    // Match:MatchMember -> 1:N

    public class MatchGame
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 


        public Guid GameId { get; set; }
        //public int MatchMemberId { get; set; }


        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Game game { get; set; }
        public List<MatchMember> matchMembers { get; set; }
    }



    
}
