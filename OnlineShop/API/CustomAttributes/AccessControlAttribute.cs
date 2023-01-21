using Microsoft.AspNetCore.Mvc.Filters;

namespace API.CustomAttributes;
public class AccessControlAttribute:ActionFilterAttribute
{
    public string Permission { get; set; }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //1 Permission
        //2 User 
        //3 ApplicationId
        var userId = context.HttpContext.User.Claims.FirstOrDefault(q => q.Type=="UserId").Value;
        base.OnActionExecuting(context);
    }
}