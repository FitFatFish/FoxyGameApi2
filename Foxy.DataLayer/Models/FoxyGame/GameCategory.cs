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
    public class GameCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 

        public string Title { get; set; } // Title

        public Guid? CreatedBy { get; set; } // CreatedBy , CreatedAt
        public Guid? UpdatedBy { get; set; } // UpdatedBy


        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }


        // CreateBy, UpdatedBy

        // One Game Category Has Many Games
        #region Relations
              
        public List<Game> games { get; set; }
        #endregion
    }
}
