using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.Core.Dtos.RequestDtos;
 
    public class SuggestionVoteResDto
{
    public Guid? Id { get; set; }
    public Guid SuggestionId { get; set; }
    public Guid UserProfileId { get; set; }
    public bool Like { get; set; }
}
 
