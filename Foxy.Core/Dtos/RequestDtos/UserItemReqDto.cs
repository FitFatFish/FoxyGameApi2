namespace Foxy.Core.Dtos.RequestDtos;

public class UserItemReqDto
{
    public Guid? Id { get; set; }
    public Guid StoreItemId { get; set; }
    public Guid UserProfileId { get; set; }
}
