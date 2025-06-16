using Foxy.DataLayer.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.Core.Repository
{
    public interface ISupportService
    {
        #region suggestion

        Task Addsuggeston(Suggestion suggestion);

        Task AddSuggestionVote(SuggestionVote vote);

        Task<List<Suggestion>> GetSuggestions();

        #endregion
    }
}
