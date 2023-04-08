using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace TaskAssignmentAppNTier.Attributes
{
  /// <summary>
  /// AOP Programing sağlar.
  /// </summary>
  public class RoleAttribute : Attribute, IAsyncActionFilter
  {
    /// <summary>
    /// Web uygulamasındaki HttpContext class üzerinden erişmemizi sağlayan bir interface
    /// </summary>
    private string[] roleName;
   


    public RoleAttribute(params string[] roleName)
    {

      this.roleName = roleName;
    }

    /// <summary>
    /// Async tercih sebebi middleware gibi çalışsınm aynı zamanda, ayrı bir task açsın uygulamanın yük bindirmesin diye tercih ettik.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      if (context.HttpContext.User.Identity.IsAuthenticated)
      {
        if (context.HttpContext.User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value.Contains(roleName[0])))
        {
          // permissiona izin verdik.
          await next();

        }
        else
        {
          // işi kes adam login ama yetkisi yok o yüzden forbidden result döndür
          context.Result = new ForbidResult(JwtBearerDefaults.AuthenticationScheme);
          await Task.CompletedTask;
        }
      }
      else
      {
        context.Result = new UnauthorizedResult(); // 401 döndür. login olamamış bu durumda 401 ile login olması gerektiğini söyle
        await Task.CompletedTask;
      }

      
    }

  
  }
}
