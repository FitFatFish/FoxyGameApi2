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
        CreateMap<GameCategoryResDto, GameCategory>();

        CreateMap<GameReqDto, Game>();
        CreateMap<GameResDto, Game>();

        CreateMap<MatchReqDto, Match>();
        CreateMap<MatchResDto, Match>();

        CreateMap<SuggestionReqDto, Suggestion>();
        CreateMap<SuggestionResDto, Suggestion>();

        CreateMap<SuggestionVoteReqDto, SuggestionVote>();
        CreateMap<SuggestionVoteResDto, SuggestionVote>();

        CreateMap<TicketReqDto, Ticket>();
        CreateMap<TicketResDto, Ticket>();

        CreateMap<StoreItemReqDto, StoreItem>();
        CreateMap<StoreItemResDto, StoreItem>();

        CreateMap<UserItemReqDto, UserItem>();
        CreateMap<UserItemResDto, UserItem>();

        CreateMap<UserProfileReqDto, UserProfile>();
        CreateMap<UserProfileResDto, UserProfile>();

    }
}

