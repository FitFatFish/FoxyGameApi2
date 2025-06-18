using Foxy.DataLayer.Models.Games;
using Foxy.DataLayer.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foxy.DataLayer.Models.Generals
{
    public class StoreItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 


        public Guid? GameId { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }

        public string Image { get; set; } //size(40)

        public StoreTypeEnum Type { get; set; }

        // CUD
        public Guid? CreatedBy { get; set; } // CreatedBy , CreatedAt
        public Guid? UpdatedBy { get; set; } // UpdatedBy

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public Game game { get; set; }

        public List<UserItem> userItems { get; set; }
    }
}
