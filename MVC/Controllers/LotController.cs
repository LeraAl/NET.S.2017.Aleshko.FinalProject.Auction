using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Services;
using MVC.Infrasrtucture.Mappers;
using MVC.Models;
using MVC.Models.Lot;

namespace MVC.Controllers
{
    [Authorize]
    public class LotController : Controller
    {
        private readonly ILotService _lotService;
        private readonly IUserService _userService;
        private readonly IRateService _rateService;
        private readonly ICategoryService _categoryService;

        public LotController(ILotService lotService, IUserService userService, IRateService rateService, ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
        }

        // GET: Lot/LotName/02-09-2017
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var lot = _lotService.GetById((int) id).ToLotVM();
            ViewBag.CanEdit = _lotService.CanUserDelete(lot.Id);
            ViewBag.Rates = _rateService.GetLotRates(lot.Id).OrderByDescending(r => r.Datetime);
            return View(lot);
        }
        

        // GET: Lot/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Lot/Create
        [HttpPost]
        public ActionResult Create(LotCreateModel model, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    model.Image = new byte[uploadImage.ContentLength];
                    uploadImage.InputStream.Read(model.Image, 0, uploadImage.ContentLength);
                }
                var lot = model.ToBLLLot();
                lot.OwnerId = _userService.GetByLogin(User.Identity.Name).Id;
                lot.StateId = _lotService.GetStateId("Active");
                _lotService.Create(lot);

                return RedirectToAction("Lots", "Profile");
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View(model);
        }

        // GET: Lot/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            var lot = _lotService.GetById(id);
            return View(lot.ToLotCreateModel());
        }

        // POST: Lot/Edit/5
        [HttpPost]
        public ActionResult Edit(LotCreateModel model, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    model.Image = new byte[uploadImage.ContentLength];
                    uploadImage.InputStream.Read(model.Image, 0, uploadImage.ContentLength);
                }
                _lotService.Update(model.ToBLLLot());
                return RedirectToAction("Details", new{ id = model.Id});
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View(model);
        }

        //// GET: Lot/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Lot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var lot = _lotService.GetById(id);
            lot.StateId = _lotService.GetStateId("DeletedByUser");
            _lotService.UpdateState(lot);
            return RedirectToAction("Lots", "Profile");
        }

        [HttpPost]
        public ActionResult MakeRate(RateCreateViewModel model)
        {
            if (ModelState.IsValid && model.RateSize > 0)
            {
                var newRate = new BLLRate
                {
                    UserName = User.Identity.Name,
                    LotId = model.LotId,
                    UserId = _userService.GetByLogin(User.Identity.Name).Id,
                    Datetime = DateTime.Now,
                    RateSize = model.RateSize
                };
                _lotService.AddRate(model.LotId, newRate);
                decimal price = _lotService.GetById(model.LotId).CurrentPrice;

                if (Request.IsAjaxRequest())
                    return Json(new {rate = new { User = newRate.UserName,
                                                  RateSize = newRate.RateSize,
                                                  Datetime = newRate.Datetime.ToString(DateTimeFormatInfo.InvariantInfo)},
                                    newPrice = price});
                
            }

            return RedirectToAction("Details", new { id = model.LotId });
        }
    }
}
