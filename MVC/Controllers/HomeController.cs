using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MVC.Infrasrtucture.Mappers;
using MVC.Models.Lot;

namespace MVC.Controllers
{
    [AllowAnonymous]
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
            IEnumerable<LotShortViewModel> lots;
            if (id == null || id == 0)
                lots = _lotService.GetByState("Active").Select(l => l.ToLotShortVM());
            else
            {
                int activeState = _lotService.GetStateId("Active");
                lots = _lotService.GetByCategory((int)id).Where(l => l.StateId == activeState).Select(l => l.ToLotShortVM());
            }
            
            return PartialView("_LotsList", lots);
        }
        
        public ActionResult GetImage(int id)
        {
            byte[] image = _lotService.GetById(id).Image;
            if (image != null)
            {
                return File(image, "image/jpeg");
            }
            return null;
        }
    }
}