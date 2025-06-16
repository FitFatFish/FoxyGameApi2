using Foxy.Core.Repository;
using Foxy.DataLayer.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.Core.Services
{
    public class SupportService : ISupportService
    {
        public Task AddSuggestionVote(SuggestionVote vote)
        {
            throw new NotImplementedException();
        }

        public Task Addsuggeston(Suggestion suggestion)
        {
            throw new NotImplementedException();
        }

        public Task<List<Suggestion>> GetSuggestions()
        {
            throw new NotImplementedException();
        }
    }
}
