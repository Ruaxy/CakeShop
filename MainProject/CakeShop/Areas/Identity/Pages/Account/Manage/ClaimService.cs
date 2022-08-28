using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CakeShop.Areas.Identity.Data;

namespace CakeShop.Areas.Identity.Pages.Account.Manage
{
    public class ClaimService : UserClaimsPrincipalFactory<User>
    {
        public ClaimService(
        UserManager<User> userManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var isMinimumAge = user.DateOfBirth.AddYears(18) >= DateTime.Now;
            identity.AddClaim(new Claim(UserClaims.isMinimumAge, isMinimumAge.ToString()));
            identity.AddClaim(new Claim(UserClaims.FullName, $"{user.FirstName} {user.LastName}"));

            foreach (var role in await UserManager.GetRolesAsync(user))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }
}
