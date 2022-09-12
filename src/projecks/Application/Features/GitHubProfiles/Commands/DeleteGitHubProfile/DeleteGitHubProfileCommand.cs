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

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile
{
    public class DeleteGitHubProfileCommand : IRequest<DeletedGitHubProfileDto>
    {
        public int Id { get; set; }

        public class DeleteGitHubProfileCommandHandler : IRequestHandler<DeleteGitHubProfileCommand,DeletedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;

            public DeleteGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
            }

            public async Task<DeletedGitHubProfileDto> Handle(DeleteGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                GitHubProfile mappedGitHubProfile = _mapper.Map<GitHubProfile>(request);
                GitHubProfile deleteGitHubProfile = await _gitHubProfileRepository.DeleteAsync(mappedGitHubProfile);
                DeletedGitHubProfileDto deletedGitHubProfileDto = _mapper.Map<DeletedGitHubProfileDto>(deleteGitHubProfile);
                return deletedGitHubProfileDto;
            }
        }
    }
}
