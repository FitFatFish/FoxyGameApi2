namespace Foxy.Core.Dtos.RequestDtos;

public class UserProfileReqDto
{
    public Guid? Id { get; set; }
    public string AccountId { get; set; }
    public string Bio { get; set; }
    public string HeaderImageGuid { get; set; }
    public string ProfileImageGuid { get; set; }
}
