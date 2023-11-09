using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresEmailVerifiedAttribute : Attribute, IAuthorizationFilter
{
  public void OnAuthorization(AuthorizationFilterContext context)
  {
    ClaimsPrincipal user = context.HttpContext.User;
    Claim? isVerifiedClaim = user.FindFirst(c => c.Type == "IsEmailVerified");
    if (isVerifiedClaim is null || isVerifiedClaim.Value == "0")
    {
      context.Result = new ForbidResult();
      return;
    }
  }
}