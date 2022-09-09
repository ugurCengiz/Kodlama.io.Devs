using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Rules
{
    public class GitHubProfileBusinessRules
    {
        private readonly IGitHubProfileRepository _gitHubProfileRepository;


        public GitHubProfileBusinessRules(IGitHubProfileRepository gitHubProfileRepository)
        {
            _gitHubProfileRepository = gitHubProfileRepository;
        }

        public async Task GitHubProfileCanNotBeDuplicatedWhenInserted(int userId)
        {
            GitHubProfile result = await _gitHubProfileRepository.GetAsync(b => b.DeveloperId == userId);
            if (result != null) throw new BusinessException("There is already a GitHub profile assigned");
        }

        public void ProgrammingLanguageShouldExist(GitHubProfile gitHubProfile)
        {
            if (gitHubProfile == null) throw new BusinessException("Requested GitHub profile does not exist");
        }



    }
}
