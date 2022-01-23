using ASPTEST.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Business.Services;
using Business.Interop.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ASPTEST.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IReaderService _readerService;

        public HomeController(
            ILogger<HomeController> logger, 
            IReaderService readerService
            )
        {
            _logger = logger;
            _readerService = readerService;
        }

        public IActionResult Index()
        {
            ViewBag.Controller = nameof(HomeController);
            ViewBag.Action = nameof(Index);

            return View(_readerService.GetAll());
        }

        [Route("register/")]
        public IActionResult Register()
        {
            ViewBag.Controller = nameof(HomeController);
            ViewBag.Action = nameof(Register);

            return View();
        }

        [Route("register/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromForm] ReaderDto dto)
        {
            ViewBag.Controller = nameof(HomeController);
            ViewBag.Action = nameof(Register);

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            _readerService.CreateReader(dto);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
