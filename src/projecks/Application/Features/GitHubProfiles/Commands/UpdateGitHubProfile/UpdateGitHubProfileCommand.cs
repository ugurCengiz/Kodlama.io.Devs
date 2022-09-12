using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.GitHubProfiles.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommand : IRequest<UpdatedGitHubProfileDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GitHubLink { get; set; }


        public class UpdateGitHubProfileCommandHandler : IRequestHandler<UpdateGitHubProfileCommand,UpdatedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;

            public UpdateGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                GitHubProfile mappedGitHubProfile = _mapper.Map<GitHubProfile>(request);
                GitHubProfile updatedGitHubProfile = await _gitHubProfileRepository.UpdateAsync(mappedGitHubProfile);
                UpdatedGitHubProfileDto updatedGitHubProfileDto = _mapper.Map<UpdatedGitHubProfileDto>(updatedGitHubProfile);
                return updatedGitHubProfileDto;
            }
        }
    }
}
