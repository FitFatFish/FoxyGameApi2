using Foxy.DataLayer.Models.FoxyUser;
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
    // User:UserGameScore -> 1:N
    // Game:UserGameScore -> 1:N

    public class UserGameScore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public int Level { get; set; }

        public int MachCount { get; set; }
        public int CountableMachCount { get; set; }
        public int WinCount { get; set; }

        public DateTime LastPlayDate { get; set; }

        public Game game { get; set; }
        public FoxyUserInfo foxyUser { get; set; }

    }
}
