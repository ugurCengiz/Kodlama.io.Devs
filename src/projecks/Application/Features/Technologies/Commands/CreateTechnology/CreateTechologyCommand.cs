﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechologyCommand : IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }



        public class CreateTechologyCommandHandler : IRequestHandler<CreateTechologyCommand,CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;


            public CreateTechologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechologyCommand request, CancellationToken cancellationToken)
            {

                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);

                Technology mappedTechnology = _mapper.Map<Technology>(request);
                Technology createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
                CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);

                return createdTechnologyDto;



            }
        }

    }
}
