using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAPI.Models;

namespace DotNetAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}