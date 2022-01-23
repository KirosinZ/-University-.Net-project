using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Services;
using Business.Interop.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ASPTEST.Controllers
{
    public class BooksController : Controller
    {
        IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Route("books")]
        public ActionResult Index()
        {
            ViewBag.Controller = nameof(BooksController);
            ViewBag.Action = nameof(Index);

            return View(_bookService.GetAll());
        }

        [Route("books/{id}")]
        public ActionResult Details(int id)
        {
            ViewBag.Controller = nameof(BooksController);
            ViewBag.Action = nameof(Details);

            return View(_bookService.GetAll().First(b => b.Id == id));
        }
    }
}
