using Foxy.DataLayer.Models.FoxyGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.Core.Repository
{
    public interface IStoreItemService
    {
        #region storeitem

        Task<List<StoreItem>> GetStoreItems();

        #endregion

        #region useritem

        Task AddUserItem(UserItem userItem);

        Task RemoveUserItem(Guid id);

        Task<List<UserItem>> GetUserItems(Guid? userid=null);


        #endregion
    }
}
