using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CakeShop.Models;

namespace CakeShop.Resources
{
    public class CakeEditRes : AuthorizationHandler<EditRes, CakeWithTypeModel>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            EditRes requirement,
            CakeWithTypeModel cake)
        {
            if (cake.OwnerSpecification == context.User.FindFirst(ClaimTypes.Name).Value)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
