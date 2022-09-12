using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Rules
{
    public class GitHubProfileRules
    {
        private readonly IGitHubProfileRepository _repository;

        public GitHubProfileRules(IGitHubProfileRepository repository)
        {
            _repository = repository;
        }

        public void UserNotFound(int user)
        {
            
            if (user == null) throw new BusinessException("User Not Found");
        }
    }
}
