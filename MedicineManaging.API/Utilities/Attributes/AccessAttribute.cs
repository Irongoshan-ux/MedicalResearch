using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MedicineManaging.API.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class AccessAttribute : Attribute, IAuthorizationFilter
    {
        public string[] Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = JwtTokenParser.ParseSecurityToken(context.HttpContext);

            if(!UserManager.IsUserInRole(token, Roles)) 
                context.Result = new UnauthorizedResult();
        }
    }
}
