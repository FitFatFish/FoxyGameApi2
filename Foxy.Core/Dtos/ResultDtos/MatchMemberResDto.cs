using Foxy.Core.Infrastructures.Enums;

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
