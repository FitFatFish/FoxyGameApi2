using Foxy.DataLayer.Models.Games;
using Foxy.DataLayer.Models.Users;
using Foxy.Core.Infrastructures.Enums;

namespace Foxy.DataLayer.Models.Generals
{
    public class StoreItem
    {
        public Guid Id { get; set; }

        public Guid? GameId { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public string Image { get; set; }
        public StoreTypeEnum Type { get; set; }

        public Game Game { get; set; }

        public ICollection<UserItem> UserItems { get; set; }
    }
}
