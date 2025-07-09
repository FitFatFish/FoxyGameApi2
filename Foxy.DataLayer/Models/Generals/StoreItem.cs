using Foxy.DataLayer.Models.Games;
using Foxy.DataLayer.Models.Users;
using Foxy.Core.Infrastructures.Enums;
using Foxy.DataLayer.Models.Base;

namespace Foxy.DataLayer.Models.Generals
{
    public class StoreItem : BaseCrudModel
    {
        public Guid Id { get; set; }

        public Guid? GameId { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public string ImageGuid { get; set; }
        public StoreItemTypeEnum Type { get; set; }

        public Game Game { get; set; }

        public ICollection<UserItem> UserItems { get; set; }
    }
}
