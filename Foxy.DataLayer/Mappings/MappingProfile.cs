using AutoMapper;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Games;
using Foxy.DataLayer.Models.Generals;
using Foxy.DataLayer.Models.Support;
using Foxy.DataLayer.Models.Users;

namespace Foxy.DataLayer.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GameCategoryReqDto, GameCategory>();
        CreateMap<GameCategory, GameCategoryResDto >();

        CreateMap<GameReqDto, Game>();
        CreateMap<Game,GameResDto>();

        CreateMap<MatchReqDto, Match>();
        CreateMap<Match,MatchResDto >();

        CreateMap<MatchMemberReqDto, MatchMember>();
        CreateMap<MatchMember, MatchMemberResDto>();

        CreateMap<SuggestionReqDto, Suggestion>();
        CreateMap<Suggestion,SuggestionResDto >();

        CreateMap<SuggestionVoteReqDto, SuggestionVote>();
        CreateMap<SuggestionVote, SuggestionVoteResDto>();

        CreateMap<TicketReqDto, Ticket>();
        CreateMap<Ticket, TicketResDto>();

        CreateMap<StoreItemReqDto, StoreItem>();
        CreateMap<StoreItem, StoreItemResDto>();

        CreateMap<UserItemReqDto, UserItem>();
        CreateMap<UserItem, UserItemResDto>();

        CreateMap<UserProfileReqDto, UserProfile>();
        CreateMap<UserProfile, UserProfileResDto>();

    }
}

