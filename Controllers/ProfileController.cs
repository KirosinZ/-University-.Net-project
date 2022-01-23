using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Services;
using Business.Interop.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ASPTEST.Models;


namespace ASPTEST.Controllers
{
    public class ProfileController : Controller
    {
        IReaderService _readerService;

        public ProfileController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [Route("user/{id}")]
        public ActionResult Index(int id, [FromServices] ILiteratureTypeService lts, [FromServices] ISubscriptionService ss, [FromServices] IOwnershipService os)
        {
            ViewBag.Controller = nameof(ProfileController);
            ViewBag.Action = nameof(Index);
            ReaderViewModel model = new()
            {
                ReaderId = id,
                Reader = _readerService.GetAll().First(r => r.Id == id),
                SubData = lts.GetAll().Select(lt => (Type: lt, Sub: ss.GetAll().FirstOrDefault(s => s.End >= DateTime.Today && s.TypeId == lt.Id && s.OwnerId == id))),
                BookData = os.GetAll().Where(o => o.Subscription.OwnerId == id && o.ReturnDate == null)
            };

            return View(model);

        }

        [Route("user/{id}/edit")]
        public ActionResult Edit(int id)
        {
            ViewBag.Controller = nameof(ProfileController);
            ViewBag.Action = nameof(Edit);

            return View(_readerService.GetAll().First(r => r.Id == id));
        }

        [Route("user/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] ReaderDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _readerService.UpdateReader(model);

            return RedirectToAction(nameof(Index), new { id = model.Id });
        }

        [Route("user/{id}/deposit")]
        public ActionResult Deposit(int id)
        {
            ViewBag.Controller = nameof(ProfileController);
            ViewBag.Action = nameof(Deposit);

            DepositViewModel model = new()
            {
                ReaderId = id,
                Reader = _readerService.GetAll().First(r => r.Id == id),
                DepositAmount = 0
            };

            return View(model);
        }

        [Route("user/{id}/deposit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind("Reader", "DepositAmount")][FromForm] DepositViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Deposit), model);
            }

            model.Reader = _readerService.GetAll().First(r => r.Id == model.Reader.Id);

            model.Reader.Balance += model.DepositAmount;
            _readerService.UpdateReader(model.Reader);

            return RedirectToAction(nameof(Index), new { id = model.Reader.Id });
        }

        [Route("user/{id}/delete")]
        public ActionResult Delete(int id)
        {
            ViewBag.Controller = nameof(ProfileController);
            ViewBag.Action = nameof(Delete);

            var reader = _readerService.GetAll().First(r => r.Id == id);
            var leftover = reader.Subscriptions.SelectMany(s => s.Ownerships).Where(o => o.ReturnDate == null);

            

            if (leftover.Any()) return View("BooksLeftOver", leftover);
            if (reader.Balance < 0) return View("ReaderInDebt", reader);

            return View(id);
        }

        public ActionResult DeleteConfirm(int id, [FromServices] IOwnershipService os, [FromServices] ISubscriptionService ss)
        {
            var reader = _readerService.GetAll().First(r => r.Id == id);
            foreach(var s in reader.Subscriptions)
            {
                foreach(var o in s.Ownerships)
                {
                    os.DeleteOwnership(o.Id);
                }
                ss.DeleteSubscription(s.Id);
            }
            _readerService.DeleteReader(id);

            return RedirectToAction("Index", "Home");
        }
    }
}
