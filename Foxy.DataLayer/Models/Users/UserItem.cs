using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Foxy.DataLayer.Models.Generals;

namespace Foxy.DataLayer.Models.Users
{
    public class UserItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        public Guid UserProfileId { get; set; }
        public Guid StoreItemId { get; set; }


        public UserProfile foxyUser { get; set; }
        public StoreItem storeItem { get; set; }
    }
}
