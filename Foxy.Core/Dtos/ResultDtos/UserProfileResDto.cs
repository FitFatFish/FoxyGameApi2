namespace Foxy.Core.Dtos.ResultDtos;

public class UserProfileResDto
{
    public Guid Id { get; set; }
    public string AccountId { get; set; }
    public string Bio { get; set; }
    public string HeaderImageGuid { get; set; }
    public string ProfileImageGuid { get; set; }
}
