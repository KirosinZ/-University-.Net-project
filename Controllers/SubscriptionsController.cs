using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Services;
using Business.Interop.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

using ASPTEST.Models;

namespace ASPTEST.Controllers
{
    public class SubscriptionsController : Controller
    {
        ISubscriptionService _subscriptionService;
        IReaderService _readerService;
        ILiteratureTypeService _literatureTypeService;

        public SubscriptionsController(
            ISubscriptionService subscriptionService,
            IReaderService readerService,
            ILiteratureTypeService literatureTypeService
            )
        {
            _subscriptionService = subscriptionService;
            _readerService = readerService;
            _literatureTypeService = literatureTypeService;
        }

        [Route("sub/issue")]
        public ActionResult Issue(int readerId, int literatureTypeId)
        {
            ViewBag.Controller = nameof(SubscriptionsController);
            ViewBag.Action = nameof(Issue);

            var reader = _readerService.GetAll().First(r => r.Id == readerId);
            var ltype = _literatureTypeService.GetAll().First(lt => lt.Id == literatureTypeId);

            if (reader.Balance < ltype.Price) return View("InsufficientFunds", readerId);

            var model = new SubscriptionIssueViewModel()
            {
                Reader = reader,
                Type = ltype,
                AcquisitionDate = DateTime.Today,
                Period = 0
            };

            return View(model);
        }

        [Route("sub/issue")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Issue([Bind("Reader", "Type", "AcquisitionDate", "Period")][FromForm] SubscriptionIssueViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(nameof(Issue), new { readerId = model.Reader.Id, literatureTypeId = model.Type.Id });
            }

            var s = new SubscriptionDto()
            {
                OwnerId = model.Reader.Id,
                TypeId = model.Type.Id,
                Start = model.AcquisitionDate,
                End = model.AcquisitionDate.AddDays(7 * model.Period)
            };
            _subscriptionService.CreateSubscription(s);

            var rd = _readerService.GetAll().First(r => r.Id == model.Reader.Id);
            rd.Balance -= model.Period * _literatureTypeService.GetAll().First(lt => lt.Id == model.Type.Id).Price;
            _readerService.UpdateReader(rd);

            return RedirectToAction("Index", "Profile", new { id = model.Reader.Id });
        }

        [Route("sub/{id}/extend")]
        public ActionResult Extend(int id)
        {
            ViewBag.Controller = nameof(SubscriptionsController);
            ViewBag.Action = nameof(Extend);

            var sub = _subscriptionService.GetAll().First(s => s.Id == id);

            if (sub.Owner.Balance < sub.Type.Price) return View("InsufficientFunds", sub.OwnerId);

            var model = new SubscriptionExtendViewModel()
            {
                Subscription = sub,
                Extension = 0
            };

            return View(model);
        }

        [Route("sub/{id}/extend")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Extend([Bind("Subscription", "Extension")][FromForm] SubscriptionExtendViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Extend), new { id = model.Subscription.Id });
            }

            model.Subscription = _subscriptionService.GetAll().First(s => s.Id == model.Subscription.Id);
            model.Subscription.End = model.Subscription.End.AddDays(7 * model.Extension);
            _subscriptionService.UpdateSubscription(model.Subscription);

            var rd = _readerService.GetAll().First(r => r.Id == model.Subscription.OwnerId);
            rd.Balance -= model.Extension * _literatureTypeService.GetAll().First(lt => lt.Id == model.Subscription.TypeId).Price;
            _readerService.UpdateReader(rd);

            return RedirectToAction("Index", "Profile", new { id = model.Subscription.OwnerId });
        }
    }
}
