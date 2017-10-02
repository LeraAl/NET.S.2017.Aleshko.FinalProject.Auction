using System.Web.Mvc;

namespace MVC.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
        
    }
}