﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Service.Token
{
    public record TokenResponseDto(string AccessToken, string RefreshToken);

    //public class TokenResponseDto(string AccessToken);
}
