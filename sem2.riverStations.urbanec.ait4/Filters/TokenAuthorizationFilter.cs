using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using sem2.riverStations.urbanec.ait4.Pages;

namespace sem2.riverStations.urbanec.ait4.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TokenAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var token = context.HttpContext.Request.Headers["Security-Token"].FirstOrDefault();

            var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

            if (string.IsNullOrEmpty(token) || !dbContext.TokenSetUp.Any(t => t.Token == token))
            {
                context.Result =new UnauthorizedResult();
                return ;
            }
        }
    }
}
