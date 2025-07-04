using Foxy.Core.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.Core.Dtos.RequestDtos;

    public class SuggestionResDto
    {
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string ConfirmedDescription { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
    public PublishTypeEnum Published { get; set; }
}

