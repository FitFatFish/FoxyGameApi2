using Foxy.Core.Repository;
using Foxy.DataLayer.Models.FoxyGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.Core.Services
{
    public class StoreItemService : IStoreItemService
    {
        public Task AddUserItem(UserItem userItem)
        {
            throw new NotImplementedException();
        }

        public Task<List<StoreItem>> GetStoreItems()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserItem>> GetUserItems(Guid? userid = null)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
