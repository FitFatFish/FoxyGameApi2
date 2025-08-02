namespace Foxy.Core.Dtos.ResultDtos;

public class TicketResDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid UserProfileId { get; set; }

    #region Create
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    #endregion

    #region Update
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdateAt { get; set; }
    #endregion
}
