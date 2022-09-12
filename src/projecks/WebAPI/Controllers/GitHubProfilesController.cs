using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubProfilesController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateGitHubProfileCommand createGitHubProfileCommand)
        {
            CreatedGitHubProfileDto result = await Mediator.Send(createGitHubProfileCommand);
            return Created("", result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteGitHubProfileCommand deleteGitHubProfileCommand)
        {
            DeletedGitHubProfileDto result = await Mediator.Send(deleteGitHubProfileCommand);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateGitHubProfileCommand updateGitHubProfileCommand)
        {
            UpdatedGitHubProfileDto result = await Mediator.Send(updateGitHubProfileCommand);
            return Ok(result);
        }
    }
}
