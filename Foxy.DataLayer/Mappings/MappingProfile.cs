using AutoMapper;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Games;

namespace Foxy.DataLayer.Mappings;
    public class MappingProfile:Profile
    {
    public MappingProfile()
    {
        CreateMap<GameCategoryReqDto, GameCategory>();
        CreateMap<GameCategoryResDto, GameCategory>();
        CreateMap<GameReqDto, Game>();
        CreateMap<GameResDto, Game>();
    }
    }

