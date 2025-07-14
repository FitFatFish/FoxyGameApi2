using Foxy.Core.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.Core.Dtos.ResultDtos
{
    public class MatchMemberResDto
    {
        public Guid Id { get; set; }

        public Guid MatchId { get; set; }
        public Guid UserProfileId { get; set; }

        public string TeamName { get; set; }
        public int Score { get; set; }
        public ResultTypeEnum ResultType { get; set; }
    }
}
