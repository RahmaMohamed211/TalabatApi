using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.core.Entities.identity;

namespace Talabat.APIs.Exentations
{
    public static class UserMangerExtentions
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsyn(this UserManager<AppUser> userManager,ClaimsPrincipal Currentuser)
        {
            var email= Currentuser.FindFirstValue(ClaimTypes.Email);
            var user=await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email==email);


            return user;
        }
    }
}
