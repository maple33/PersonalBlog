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

        public HomeController(IBlogBusinessManager blogBusinessManager)
        {
            this.blogBusinessManager = blogBusinessManager;
        }

        public IActionResult Index(string searchString, int? page)
        {
            return View(blogBusinessManager.GetIndexViewModel(searchString, page));
        }
    }
}
