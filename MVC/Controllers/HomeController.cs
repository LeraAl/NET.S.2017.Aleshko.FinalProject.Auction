using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILotService _lotService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILotService lotService, ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
        }


        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult CategoriesMenu()
        {
            var categories = _categoryService.GetAll();
            return PartialView("_CategoriesMenu", categories);
        }

        public PartialViewResult GetLots(int? id)
        {
            IEnumerable<BLLLot> lots;
            if (id == null || id == 0)
            {
                lots = _lotService.GetAll();
            }
            else
            {
                lots = _lotService.GetByCategory((int)id);
            }

            return PartialView("_LotsList", lots);
        }

        public JsonResult SearchLot(string term)
        {
            var lots = _lotService.GetLotByRegex(term)
                .Select(l => new {id = l.Id, label = l.Name, value = l.Name});

            return Json(lots, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}