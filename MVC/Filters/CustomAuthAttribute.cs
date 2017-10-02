using System.Web.Mvc;

namespace MVC.Filters
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            
            bool auth = false;

            foreach (var role in Roles.Split(','))
            {
                auth = auth || filterContext.HttpContext.User.IsInRole(role.Trim());
            }
            
            if (!auth)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                        { "controller", "Error" }, { "action", "PageNotFound" }
                    });
            }
        }
    }
}