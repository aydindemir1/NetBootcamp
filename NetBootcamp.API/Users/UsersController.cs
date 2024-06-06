﻿using Bootcamp.Service.Token;
using Bootcamp.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;

namespace NetBootcamp.API.Users
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserService userService) : CustomBaseController
    {
        
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequestDto request)
        {
            return CreateActionResult(await userService.SignUp(request));
        }


        
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInRequestDto request)
        {
            return CreateActionResult(await userService.SignIn(request));
        }

        [HttpPost("SignInByRefreshToken")]
        public async Task<IActionResult> SignInByRefreshToken(SigninByRefreshTokenRequestDto request)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await userService.SignInByRefreshToken(request);
            return CreateActionResult(response);
        }
    }
}
