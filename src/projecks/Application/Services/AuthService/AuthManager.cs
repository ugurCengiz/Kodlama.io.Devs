using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly TokenOptions _tokenOptions;


        public AuthManager(ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IRefreshTokenRepository refreshTokenRepository, TokenOptions tokenOptions)
        {
            _tokenHelper = tokenHelper;
            _userOperationClaimRepository = userOperationClaimRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenOptions = tokenOptions;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IPaginate<UserOperationClaim> userOperationClaims =
                await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id,
                    include: u => u.Include(u => u.OperationClaim));
            List<OperationClaim> operationClaims = userOperationClaims.Items.Select(x => new OperationClaim
            { Id = x.OperationClaim.Id, Name = x.OperationClaim.Name }).ToList();

            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
            return accessToken;

        }

        public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
        {
            RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
            return Task.FromResult(refreshToken);
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            return addedRefreshToken;
        }

        public async Task DeleteOldRefreshTokens(int userId)
        {
            IList<RefreshToken> refreshTokens = (await _refreshTokenRepository.GetListAsync(r =>
                    r.UserId == userId &&
                    r.Revoked == null && r.Expires >= DateTime.UtcNow &&
                    r.Created.AddDays(_tokenOptions.RefreshTokenTTL) <=
                    DateTime.UtcNow)
                ).Items;
            foreach (RefreshToken refreshToken in refreshTokens)
                await _refreshTokenRepository.DeleteAsync(refreshToken);
        }
    }
}
