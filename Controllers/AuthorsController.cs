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
    public class AuthorsController : Controller
    {
        IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        
        [Route("authors")]
        public ActionResult Index()
        {
            ViewBag.Controller = nameof(AuthorsController);
            ViewBag.Action = nameof(Index);

            return View(_authorService.GetAll());
        }

        [Route("authors/{id}")]
        public ActionResult Details(int id)
        {
            ViewBag.Controller = nameof(AuthorsController);
            ViewBag.Action = nameof(Details);

            return View(_authorService.GetAll().First(b => b.Id == id));
        }
    }
}
