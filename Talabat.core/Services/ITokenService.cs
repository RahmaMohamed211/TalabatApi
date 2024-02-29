using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.identity;

namespace Talabat.core.Services
{
    public interface ITokenService
    {
            Task<string> CreateTokenAsyn(AppUser user,UserManager<AppUser> userManager);

    }
}
