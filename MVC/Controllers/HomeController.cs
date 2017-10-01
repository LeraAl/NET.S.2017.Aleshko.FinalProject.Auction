using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Services;
using MVC.Infrasrtucture.Mappers;
using MVC.Models.Lot;

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
            IEnumerable<LotShortViewModel> lots;
            if (id == null || id == 0)
                lots = _lotService.GetAll().Select(l => l.ToLotShortVM());
            else
                lots = _lotService.GetByCategory((int)id).Select(l => l.ToLotShortVM());
            
            return PartialView("_LotsList", lots);
        }

        public JsonResult SearchLot(string term)
        {
            var lots = _lotService.GetLotByRegex(term)
                .Select(l => new {id = l.Id, label = l.Name, value = l.Name});

            return Json(lots, JsonRequestBehavior.AllowGet);
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

        public ActionResult Error()
        {
            return View();
        }
        
    }
}