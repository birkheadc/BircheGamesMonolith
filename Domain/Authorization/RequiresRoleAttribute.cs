using Microsoft.AspNetCore.Mvc.Filters;
using Domain.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresRoleAttribute : Attribute, IAuthorizationFilter
{
  private readonly UserRole requiredRole;

  public RequiresRoleAttribute(UserRole requiredRole)
  {
    this.requiredRole = requiredRole;
  }
  public void OnAuthorization(AuthorizationFilterContext context)
  {
    ClaimsPrincipal user = context.HttpContext.User;
    Claim? roleClaim = user.FindFirst(c => c.Type == ClaimTypes.Role);
    if (roleClaim is null)
    {
      context.Result = new ForbidResult();
      return;
    }
    try
    {
      UserRole userRole = (UserRole)int.Parse(roleClaim.Value);
      if (userRole < requiredRole)
      {
        context.Result = new ForbidResult();
        return;
      }
    }
    catch
    {
      context.Result = new ForbidResult();
      return;
    }

  }
}