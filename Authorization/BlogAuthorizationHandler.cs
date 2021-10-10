using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Authorization
{
    public class BlogAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Post>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public BlogAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Post resource)
        {
            var applictionUser = await userManager.GetUserAsync(context.User);
            if ((requirement.Name == Operations.Update.Name || requirement.Name == Operations.Delete.Name) && applictionUser == resource.Creator)
            {
                context.Succeed(requirement);
            }
            if (requirement.Name == Operations.Read.Name && !resource.Published && applictionUser == resource.Creator)
            {
                context.Succeed(requirement);
            }
        }
    }
}
