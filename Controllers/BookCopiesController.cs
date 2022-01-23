using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Services;
using Business.Interop.Data;
using ASPTEST.Models;
using System.Diagnostics.CodeAnalysis;

namespace ASPTEST.Controllers
{
    public class BookCopiesController : Controller
    {

        IReaderService _readerService;
        IBookCopyService _bookCopyService;

        public BookCopiesController(
            IReaderService readerService,
            IBookCopyService bookCopyService
            )
        {
            _readerService = readerService;
            _bookCopyService = bookCopyService;
        }

        [Route("books/browse")]
        public ActionResult Browse(int readerId)
        {
            ViewBag.Controller = nameof(BookCopiesController);
            ViewBag.Action = nameof(Browse);

            var rd = _readerService.GetAll().First(r => r.Id == readerId);
            if (rd.Balance < 0) return View("ReaderInDebt", rd);

            var activesubs = rd.Subscriptions.Where(s => s.End > DateTime.Today);
            if (!activesubs.Any()) return View("NoActiveSubscriptions", readerId);

            var books = _bookCopyService.GetAll().Where(bc => bc.Available).Distinct(new BookCopyEqualityComparer()).Select(bc => (Copy: bc, BookInfo: bc.Book)).OrderByDescending(t => t.BookInfo.Price);

            var booksbytype = activesubs.Select(s => books.Where(bc => bc.BookInfo.Genre.TypeId == s.TypeId)).Where(ar => ar.Count() != 0);

            if (!activesubs.Any(s => books.Any(bc => bc.BookInfo.Genre.TypeId == s.TypeId))) return View("NoBooksAvailable", readerId);

            var model = new BookBrowserViewModel
            {
                ReaderId = readerId,
                Books = booksbytype,
                ActiveSubs = activesubs
            };

            return View(model);
        }

        [Route("books/take/{bookId}")]
        public ActionResult Take(int readerId, int bookId)
        {
            ViewBag.Controller = nameof(BookCopiesController);
            ViewBag.Action = nameof(Take);

            var reader = _readerService.GetAll().First(r => r.Id == readerId);
            var book = _bookCopyService.GetAll().First(bc => bc.Id == bookId);

            if (reader.Balance < book.Book.Price) return View("InsufficientFunds", readerId);

            var activesub = reader.Subscriptions.First(s => s.End > DateTime.Now && s.TypeId == book.Book.Genre.TypeId);

            int maxperiod = (activesub.End - DateTime.Today).Days;


            var model = new BookTakeViewModel
            {
                Reader = reader,
                Sub = activesub,
                Copy = book,
                MaxPeriod = Math.Min(maxperiod, reader.Balance / book.Book.Price),
                Period = 0
            };

            return View(model);
        }

        [Route("books/take/{bookId}")]
        [HttpPost]
        public ActionResult Take([FromForm] BookTakeViewModel model, [FromServices] IOwnershipService os)
        {
            var ow = new OwnershipDto
            {
                AcquisitionDate = DateTime.Today,
                PromisedReturnDate = DateTime.Today.AddDays(model.Period),
                ReturnDate = null,
                BookId = model.Copy.Id,
                SubscriptionId = model.Sub.Id,
            };
            
            os.CreateOwnership(ow);
            ow = os.GetAll().First(o => o.BookId == model.Copy.Id && o.SubscriptionId == model.Sub.Id);
            ow.Book.Available = false;
            ow.Subscription.Owner.Balance -= model.Period * ow.Book.Book.Price;
            _bookCopyService.UpdateBookCopy(ow.Book);
            _readerService.UpdateReader(ow.Subscription.Owner);
            


            return RedirectToAction("Index", "Profile", new { id = model.Reader.Id });
        }

        class BookCopyEqualityComparer : EqualityComparer<BookCopyDto>
        {
            public override bool Equals(BookCopyDto x, BookCopyDto y)
            {
                return (x.BookId == y.BookId) && (x.LanguageId == y.LanguageId);
            }

            public override int GetHashCode([DisallowNull] BookCopyDto obj)
            {
                return (obj.BookId ^ obj.LanguageId).GetHashCode();
            }
        }
    }
}
