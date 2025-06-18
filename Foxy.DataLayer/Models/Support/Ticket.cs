using Foxy.DataLayer.Models.Base;
using Foxy.DataLayer.Models.Users;

namespace Foxy.DataLayer.Models.Support;

public class Ticket : BaseCrudModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public UserProfile UserProfile { get; set; }
}

