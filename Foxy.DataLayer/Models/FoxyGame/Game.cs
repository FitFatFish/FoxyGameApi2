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
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }= Guid.NewGuid();

        public Guid CategoryId { get; set; }

        public string GameName { get; set; }

        public string ImageName { get; set; }
        //public string VideoLink { get; set; }

        public string Documentation { get; set; } // MD File, Documentation

        public Guid? CreatedBy { get; set; } // CreatedBy , CreatedAt
        public Guid? UpdatedBy { get; set; } // UpdatedBy
        public Guid? DeleteBy { get; set; } // UpdatedBy

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public DateTime? UpdateAt { get; set; }


        
        public GameCategory gameCategory { get; set; }
        public StoreItem storeItem { get; set; }

        public List<MatchGame> matches { get; set; }
        public List<UserGameScore> gameScores { get; set; }
    }
}
