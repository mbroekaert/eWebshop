﻿using Shared.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<(bool success, string content)> CreateTokenAsync(TokenRequestDto token);
    }
}
