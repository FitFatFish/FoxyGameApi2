using Foxy.DataLayer.Models.Base;

namespace Foxy.DataLayer.Models.Support;
public class Suggestion : BaseCrudModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string ConfirmedDescription { get; set; }

    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }

    //todo: create this later 
    //public PublishTypeEnum Published { get; set; }

    public ICollection<SuggestionVote> SuggestionVotes { get; set; }
}
