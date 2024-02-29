using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.identity;

namespace Talabat.Repository.Identity
{
    public static class AddIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Rahma Mohmad",
                    Email = "rahma.mohamed@gmail.com",
                    UserName = "rahma.mohamed",
                    PhoneNumber = "01120302464"
                };
                await userManager.CreateAsync(User, "P@ssw0rd");

            }


        }
    }
}

