using Bootcamp.Service.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;

namespace NetBootcamp.API.Token
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController(ITokenService tokenService) : CustomBaseController
    {
        [HttpPost("CreateClientCredential")]
        public async Task<IActionResult> CreateClientToken(GetAccessTokenRequestDto request)
        {
            var response = await tokenService.CreateClientAccessToken(request);
            return CreateActionResult(response);
        }
    }
}
