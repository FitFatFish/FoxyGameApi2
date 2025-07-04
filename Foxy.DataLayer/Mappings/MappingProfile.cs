using AutoMapper;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

