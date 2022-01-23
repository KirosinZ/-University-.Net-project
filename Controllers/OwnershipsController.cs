using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Services;
using ASPTEST.Models;

namespace ASPTEST.Controllers
{
    public class OwnershipsController : Controller
    {

        IOwnershipService _ownershipService;

        public OwnershipsController(IOwnershipService ownershipService)
        {
            _ownershipService = ownershipService;
        }

        [Route("ownership/{id}/extend")]
        public ActionResult Extend(int id)
        {
            ViewBag.Controller = nameof(OwnershipsController);
            ViewBag.Action = nameof(Extend);

            var os = _ownershipService.GetAll().First(o => o.Id == id);

            if (os.Subscription.Owner.Balance < os.Book.Book.Price) return View("InsufficientFunds", os.Subscription.OwnerId);
            if (os.Subscription.End <= os.PromisedReturnDate) return View("SubscriptionExpiring", os.Subscription.OwnerId);


            var model = new OwnershipExtendViewModel
            {
                Ownership = os,
                MaxExtension = Math.Min((os.Subscription.End - os.PromisedReturnDate).Days, os.Subscription.Owner.Balance / os.Book.Book.Price),
                Extension = 0
            };

            return View(model);
        }

        [Route("ownership/{id}/extend")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Extend([FromForm] OwnershipExtendViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model.Ownership.Id);
            }

            model.Ownership = _ownershipService.GetAll().First(o => o.Id == model.Ownership.Id);

            model.Ownership.PromisedReturnDate = model.Ownership.PromisedReturnDate.AddDays(model.Extension);
            model.Ownership.Subscription.Owner.Balance -= model.Extension * model.Ownership.Book.Book.Price;
            _ownershipService.UpdateOwnership(model.Ownership);

            return RedirectToAction("Index", "Profile", new { id = model.Ownership.Subscription.OwnerId });
        }

        [Route("ownership/{id}/return")]
        public ActionResult Return(int id)
        {
            ViewBag.Controller = nameof(OwnershipsController);
            ViewBag.Action = nameof(Return);

            var ownership = _ownershipService.GetAll().First(o => o.Id == id);          
            ownership.ReturnDate = DateTime.Today;
            ownership.Book.Available = true;

            int delay = (ownership.ReturnDate - ownership.PromisedReturnDate).Value.Days;
            if (delay < 0) delay = 0;

            ownership.Subscription.Owner.Balance -= ownership.Book.Book.Price * delay;

            _ownershipService.UpdateOwnership(ownership);

            var model = new BookReturnViewModel
            {
                Reader = ownership.Subscription.Owner,
                Book = ownership.Book.Book,
                Delay = delay,
                Fee = ownership.Book.Book.Price * delay
            };

            return View(model);
        }
    }
}
