using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authorizations.Dtos;
using Application.Features.Authorizations.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Authorizations.Commands.Login
{
    public class LoginCommand : IRequest<LoggedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IPAddress { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
        {
            private readonly IUserService _userService;
            private readonly IAuthService _authService;
            private readonly AuthorizationsBusinessRules _authorizationsBusinessRules;


            public LoginCommandHandler(IUserService userService, IAuthService authService, AuthorizationsBusinessRules authorizationsBusinessRules)
            {
                _userService = userService;
                _authService = authService;
                _authorizationsBusinessRules = authorizationsBusinessRules;
            }

            public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User user = await _userService.GetByMail(request.UserForLoginDto.Email);

                await _authorizationsBusinessRules.UserShouldBeExists(user);

                await _authorizationsBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

                LoggedDto loggedDto = new ();

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                await _authService.DeleteOldRefreshTokens(user.Id);

                loggedDto.AccessToken = createdAccessToken;
                loggedDto.RefreshToken = addedRefreshToken;

                return loggedDto;
            }
        }


    }
}
