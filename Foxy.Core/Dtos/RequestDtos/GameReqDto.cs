namespace Foxy.Core.Dtos.RequestDtos;

public class GameReqDto
{
    public Guid? Id { get; set; }
    public Guid GameCategoryId { get; set; }

    public string Title { get; set; }
    public string ImageGuid { get; set; }
    public string Documentation { get; set; }

    #region Create
    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    #endregion

    #region Update
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdateAt { get; set; }
    #endregion
}