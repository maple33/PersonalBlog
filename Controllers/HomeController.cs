using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogBusinessManager blogBusinessManager;
        private readonly IHomeBusinessManager homeBusinessManager;

        public HomeController(IBlogBusinessManager blogBusinessManager, IHomeBusinessManager homeBusinessManager)
        {
            this.blogBusinessManager = blogBusinessManager;
            this.homeBusinessManager = homeBusinessManager;
        }

        public IActionResult Index(string searchString, int? page)
        {
            return View(blogBusinessManager.GetIndexViewModel(searchString, page));
        }

        public IActionResult Author(string authorId, string searchString, int? page)
        {
            var actionResult = homeBusinessManager.GetAuthorViewModel(authorId, searchString, page);
            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }
    }
}
