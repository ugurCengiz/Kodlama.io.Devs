using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.GitHubProfiles.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.GitHubProfiles.Models
{
    public class GitHubProfileListModel :BasePageableModel
    {
        public IList<GetListGitHubProfileDto> Items { get; set; }

    }
}
