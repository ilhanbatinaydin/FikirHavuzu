using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Web.Security.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FikirHavuzu.Web.Security.Handlers
{
    public class ProfileAccessHandler : AuthorizationHandler<ProfileAccessRequirement, UserDto>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProfileAccessRequirement requirement, UserDto resource)
        {
            var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Task.CompletedTask;
            }

            int currentUserId = int.Parse(userIdClaim);
            bool isOwner = (currentUserId == resource.Id);
            bool hasManagePermission = context.User.HasClaim("Permission", "User.Manage");

            if (isOwner || hasManagePermission)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
            
        }
    }
}
