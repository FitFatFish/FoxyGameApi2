using Foxy.DataLayer.Models.Base;

namespace Foxy.DataLayer.Models.Games;

public class GameCategory : BaseCrudModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public ICollection<Game> Games { get; set; }
}

