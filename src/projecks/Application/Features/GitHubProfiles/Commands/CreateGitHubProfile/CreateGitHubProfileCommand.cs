using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile
{
    public class CreateGitHubProfileCommand : IRequest<CreatedGitHubProfileDto>
    {
        public int UserId { get; set; }
        public string GitHubLink { get; set; }
        public bool IsActive { get; set; }


        public class CreateGitHubProfileCommandHandler : IRequestHandler<CreateGitHubProfileCommand, CreatedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileRules _gitHubProfileRules;

            public CreateGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper, GitHubProfileRules gitHubProfileRules)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
                _gitHubProfileRules = gitHubProfileRules;
            }

            public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                 _gitHubProfileRules.UserNotFound(request.UserId);

                GitHubProfile mappedGitHubProfile = _mapper.Map<GitHubProfile>(request);
                GitHubProfile createGitHubProfile = await _gitHubProfileRepository.AddAsync(mappedGitHubProfile);
                CreatedGitHubProfileDto createdGitHubProfileDto =
                    _mapper.Map<CreatedGitHubProfileDto>(createGitHubProfile);
                return createdGitHubProfileDto;


            }
        }
    }
}
