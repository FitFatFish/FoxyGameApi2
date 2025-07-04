using Foxy.DataLayer.Models.Base;

namespace Foxy.DataLayer.Models.Games
{
    public class Game : BaseCrudModel
    {
        public Guid Id { get; set; } 
        public Guid GameCategoryId { get; set; }

        public string Title { get; set; }
        public string ImageGuid { get; set; }
        public string Documentation { get; set; }

        public GameCategory GameCategory { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
