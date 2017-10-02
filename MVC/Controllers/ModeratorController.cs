using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MVC.Filters;
using MVC.Infrasrtucture.Mappers;

namespace MVC.Controllers
{
    [CustomAuth(Roles = "Moderator")]
    public class ModeratorController : Controller
    {
        private readonly ILotService _lotService;
        private readonly IRateService _rateService;

        public ModeratorController(ILotService lotService, IRateService rateService)
        {
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
            _lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
        }


        public ActionResult Lots()
        {
            var lots = _lotService.GetByState("Active").Select(l =>
            {
                var lot = l.ToLotModeratoVM();
                lot.RateCount = _rateService.GetLotRates(lot.Id).Count();
                lot.LastRateDateTime = _rateService.GetLotRates(lot.Id).OrderByDescending(r => r.Datetime)
                    .FirstOrDefault()?.Datetime;
                return lot;
            });

            return View(lots);
        }

        public ActionResult LotDetails(int? id)
        {
            if (id == null) return RedirectToAction("PageNotFound", "Error");

            var lot = _lotService.GetById((int)id).ToLotVM();

            return View(lot);
        }

        [HttpPost]
        public ActionResult DeleteLot(int id)
        {
            var lot = _lotService.GetById(id);
            _lotService.Delete(lot);

            return RedirectToAction("Lots");
        }
    }
}