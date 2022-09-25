using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using Application.Features.GitHubProfiles.Queries.GetListGitHub;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, CreateGitHubProfileCommand>().ReverseMap();

            CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, DeleteGitHubProfileCommand>().ReverseMap();

            CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, UpdateGitHubProfileCommand>().ReverseMap();

            CreateMap<IPaginate<GitHubProfile>, GitHubProfileListModel>().ReverseMap();
            CreateMap<GitHubProfile, GetListGitHubProfileDto>()
                .ForMember(x=>x.UserFirstName , opt=>opt.MapFrom(z=>z.User.FirstName))
                .ForMember(x=>x.UserLastName , opt=>opt.MapFrom(z=>z.User.LastName))
                .ReverseMap();
            CreateMap<GitHubProfile, GetListGitHubProfileQuery>().ReverseMap();



        }
    }
}
