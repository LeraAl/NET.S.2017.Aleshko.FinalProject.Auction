﻿using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Services;
using MVC.Infrasrtucture.Mappers;
using MVC.Models;
using MVC.Models.Lot;

namespace MVC.Controllers
{
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

	        int userId = _userService.GetByLogin(User.Identity.Name).Id;
			var lot = _lotService.GetById((int) id).ToLotVM();
            if (lot.State.Equals("Sold"))
                lot.FinalBuyer = _rateService.GetLotLastRate(lot.Id).UserName;
            ViewBag.CanEdit = _lotService.CanUserUpdate(lot.Id);
            ViewBag.Rates = _rateService.GetLotRates(lot.Id).OrderByDescending(r => r.Datetime);
	        ViewBag.IsFavorite = _lotService.GetFavorites(userId).Any(l => l.Id == id);
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var lot = _lotService.GetById(id);
            _lotService.Delete(lot);
            return RedirectToAction("Lots", "Profile");
        }

	    [HttpPost]
	    public ActionResult DeleteFavorite(int id)
	    {
		    int userId = _userService.GetByLogin(User.Identity.Name).Id;
		    _lotService.RemoveFromFavorites(id, userId);
			return RedirectToAction("Details", new { id = id });
		}

	    [HttpPost]
	    public ActionResult AddFavorite(int id)
	    {
			int userId = _userService.GetByLogin(User.Identity.Name).Id;
		    _lotService.AddToFavorites(id, userId);
		    return RedirectToAction("Details", new { id = id });
		}

		[HttpPost]
        public ActionResult Sell(int id)
        {
            var lot = _lotService.GetById(id);
            lot.StateId = _lotService.GetStateId("Sold");
            _lotService.UpdateState(lot);
            return RedirectToAction("Details", "Lot", new {id = id});
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
            if (Request.IsAjaxRequest())
                return Json(null);
            return RedirectToAction("Details", new { id = model.LotId });
        }
    }
}
