using Foxy.DataLayer.Models.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foxy.DataLayer.Models.Users;

namespace Foxy.DataLayer.Models.FoxyGame
{
    public class MatchMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MatchId { get; set; }
        public Guid UserId { get; set; }
        public string TeamName { get; set; }

        public int Score { get; set; }
        public ResultTypeEnum ResultType { get; set; } // Undefined, Win, Loss, Draw

        public MatchGame  match { get; set; }
        public  UserProfile foxyUser { get; set; }
    }
}
