namespace Application.Features.GitHubProfiles.Dtos;

public class UpdatedGitHubProfileDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string GitHubLink { get; set; }
    public bool IsActive { get; set; }
}