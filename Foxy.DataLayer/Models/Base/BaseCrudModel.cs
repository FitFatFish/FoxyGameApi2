namespace Foxy.DataLayer.Models.Base;

public class BaseCrudModel
{
    #region Create
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    #endregion

    #region Update
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdateAt { get; set; }
    #endregion

}