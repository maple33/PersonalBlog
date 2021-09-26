using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.ViewModels;

namespace PersonalBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogBusinessManager blogBusinessManager;
        public BlogController(IBlogBusinessManager blogBusinessManager)
        {
            this.blogBusinessManager = blogBusinessManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new CreateBlogViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateBlogViewModel createBlogViewModel)
        {
            await blogBusinessManager.CreateBlogAsync(createBlogViewModel, User);
            return RedirectToAction("Create", createBlogViewModel);
        }
    }
}
