using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foxy.DataLayer.Models.Users;

namespace Foxy.DataLayer.Models.FoxyGame
{
    public class UserItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        public Guid UserId { get; set; }
        public Guid StoreItemId { get; set; }


        public UserProfile foxyUser { get; set; }
        public StoreItem storeItem { get; set; }
    }
}
