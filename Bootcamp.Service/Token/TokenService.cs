﻿using Bootcamp.Repository.Tokens;
using Bootcamp.Repository;
using Bootcamp.Service.SharedDTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bootcamp.Service.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bootcamp.Service.Token
{

    public interface ITokenService
    {
        Task<ResponseModelDto<TokenResponseDto>> CreateClientAccessToken(GetAccessTokenRequestDto request);
        Task<ResponseModelDto<NoContent>> RevokeRefreshToken(Guid code);
    }
    // Token ürettiğimiz yer.
    public class TokenService(
        IOptions<CustomTokenOptions> tokenOptions,
        IOptions<Clients> clients,
        IGenericRepository<RefreshToken> refreshTokenRepository,
        IUnitOfWork unitOfWork,
        UserService userService) :ITokenService
    {
        public  Task<ResponseModelDto<TokenResponseDto>> CreateClientAccessToken(GetAccessTokenRequestDto request)
        {
            if (!clients.Value.Items.Any(x => x.Id == request.ClientId && x.Secret == request.ClientSecret))
            {
                return 
                   Task.FromResult(ResponseModelDto<TokenResponseDto>.Fail("Client not found"));
            }

            var claims = new List<Claim>()
            {
                new Claim("clientId", request.ClientId)
            };

            tokenOptions.Value.Audience.ToList()
               .ForEach(x => { claims.Add(new Claim(JwtRegisteredClaimNames.Aud, x)); });

            var tokenExpire = DateTime.Now.AddHours(tokenOptions.Value.ExpireByHour);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Value.Signature));

            //DateTimeOffset.Now.ToUnixTimeSeconds()
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: tokenExpire,
                issuer: tokenOptions.Value.Issuer,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtToken);

            return Task.FromResult(ResponseModelDto<TokenResponseDto>.Success(new TokenResponseDto(token, string.Empty)));

        }

        public async Task<ResponseModelDto<NoContent>> RevokeRefreshToken(Guid code)
        {
            var hasRefreshToken = await refreshTokenRepository.Where(x => x.Code == code).SingleOrDefaultAsync();


            if (hasRefreshToken is null)
            {
                return ResponseModelDto<NoContent>.Fail("Refresh token not found");
            }


            await refreshTokenRepository.Delete(hasRefreshToken.Id);
            await unitOfWork.CommitAsync();


            return ResponseModelDto<NoContent>.Success();
        }





    }
}
